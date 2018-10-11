using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaptiX.RAMHX.eDoctorBrand.Models
{
    public class SessionManager
    {
        public static string OTP
        {
            get
            {
                if (HttpContext.Current.Session["OTP"] == null)
                {
                    return null;
                }
                else
                {
                    return (string)HttpContext.Current.Session["OTP"];
                }
            }

            set
            {
                HttpContext.Current.Session["OTP"] = value;
            }
        }
    }
}