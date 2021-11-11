using System.ComponentModel.DataAnnotations;

namespace SolarCalculator.ViewModel
{
    public class CreateKwhCostViewModel
    {
        [Required(ErrorMessage = "Campo Estado é obrigatório")]
        public string State { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Campo Custo KW/h é obrigatório (deve ser maior que 0.1)")]
        public double Cost { get; set; }
    }
}
