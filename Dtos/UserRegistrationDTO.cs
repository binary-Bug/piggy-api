using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Dtos
{
    public class UserRegistrationDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber {  get; set; } = string.Empty;

        [Required]
        public int RegionId { get; set; }

        [Required]
        public List<string> Roles { get; set; } = [];
    }
}
