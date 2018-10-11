using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HaptiX.RAMHX.eDoctor.Repository
{
    public abstract class BaseRepository
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected eDoctorEntities dataContext = new eDoctorEntities();
    }
}
