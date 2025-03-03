﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOL.Identity.Domain.Entities;

namespace SOL.Identity.Infrastructure.Data.Context
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
