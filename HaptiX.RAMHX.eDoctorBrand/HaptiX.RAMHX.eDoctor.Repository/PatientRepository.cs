using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class PatientRepository : BaseRepository
    {
        public List<Patient> GetPaitients()
        {
            return dataContext.Patients.ToList();
        }

        /// <summary>
        /// Search patient based on Full Name, Mobile, Address, email
        /// </summary>
        /// <param name="keyword">Input keyword</param>
        /// <returns></returns>
        public List<Patient> GetPaitients(string keyword, int noOfResult)
        {
            List<Patient> results = new List<Patient>();

            string[] qry = keyword.Split(' ');
            qry = qry.Distinct().ToArray();

            foreach (var q in qry)
            {
                results.AddRange(dataContext.Patients.Where(x=> x.FullName.Contains(q) || x.Mobile.Contains(q) || x.Address.Contains(q) || x.Email.Contains(q)));
            }

            return results.Distinct().Take(noOfResult).ToList();
        }

        public Patient GetPaitient(Guid patientId)
        {
            return dataContext.Patients.FirstOrDefault(h => h.PatientId == patientId);
        }

        public Patient AddUpdatePaitient(Patient paitient)
        {
            Patient pt = dataContext.Patients.FirstOrDefault(x => x.PatientId == paitient.PatientId);
            if (pt == null)
            {
                pt = AddPaitient(paitient);
            }
            else
            {
                pt.FullName = paitient.FullName;
                pt.BirthDate = paitient.BirthDate;
                pt.Mobile = paitient.Mobile;
                pt.Email = paitient.Email;
                pt.Gender = paitient.Gender;
                pt.Address = paitient.Address;
                pt.PhotoUrl = paitient.PhotoUrl;
                dataContext.SaveChanges();
            }

            return pt;
        }

        public Patient AddPaitient(Patient paitient)
        {
            Patient pt = new Patient();
            pt.PatientId = Guid.NewGuid();
            pt.FullName = paitient.FullName;
            pt.BirthDate = paitient.BirthDate;
            pt.Mobile = paitient.Mobile;
            pt.Email = paitient.Email;
            pt.Gender = paitient.Gender;
            pt.Address = paitient.Address;
            pt.PhotoUrl = paitient.PhotoUrl;
            dataContext.Patients.Add(pt);
            dataContext.SaveChanges();
            return pt;
        }
    }
}
