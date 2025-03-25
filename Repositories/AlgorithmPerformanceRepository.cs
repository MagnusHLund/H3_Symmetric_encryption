using H3_Symmetric_encryption.Data;
using H3_Symmetric_encryption.Entities;
using H3_Symmetric_encryption.Interfaces.Repositories;

namespace H3_Symmetric_encryption.Repositories
{
    public class AlgorithmPerformanceRepository : IAlgorithmPerformanceRepository
    {
        private readonly AlgorithmPerformanceDbContext _context;

        public AlgorithmPerformanceRepository(AlgorithmPerformanceDbContext context)
        {
            _context = context;
        }

        public bool SavePerformanceTestResults(AlgorithmPerformanceEntity[] newTestResults)
        {
            foreach (AlgorithmPerformanceEntity newTestResult in newTestResults)
            {
                AlgorithmPerformanceEntity? existingResult = _context.AlgorithmPerformanceEntities
                    .FirstOrDefault(existing =>
                        existing.AlgorithmId == newTestResult.AlgorithmId &&
                        existing.Workload == newTestResult.Workload &&
                        existing.PlainTextSizeInBytes == newTestResult.PlainTextSizeInBytes);

                if (existingResult != null)
                {
                    // Saves the latest values of the performance test results
                    existingResult.SecondsPerBlock = newTestResult.SecondsPerBlock;
                    existingResult.BytesPerSecondInMemory = newTestResult.BytesPerSecondInMemory;
                    existingResult.BytesPerSecondOnDisk = newTestResult.BytesPerSecondOnDisk;

                    _context.AlgorithmPerformanceEntities.Update(existingResult);
                }
                else
                {
                    _context.AlgorithmPerformanceEntities.Add(newTestResult);
                }
            }

            _context.SaveChanges();
            return true;
        }

        public List<AlgorithmPerformanceEntity> GetPerformanceTestResults(string workload)
        {
            List<AlgorithmPerformanceEntity> previousPerformanceTestResults = _context.AlgorithmPerformanceEntities
                .Where(testResult => testResult.Workload == workload)
                .ToList();

            return previousPerformanceTestResults;
        }
    }
}