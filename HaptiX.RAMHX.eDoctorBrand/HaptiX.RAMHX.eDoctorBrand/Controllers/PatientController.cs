using HaptiX.RAMHX.eDoctor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class PatientController : Controller
    {
        PatientRepository pr = new PatientRepository();

        // GET: Patient
        public ActionResult SearchPatient(string keyword, int maxResults)
        {
            var result = from r in pr.GetPaitients(keyword, maxResults)
                         select new {r.FullName, r.Mobile, r.Address , r.Email, r.PatientId};

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}