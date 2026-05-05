﻿using Application.Core.Defults;
using Application.Core.Errors;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.UnitOfWork;
using Application.Repository;
using Application.Repository.Data.Contexts;
using Application.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace School.Api.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuildInService();
            services.AddSwaggerServices();
            services.AddUserServices();
            services.AddAllowAngularApp(configuration);
            services.AddTokenServices(configuration);
            services.AddDbContextServices(configuration);
            services.AddErrorHandlingServices();
            return services;
        }
        private static IServiceCollection AddBuildInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddAllowAngularApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp",
                    builder =>
                    {
                        //builder.WithOrigins(configuration["Apphttp"], configuration["Apphttps"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            return services;
        }
        private static IServiceCollection AddTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = AppDefults.Jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = AppDefults.Jwt.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppDefults.Jwt.Key))
                };
            });
            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SQL"));
            });
            return services;
        }
        private static IServiceCollection AddErrorHandlingServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return new BadRequestObjectResult(new ApiValidationError(errors));
                };
            });
            return services;
        }
        private static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISupervisorService, SupervisorService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IScoreService, ScoreService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IAbsentService, AbsentService>();
            services.AddScoped<IAdminstrationService, AdminstrationService>();
            services.AddScoped<IDirectorateService, DirectorateService>();
            services.AddScoped<IControlService, ControlService>();
            services.AddScoped<ITekStaticsService, TekStaticsService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<ISocialTekService, SocialTekService>();
            services.AddScoped<ISubControlService, SubControlService>();
            return services;
        }
    }
}