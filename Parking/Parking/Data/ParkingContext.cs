using Microsoft.EntityFrameworkCore;
using Parking.Models;
using System.Collections.Generic;

namespace Parking.Data
{
    public class ParkingContext : DbContext
    {
        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options) { }

        public DbSet<PriceTable> PriceTables { get; set; }
        public DbSet<VehicleRegistration> VehicleRegistrations { get; set; }

    }
}
