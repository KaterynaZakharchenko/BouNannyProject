using BouNanny.Models;
using System.Data.Entity;

namespace BouNanny.DAL.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        } 

        public DbSet<Level> Levels { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TimePeriod> TimePeriods { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
