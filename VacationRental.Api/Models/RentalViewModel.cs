namespace VacationRental.Api.Models
{
    public class RentalViewModel
    {
        public RentalViewModel(){}
        public RentalViewModel(Rental rental)
        {
            Id=rental.Id;
            Units=rental.Units.Count;
        }
        public int Id { get; set; }
        public int Units { get; set; }
    }
}
