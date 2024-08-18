using AngularWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngularWebApi.Data
{
    public partial class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
