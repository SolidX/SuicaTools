﻿using Microsoft.EntityFrameworkCore;
using Solidus.SuicaTools.Data.Converters;
using Solidus.SuicaTools.Data.Entities;
using Solidus.SuicaTools.Data.Entities.EkiData;

namespace Solidus.SuicaTools.Data
{
    public class TransitContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<LineType> LineTypes { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Prefecture> Prefectures { get; set; }
        public DbSet<SaibaneCode> SaibaneCodes { get; set; }
        public DbSet<SaibaneCodeEkiDataStation> SaibaneCodeMappings { get; set; }
        public DbSet<IruCaBusStop> IruCaBusStops { get; set; }

        public TransitContext(DbContextOptions<TransitContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>().HaveConversion<DateOnlyConverter>().HaveColumnType("date");
            builder.Properties<DateOnly?>().HaveConversion<NullableDateOnlyConverter>().HaveColumnType("date");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("EkiData_Statuses");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.ToTable("EkiData_CompanyTypes");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<LineType>(entity =>
            {
                entity.ToTable("EkiData_LineTypes");
                entity.HasKey(s => s.Id);
            });

            modelBuilder.Entity<Prefecture>(entity =>
            {
                entity.ToTable("Prefectures");
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("EkiData_Companies");
                entity.HasKey(c => c.CompanyCode);

                entity.Property(c => c.StatusId).HasColumnName("Status");
                entity.Property(c => c.CompanyTypeId).HasColumnName("Type");

                entity.HasOne(c => c.Type).WithMany().HasForeignKey(c => c.CompanyTypeId);
                entity.HasOne(c => c.Status).WithMany().HasForeignKey(c => c.StatusId);
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.ToTable("EkiData_Lines");
                entity.HasKey(l => l.LineCode);

                entity.Property(l => l.StatusId).HasColumnName("Status");
                entity.Property(l => l.LineTypeId).HasColumnName("Type");
                entity.Property(l => l.ColorCode).HasMaxLength(6);
                entity.Property(l => l.Latitude).HasColumnType("decimal").HasPrecision(8);
                entity.Property(l => l.Longitude).HasColumnType("decimal").HasPrecision(8);

                entity.HasOne(l => l.Company).WithMany().HasForeignKey(l => l.CompanyCode);
                entity.HasOne(l => l.Type).WithMany().HasForeignKey(l => l.LineTypeId);
                entity.HasOne(l => l.Status).WithMany().HasForeignKey(l => l.StatusId);
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.ToTable("EkiData_Stations");
                entity.HasKey(s => s.StationCode);

                entity.Property(s => s.StatusId).HasColumnName("Status");
                entity.Property(s => s.PrefectureId).HasColumnName("Prefecture");
                entity.Property(s => s.Latitude).HasColumnType("decimal").HasPrecision(8);
                entity.Property(s => s.Longitude).HasColumnType("decimal").HasPrecision(8);

                entity.HasOne(s => s.Line).WithMany().HasForeignKey(s => s.LineCode);
                entity.HasOne(s => s.Prefecture).WithMany().HasForeignKey(s => s.PrefectureId);
                entity.HasOne(s => s.Status).WithMany().HasForeignKey(s => s.StatusId);
            });

            modelBuilder.Entity<SaibaneCode>(entity =>
            {
                entity.ToTable("SaibaneCodes");
                entity.HasKey(sc => new { sc.RegionCode, sc.LineCode, sc.StationCode });
            });

            modelBuilder.Entity<SaibaneCodeEkiDataStation>(entity =>
            {
                entity.ToTable("SaibaneCodeEkiDataStations");
                entity.HasKey(m => new { m.RegionCode, m.LineCode, m.StationCode, m.EkiData_StationCode });

                entity.HasOne(m => m.Saibane).WithMany().HasForeignKey(m => new { m.RegionCode, m.LineCode, m.StationCode });
                entity.HasOne(m => m.EkiDataStation).WithMany().HasForeignKey(m => m.EkiData_StationCode);
            });

            modelBuilder.Entity<IruCaBusStop>(entity =>
            {
                entity.ToTable("IruCa_BusStop");
                entity.HasKey(b => b.Id);
            });
        }
    }
}
