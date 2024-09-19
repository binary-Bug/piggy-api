using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
    }
}
