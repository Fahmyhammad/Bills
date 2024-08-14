using bill_Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Unit> units { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<tableItem> Items { get; set; }
        public DbSet<SalesInvoice> salesInvoices { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AppUser> appUsers  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tableItem>()
                .HasOne(i => i.company)
                .WithMany()
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<tableItem>()
                .HasOne(i => i.type)
                .WithMany()
                .HasForeignKey(i => i.TypeId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
