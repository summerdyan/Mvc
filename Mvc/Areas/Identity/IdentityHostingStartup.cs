// identity services that happen at startup
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Areas.Identity.Data;
using Mvc.Data;

[assembly: HostingStartup(typeof(Mvc.Areas.Identity.IdentityHostingStartup))]
namespace Mvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            /*
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MvcMovieContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("MvcMovieContext")));

            
                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    //.AddEntityFrameworkStores<MvcIdentityDbContext>();
            });
            */
        }
    }
}