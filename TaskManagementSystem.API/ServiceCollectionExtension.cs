using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Implementations;

namespace TaskManagementSystem.API
{
    /// <summary>
    /// class Service collection extension
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// CONFIGURE IDENTITY
        /// </summary>
        /// <param name="services">the services</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
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

        /// <summary>
        /// CONFIGURE JWT
        /// </summary>
        /// <param name="services">the services collection</param>
        /// <param name="configuration">the configuration</param>
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

        /// <summary>
        /// CONFIGURE SWAGGER
        /// </summary>
        /// <param name="services">the services</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Management System API",
                    Version = "v1",
                    Description = "TMS Platform",
                    Contact = new OpenApiContact
                    {
                        Name = "TMS",
                        Email = "tms@info.com"
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
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        
        
        }

        /// <summary>
        /// ADD ENTITY FRAMEWORK DATABASE CONTEXT
        /// </summary>
        /// <param name="services">the services</param>
        /// <param name="configuration">the configuration</param>
        public static void AddEntityFrameworkDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    dbConnectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        /// <summary>
        /// REGISTER SERVICE
        /// </summary>
        /// <param name="services">the services</param>
        /// <param name="iConfiguration">the configuration</param>
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

        /// <summary>
        /// ADD HANGFIRE
        /// </summary>
        /// <param name="builder">the builder</param>
        /// <param name="configuration">the configuration</param>
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

        /// <summary>
        /// ENRICH RESPONSE HEADER
        /// </summary>
        /// <param name="context">the context</param>
        public static void EnrichResponseHeader(HttpContext context)
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; img-src https://*; child-src 'none';");
            context.Response.Headers.Add("Referrer-Policy", "strict-origin");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        }

    }


}
