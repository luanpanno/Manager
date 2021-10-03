using System.ComponentModel.DataAnnotations;

namespace Manager.Api.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(180)]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
