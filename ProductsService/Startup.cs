using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using ProductsService.Model;
using Microsoft.Data.Entity;

namespace ProductsService
{
    public class Startup
    {
        private IConfiguration Config { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

             Config = builder.Build();
         
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()   
            .AddNpgsql()
            .AddDbContext<ProductsContext>(options =>
                options.UseNpgsql(Config["Data:DefaultConnection:ConnectionString"]));
        }

    }
}
