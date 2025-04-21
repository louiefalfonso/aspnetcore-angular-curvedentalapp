using System.ComponentModel;
using System.Net;
using System.Numerics;
using CurveDentalManagement.API.Data;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CurveDentalManagement.API.Tests.Repository
{
    public class StaffRepositoryTests
    {
        private readonly ApplicationDbContext dbContext;
        private readonly StaffRepository staffRepository;
        public StaffRepositoryTests() 
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            staffRepository = new StaffRepository(dbContext);
        }

        // Add New Staff
        [Fact]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Act
            var result = await staffRepository.CreateAsync(staff);

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.Equal(staff.Id, result.Id);
            Assert.Equal(staff.FirstName, result.FirstName);
            Assert.Equal(staff.LastName, result.LastName);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // Arrange
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Add the staff to the database
            await staffRepository.CreateAsync(staff);

            // Act
            var result = await staffRepository.GetByIdAsync(staff.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(staff.Id, result.Id);
            Assert.Equal(staff.FirstName, result.FirstName);
            Assert.Equal(staff.LastName, result.LastName);
        }

        [Fact]
        public async Task GetAllAsync_Success()
        {
            // Arrange
            var staff1 = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                StaffRole = "Dentist",
                Email = "emilyjohnson@gmail.com",
                Phone = "07-123456789",
                Sex = "Female",
                Age = "27",
                Address = "123 Elm Street, Springfield, IL 62704"
            };

            var staff2 = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Brown",
                StaffRole = "Dentist",
                Email = "michaelbrown@gmail.com",
                Phone = "07-987654321",
                Sex = "Male",
                Age = "45",
                Address = "789 Maple Avenue, Anytown, TX 75001",
            };

            // Add the staffs to the database
            await staffRepository.CreateAsync(staff1);
            await staffRepository.CreateAsync(staff2);

            // Act
            var result = await staffRepository.GetAllAsync();

            // Assert
            Assert.Contains(result, p => p.FirstName == "Emily" && p.LastName == "Johnson");
            Assert.Contains(result, p => p.FirstName == "Michael" && p.LastName == "Brown");
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Add the staff to the database
            await staffRepository.CreateAsync(staff);

            // Act
            staff.FirstName = "Emily";
            staff.LastName = "Johnson";
            staff.StaffRole = "Dentist";
            staff.Email = "emilyjohnson@gmail.com";
            staff.Phone = "07-123456789";
            staff.Sex = "Female";
            staff.Age = "27";
            staff.Address = "123 Elm Street, Springfield, IL 62704";

            // Update the staff in the database
            var updatedPatient = await staffRepository.UpdateAsync(staff);

            // Assert
            Assert.NotNull(updatedPatient);
            Assert.Equal("Emily", updatedPatient.FirstName);
            Assert.Equal("Johnson", updatedPatient.LastName);
            Assert.Equal("Dentist", updatedPatient.StaffRole);
            Assert.Equal("emilyjohnson@gmail.com", updatedPatient.Email);
            Assert.Equal("07-123456789", updatedPatient.Phone);
            Assert.Equal("Female", updatedPatient.Sex);
            Assert.Equal("27", updatedPatient.Age);
            Assert.Equal("123 Elm Street, Springfield, IL 62704", updatedPatient.Address);
    

        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Add the staff to the database
            await staffRepository.CreateAsync(staff);

            // Act
            var result = await staffRepository.DeleteAsync(staff.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(staff.Id, result.Id);

            // Verify that the staff was removed from the database
            var deletedPatient = await staffRepository.GetByIdAsync(staff.Id);
            Assert.Null(deletedPatient);
        }

        [Fact]
        public async Task GetStaffsTotal_Success()
        {

            // Arrange
            var staff1 = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "Emily",
                LastName = "Johnson",
                StaffRole = "Dentist",
                Email = "emilyjohnson@gmail.com",
                Phone = "07-123456789",
                Sex = "Female",
                Age = "27",
                Address = "123 Elm Street, Springfield, IL 62704"

            };

            var staff2 = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = "Michael",
                LastName = "Brown",
                StaffRole = "Dentist",
                Email = "michaelbrown@gmail.com",
                Phone = "07-987654321",
                Sex = "Male",
                Age = "45",
                Address = "789 Maple Avenue, Anytown, TX 75001",
            };

            // Add the staffs to the database
            await staffRepository.CreateAsync(staff1);
            await staffRepository.CreateAsync(staff2);

            // Act
            var count = await staffRepository.GetCount();

             // Assert
            Assert.Equal(2, count);
        }
    }
}


