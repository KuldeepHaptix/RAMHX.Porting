using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class AppointmentRepository : BaseRepository
    {
        public string GenerateOTP()
        {
            string numbers = "1234567890";

            string characters = numbers;

            string otp = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public bool BookAppointment(Appointment appo)
        {
            try
            {
                appo.ApplicationOn = DateTime.Now;
                appo.AppointmentId = Guid.NewGuid();
                appo.CreatedBy = new Guid();
                appo.CreatedDateTime = DateTime.Now;
                dataContext.Appointments.Add(appo);
                dataContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("Error Occured in Book Appointment :- " + ex.Message + "", ex);
                return false;
            }
        }

        public Appointment GetAppoiment(Guid appointmentId)
        {
            return dataContext.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
        }

        public List<Appointment> GeAppoiments()
        {
            return dataContext.Appointments.ToList();
        }

        public Appointment ChangeAppoimentStatus(Guid appointmentId,  int status, Guid updatedBy, Guid patientId  = new Guid())
        {
            Appointment ap = dataContext.Appointments.FirstOrDefault(x => x.AppointmentId == appointmentId);
            if (ap != null)
            {
                ap.Status = status;
                ap.UpdatedBy = updatedBy;
                if (patientId != new Guid())
                ap.PatientId = patientId;
                ap.UpdatedDate = DateTime.Now;
                dataContext.SaveChanges();
            }

            return ap;
        }

        public Appointment AddUpdateAppointment(Appointment appointment)
        {
            Appointment ap = dataContext.Appointments.FirstOrDefault(x => x.AppointmentId == appointment.AppointmentId);
            if (ap == null)
            {
                ap = AddAppointment(appointment);
            }
            else
            {
                ap.DoctorId = appointment.DoctorId;
                ap.LocationId = appointment.LocationId;
                ap.Status = appointment.Status;
                ap.FullName = appointment.FullName;
                ap.BirthDate = appointment.BirthDate;
                ap.Mobile = appointment.Mobile;
                ap.Email = appointment.Email;
                ap.Gender = appointment.Gender;
                ap.ApplicationOn = appointment.ApplicationOn;
                ap.PatientId = appointment.PatientId;
                ap.UpdatedDate = DateTime.Now;
                ap.UpdatedBy = appointment.UpdatedBy;
                dataContext.SaveChanges();
            }
            return ap;

        }

        public Appointment AddAppointment(Appointment appointment)
        {
            Appointment ap = new Appointment();
            ap.AppointmentId = Guid.NewGuid();
            ap.DoctorId = appointment.DoctorId;
            ap.LocationId = appointment.LocationId;
            ap.Status = appointment.Status;
            ap.FullName = appointment.FullName;
            ap.BirthDate = appointment.BirthDate;
            ap.Mobile = appointment.Mobile;
            ap.Email = appointment.Email;
            ap.Gender = appointment.Gender;
            ap.ApplicationOn = appointment.ApplicationOn;
            ap.PatientId = appointment.PatientId;

            ap.CreatedDateTime = DateTime.Now;
            ap.CreatedBy = appointment.CreatedBy;

            dataContext.Appointments.Add(ap);
            dataContext.SaveChanges();

            return ap;
        }


        //public List<Doctor> DoctorList()
        //{
        //    return dataContext.Doctors.ToList();
        //}

        //public List<Location> LocationList()
        //{
        //    return dataContext.Locations.ToList();
        //}

        //public Location GetLocation(Guid locationId)
        //{
        //    return dataContext.Locations.FirstOrDefault(l => l.LocationId == locationId);
        //}

        //public Doctor GetDoctor(Guid doctorId)
        //{
        //    return dataContext.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        //}

    }
}