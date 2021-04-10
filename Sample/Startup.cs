using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Panosen.AspNetCore.Authentication.Basic;
using Panosen.AspNetCore.Authentication.Header;

namespace Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBasicAuthenticationService, SampleIBasicAuthenticationService>();
            services.AddSingleton<IHeaderAuthenticationService, SampleHeaderAuthenticationService>();

            ////��ӻ��������֤
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme).AddBasic();

            //���Header�����֤
            services.AddAuthentication(HeaderAuthenticationDefaults.AuthenticationScheme).AddHeader(options =>
            {
                options.HeaderKey = "NUGET-API-KEY";
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //ʹ�û��������֤
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
