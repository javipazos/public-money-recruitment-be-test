namespace VacationRental.Api.Models
{
    public class RentalViewModel
    {
        public RentalViewModel(){}
        public RentalViewModel(Rental rental)
        {
            Id=rental.Id;
            Units=rental.Units.Count;
            PreparationTimeInDays=rental.PreparationTimeInDays;
        }
        public int Id { get; set; }
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
