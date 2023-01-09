using System;
using CarRentalRestApi.Data;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Auth;
using CarRentalRestApi.Models.Mailing;
using CarRentalRestApi.Repository;
using CarRentalRestApi.Services;
using CarRentalRestApi.Services.AuthService;
using CarRentalRestApi.Services.BrandService;
using CarRentalRestApi.Services.FilesService;
using CarRentalRestApi.Services.MailingService;
using CarRentalRestApi.Services.RentService;
using CarRentalRestApi.Services.VehicleService;
using CarRentalRestApi.Utils.AuthUtils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CarRentalRestApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers();
            
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnectionSqlite")));
            
            services.Configure<MailSettings>(_configuration.GetSection("MailingSettings"));
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CarRentalRestApi", Version = "v1"});
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Bearer token authorization. Input <Bearer YOUR_TOKEN>",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var authConfig = new AuthConfig();
            _configuration.Bind("JwtTokenSettings", authConfig);

            services.AddSingleton(authConfig);

            services.AddAutoMapper(typeof(Startup));
            
            services.AddScoped<IVehicleService, VehicleService>();

            services.AddScoped<IRentService, RentService>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IJwtTokenUtils, JwtTokenUtils>();

            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IFileService, FileService>();
            
            services.AddScoped<ChassisTypeService>();
            
            services.AddScoped<BrandAndModelService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.ASCII.GetBytes(authConfig.AccessTokenSecret)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRentalRestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()); 
            
            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}