using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaptiX.RAMHX.eDoctorBrand.Models
{
    public class Constatnts
    {
        public const string SendOTPSubject = "One Time Password";
        public const string AppointmentSubject = "Your appointment has been confirmed";
        public const string AppointmentSubjectAdmin = "New Appointment Registered";
        public const string Dr_Phno = "#DOCTOR_PHNO#";
        public const string Name = "#NAME#";
        public const string OTP = "#OTP#";
        public const string BookingDate = "#BOOKDATE#";
        public const string BookingTime = "#BOOKTIME#";
        public const string Location = "#LOCATION#";
        public const string DoctorName = "#DOCTOR#";
        public const string HtmlModuleCode_OTP_Tamplate = "EMAIL_TMPL_OTP";
        public const string HtmlModuleCode_Email_Tamplate = "EMAIL_TMPL_BOOKAPPOINTMENT";
        public const string SmtpUsername = "SmtpUsername";
        public const string FromEmail = "From.EmailID";
    }
}