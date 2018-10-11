using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class DoctorLocationRepository : BaseRepository
    {
        public List<DoctorLocation> GetDoctorLocation()
        {
            return dataContext.DoctorLocations.ToList();
        }

    }
}
