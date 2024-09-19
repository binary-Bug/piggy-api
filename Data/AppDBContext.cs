using AngularWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApi.Data
{
    public partial class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public virtual DbSet<Region> LURegions { get; set; }
        public virtual DbSet<UserRegions> UserRegionMap { get; set; }
        public virtual DbSet<Restaurent> LURestaurents { get; set; }
        public virtual DbSet<RestaurentType> LURestaurentTypes { get; set; }
    }
}
