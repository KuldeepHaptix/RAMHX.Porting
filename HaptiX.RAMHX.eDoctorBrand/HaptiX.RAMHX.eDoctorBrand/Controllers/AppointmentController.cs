using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctor.Repository;
using RAMHX.CMS.Repository;
using HaptiX.RAMHX.eDoctorBrand.Models;
using RAMHX.CMS.Web;
using System.Globalization;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentRepository appoinmentRepo = new AppointmentRepository();
        DoctorRepository doctorRepo = new DoctorRepository();
        LocationRepository locationRepo = new LocationRepository();
        PatientRepository patientRepo = new PatientRepository();
        CultureInfo provider = CultureInfo.InvariantCulture;
        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        public JsonResult SendOTP(Appointment appo)
        {
            SessionManager.OTP = appoinmentRepo.GenerateOTP();
            SendEmailRepo sendEmail = new SendEmailRepo();
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add(Constatnts.Name, appo.FullName);
            paras.Add(Constatnts.OTP, SessionManager.OTP);
            string message = sendEmail.GetEmailBody(Constatnts.HtmlModuleCode_OTP_Tamplate, paras);
            sendEmail.SendEmail(Constatnts.SendOTPSubject, appo.Email, System.Configuration.ConfigurationManager.AppSettings[Constatnts.SmtpUsername], message);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult BookAppointment(Appointment appo, string OTP)
        {
            if (OTP == SessionManager.OTP)
            {
                bool saved = appoinmentRepo.BookAppointment(appo);
                if (saved)
                {
                    SendEmailRepo sendEmail = new SendEmailRepo();
                    Dictionary<string, string> paras = new Dictionary<string, string>();
                    paras.Add(Constatnts.Name, appo.FullName);
                    paras.Add(Constatnts.BookingDate, appo.ApplicationOn.ToString());
                    paras.Add(Constatnts.DoctorName, doctorRepo.GetDoctor((Guid)appo.DoctorId).FullName);
                    paras.Add(Constatnts.Location, locationRepo.GetLocation((Guid)appo.LocationId).Name);
                    paras.Add(Constatnts.Dr_Phno, doctorRepo.GetDoctor((Guid)appo.DoctorId).Mobile);
                    paras.Add(Constatnts.BookingTime, appo.ApplicationOn.Hour.ToString() + ":" + appo.ApplicationOn.Minute.ToString());
                    string message = sendEmail.GetEmailBody(Constatnts.HtmlModuleCode_Email_Tamplate, paras);
                    sendEmail.SendEmail(Constatnts.AppointmentSubject, appo.Email, System.Configuration.ConfigurationManager.AppSettings[Constatnts.SmtpUsername], message);
                    sendEmail.SendEmail(Constatnts.AppointmentSubjectAdmin, System.Configuration.ConfigurationManager.AppSettings[Constatnts.FromEmail], System.Configuration.ConfigurationManager.AppSettings[Constatnts.SmtpUsername], message);
                    return Json(saved, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false);
        }

        //public JsonResult GetDoctors()
        //{
        //    return Json(doctorRepo.GetDoctors(), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult GetLocations()
        {
            return Json(locationRepo.GetLocations(), JsonRequestBehavior.AllowGet);
        }

        // GET: Appoiments
        public ActionResult GeAppoiments()
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var result = (from appoinment in appoinmentRepo.GeAppoiments()
                              join patient in patientRepo.GetPaitients() on appoinment.PatientId equals patient.PatientId into dt
                              from subPatient in dt.DefaultIfEmpty()
                              select new
                              {
                                  AppointmentId = appoinment.AppointmentId,
                                  DoctorId = appoinment.DoctorId,
                                  LocationId = appoinment.LocationId,
                                  Status = appoinment.Status,
                                  FullName = appoinment.FullName,
                                  BirthDate = appoinment.BirthDate,
                                  Mobile = appoinment.Mobile,
                                  Email = appoinment.Email,
                                  Gender = appoinment.Gender,
                                  ApplicationOn = appoinment.ApplicationOn,
                                  PatientId = appoinment.PatientId,
                                  AppointmentOrder = appoinment.AppointmentOrder,

                                  PatientFullName = subPatient?.FullName,
                                  PatientBirthDate = subPatient?.BirthDate,
                                  PatientMobile = subPatient?.Mobile,
                                  PatientEmail = subPatient?.Email,
                                  PatientGender = subPatient?.Gender
                              });

                response.Data = result;
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAppoimentStatus(string appointmentid, string statuscode)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = appoinmentRepo.ChangeAppoimentStatus(Guid.Parse(appointmentid), int.Parse(statuscode), /*SiteContext.CurrentUser_Guid*/ Guid.NewGuid());
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult UpdateAppoimentStatusWithPatient(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                Guid patientId = Guid.Empty;
                Guid appoinmentId = Guid.Empty;

                Guid.TryParse(collection["PatientId"], out patientId);
                Guid.TryParse(collection["AppointmentId"], out appoinmentId);

                // Update status of Appoinment 
                if (patientId != Guid.Empty && appoinmentId != Guid.Empty)
                {
                    appoinmentRepo.ChangeAppoimentStatus(appoinmentId, (int)Enums.AppoinmentStatus.Ongoing, SiteContext.CurrentUser_Guid, patientId);
                }
                // Create new appoinment for existing patient
                else if (patientId != Guid.Empty && appoinmentId == Guid.Empty)
                {
                    AddUpdateAppointment(collection, patientId);
                }
                // Create new patient for existing appoinment 
                else if (patientId == Guid.Empty && appoinmentId != Guid.Empty)
                {
                    patientId = this.AddPatient(collection).PatientId;
                    appoinmentRepo.ChangeAppoimentStatus(appoinmentId, int.Parse(collection["Status"]), SiteContext.CurrentUser_Guid, patientId);
                }
                // Create new appoinment as well as paitient
                else
                {
                    patientId = this.AddPatient(collection).PatientId;
                    AddUpdateAppointment(collection, patientId);
                }
                //response.Data = appoinmentRepo.ChangeAppoimentStatus(Guid.Parse(appointmentid), int.Parse(statuscode), /*SiteContext.CurrentUser_Guid*/ Guid.NewGuid());

                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = "Please insert valid data. " + ex.Message;

            }
            return Json(response, JsonRequestBehavior.AllowGet);

        }

        // GET: Appoiments
        public JsonResult GetAppoiment(string appointmentid)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = appoinmentRepo.GetAppoiment(Guid.Parse(appointmentid));
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Userd to add new paitient
        /// </summary>
        /// <param name="collection">FormColletion as a </param>
        /// <returns>Newly added paitient</returns>
        private Patient AddPatient(FormCollection collection)
        {
            Patient patient = new Patient();
            patient.FullName = collection["FullName"];
            patient.Gender = collection["Gender"];
            patient.BirthDate = DateTime.ParseExact(collection["BirthDate"], Contants.DATEFORMATE, provider);
            patient.Mobile = collection["Mobile"];
            patient.Email = collection["Email"];
            patient.Address = collection["Address"];

            return patientRepo.AddPaitient(patient);
        }

        private Appointment AddUpdateAppointment(FormCollection collection, Guid patientId)
        {
            Appointment appointment = new Appointment();
            Patient patient = patientRepo.GetPaitient(patientId);

            appointment.FullName = patient.FullName;
            appointment.Gender = patient.Gender;
            appointment.BirthDate = patient.BirthDate;
            appointment.Mobile = patient.Mobile;
            appointment.Email = patient.Email;
            appointment.DoctorId = Guid.Parse(collection["DoctorId"]);
            appointment.LocationId = Guid.Parse(collection["LocationId"]);
            appointment.Status = int.Parse(collection["Status"]);
            if (!string.IsNullOrEmpty(collection["ApplicationOn"]) && !string.IsNullOrEmpty(collection["Time"]))
            {
                appointment.ApplicationOn = DateTime.ParseExact(collection["ApplicationOn"], Contants.DATEFORMATE, provider);
                appointment.ApplicationOn.AddHours(int.Parse(collection["Time"].Split(':')[0]));
                appointment.ApplicationOn.AddMinutes(int.Parse(collection["Time"].Split(':')[1]));
            }
            else
            {
                appointment.ApplicationOn = DateTime.Now;
            }

            appointment.PatientId = patientId;

            appoinmentRepo.AddUpdateAppointment(appointment);
            return appointment;
        }

        /// <summary>
        /// Used to appoinment by staff member 
        /// </summary>
        /// <param name="collection">Input form data</param>
        /// <returns></returns>
        [Authorize]
        public JsonResult BookAppointmentByStaff(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                Appointment appointment = new Appointment();

                appointment.FullName = collection["FullName"];
                appointment.Gender = collection["Gender"];
                //appointment.BirthDate = patient.BirthDate;
                appointment.Mobile = collection["Mobile"];
                appointment.Email = collection["Email"];
                appointment.DoctorId = Guid.Parse(collection["DoctorId"]);
                appointment.LocationId = Guid.Parse(collection["LocationId"]);
                appointment.Status = (int)Enums.AppoinmentStatus.Pending;
                appointment.ApplicationOn = DateTime.ParseExact(collection["ApplicationOn"], Contants.DATEFORMATE, provider);
                appointment.ApplicationOn.AddHours(int.Parse(collection["Time"].Split(':')[0]));
                appointment.ApplicationOn.AddMinutes(int.Parse(collection["Time"].Split(':')[1]));
                appointment.CreatedBy = SiteContext.CurrentUser_Guid;

                if (!string.IsNullOrEmpty(collection["PatientId"]))
                    appointment.PatientId = Guid.Parse(collection["PatientId"]);

                appoinmentRepo.AddAppointment(appointment);
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = "Please insert valid data. " + ex.Message;
                logger.Error("Error occur while booking appoinment by staff member ", ex);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}