using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class ServiceController : Controller
    {
        ServiceRepository ServiceRepo = new ServiceRepository();

        // GET: Service
        public ActionResult GetServices()
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.Data = ServiceRepo.GetServices();
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
        public ActionResult GetService(string serviceid)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.Data = ServiceRepo.GetService(Guid.Parse(serviceid));
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
                Service service = new Service();
                if (!string.IsNullOrEmpty(collection["ServiceId"]))
                    service.ServiceId = Guid.Parse(collection["ServiceId"]);

                service.Name = collection["Name"];
                service.IsActive = bool.Parse(collection["IsActive"]);

                response.Data = ServiceRepo.AddUpdateService(service);
                response.Status = Contants.APISTATUSSUCCESS;
                // TODO: Add insert logic here

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
