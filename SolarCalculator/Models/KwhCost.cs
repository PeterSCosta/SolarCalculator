using System;

namespace SolarCalculator.Models
{
    public class KwhCost
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public double Cost { get; set; }
    }
}
