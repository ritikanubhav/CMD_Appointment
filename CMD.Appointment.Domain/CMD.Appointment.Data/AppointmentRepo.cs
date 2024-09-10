using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD.Appointment.Domain.Entities;
using CMD.Appointment.Domain.Enums;
using CMD.Appointment.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CMD.Appointment.Data
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppointmentDbContext db;
        public AppointmentRepo(AppointmentDbContext db) { this.db = db; }
        public Task AddAppointment(AppointmentModel appointmentModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppointmentModel>> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public Task<AppointmentModel> GetAppointmentById(int id)
        {
            //only for the appointment ID
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

        public async Task<List<AppointmentModel>> FilterAppointmentsByDate(DateOnly date)
        {
            return await db.Appointments.Where(a => a.Date == date).ToListAsync();
            
        }

        public async Task<List<AppointmentModel>> FilterAppointmentsByStatus(string status)
        {
            return await db.Appointments.Where(a=>a.Status.ToString()==status).ToListAsync();
        }

        public Task<List<AppointmentModel>> GetActiveAppointments()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppointmentModel>> GetInactiveAppointments()
        {
            return await db.Appointments.Where(a=>a.Status==AppointmentStatus.CANCELLED ||a.Status==AppointmentStatus.CLOSED).ToListAsync();
        }

        public async Task CancelAppointment(int id)
        {
            var exitingRecord = await db.Appointments.FindAsync(id);
            if (exitingRecord != null) 
            {
                exitingRecord.Status = AppointmentStatus.CANCELLED;
                exitingRecord.LastModifiedDate = DateTime.UtcNow;
                db.SaveChanges();
            }

        }
    }
}
