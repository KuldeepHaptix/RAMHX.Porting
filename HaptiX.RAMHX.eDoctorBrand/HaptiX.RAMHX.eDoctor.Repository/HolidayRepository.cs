using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class HolidayRepository : BaseRepository
    {
        public List<Holiday> GetHolidays()
        {
            return dataContext.Holidays.ToList();
        }

        public Holiday GetHoliday(Guid holidayId)
        {
            return dataContext.Holidays.FirstOrDefault(h => h.HolidayId == holidayId);
        }

        public Holiday AddUpdateHoliday(Holiday holiday)
        {
            Holiday hl = dataContext.Holidays.FirstOrDefault(x => x.HolidayId == holiday.HolidayId);
            if (hl == null)
            {
                hl = new Holiday();
                hl.HolidayId = Guid.NewGuid();
                hl.HolidayDate = holiday.HolidayDate;
                hl.Name = holiday.Name;
                dataContext.Holidays.Add(hl);
            }
            else
            {
                hl.HolidayDate = holiday.HolidayDate;
                hl.Name = holiday.Name;
            }

            dataContext.SaveChanges();

            return hl;
        }
    }
}
