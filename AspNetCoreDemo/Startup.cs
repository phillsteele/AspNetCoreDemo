using AspNetCoreDemo.Authorization;
using AspNetCoreDemo.Pipeline;
using AspNetCoreDemo.Security;
using AspNetCoreDemo.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

// Info on middleware: https://docs.microsoft.com/en-gb/aspnet/core/fundamentals/middleware/index?view=aspnetcore-2.2

namespace AspNetCoreDemo
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(
            //        Policies.HasClaim,
            //        policy => policy.Requirements.Add(new HasClaimRequirement(CustomClaimTypes.FulfilGet)));
            //});

            // Configure DI for the services
            services.AddSingleton<IAuthorizationHandler, HasClaimHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, HasClaimPolicyProvider>();
            services.AddSingleton<IUserList, CustomUserList>();
            services.AddSingleton<IUserService, SimpleUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // This approach does not require a separate controller to handle error responses and is more suited to APIs
            app.UseExceptionHandler(errorApp =>
                errorApp.Run(async context => await UnexpectedExceptionHandler.Invoke(context))
            );

            //if (env.IsDevelopment())
            //{
            //    // Don't use this in production.  It gives a very pretty stack trace and shows in the code where the exception occurred.
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // This redirects the flow of the request to a different path entirely, that path returns an appropriate content which is then given a 500 status code
            //    app.UseExceptionHandler("/home/error");

            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    // HSTS = "HTTP Strict Transport Security Protocol"
            //    //app.UseHsts();
            //}

            app
               .UseAuthentication()
               .UseHttpsRedirection()
               .UseMvc();
        }

        private Task async(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
