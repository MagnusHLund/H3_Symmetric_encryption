using H3_Symmetric_encryption.Views;
using H3_Symmetric_encryption.Entities;
using H3_Symmetric_encryption.Mappers;

namespace H3_Symmetric_encryption.Models
{
    public class PerformanceTable
    {
        public string TableName { get; }
        public List<AlgorithmPerformanceEntity> AlgorithmPerformanceEntities { get; set; }
        public int PlainTextSizeInBytes { get; set; }
        public string Workload { get; set; }

        public PerformanceTable(List<AlgorithmPerformanceEntity> algorithmPerformanceEntities, int plainTextSizeInBytes, string workload)
        {
            AlgorithmPerformanceEntities = algorithmPerformanceEntities;
            PlainTextSizeInBytes = plainTextSizeInBytes;
            Workload = workload;

            TableName = $"{workload} Performance Table for {plainTextSizeInBytes} bytes";
        }

        public void DrawTable()
        {
            // Prepare headers
            List<string> headers = new List<string> { "Metric" };
            headers.AddRange(AlgorithmPerformanceEntities.Select(entity =>
            {
                AlgorithmEntity algorithm = AlgorithmMapper.GetAlgorithmById(entity.AlgorithmId);
                return $"{algorithm.Name}";
            }));

            // Prepare rows
            var rows = new List<List<string>>
            {
                new List<string> { "SecondsPerBlock" }
                    .Concat(AlgorithmPerformanceEntities.Select(entity => entity.SecondsPerBlock.ToString("F4"))).ToList(),

                new List<string> { "BytesPerSecondInMemory" }
                    .Concat(AlgorithmPerformanceEntities.Select(entity => entity.BytesPerSecondInMemory.ToString("F2"))).ToList(),

                new List<string> { "BytesPerSecondOnDisk" }
                    .Concat(AlgorithmPerformanceEntities.Select(entity => entity.BytesPerSecondOnDisk.ToString("F2"))).ToList()
            };

            TestResultsView.RenderPerformanceTable(TableName, headers, rows);
        }
    }
}