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
            this.appointmentRepo=appointmentRepo;
        }


    }
}
