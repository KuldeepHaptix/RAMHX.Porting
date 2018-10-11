using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctor.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaptiX.RAMHX.eDoctorBrand.Controllers
{
    public class LocationNavigationController : Controller
    {
        DoctorRepository doctorRepo = new DoctorRepository();
        LocationRepository locationRepo = new LocationRepository();
        DoctorLocationRepository doclocRepo = new DoctorLocationRepository();


        // GET: LocationNavigation
        public ActionResult Index()
        {
            List<DoctorLocation> docLoc = doclocRepo.GetDoctorLocation();
            List<Guid> doctorIds = docLoc.Select(x => x.DoctorId).ToList();
            List<Guid> locationIds = docLoc.Select(x => x.LocationId).ToList();


            ViewBag.GetDoctorLocation = docLoc;

            ViewBag.Doctors = doctorRepo.GetDoctors().Where(x=> doctorIds.Contains(x.DoctorId) );

            ViewBag.Locations = locationRepo.GetLocations().Where(x => locationIds.Contains(x.LocationId)); 

            

            return View();
        }
    }
}