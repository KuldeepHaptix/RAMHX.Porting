using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMHX.CMS.Repository
{
    public class RedirectsRepository : BaseRepository
    {
        public string Get301RedirectUrl(string fromUrl)
        {
            try
            {
                var redirectPage = dataContext.cms_301Redirection.FirstOrDefault(rd => rd.fromUrl.ToLower() == fromUrl.ToLower() && rd.Active == true);
                if (redirectPage != null && !string.IsNullOrEmpty(redirectPage.toUrl))
                {
                    return redirectPage.toUrl;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error in Get301RedirectUrl", ex);
            }

            return string.Empty;
        }
    }
}
