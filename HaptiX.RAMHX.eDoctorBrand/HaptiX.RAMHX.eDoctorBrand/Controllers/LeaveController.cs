using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctor.Repository;
using RAMHX.CMS.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class LeaveController : Controller
    {
        LeaveRepository LeaveRepo = new LeaveRepository();

        // GET: Service
        public ActionResult GetHolidays()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.Data = LeaveRepo.GetLeaves();
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
                response.Data = LeaveRepo.GetLeave(Guid.Parse(holidayid));
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
                Leave leave = new Leave();
                if (!string.IsNullOrEmpty(collection["LeaveId"]))
                    leave.LeaveId = Guid.Parse(collection["LeaveId"]);

                leave.UserId =  SiteContext.CurrentUser_Guid;
                leave.Date = DateTime.Parse(collection["Date"]);
                leave.TypeId = int.Parse(collection["TypeId"]);
                leave.Comment = collection["Comment"];
                leave.StatusId = int.Parse(collection["StatusId"]);
                
                response.Data = LeaveRepo.AddUpdateLeave(leave);
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