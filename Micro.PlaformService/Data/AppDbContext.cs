﻿using Micro.PlaformService.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro.PlaformService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {
            

        }
        public DbSet<Platform> Platforms { get; set; }
    }
}
