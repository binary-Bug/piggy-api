using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Dtos
{
    public class LoginCredentialsDTO
    {
        [Required]
        public string UsernameOrEmail { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
