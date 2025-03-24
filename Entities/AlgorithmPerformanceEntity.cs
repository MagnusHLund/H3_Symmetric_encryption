namespace H3_Symmetric_encryption.Entities
{
    public class AlgorithmPerformanceEntity
    {
        public int AlgorithmPerformanceId { get; set; }
        public int PlainTextSizeInBytes { get; set; }
        public double SecondsPerBlock { get; set; }
        public double BytesPerSecondInMemory { get; set; }
        public double BytesPerSecondOnDisk { get; set; }
        public string Workload { get; set; }

        public int AlgorithmId { get; set; }
        public AlgorithmEntity Algorithm { get; set; }

        public AlgorithmPerformanceEntity(int algorithmId, int plainTextSizeInBytes, double secondsPerBlock, double bytesPerSecondInMemory, double bytesPerSecondOnDisk, string workload)
        {
            AlgorithmId = algorithmId;
            PlainTextSizeInBytes = plainTextSizeInBytes;
            SecondsPerBlock = secondsPerBlock;
            BytesPerSecondInMemory = bytesPerSecondInMemory;
            BytesPerSecondOnDisk = bytesPerSecondOnDisk;
            Workload = workload;
        }

        private AlgorithmPerformanceEntity() { }
    }
}