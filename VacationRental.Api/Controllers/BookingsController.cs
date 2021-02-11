using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, Rental> _rentals;
        private readonly IDictionary<int, Booking> _bookings;

        public BookingsController(
            IDictionary<int, Rental> rentals,
            IDictionary<int, Booking> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return new BookingViewModel(_bookings[bookingId]);
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_rentals.ContainsKey(model.RentalId))
                throw new ApplicationException("Rental not found");


            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            Booking booking = new Booking
            {
                Id = key.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                StartDate = model.Start.Date,
                PreparationDays = _rentals[model.RentalId].PreparationTimeInDays
            };

            int? unitId = GetFreeUnitId(booking);
            if (unitId is null)
                throw new ApplicationException("Not available");
            booking.UnitId= (int)unitId;
            _bookings.Add(key.Id, booking);

            return key;
        }


        private int? GetFreeUnitId(Booking booking)
        {
            var rental = _rentals[booking.RentalId];
            var occuppiedUnitIds = new List<int>();
            foreach (var unit in rental.Units)
            {
                occuppiedUnitIds.AddRange(_bookings.Values.Where(b => b.UnitId == unit.Id && NewBookingOverlapsExisting(booking, b) ).Select(b => b.UnitId));
            }

            return rental.Units.FirstOrDefault(u=>!occuppiedUnitIds.Contains(u.Id))?.Id;
        }

        private static bool NewBookingOverlapsExisting(Booking newBooking, Booking existingBooking)
        {
            return existingBooking.RentalId == newBooking.RentalId
                && existingBooking.DateRangesOverlaps(existingBooking.StartDate, existingBooking.EndDate, newBooking.StartDate, newBooking.EndDate);
        }
    }
}
