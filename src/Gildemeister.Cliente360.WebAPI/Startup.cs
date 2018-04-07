using Gildemeister.Cliente360.Application;
using Gildemeister.Cliente360.Application.Interfaces;
using Gildemeister.Cliente360.Contracts.Repository;
using Gildemeister.Cliente360.Contracts.Repository.DWProd;
using Gildemeister.Cliente360.Contracts.Repository.SGAPROD;
using Gildemeister.Cliente360.Infrastructure;
using Gildemeister.Cliente360.Infrastructure.Security;
using Gildemeister.Cliente360.Persistence;
using Gildemeister.Cliente360.Persistence.Repository;
using Gildemeister.DWProd.Persistence.Database;
using Gildemeister.DWProd.Persistence.Repository;
using Gildemeister.SGAProd.Persistence.Database;
using Gildemeister.SGAProd.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Gildemeister.Cliente360.WebAPI
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
            // Add framework services.
            services.AddDbContext<Cliente360DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DWProdDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DWPRODConnection")));

            services.AddDbContext<SGAProdDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("SGAProdDbConnection")));

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ConfigureSync>(Configuration.GetSection("ConfigureSync"));

            services.Configure<IISOptions>(options => { });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cliente 360", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            // Add Jwt Authentication

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = JwtSettings.JwtIssuer,
                    ValidAudience = JwtSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddAuthenticationHundredToken();

            //Dependency injection service
            services.AddTransient<ICotizacionService, CotizacionService>();
            services.AddTransient<ITestDriveService, TestDriveService>();
            services.AddTransient<IVentaService, VentaService>();
            services.AddTransient<IServicioService, ServicioService>();
            services.AddTransient<IAsesorService, AsesorService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IPersonaService, PersonaService>();
            services.AddTransient<ITablaGeneralService, TablaGeneralService>();
            services.AddTransient<ITablaDetalleService, TablaDetalleService>();
            services.AddTransient<IUbigeoService, UbigeoService>();
            services.AddTransient<ISeguridadService, SeguridadService>();
            services.AddTransient<IPaisService, PaisService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IServiceClient, ServiceClient>();
            services.AddTransient<ITokenAccesoService, TokenAccesoService>();
            services.AddTransient<ISincronizarService, SincronizarService>();
            services.AddTransient<IApplicacionLlaveService, ApplicacionLlaveService>();

            //Dependency injection repository
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICotizacionRepository, DWProd.Persistence.Repository.CotizacionRepository>();
            services.AddTransient<ITestDriveRepository, TestDriveRepository>();
            services.AddTransient<IVentaRepository, VentaRepository>();
            services.AddTransient<IServicioRepository, ServicioRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteDWProdRepository, ClienteDWProdRepository>();
            services.AddTransient<ITablaGeneralRepository, TablaGeneralRepository>();
            services.AddTransient<ITablaDetalleRepository, TablaDetalleRepository>();
            services.AddTransient<IUbigeoRepository, UbigeoRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ITokenAccesoRepository, TokenAccesoRepository>();
            services.AddTransient<IApplicacionLlaveRepository, ApplicacionLlaveRepository>();

            services.AddTransient<IMarcaRepository, MarcaRepository>();
            services.AddTransient<IModeloRepository, ModeloRepository>();
            services.AddTransient<IAsesorRepository, AsesorRepository>();
            services.AddTransient<IPuntoVentaRepository, PuntoVentaRepository>();

            services.AddCors(o => o.AddPolicy("cors", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("cors"));
            });


            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMapper());
                cfg.AddProfile(new DTOToDomainMapper());
            });

            var mapper = config.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.HttpMethod == null) return false;
                    return true;
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {

            loggerFactory.AddNLog();
            app.AddNLogWeb();
            env.ConfigureNLog("Nlog.Config");
            LogManager.Configuration.Variables["NlogConnection"] = Configuration.GetConnectionString("DefaultConnection");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente 360 API V1");
            });

            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
            app.UseCors("cors");

            app.UseMvc();
        }
    }
}
