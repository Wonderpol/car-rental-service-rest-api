using CarRentalRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}