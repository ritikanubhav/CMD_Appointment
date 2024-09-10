using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD.Appointment.Domain.Enums;

namespace CMD.Appointment.Domain.Entities
{
    public class AppointmentModel
    {
        public int Id {  get; set; }
        public string PurposeOfVisit {  get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public AppointmentStatus Status { get; set; }
        public string Message {  get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy {  get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int PatientId {  get; set; }
        public int DoctorId {  get; set; }
    }
}
