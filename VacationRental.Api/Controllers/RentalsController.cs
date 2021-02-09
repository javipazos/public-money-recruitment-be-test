using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, Rental> _rentals;

        public RentalsController(IDictionary<int, Rental> rentals)
        {
            _rentals = rentals;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return new RentalViewModel(_rentals[rentalId]);
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            var rental = new Rental(key.Id, model.Units);
            _rentals.Add(rental.Id,rental);
            return key;
        }
    }
}
