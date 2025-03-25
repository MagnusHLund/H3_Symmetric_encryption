using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace H3_Symmetric_encryption.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AlgorithmPerformanceDbContext>
    {
        public AlgorithmPerformanceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AlgorithmPerformanceDbContext>();
            optionsBuilder.UseSqlite("Data Source=AlgorithmPerformance.db");

            return new AlgorithmPerformanceDbContext(optionsBuilder.Options);
        }
    }
}