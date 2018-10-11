using HaptiX.RAMHX.eDoctor.DataAccess;
using HaptiX.RAMHX.eDoctorBrand.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class DoctorRepository : BaseRepository
    {
        public List<Doctor> GetDoctors()
        {
            return dataContext.Doctors.ToList();
        }

        public List<Doctor> GetDoctor(List<Guid> doctorIds)
        {
            return dataContext.Doctors.Where(d => doctorIds.Contains(d.DoctorId)).ToList();
        }

        public Doctor GetDoctor(Guid doctorId)
        {
            return dataContext.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }

        public List<DropDownValueList> GetDoctorsForDropdown()
        {
            return dataContext.Doctors.Select(x => new DropDownValueList() { Id = x.DoctorId.ToString(), Name = x.FullName }).ToList();
        }
    }
}
