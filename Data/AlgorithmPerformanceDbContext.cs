using Microsoft.EntityFrameworkCore;
using H3_Symmetric_encryption.Mappers;
using H3_Symmetric_encryption.Entities;

namespace H3_Symmetric_encryption.Data
{
    public class AlgorithmPerformanceDbContext : DbContext
    {
        public DbSet<AlgorithmEntity> AlgorithmEntities { get; set; }
        public DbSet<AlgorithmPerformanceEntity> AlgorithmPerformanceEntities { get; set; }

        public AlgorithmPerformanceDbContext(DbContextOptions<AlgorithmPerformanceDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=AlgorithmPerformance.db");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Algorithm table configuration
            modelBuilder.Entity<AlgorithmEntity>()
                .ToTable("Algorithm");

            modelBuilder.Entity<AlgorithmEntity>()
                .HasKey(a => a.AlgorithmId);

            modelBuilder.Entity<AlgorithmEntity>()
                .Property(a => a.AlgorithmId)
                .ValueGeneratedNever();

            modelBuilder.Entity<AlgorithmEntity>()
                .HasIndex(a => new { a.Name, a.KeySizeBits })
                .IsUnique();

            // AlgorithmPerformance table configuration
            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .ToTable("AlgorithmPerformance");

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasKey(ap => ap.AlgorithmPerformanceId);

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasOne(ap => ap.Algorithm)
                .WithMany(a => a.AlgorithmPerformanceEntities)
                .HasForeignKey(ap => ap.AlgorithmId);

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasIndex(a => a.AlgorithmId);

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasIndex(a => a.PlainTextSizeInBytes);

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasIndex(a => a.Workload);

            modelBuilder.Entity<AlgorithmPerformanceEntity>()
                .HasIndex(a => new { a.AlgorithmId, a.PlainTextSizeInBytes, a.Workload })
                .IsUnique();

            // Seeding
            List<AlgorithmEntity> algorithms = AlgorithmMapper.GetAllAlgorithms();

            modelBuilder.Entity<AlgorithmEntity>()
                .HasData(algorithms);
        }
    }
}