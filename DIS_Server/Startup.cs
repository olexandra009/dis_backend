using System;
using Autofac;
using DIS_data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DIS_Server.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DIS_Server
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
            #region Authentification
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; //SSL no need 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,                                      
                        ValidIssuer = AuthOption.ISSUER,                           
                        ValidateAudience = true,                                    
                        ValidAudience = AuthOption.AUDIENCE,                        
                        ValidateLifetime = true,                                   
                        IssuerSigningKey = AuthOption.GetSymmetricSecurityKey(),   
                        ValidateIssuerSigningKey = true                              
                    };
                });
            #endregion

            var database = Environment.GetEnvironmentVariable("DATABASE_URL") ?? Configuration.GetConnectionString("ConnectionString");
           
            ConnectionStringObject mainObject = new ConnectionStringObject(database);
            
            string connection =
                $"host={mainObject.Host};port={mainObject.Port};database={mainObject.Database};user id={mainObject.UserId}; password={mainObject.Password};Pooling=true;SSLMode=Require; TrustServerCertificate=True;";

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<DisContext>(options => options.UseNpgsql(connection));
            
            services.AddAutoMapper((configuration) => configuration.AddProfile<MapperProfile>(),
                typeof(Startup)); // scan and register automapper profiles in this assembly

            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DIS_Server", Version = "v1" });
            });
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterModule(new AutofacServiceModule());

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DIS_Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
