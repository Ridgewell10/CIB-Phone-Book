using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infustructure.Models
{
    public partial class CIBDbContext : DbContext
    {
        public CIBDbContext()
        {
        }

        public CIBDbContext(DbContextOptions<CIBDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<PhoneBook> PhoneBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PhoneBook)
                    .WithMany(p => p.Entry)
                    .HasForeignKey(d => d.PhoneBookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entry_Entry");
            });

            modelBuilder.Entity<PhoneBook>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
