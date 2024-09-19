using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Models
{
    public class RestaurentType
    {
        [Key]
        public int RestaurentTypeId { get; set; }
        [Required]
        public string RestaurentTypeLabel { get; set; } = string.Empty;
    }
}
