using System.ComponentModel;
using CurveDentalManagement.API.Controllers;
using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CurveDentalManagement.API.Tests.Controller
{
    public class StaffsControllerTests
    {
        private readonly Mock<IStaffRepository> mockStaffRepository;
        private readonly StaffsController staffController;
        public StaffsControllerTests()
        {
            mockStaffRepository = new Mock<IStaffRepository>();

            staffController = new StaffsController(
                mockStaffRepository.Object
            );
        }

        [Fact]
        public async Task CreateNewStaff_Success()
        {
            // Arrange 
            var staff = new Staff
            {
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            var request = new CreateStaffRequestDto
            {
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Simulate Staff Repository
            mockStaffRepository
                .Setup(repo => repo.CreateAsync(It.IsAny<Staff>()))
                .ReturnsAsync(staff);

            // Act 
            var result = await staffController.CreateNewStaff(request);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var returnedStaff = Assert.IsType<StaffDto>(createdResult.Value);
            Assert.Equal(staff.Id, returnedStaff.Id);
        }

        [Fact]
        public async Task GetAllStaffs_Success()
        {
            // Arrange
            var staffs= new List<Staff>
            {
                new Staff
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
                },
                new Staff
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
                },
            };

            // Simulate Staff Repository
            mockStaffRepository
                .Setup(repo => repo.GetAllAsync(null, null, null, 1, 100))
                .ReturnsAsync(staffs);

            // Act
            var result = await staffController.GetAllStaffs(null, null, null, 1, 100);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStaffs = Assert.IsType<List<StaffDto>>(okResult.Value);
            Assert.Equal(2,returnedStaffs.Count);

            // Verify properties of returned patients
            Assert.Equal("Emily", returnedStaffs[0].FirstName);
            Assert.Equal("Johnson", returnedStaffs[0].LastName);
            Assert.Equal("Dentist", returnedStaffs[0].StaffRole);

            // Verify properties of returned patients
            Assert.Equal("Michael", returnedStaffs[1].FirstName);
            Assert.Equal("Brown", returnedStaffs[1].LastName);
            Assert.Equal("Dentist", returnedStaffs[1].StaffRole);
        }

        [Fact]
        public async Task GetStaffById_Success()
        {
            // Arrange
            var staffId = Guid.NewGuid();
            var staff = new Staff
            {
                Id = staffId,
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Simulate Staff Repository
            mockStaffRepository
                .Setup(repo => repo.GetByIdAsync(staffId))
                .ReturnsAsync(staff);

            // Act
            var result = await staffController.GetStaffById(staffId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStaff = Assert.IsType<StaffDto>(okResult.Value);
            Assert.Equal(staffId, returnedStaff.Id);
        }

        [Fact]
        public async Task UpdateStaff_Success()
        {
            // Arrange
            var staffId = Guid.NewGuid();
            var staff = new Staff
            {
                Id = staffId,
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"

            };

            var request = new UpdateStaffRequestDto
            {
                FirstName = "Micheal",
                LastName = "Brown",
                StaffRole = "Dentist",
                Email = "michaelbrown@gmail.com",
                Phone = "07-987654321",
                Sex = "Male",
                Age = "45",
                Address = "789 Maple Avenue, Anytown, TX 75001",
            };

            // Simulate repository and returns the updated staff with the correct Id
            mockStaffRepository
               .Setup(repo => repo.UpdateAsync(It.IsAny<Staff>()))
               .ReturnsAsync((Staff updatedStaff) =>
               {
                   updatedStaff.Id = staffId;
                   return updatedStaff;
               });

            // Act
            var result = await staffController.UpdateStaff(staffId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStaff = Assert.IsType<StaffDto>(okResult.Value);
            Assert.Equal(staffId, returnedStaff.Id);
            Assert.Equal("Micheal", returnedStaff.FirstName);
            Assert.Equal("Brown", returnedStaff.LastName);
            Assert.Equal("Dentist", returnedStaff.StaffRole);
        }

        [Fact]
        public async Task DeletePatientTest_Success()
        {
            // Arrange 
            var staffId = Guid.NewGuid();
            var staff = new Staff
            {
                Id = staffId,
                FirstName = "John",
                LastName = "Doe",
                StaffRole = "Dentist",
                Email = "johndoe@gmail.com",
                Phone = "07-123456789",
                Sex = "Male",
                Age = "35",
                Address = "123 Main Street, Springfield, IL 62704"
            };

            // Simulate Staff Repository
            mockStaffRepository
               .Setup(repo => repo.DeleteAsync(staffId))
               .ReturnsAsync(staff);

            // Act
            var result = await staffController.DeleteStaff(staffId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedStaff = Assert.IsType<StaffDto>(okResult.Value);
            Assert.Equal(staffId, returnedStaff.Id);
            Assert.Equal("John", returnedStaff.FirstName);
            Assert.Equal("Doe", returnedStaff.LastName);
            Assert.Equal("Dentist", returnedStaff.StaffRole);
        }

        [Fact]
        public async Task GetStaffsTotalCount_Success()
        {
            // Arrange
            var count = 5;
            mockStaffRepository
                 .Setup(repo => repo.GetCount())
                 .ReturnsAsync(count);

            // Act
            var result = await staffController.GetStaffsTotal();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCount = Assert.IsType<int>(okResult.Value);
            Assert.Equal(count, returnedCount);
        }

    }
}
