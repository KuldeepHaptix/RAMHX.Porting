using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaptiX.RAMHX.eDoctorBrand.Models
{
    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int DurationInMinute { get; set; }
        public Guid DoctorId { get; set; }
        public Guid LocationId { get; set; }
        public DateTime Date { get; set; }
    }
}