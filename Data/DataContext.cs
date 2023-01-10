using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.RentVehicleModels;
using CarRentalRestApi.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;

namespace CarRentalRestApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Caravan> Caravans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Rent> Rents { get; set; }
        public DbSet<ChassisType> ChassisTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        
        
    }
}