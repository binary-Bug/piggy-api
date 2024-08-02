using Microsoft.EntityFrameworkCore;

namespace AngularWebApi.Models
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions
        <AppDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>(entity => {
                entity.HasKey(k => k.Id);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
