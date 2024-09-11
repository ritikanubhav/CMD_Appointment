using CMD.Appointment.Domain.Entities;
using CMD.Appointment.Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMD.Appointment.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepo appointmentRepo;

        public AppointmentsController(IAppointmentRepo appointmentRepo)
        {
            this.appointmentRepo = appointmentRepo;
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentModel appointment)
        {
            try
            {
                await appointmentRepo.AddAppointment(appointment);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Put : api/appointments
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(AppointmentModel appointment)
        {
            try
            {
                await appointmentRepo.UpdateAppointment(appointment);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET : api/appintments/1
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointment=await appointmentRepo.GetAppointmentById(id);
                return Ok(appointment);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET : api/appintments?pageNo=1?pageLimit=10
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments([FromQuery] int pageNo, [FromQuery] int pageLimit)
        {
            var allAppointments= await appointmentRepo.GetAllAppointments(pageNo, pageLimit);
            if(allAppointments==null||allAppointments.Count()==0)
                return NotFound("No appointments found.");
            return Ok(allAppointments);
        }

        // GET: api/Appointments/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveAppointments([FromQuery] int pageNo, [FromQuery] int pageLimit)
        {
            var result = await appointmentRepo.GetActiveAppointments(pageNo, pageLimit);

            if (result == null || result.Count == 0)
                return NotFound("No Active appointments found.");

            return Ok(result);
        }

        // GET: api/Appointments/Inactive
        [HttpGet("Inactive")]
        public async Task<IActionResult> GetInactiveAppointments([FromQuery] int pageNo, [FromQuery] int pageLimit)
        {
            var result = await appointmentRepo.GetInactiveAppointments(pageNo, pageLimit);

            if (result == null || result.Count == 0)
                return NotFound("No inactive appointments found.");

            return Ok(result);
        }

        // GET: api/Appointments/FilterByDate
        [HttpGet("FilterByDate")]
        public async Task<IActionResult> FilterAppointmentsByDate(DateTime date)
        {
            var result = await appointmentRepo.FilterAppointmentsByDate(DateOnly.FromDateTime(date));

            if (result == null || result.Count == 0)
                return NotFound("No appointments found for the specified date.");

            return Ok(result);
        }

        // GET: api/Appointments/FilterByStatus
        [HttpGet("FilterByStatus")]
        public async Task<IActionResult> FilterAppointmentsByStatus(string status)
        {
            var result = await appointmentRepo.FilterAppointmentsByStatus(status);

            if (result == null || result.Count == 0)
                return NotFound($"No appointments found with status: {status}");

            return Ok(result);
        }

        // PUT: api/Appointments/Cancel/{id}
        [HttpPut("Cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            await appointmentRepo.CancelAppointment(id);
            return Ok("Appointment cancelled successfully.");
        }
    }
}
