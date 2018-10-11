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
    public class LocationController : Controller
    {
        LocationRepository LocationRepo = new LocationRepository();

        // GET: Locations
        public ActionResult Index()
        {
            return View();
        }

        // GET: Locations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/AddUpdate
        [HttpPost]
        public ActionResult AddUpdate(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                Location location = new Location();
                if (!string.IsNullOrEmpty(collection["LocationId"]))
                    location.LocationId = Guid.Parse(collection["LocationId"]);

                location.CreatedBy = location.UpdatedBy = Guid.NewGuid();
                location.Name = collection["Name"];
                location.FullAddress = collection["FullAddress"];
                location.City = collection["City"];
                location.PinCode = int.Parse(collection["PinCode"]);
                location.IsActive = bool.Parse(collection["IsActive"]);

                response.Data = LocationRepo.AddUpdateLocation(location);
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

        // GET: Locations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetLocationsByDoctor(string docid)
        {
            //TODO : Make it dynamic for Doctor
            List<Location> locations = LocationRepo.GetLocations(Guid.Parse(docid));
            return PartialView("/Views/SlotViewer/DoctorAvailabilityFilter.cshtml", locations);
        }

        public JsonResult GetLocations()
        {
            return Json(LocationRepo.GetLocations(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocation(string locationid)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = LocationRepo.GetLocation(Guid.Parse(locationid));
                response.Status = Contants.APISTATUSSUCCESS;

            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctorsForDrodown()
        {
            return Json(LocationRepo.GetLocationsForDropdown(), JsonRequestBehavior.AllowGet);
        }
    }
}
