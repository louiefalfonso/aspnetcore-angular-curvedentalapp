using CurveDentalManagement.API.Models.Domain;
using CurveDentalManagement.API.Models.DTO;
using CurveDentalManagement.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurveDentalManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository staffRepository;

        public StaffsController( IStaffRepository staffRepository)
        {
            this.staffRepository = staffRepository;
        }

        // Add New Staff
        [HttpPost]
        public async Task<IActionResult> CreateNewStaff([FromBody] CreateStaffRequestDto request)
        {
            // mapp dto to domain model
            var staff = new Staff
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                StaffRole = request.StaffRole,
                Email = request.Email,
                Sex = request.Sex,
                Phone = request.Phone,
                Age = request.Age,
                Address = request.Address,
            };

            // add new staff to repository
            await staffRepository.CreateAsync(staff);

            // map domain model to dto
            var response = new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                StaffRole = staff.StaffRole,
                Email = staff.Email,
                Sex = staff.Sex,
                Phone = staff.Phone,
                Age = staff.Age,
                Address = staff.Address,
            };
            return Ok(response);
        }

        // Get Staff By ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetStaffById([FromRoute] Guid id)
        {
            // get staff by ID at repository
            var staff = await staffRepository.GetByIdAsync(id);

            // check if staff is null
            if (staff == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                StaffRole = staff.StaffRole,
                Email = staff.Email,
                Sex = staff.Sex,
                Phone = staff.Phone,
                Age = staff.Age,
                Address = staff.Address,
            };

            return Ok(response);
        }

        // Get All Staffs
        [HttpGet]
        public async Task<IActionResult> GetAllStaffs
            (
                // add filtering, sorting & pagination
                [FromQuery] string? query,
                [FromQuery] string? sortBy,
                [FromQuery] string? sortDirection,
                [FromQuery] int? pageNumber,
                [FromQuery] int? pageSize
            )
        {
            // get all staffs from repository
            var staffs = await staffRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);

            // map Domain model to DTO
            var response = new List<StaffDto>();
            foreach (var staff in staffs)
            {
                response.Add(new StaffDto
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    StaffRole = staff.StaffRole,
                    Email = staff.Email,
                    Sex = staff.Sex,
                    Phone = staff.Phone,
                    Age = staff.Age,
                    Address = staff.Address,
                });
            }
            return Ok(response);
        }

        // Update Staff
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateStaff([FromRoute] Guid id, UpdateStaffRequestDto request)
        {
            // check if staff is null
            if (request == null)
            {
                return BadRequest();
            }

            // convert dto to domain model
            var staff = new Staff
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                StaffRole = request.StaffRole,
                Email = request.Email,
                Sex = request.Sex,
                Phone = request.Phone,
                Age = request.Age,
                Address = request.Address,
            };

            // update staff in repository
            staff = await staffRepository.UpdateAsync(staff);

            // check if staff is null
            if (staff == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                StaffRole = staff.StaffRole,
                Email = staff.Email,
                Sex = staff.Sex,
                Phone = staff.Phone,
                Age = staff.Age,
                Address = staff.Address,
            };
            return Ok(response);
        }

        // Delete Staff
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteStaff([FromRoute] Guid id)
        {
            // delete staff from repository
            var staff = await staffRepository.DeleteAsync(id);

            //check if staff is null
            if (staff == null)
            {
                return NotFound();
            }

            // map domain model to dto
            var response = new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                StaffRole = staff.StaffRole,
                Email = staff.Email,
                Sex = staff.Sex,
                Phone = staff.Phone,
                Age = staff.Age,
                Address = staff.Address,
            };
            return Ok(response);
        }

        // Get Count
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetStaffsTotal()
        {
            var count = await staffRepository.GetCount();
            return Ok(count);
        }
    }
}