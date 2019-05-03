﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Exceptions;
using AspNetCoreDemo.Models.SlcsOutbound;
using AspNetCoreDemo.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // This approach does not require a separate controller to handle error responses and is more suited to APIs
            app.UseExceptionHandler(errorApp =>
                errorApp.Run(async context => await UnexpectedExceptionHandler.HandleUnexpectedException(context))
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
               .UseHttpsRedirection()
               .UseMvc();
        }

        private Task async(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}