using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeStaff.Data.Models;

namespace OfficeStaff.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions optins) : base(optins)
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
                builder.HasKey(pc => pc.ChangeId);
                builder
                    .HasMany(e => e.Employees)
                    .WithMany(e => e.PositionHistory);
                builder
                    .HasMany(e => e.Positions)
                    .WithMany(e => e.PositionHistory);
            }
        }

        public class DepartementConfiguration : IEntityTypeConfiguration<Department>
        {
            public void Configure(EntityTypeBuilder<Department> builder)
            {
                builder.HasOne(e => e.Location).WithMany(c => c.Departments).HasForeignKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
                builder.Property(e => e.Name).IsRequired().HasMaxLength(40);
            }
        }

        public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.HasOne(e => e.Position).WithMany(c => c.Employees).OnDelete(DeleteBehavior.SetNull);
                builder.Property(e => e.FirstName).HasMaxLength(30);
                builder.Property(e => e.LastName).HasMaxLength(30);
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
            }
        }

        public class LocationConfiguration : IEntityTypeConfiguration<Location>
        {
            public void Configure(EntityTypeBuilder<Location> builder)
            {
                builder.HasOne(e => e.Country).WithMany(e => e.Locations).HasForeignKey(e => e.Id).OnDelete(DeleteBehavior.Cascade);
                builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
                builder.HasIndex(e => e.Name).IsUnique();
            }
        }
    }
}