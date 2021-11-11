using System;

namespace SolarCalculator.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
    }
}
