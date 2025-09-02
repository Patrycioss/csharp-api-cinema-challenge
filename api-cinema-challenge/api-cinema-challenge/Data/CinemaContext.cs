using api_cinema_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Data
{
    public sealed class CinemaContext : DbContext
    {
        private readonly string _connectionString;
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.UseInMemoryDatabase("Database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        
        public DbSet<Customer>  Customers { get; set; }
        public DbSet<Movie>  Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
    }
}
