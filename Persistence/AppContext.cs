using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeStaff.Data.Models;
using static OfficeStaff.Persistence.AppContext.PositionHistoryConfiguration;

namespace OfficeStaff.Persistence
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions optins) : base(optins)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionHistory> PositionHistory { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PositionHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DepartementConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
        }

        public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
        {
            public void Configure(EntityTypeBuilder<PositionHistory> builder)
            {
                builder.HasKey(pc => new { pc.EmployeeId, pc.PositionId });
                builder
                    .HasOne(e => e.Employee)
                    .WithMany(ph => ph.PositionHistory)
                    .HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.Restrict);
                builder
                    .HasOne(p => p.Position)
                    .WithMany(ph => ph.PositionHistory)
                    .HasForeignKey(p => p.PositionId).OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class DepartementConfiguration : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.HasOne(e => e.Location).WithMany(c => c.Departments).HasForeignKey(e => e.Id).OnDelete(DeleteBehavior.Restrict);
                builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
                builder.HasIndex(e => e.Name).IsUnique();
            }
        }

        public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.Property(e => e.FirstName).HasMaxLength(30);
                builder.Property(e => e.LastName).HasMaxLength(30);
                builder.Property(e => e.Name).HasComputedColumnSql("FirstName + ' ' + LastName");
            }
        }

        public class CountryConfiguration : IEntityTypeConfiguration<Country>
        {
            public void Configure(EntityTypeBuilder<Country> builder)
            {
                builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
                builder.HasIndex(e => e.Name).IsUnique();
            }
        }

        public class PositionConfiguration : IEntityTypeConfiguration<Position>
        {
            public void Configure(EntityTypeBuilder<Position> builder)
            {
                builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
                builder.HasIndex(e => e.Name).IsUnique();
                builder.HasData(
                    new Position { Id = 1, Name = "Менеджер" },
                    new Position { Id = 2, Name = "Сотрудник"}
                );
            }
        }

        public class LocationConfiguration : IEntityTypeConfiguration<Location>
        {
            public void Configure(EntityTypeBuilder<Location> builder)
            {
                builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
                builder.HasIndex(e => e.Name).IsUnique();
            }
        }
    }
}