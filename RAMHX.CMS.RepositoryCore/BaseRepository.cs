using Microsoft.AspNetCore.Http;
using RAMHX.CMS.DataAccessCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMHX.CMS.RepositoryCore
{
    public abstract class BaseRepository:BaseClass
    {
        public BaseRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected DatabaseEntities dataContext = new DatabaseEntities();
    }
}
