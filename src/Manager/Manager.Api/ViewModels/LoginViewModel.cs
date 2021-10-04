using System.ComponentModel.DataAnnotations;

namespace Manager.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
