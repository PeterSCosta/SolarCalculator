using System.ComponentModel.DataAnnotations;

namespace SolarCalculator.ViewModel
{
    public class EditKwhCostViewModel
    {
        [Range(0.1, double.MaxValue, ErrorMessage = "Campo Custo KW/h é obrigatório (deve ser maior que 0.1)")]
        public double Cost { get; set; }
    }
}
