using H3_Symmetric_encryption.Entities;
using H3_Symmetric_encryption.Interfaces.Repositories;

namespace H3_Symmetric_encryption.Repositories
{
    public class AlgorithmPerformanceRepository : IAlgorithmPerformanceRepository
    {
        public bool SavePerformanceTestResults(AlgorithmPerformanceEntity[] testResults)
        {
            throw new NotImplementedException();
        }

        public List<AlgorithmPerformanceEntity> GetPerformanceTestResults(string workLoad)
        {
            throw new NotImplementedException();
        }
    }
}