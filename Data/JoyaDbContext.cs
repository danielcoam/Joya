using Joya.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Joya.Data
{
    public class JoyaDbContext : IdentityDbContext
    {
        public JoyaDbContext(DbContextOptions<JoyaDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    //string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        //    //optionsBuilder.UseSqlite("Data Source=App_Data/blogging.db");


        //}
        public DbSet<Ring> Rings { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bracelet> Bracelets { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
