using System.ComponentModel.DataAnnotations;

namespace SolarCalculator.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Campo Nome de usuário obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Campo Senha obrigatória")]
        public string Password { get; set; }
    }
}
