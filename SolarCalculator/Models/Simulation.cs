using System;

namespace SolarCalculator.Models
{
    public class Simulation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double QuantityKWhMonthly { get; set; }
        public double GeneratorCost { get; set; }
        public double KwhCost { get; set; }
        public double Total { get; set; }
        public Address Address { get; set; }
        public int Months { get; set; }
    }
}
