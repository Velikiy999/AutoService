using AutoService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<AutoService.Models.FinancialTransactions> FinancialTransactions { get; set; } = default!;
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ServiceRecordSparePart> ServiceRecordSpareParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceRecordSparePart>()
                .HasKey(srsp => new { srsp.ServiceRecordId, srsp.SparePartId });

            modelBuilder.Entity<ServiceRecordSparePart>()
                .HasOne(srsp => srsp.ServiceRecord)
                .WithMany(sr => sr.ServiceRecordSpareParts)
                .HasForeignKey(srsp => srsp.ServiceRecordId);

            modelBuilder.Entity<ServiceRecordSparePart>()
                .HasOne(srsp => srsp.SparePart)
                .WithMany()
                .HasForeignKey(srsp => srsp.SparePartId);
        }
    }
}
