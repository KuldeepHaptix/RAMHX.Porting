using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctorBrand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class LocationRepository : BaseRepository
    {
        public List<Location> GetLocations()
        {
            return dataContext.Locations.ToList();
        }

        public List<Location> GetLocations(Guid docId)
        {
            List<Guid> locationsIds = dataContext.DoctorLocations.Where(x => x.DoctorId == docId).Select(s => s.LocationId).ToList();
            return dataContext.Locations.Where(x => locationsIds.Contains(x.LocationId)).ToList();
        }

        public Location GetLocation(Guid locationId)
        {
            return dataContext.Locations.FirstOrDefault(l => l.LocationId == locationId);
        }

        public List<DropDownValueList> GetLocationsForDropdown()
        {
            return dataContext.Locations.Select(x => new DropDownValueList() { Id = x.LocationId.ToString(), Name = x.Name }).ToList();
        }

        public Location AddUpdateLocation(Location location)
        {
            Location loc = dataContext.Locations.FirstOrDefault(x => x.LocationId == location.LocationId);
            if (loc == null)
            {
                loc = new Location();
                loc.LocationId = Guid.NewGuid();
                loc.CreatedBy = location.CreatedBy;
                loc.CreatedDate = DateTime.Now;
                loc.Name = location.Name;
                loc.FullAddress = location.FullAddress;
                loc.City = location.City;
                loc.PinCode = location.PinCode;
                loc.IsActive = location.IsActive;
                dataContext.Locations.Add(loc);
            }
            else
            {
                loc.UpdatedBy = location.UpdatedBy;
                loc.UpdatedDate = DateTime.Now;
                loc.Name = location.Name;
                loc.FullAddress = location.FullAddress;
                loc.City = location.City;
                loc.PinCode = location.PinCode;
                loc.IsActive = location.IsActive;
            }

            dataContext.SaveChanges();

            return loc;
        }
    }
}
