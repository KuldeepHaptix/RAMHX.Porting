using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Infra;
using HaptiX.RAMHX.eDoctor.Repository;
using RAMHX.CMS.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class AvailabilityController : Controller
    {
        CultureInfo provider = CultureInfo.InvariantCulture;

        AvailabilityRepository availRepo = new AvailabilityRepository();

        // GET: Availability
        public ActionResult GetAvailabilities()
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = availRepo.GetAvailabilities();
                response.Status = Contants.APISTATUSSUCCESS;
            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Availability By AvailabilityId
        public ActionResult GetAvailability(string availabilityid)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = availRepo.GetAvailability(Guid.Parse(availabilityid));
                response.Status = Contants.APISTATUSSUCCESS;

            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Availability By AvailabilityId
        public ActionResult GetAvailabilityByDoctorAndlocation(string doctorid, string locationid)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = availRepo.GetAvailability(Guid.Parse(doctorid), Guid.Parse(locationid));
                response.Status = Contants.APISTATUSSUCCESS;

            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Availability By AvailabilityId
        public ActionResult GetAvailabilityByDoctor(string doctorid)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                response.Data = availRepo.GetAvailabilitiesByDoctorId(Guid.Parse(doctorid));
                response.Status = Contants.APISTATUSSUCCESS;

            }
            catch (Exception ex)
            {
                response.Status = Contants.APISTATUSERROR;
                response.Message = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // POST: Availability/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Availability/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Availability/Edit/5
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

        // GET: Availability/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Availability/Delete/5
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

        // POST: Availability/Edit/5
        [HttpPost]
        public JsonResult AddUpdate(FormCollection collection)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                // TODO: Add update logic here
                Availability availability = new Availability();
                availability = new Availability();

                availability.UpdatedBy = availability.CreatedBy = SiteContext.CurrentUser_Guid;
                if (SiteContext.CurrentUser_Roles.Select(x=> x.Name).Contains(Contants.USERROLE_DOCTOR))
                {
                    availability.DoctorId = SiteContext.CurrentUser_Guid;
                }
                else
                {
                    availability.DoctorId = Guid.Parse(collection["DoctorId"]);
                }

                availability.OnSunday = bool.Parse(collection["OnSunday"]);
                availability.OnMonday = bool.Parse(collection["OnMonday"]);
                availability.OnTuesday = bool.Parse(collection["OnTuesday"]);
                availability.OnWednesday = bool.Parse(collection["OnWednesday"]);
                availability.OnThursday = bool.Parse(collection["OnThursday"]);
                availability.OnFriday = bool.Parse(collection["OnFriday"]);

                availability.OnSaturday = bool.Parse(collection["OnSaturday"]);

                if (!string.IsNullOrEmpty(collection["AvailabilityId"]))
                    availability.AvailabilityId = Guid.Parse(collection["AvailabilityId"]);
                
                availability.LocationId = Guid.Parse(collection["LocationId"]);
                availability.StartDate = DateTime.ParseExact(collection["StartDate"], Contants.DATEFORMATE, provider);
                availability.EndDate = DateTime.ParseExact(collection["EndDate"], Contants.DATEFORMATE, provider);
                availability.DurationInMinute = Convert.ToInt32(collection["DurationInMinute"]);

                if (availability.OnSunday)
                {
                    availability.SundayMorningStart = TimeSpan.Parse(collection["SundayMorningStart"]);
                    availability.SundayMorningEnd = TimeSpan.Parse(collection["SundayMorningEnd"]);
                    availability.SundayEveningStart = TimeSpan.Parse(collection["SundayEveningStart"]);
                    availability.SundayEveningEnd = TimeSpan.Parse(collection["SundayEveningEnd"]);
                }

                if (availability.OnMonday)
                {
                    availability.MondayMorningStart = TimeSpan.Parse(collection["MondayMorningStart"]);
                    availability.MondayMorningEnd = TimeSpan.Parse(collection["MondayMorningEnd"]);
                    availability.MondayEveningStart = TimeSpan.Parse(collection["MondayEveningStart"]);
                    availability.MondayEveningEnd = TimeSpan.Parse(collection["MondayEveningEnd"]);
                }

                if (availability.OnTuesday)
                {
                    availability.TuesdayMorningStart = TimeSpan.Parse(collection["TuesdayMorningStart"]);
                    availability.TuesdayMorningEnd = TimeSpan.Parse(collection["TuesdayMorningEnd"]);
                    availability.TuesdayEveningStart = TimeSpan.Parse(collection["TuesdayEveningStart"]);
                    availability.TuesdayEveningEnd = TimeSpan.Parse(collection["TuesdayEveningEnd"]);
                }

                if (availability.OnWednesday)
                {
                    availability.WednesdayMorningStart = TimeSpan.Parse(collection["WednesdayMorningStart"]);
                    availability.WednesdayMorningEnd = TimeSpan.Parse(collection["WednesdayMorningEnd"]);
                    availability.WednesdayEveningStart = TimeSpan.Parse(collection["WednesdayEveningStart"]);
                    availability.WednesdayEveningEnd = TimeSpan.Parse(collection["WednesdayEveningEnd"]);
                }

                if (availability.OnThursday)
                {
                    availability.ThursdayMorningStart = TimeSpan.Parse(collection["ThursdayMorningStart"]);
                    availability.ThursdayMorningEnd = TimeSpan.Parse(collection["ThursdayMorningEnd"]);
                    availability.ThursdayEveningStart = TimeSpan.Parse(collection["ThursdayEveningStart"]);
                    availability.ThursdayEveningEnd = TimeSpan.Parse(collection["ThursdayEveningEnd"]);
                }

                if (availability.OnFriday)
                {
                    availability.FridayMorningStart = TimeSpan.Parse(collection["FridayMorningStart"]);
                    availability.FridayMorningEnd = TimeSpan.Parse(collection["FridayMorningEnd"]);
                    availability.FridayEveningStart = TimeSpan.Parse(collection["FridayEveningStart"]);
                    availability.FridayEveningEnd = TimeSpan.Parse(collection["FridayEveningEnd"]);
                }

                if (availability.OnSaturday)
                {
                    availability.SaturdayMorningStart = TimeSpan.Parse(collection["SaturdayMorningStart"]);
                    availability.SaturdayMorningEnd = TimeSpan.Parse(collection["SaturdayMorningEnd"]);
                    availability.SaturdayEveningStart = TimeSpan.Parse(collection["SaturdayEveningStart"]);
                    availability.SaturdayEveningEnd = TimeSpan.Parse(collection["SaturdayEveningEnd"]);
                }

                response.Data = availRepo.AddUpdateAvaibility(availability);
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

        public DateTime GetTest()
        {
            return DateTime.Now;
        }

        public ActionResult GetAvailabilityView(string doctorid, string locationid)
        {
            // TODO: Make ajax call to make booking 
            Availability avail = availRepo.GetAvailability(Guid.Parse(doctorid), Guid.Parse(locationid));
            if (avail != null)
                return PartialView("/Views/SlotViewer/SlotViewerPartialView.cshtml", avail);
            else
                return Content("");
        }
    }
}
