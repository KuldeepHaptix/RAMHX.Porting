using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Infra
{
    public class Contants
    {
        public const string DATEFORMATE = "dd/MM/yyyy";
        public const string APISTATUSSUCCESS = "success";
        public const string APISTATUSERROR = "error";

        public const string USERROLE_DOCTOR = "Doctor";
        public const string USERROLE_STAFF = "Staff";

        public const string EMAILPARA_NAME = "#NAME#";
        public const string EMAILPARA_MOBILE = "#MOBILE#";
        public const string EMAILPARA_EMAIL = "#EMAIL#";
        public const string EMAILPARA_MESSAGE = "#MESSAGE#";
        public const string HTMLMODULECODE_CONTACTUS_TAMPLATE = "EMAIL_TMPL_CONTACTUS";
        public const string CONTACTUS_SUBJECT = "Message from Contact us";
        public const string CONTACTUS_TO_EMAIL = "ContactUs.To.EmailID";
    }
}
