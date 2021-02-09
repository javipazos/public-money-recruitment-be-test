using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationRental.Api.Models
{
    public class Rental
    {
        public Rental(int id, int units)
        {
            Id = id;
            Units = new List<Unit>();
            for (int i = 0; i < units; i++)
            {
                Units.Add(new Unit(i));
            }
        }

        public List<Unit> Units { get; set; }
        public int Id { get; internal set; }
    }
}
