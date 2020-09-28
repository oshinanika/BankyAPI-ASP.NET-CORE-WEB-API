using BankyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankyAPI.Data
{
    public class BankyDbContext : DbContext
    {
        public BankyDbContext(DbContextOptions<BankyDbContext> options) : base(options)
        {

        }

        public DbSet<Bank> Bank { get; set; }
    }
} 
