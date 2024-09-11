using CMD.Appointment.Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMD.Appointment.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo appointmentRepo;

        public AppointmentController(IAppointmentRepo appointmentRepo)
        {
            this.appointmentRepo = appointmentRepo;
        }


        [Route("api/[controller]")]
        [ApiController]
        public class AppointmentsController : ControllerBase
        {
            private readonly IAppointmentRepo _appointmentRepo;

            public AppointmentsController(IAppointmentRepo appointmentRepo)
            {
                _appointmentRepo = appointmentRepo;
            }

            // GET: api/Appointments/FilterByDate
            [HttpGet("FilterByDate")]
            public async Task<IActionResult> FilterAppointmentsByDate(DateTime date)
            {
                var result = await _appointmentRepo.FilterAppointmentsByDate(DateOnly.FromDateTime(date));

                if (result == null || result.Count == 0)
                    return NotFound("No appointments found for the specified date.");

                return Ok(result);
            }

            // GET: api/Appointments/FilterByStatus
            [HttpGet("FilterByStatus")]
            public async Task<IActionResult> FilterAppointmentsByStatus(string status)
            {
                var result = await _appointmentRepo.FilterAppointmentsByStatus(status);

                if (result == null || result.Count == 0)
                    return NotFound($"No appointments found with status: {status}");

                return Ok(result);
            }

            // GET: api/Appointments/Inactive
            [HttpGet("Inactive")]
            public async Task<IActionResult> GetInactiveAppointments()
            {
                var result = await _appointmentRepo.GetInactiveAppointments();

                if (result == null || result.Count == 0)
                    return NotFound("No inactive appointments found.");

                return Ok(result);
            }

            // PUT: api/Appointments/Cancel/{id}
            [HttpPut("Cancel/{id}")]
            public async Task<IActionResult> CancelAppointment(int id)
            {
                await _appointmentRepo.CancelAppointment(id);
                return Ok("Appointment cancelled successfully.");
            }


        }
    }
}
