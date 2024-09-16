using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AngularWebApi.Models
{
    [PrimaryKey("UserId", ["RegionId"])]
    public class UserRegions
    {
        public string UserId { get; set; } = string.Empty;
        public int RegionId { get; set; }
    }
}
