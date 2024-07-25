using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Parking.Data;
using Parking.Models;
using Parking.Tests;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

public class ParkingContextTests
{

    [Fact]
    public void Can_Register_Vehicle_Exit()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        using (var context = new TestDbContext(options))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var vehicle = new VehicleRegistration
            {
                LicensePlate = "XYZ789",
                Enter = DateTime.Now.AddHours(-2) // Registrando a entrada há 2 horas
            };
            context.VehicleRegistrations.Add(vehicle);
            context.SaveChanges();

            // Act
            vehicle.Exit = DateTime.Now;
            context.SaveChanges();

            // Assert
            var updatedVehicle = context.VehicleRegistrations.FirstOrDefault(v => v.LicensePlate == "XYZ789");
            Assert.NotNull(updatedVehicle);
            Assert.NotNull(updatedVehicle.Exit);
            Assert.True(updatedVehicle.Exit > updatedVehicle.Enter);
        }
    }

    [Fact]
    public void Can_Calculate_Correct_Amount_Charged()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        using (var context = new TestDbContext(options))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var vehicle = new VehicleRegistration
            {
                LicensePlate = "LMN456",
                Enter = DateTime.Now.AddHours(-3) // Registrando a entrada há 3 horas
            };
            context.VehicleRegistrations.Add(vehicle);
            context.SaveChanges();

            vehicle.Exit = DateTime.Now;
            var hoursParked = vehicle.Exit.HasValue
                ? (vehicle.Exit.Value - vehicle.Enter).TotalHours
                : 0;
            var pricePerHour = 10;
            vehicle.AmountCharged = (decimal)(hoursParked * pricePerHour);
            context.SaveChanges();

            // Assert
            var updatedVehicle = context.VehicleRegistrations.FirstOrDefault(v => v.LicensePlate == "LMN456");
            Assert.NotNull(updatedVehicle);
            Assert.Equal(30.00m, Math.Round((decimal)updatedVehicle.AmountCharged, 2));
        }
    }

    [Fact]
    public void Cannot_Add_Vehicle_With_Invalid_Plate()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        using (var context = new TestDbContext(options))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            // Act
            var vehicle = new VehicleRegistration
            {
                LicensePlate = "InvalidPlate!", // Placa inválida
                Enter = DateTime.Now
            };

            var validationContext = new ValidationContext(vehicle);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(vehicle, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid); 

            var expectedErrorMessage = "The field LicensePlate must be a string with a maximum length of 10.";
            var containsExpectedError = validationResults.Any(vr => vr.ErrorMessage.Contains(expectedErrorMessage));

            Assert.True(containsExpectedError, $"Expected error message not found. Validation results: {string.Join(", ", validationResults.Select(vr => vr.ErrorMessage))}");
        }
    }

    [Fact]
    public void Can_Initialize_Context()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        // Act
        using (var context = new TestDbContext(options))
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            // Assert
            Assert.NotNull(context);
            Assert.True(context.Database.GetService<IRelationalDatabaseCreator>().HasTables());
        }
    }


}
