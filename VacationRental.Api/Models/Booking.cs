using System;

namespace VacationRental.Api.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int UnitId { get; set; }
        public DateTime StartDate { get; set; }
        public int Nights { get; set; }
        public int PreparationDays { get; set; }
        public DateTime EndDate => StartDate.AddDays(Nights + PreparationDays);

        public bool DateIsBooked(DateTime date) => date.Date >= StartDate.Date && date.Date  < EndDate.Date;

    }
}