using RAMHX.CMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.Repository
{
    public abstract class BaseRepository
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected DatabaseEntities dataContext = new DatabaseEntities();
    }
}
