using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Models
{
    public class Restaurent
    {
        [Key]
        public int RestaurentId { get; set; }
        [Required]
        public string RestaurentName { get; set; } = string.Empty;
        [Required]
        public RestaurentType? RestaurentType { get; set; }
        [Required]
        public int RestaurentOwnerId { get; set; }
    }
}
