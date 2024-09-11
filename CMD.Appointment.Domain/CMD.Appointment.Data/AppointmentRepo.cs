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
        public async Task AddAppointment(AppointmentModel appointmentModel)
        {
            await db.Appointments.AddAsync(appointmentModel);
            await db.SaveChangesAsync();
        }

        public async Task<List<AppointmentModel>> GetAllAppointments(int pageNumber = 1, int pageSize = 10)
        {
            var pagedAppointments = await db.Appointments
                                    .Skip((pageNumber - 1) * pageSize) // Skips the records for previous pages
                                    .Take(pageSize) // Takes the records for the current page
                                    .ToListAsync();
            return pagedAppointments;
        }

        public async Task<AppointmentModel> GetAppointmentById(int id)
        {
            var appointment= await db.Appointments.FindAsync(id);
            if (appointment != null)
                return appointment;
            else
                throw new Exception("No Appointment Found for this Id");
        }

        public async Task UpdateAppointment(AppointmentModel appointmentModel)
        {
            db.Appointments.Update(appointmentModel);
            await db.SaveChangesAsync();
        }
        public async Task<List<AppointmentModel>> GetActiveAppointments(int pageNumber = 1, int pageSize = 10)
        {
             var pagedAppointments = await db.Appointments.Where(a => a.Status == AppointmentStatus.SCHEDULED)
                                    .Skip((pageNumber - 1) * pageSize) // Skips the records for previous pages
                                    .Take(pageSize) // Takes the records for the current page
                                    .ToListAsync();
            return pagedAppointments;
        }

        public async Task<List<AppointmentModel>> FilterAppointmentsByDate(DateOnly date)
        {
            return await db.Appointments.Where(a => a.Date == date).ToListAsync();
            
        }

        public async Task<List<AppointmentModel>> FilterAppointmentsByStatus(string status)
        {
            return await db.Appointments.Where(a=>a.Status.ToString()==status).ToListAsync();
        }


        public async Task<List<AppointmentModel>> GetInactiveAppointments(int pageNumber = 1, int pageSize = 10)
        {
            var pagedAppointments = await db.Appointments.Where(a => a.Status == AppointmentStatus.CANCELLED||a.Status==AppointmentStatus.CLOSED)
                                    .Skip((pageNumber - 1) * pageSize) // Skips the records for previous pages
                                    .Take(pageSize) // Takes the records for the current page
                                    .ToListAsync();
            return pagedAppointments;
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
