using HaptiX.RAMHX.eDoctor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaptiX.RAMHX.eDoctor.Repository
{
    public class ServiceRepository : BaseRepository
    {
        public List<Service> GetServices()
        {
            return dataContext.Services.ToList();
        }

        public Service GetService(Guid serviceId)
        {
            return dataContext.Services.FirstOrDefault(s => s.ServiceId == serviceId);
        }

        public Service AddUpdateService(Service service)
        {
            Service srv = dataContext.Services.FirstOrDefault(x => x.ServiceId == service.ServiceId);
            if (srv == null)
            {
                srv = new Service();
                srv.ServiceId = Guid.NewGuid();
                srv.Name = service.Name;
                srv.IsActive = service.IsActive;
                dataContext.Services.Add(srv);
            }
            else
            {
                srv.Name = service.Name;
                srv.IsActive = service.IsActive;
            }

            dataContext.SaveChanges();

            return srv;
        }

    }
}
