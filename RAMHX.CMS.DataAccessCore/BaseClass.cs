using System;
using System.Collections.Generic;
using System.Text;
using RAMHX.CMS.DataAccessCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace RAMHX.CMS.DataAccessCore
{
    public class BaseClass
    {
         IHttpContextAccessor _httpContextAccessor;
        public BaseClass(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected IHttpContextAccessor Context { get { return this._httpContextAccessor; } }

        private readonly IHostingEnvironment environment;


        public  BaseClass(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        protected readonly UserManager<AspNetUsers> _userManager;

        public BaseClass(UserManager<AspNetUsers> userManager)
        {
            _userManager = userManager;
        }
        protected readonly DatabaseEntities _dbContext;

        public BaseClass(DatabaseEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //enabling the in memory cache 
            services.AddMemoryCache();
        }
        protected static IMemoryCache _cache;
        public  BaseClass(IMemoryCache cache)
        {
            _cache = this.Context.HttpContext.RequestServices.GetService<IMemoryCache>();
        }

        

    }

}
