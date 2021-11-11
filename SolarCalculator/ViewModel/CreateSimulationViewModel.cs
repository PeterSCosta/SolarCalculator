using System.ComponentModel.DataAnnotations;

namespace SolarCalculator.ViewModel
{
    public class CreateSimulationViewModel
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo E-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Telefone é obrigatório")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Campo Estado é obrigatório")]
        public string State { get; set; }
        [Required(ErrorMessage = "Campo Cidade é obrigatório")]
        public string City { get; set; }
        [Required(ErrorMessage = "Campo CEP é obrigatório")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Campo Bairro é obrigatório")]
        public string Neighborhood { get; set; }
        [Required(ErrorMessage = "Campo Rua é obrigatório")]
        public string Street { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Campo Número é obrigatório")]
        public int Number { get; set; }
        public string Complement { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Campo Custo de Energia Mensal é obrigatório (deve ser maior que 0.1)")]
        public double EnergyCostMonthly { get; set; }
    }
}
