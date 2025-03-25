using H3_Symmetric_encryption.Entities;

namespace H3_Symmetric_encryption.Interfaces.Repositories
{
    public interface IAlgorithmPerformanceRepository
    {
        bool SavePerformanceTestResults(AlgorithmPerformanceEntity[] newTestResults);
        List<AlgorithmPerformanceEntity> GetPerformanceTestResults(string workload);
    }
}