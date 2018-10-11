using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class AvailabilityRepository : BaseRepository
    {
        public Availability AddUpdateAvaibility(Availability availability)
        {
            Availability existAvaibility = GetAvailability(availability.AvailabilityId);
            try
            {
                if (existAvaibility != null)
                {
                    existAvaibility.DoctorId = availability.DoctorId;
                    existAvaibility.LocationId = availability.LocationId;
                    existAvaibility.StartDate = availability.StartDate;
                    existAvaibility.EndDate = availability.EndDate;
                    existAvaibility.DurationInMinute = availability.DurationInMinute;
                    existAvaibility.SundayMorningStart = availability.SundayMorningStart;
                    existAvaibility.SundayMorningEnd = availability.SundayMorningEnd;
                    existAvaibility.SundayEveningStart = availability.SundayEveningStart;
                    existAvaibility.SundayEveningEnd = availability.SundayEveningEnd;
                    existAvaibility.MondayMorningStart = availability.MondayMorningStart;
                    existAvaibility.MondayMorningEnd = availability.MondayMorningEnd;
                    existAvaibility.MondayEveningStart = availability.MondayEveningStart;
                    existAvaibility.MondayEveningEnd = availability.MondayEveningEnd;
                    existAvaibility.TuesdayMorningStart = availability.TuesdayMorningStart;
                    existAvaibility.TuesdayMorningEnd = availability.TuesdayMorningEnd;
                    existAvaibility.TuesdayEveningStart = availability.TuesdayEveningStart;
                    existAvaibility.TuesdayEveningEnd = availability.TuesdayEveningEnd;
                    existAvaibility.WednesdayMorningStart = availability.WednesdayMorningStart;
                    existAvaibility.WednesdayMorningEnd = availability.WednesdayMorningEnd;
                    existAvaibility.WednesdayEveningStart = availability.WednesdayEveningStart;
                    existAvaibility.WednesdayEveningEnd = availability.WednesdayEveningEnd;
                    existAvaibility.ThursdayMorningStart = availability.ThursdayMorningStart;
                    existAvaibility.ThursdayMorningEnd = availability.ThursdayMorningEnd;
                    existAvaibility.ThursdayEveningStart = availability.ThursdayEveningStart;
                    existAvaibility.ThursdayEveningEnd = availability.ThursdayEveningEnd;
                    existAvaibility.FridayMorningStart = availability.FridayMorningStart;
                    existAvaibility.FridayMorningEnd = availability.FridayMorningEnd;
                    existAvaibility.FridayEveningStart = availability.FridayEveningStart;
                    existAvaibility.FridayEveningEnd = availability.FridayEveningEnd;
                    existAvaibility.SaturdayMorningStart = availability.SaturdayMorningStart;
                    existAvaibility.SaturdayMorningEnd = availability.SaturdayMorningEnd;
                    existAvaibility.SaturdayEveningStart = availability.SaturdayEveningStart;
                    existAvaibility.SaturdayEveningEnd = availability.SaturdayEveningEnd;
                    existAvaibility.UpdatedDate = DateTime.Now;
                    existAvaibility.UpdatedBy = availability.UpdatedBy;
                    existAvaibility.OnSunday = availability.OnSunday;
                    existAvaibility.OnMonday = availability.OnMonday;
                    existAvaibility.OnTuesday = availability.OnTuesday;
                    existAvaibility.OnWednesday = availability.OnWednesday;
                    existAvaibility.OnThursday = availability.OnThursday;
                    existAvaibility.OnFriday = availability.OnFriday;
                    existAvaibility.OnSaturday = availability.OnSaturday;
                }
                else
                {
                    existAvaibility = new Availability();
                    existAvaibility.AvailabilityId = Guid.NewGuid();
                    existAvaibility.DoctorId = availability.DoctorId;
                    existAvaibility.LocationId = availability.LocationId;
                    existAvaibility.StartDate = availability.StartDate;
                    existAvaibility.EndDate = availability.EndDate;
                    existAvaibility.DurationInMinute = availability.DurationInMinute;
                    existAvaibility.SundayMorningStart = availability.SundayMorningStart;
                    existAvaibility.SundayMorningEnd = availability.SundayMorningEnd;
                    existAvaibility.SundayEveningStart = availability.SundayEveningStart;
                    existAvaibility.SundayEveningEnd = availability.SundayEveningEnd;
                    existAvaibility.MondayMorningStart = availability.MondayMorningStart;
                    existAvaibility.MondayMorningEnd = availability.MondayMorningEnd;
                    existAvaibility.MondayEveningStart = availability.MondayEveningStart;
                    existAvaibility.MondayEveningEnd = availability.MondayEveningEnd;
                    existAvaibility.TuesdayMorningStart = availability.TuesdayMorningStart;
                    existAvaibility.TuesdayMorningEnd = availability.TuesdayMorningEnd;
                    existAvaibility.TuesdayEveningStart = availability.TuesdayEveningStart;
                    existAvaibility.TuesdayEveningEnd = availability.TuesdayEveningEnd;
                    existAvaibility.WednesdayMorningStart = availability.WednesdayMorningStart;
                    existAvaibility.WednesdayMorningEnd = availability.WednesdayMorningEnd;
                    existAvaibility.WednesdayEveningStart = availability.WednesdayEveningStart;
                    existAvaibility.WednesdayEveningEnd = availability.WednesdayEveningEnd;
                    existAvaibility.ThursdayMorningStart = availability.ThursdayMorningStart;
                    existAvaibility.ThursdayMorningEnd = availability.ThursdayMorningEnd;
                    existAvaibility.ThursdayEveningStart = availability.ThursdayEveningStart;
                    existAvaibility.ThursdayEveningEnd = availability.ThursdayEveningEnd;
                    existAvaibility.FridayMorningStart = availability.FridayMorningStart;
                    existAvaibility.FridayMorningEnd = availability.FridayMorningEnd;
                    existAvaibility.FridayEveningStart = availability.FridayEveningStart;
                    existAvaibility.FridayEveningEnd = availability.FridayEveningEnd;
                    existAvaibility.SaturdayMorningStart = availability.SaturdayMorningStart;
                    existAvaibility.SaturdayMorningEnd = availability.SaturdayMorningEnd;
                    existAvaibility.SaturdayEveningStart = availability.SaturdayEveningStart;
                    existAvaibility.SaturdayEveningEnd = availability.SaturdayEveningEnd;
                    existAvaibility.CreatedDate = DateTime.Now;
                    existAvaibility.CreatedBy = availability.CreatedBy;
                    existAvaibility.OnSunday = availability.OnSunday;
                    existAvaibility.OnMonday = availability.OnMonday;
                    existAvaibility.OnTuesday = availability.OnTuesday;
                    existAvaibility.OnWednesday = availability.OnWednesday;
                    existAvaibility.OnThursday = availability.OnThursday;
                    existAvaibility.OnFriday = availability.OnFriday;
                    existAvaibility.OnSaturday = availability.OnSaturday;
                    dataContext.Availabilities.Add(existAvaibility);

                }

                dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                existAvaibility = new Availability();
                logger.Error(ex);
            }
            //if (availability.AvailabilityId == Guid.Empty)
            //    availability.AvailabilityId = Guid.NewGuid();


            return existAvaibility;
        }

        public List<Availability> GetAvailabilities()
        {
            return dataContext.Availabilities.ToList();
        }

        public Availability GetAvailability(Guid availabilityId)
        {
            return dataContext.Availabilities.FirstOrDefault(x => x.AvailabilityId == availabilityId);
        }

        public List<Availability> GetAvailabilitiesByDoctorId(Guid doctorId)
        {
            return dataContext.Availabilities.Where(x => x.DoctorId == doctorId).ToList();
        }

        public Availability GetAvailability(Guid doctorId, Guid locationId)
        {
            return dataContext.Availabilities.FirstOrDefault(x => x.DoctorId == doctorId && x.LocationId == locationId);
        }
    }
}
