using Microsoft.EntityFrameworkCore;
using Parking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Tests
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }
        public DbSet<VehicleRegistration> VehicleRegistrations { get; set; }
        public DbSet<PriceTable> PriceTables { get; set; }
    }

}
