using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAMHX.CMS.RepositoryCore
{
    public class RedirectsRepository : BaseRepository
    {
        public RedirectsRepository(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        public string Get301RedirectUrl(string fromUrl)
        {
            try
            {
                var redirectPage = dataContext.Cms301redirection.FirstOrDefault(rd => rd.FromUrl.ToLower() == fromUrl.ToLower() && rd.Active == true);
                if (redirectPage != null && !string.IsNullOrEmpty(redirectPage.ToUrl))
                {
                    return redirectPage.ToUrl;
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
