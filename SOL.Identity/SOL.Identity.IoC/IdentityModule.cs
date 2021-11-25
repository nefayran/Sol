﻿using System;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SOL.Core.Interfaces;
using SOL.Identity.SOL.Identity.Application.Interfaces;
using SOL.Identity.SOL.Identity.Application.User;
using SOL.Identity.SOL.Identity.Domain.Commands.User;
using SOL.Identity.SOL.Identity.Domain.Commands.User.Validators;
using SOL.Identity.SOL.Identity.Domain.Interfaces.Repositories;
using SOL.Identity.SOL.Identity.Infrastructure.Data.Context;
using SOL.Identity.SOL.Identity.Infrastructure.Repositories;
using SOL.Identity.SOL.Identity.Infrastructure.Security;

namespace SOL.Identity.SOL.Identity.IoC
{
    public static class IdentityModule
    {
        public static void Register(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<IUserRepository, UserRepository>();
            // Validators.
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginUserCommandValidator>();
            // Commands.
            services.AddScoped<ICommandHandlerAsync<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<ICommandHandlerAsync<LoginUserCommand>, LoginUserCommandHandler>();
            // JWT.
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<JwtValidator>();
            // JWT key.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TokenKey"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                    opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                        };
                    });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<IdentityContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}
