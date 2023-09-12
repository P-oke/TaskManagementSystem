using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.API
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Password.RequiredUniqueChars = 1;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection("JWT");

            var secretKey = jwtConfig["Secret"];
            var validAudience = jwtConfig["ValidAudience"];
            var validIssuer = jwtConfig["ValidIssuer"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Management System API",
                    Version = "v1",
                    Description = "IronClad Platform",
                    Contact = new OpenApiContact
                    {
                        Name = "IronClad",
                        Email = "Info@ironclad.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_Lincense")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using Bearer scheme. \r\n\r\n" +
                    "Enter 'Bearer' [space] and then your token in the input below.\r\n\r\n" +
                    "Example: \"Bearer 123456\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }

            });
                c.DescribeAllParametersInCamelCase();
            });
        
        
        }

        public static void AddEntityFrameworkDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    dbConnectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        public static void RegisterService(this IServiceCollection services, IConfiguration iConfiguration)
        {

            services.AddTransient<DbContext, ApplicationDbContext>();

            services.AddScoped<IAuthenticationService, AuthenticationService>(); 
            services.AddScoped<ITaskService, TaskService>(); 
            services.AddScoped<IProjectService, ProjectService>(); 
            services.AddScoped<INotificationService, NotificationService>(); 
            services.AddScoped<IUserService, UserService>(); 

            

             services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(iConfiguration.GetConnectionString("Default"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }


        public static void AddHangfire(this IApplicationBuilder builder, IConfiguration configuration)
        {

            builder.UseHangfireDashboard(configuration.GetSection("HangfireSettings:Route").Value, new DashboardOptions
            {
                Authorization = new[] 
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = configuration.GetSection("HangfireSettings:Credentials:User").Value,
                        Pass = configuration.GetSection("HangfireSettings:Credentials:Password").Value
                    },
                },
                
                AppPath = configuration.GetSection("HangfireSettings:Dashboard:AppPath").Value,
                DashboardTitle = configuration.GetSection("HangfireSettings:Dashboard:DashboardTitle").Value
            });


            RecurringJob.AddOrUpdate<INotificationService>("SendNotificationForTasksDueDateWithin48Hours", x => x.SendNotificationForTasksDueDateWithin48Hours(), "*/30 * * * *"); //Fires every 30 minutes
            RecurringJob.AddOrUpdate<INotificationService>("SendNotificationForTasksCompleted", x => x.SendNotificationForTasksCompleted(), Cron.Daily(8)); //Fires at 08:00 UTC

        }


    }


}
