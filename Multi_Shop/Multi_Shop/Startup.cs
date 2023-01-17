using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Multi_Shop.Repository;
using Multi_Shop.Context;
using Multi_Shop.App_source;

namespace Multi_Shop
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
            services.AddControllersWithViews();
            services.AddDbContext<MultiShopContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Mul_sh"));
            });


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            IServiceCollection serviceCollection = services.AddDbContext<IdentityDbContext>(option=> 
            option.UseSqlServer(Configuration.GetConnectionString("Mul_sh"), optionsBuilder =>
                         optionsBuilder.MigrationsAssembly("Multi_Shop")
                ));

            services.AddScoped<IMessageSender, MessageSender>();
            services.AddScoped<IShopservice, Shopservice>();
            services.AddAuthentication().AddGoogle(option =>
            {
                option.ClientId = "573445038412-06lf4rk1qaq0fn4bonhu305ba39ql5ro.apps.googleusercontent.com";
                option.ClientSecret = "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";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseWebSockets(WebConfig.GetOptions());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (Context, next) =>
            {
                if (Context.WebSockets.IsWebSocketRequest)
                {
                    if (Context.Request.Path == "/wschat")
                    {
                        WsFunction wsf = new WsFunction();
                        await wsf.listenAcceptAsync(Context);
                    }
                }
                else
                {
                    await next();
                }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
