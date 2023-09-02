using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assessment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<Users> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}