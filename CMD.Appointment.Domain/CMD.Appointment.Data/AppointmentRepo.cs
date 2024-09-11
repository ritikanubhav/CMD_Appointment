using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD.Appointment.Domain.Entities;
using CMD.Appointment.Domain.IRepositories;

namespace CMD.Appointment.Data
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppointmentDbContext db;
        public AppointmentRepo(AppointmentDbContext db) { this.db = db; }
        public async Task AddAppointment(AppointmentModel appointmentModel)
        {
            await db.Appointments.AddAsync(appointmentModel);
            db.SaveChangesAsync();
        }

        public Task<List<AppointmentModel>> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentModel> GetAppointmentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAppointment(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAppointment(AppointmentModel appointmentModel)
        {
            throw new NotImplementedException();
        }
    }
}
