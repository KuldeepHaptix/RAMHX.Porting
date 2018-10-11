using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctor.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class HolidayController : Controller
    {
        HolidayRepository HolidayRepo = new HolidayRepository();
        CultureInfo provider = CultureInfo.InvariantCulture;
        // GET: Service
        public ActionResult GetHolidays()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.Data = HolidayRepo.GetHolidays();
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = Contants.APISTATUSERROR;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Service
        public ActionResult GetHoliday(string holidayid)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.Data = HolidayRepo.GetHoliday(Guid.Parse(holidayid));
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = Contants.APISTATUSERROR;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // POST: Service/Create
        [HttpPost]
        public ActionResult AddUpdate(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                Holiday holiday = new Holiday();
                if (!string.IsNullOrEmpty(collection["HolidayId"]))
                    holiday.HolidayId = Guid.Parse(collection["HolidayId"]);

                holiday.Name = collection["Name"];
                holiday.HolidayDate = DateTime.ParseExact(collection["HolidayDate"], Contants.DATEFORMATE, provider);

                response.Data = HolidayRepo.AddUpdateHoliday(holiday);
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error(ex);
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}