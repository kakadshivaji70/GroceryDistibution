using GroceryDistibution.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using GroceryDistibution.BLL.Interfaces;
using AutoMapper;
using GroceryDistibution.BLL.ViewModels;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace GroceryDistibution.BLL.Services
{
    public class GroceryDistibutionStartup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";// todo: get this from somewhere secure

        //private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public IConfiguration Configuration { get; }
        public GroceryDistibutionStartup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                          .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            //services.AddMvc(config => { config.Filters.Add(typeof(ExceptionFilter)); config.Filters.Add(typeof(LanguageActionFilter)); });
            services.AddLocalization();
            
            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = Common.Resources.ResourceMaster.Instance._supportedCultures;
            //    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            //    options.SupportedCultures = Common.Resources.ResourceMaster.Instance._supportedCultures;
            //    options.SupportedUICultures = Common.Resources.ResourceMaster.Instance._supportedCultures;
            //    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
            //    {
            //        var userLangs = context.Request.Headers["Accept-Language"].ToString();
            //        var firstLang = userLangs.Split(',').FirstOrDefault();
            //        var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;
            //        return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
            //    }));
            //});

            services.AddDbContext<GroceryDistibutionDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("GroceryDistibution.Entity"));
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = new[] { "text/plain",
                                            "text/css",
                                            "application/javascript",
                                            "text/html",
                                            "application/xml",
                                            "text/xml",
                                            "application/json",
                                            "text/json" };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUser, UserService>();
            services.AddTransient<ICommon, CommonService>();

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GroceryDistibutionStartup>());
            services.AddAutoMapper();       //Your configuration (e.g. Automapper Profiles) are singletons.
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Comment below code while publishing to prod
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions   
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/api/Home/Error");
            }

            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();


            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            //app.UseExceptionHandler(builder => { builder.Run(handler: async context => { await context.HandelException(); }); });
            app.UseExceptionHandler(options =>
            {
                options.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace }";
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });

            app.UseResponseCompression();
            //app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseSpaStaticFiles();

        }
    }
}
