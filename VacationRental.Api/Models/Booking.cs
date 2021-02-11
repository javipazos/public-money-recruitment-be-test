using System;

namespace VacationRental.Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int UnitId { get; set; }
        public DateTime StartDate { get; set; }
        public int Nights { get; set; } = 1;
        public int PreparationDays { get; set; }
        public DateTime EndDate => StartDate.AddDays(Nights-1 + PreparationDays);
        public DateTime OccupancyEndDate => StartDate.AddDays(Nights-1);

        public bool DateIsBooked(DateTime date) => date.Date >= StartDate.Date && date.Date <= OccupancyEndDate.Date;
        public bool DateIsPeparationDay(DateTime date) => date.Date >= StartDate.Date && date.Date <= EndDate.Date;
        public bool DateIsUnavailable(DateTime date) => DateIsBooked(date) || DateIsPeparationDay(date);
        public bool BookingsOverlaps(Booking booking)
        {
            return RentalId == booking.RentalId && UnitId==booking.UnitId && DateRangesOverlaps(StartDate,EndDate,booking.StartDate,booking.EndDate);
        }
        public bool DateRangesOverlaps(DateTime t1Start,DateTime t1End,DateTime t2Start,DateTime t2End)
        {
            return (t1Start <= t2Start && t1End > t2Start.Date)
                                    || (t1Start < t2End && t1End >= t2End)
                                    || (t1Start > t2Start && t1End < t2End);
        }

    }
}