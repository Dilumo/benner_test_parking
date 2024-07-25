using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Tests
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder<TContext> UseCustomInMemoryDatabase<TContext>(
            this DbContextOptionsBuilder<TContext> builder, string databaseName)
            where TContext : DbContext
        {
            builder.UseSqlite($"DataSource=:memory:");
            return builder;
        }
    }

}
