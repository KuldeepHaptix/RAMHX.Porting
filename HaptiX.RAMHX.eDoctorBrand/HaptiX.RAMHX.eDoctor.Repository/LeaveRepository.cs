using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class LeaveRepository : BaseRepository
    {
        public List<Leave> GetLeaves()
        {
            return dataContext.Leaves.ToList();
        }

        public Leave GetLeave(Guid leaveId)
        {
            return dataContext.Leaves.FirstOrDefault(l => l.LeaveId == leaveId);
        }

        public Leave AddUpdateLeave(Leave leave)
        {
            Leave lv = dataContext.Leaves.FirstOrDefault(x => x.LeaveId == leave.LeaveId);
            if (lv == null)
            {
                lv = new Leave();
                lv.LeaveId = Guid.NewGuid();
                lv.UserId = leave.UserId;
                lv.Date = leave.Date;
                lv.TypeId = leave.TypeId;
                lv.Comment = leave.Comment;
                lv.StatusId = leave.StatusId;
                lv.CreatedDate = DateTime.Now;
                lv.CreatedBy = leave.CreatedBy;

                dataContext.Leaves.Add(lv);
            }
            else
            {
                lv.UserId = leave.UserId;
                lv.Date = leave.Date;
                lv.TypeId = leave.TypeId;
                lv.Comment = leave.Comment;
                lv.StatusId = leave.StatusId;
                lv.UpdatedDate = DateTime.Now;
                lv.UpdatedBY = leave.UpdatedBY;
            }

            dataContext.SaveChanges();

            return lv;
        }
    }
}
