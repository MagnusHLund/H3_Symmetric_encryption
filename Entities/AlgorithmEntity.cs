namespace H3_Symmetric_encryption.Entities
{
    public class AlgorithmEntity
    {
        public int AlgorithmId { get; set; }
        public string Name { get; set; }
        public ushort KeySizeBits { get; set; }

        public List<AlgorithmPerformanceEntity> AlgorithmPerformanceEntities { get; set; }

        public AlgorithmEntity(int algorithmId, string name, ushort keySizeBits)
        {
            AlgorithmId = algorithmId;
            Name = name;
            KeySizeBits = keySizeBits;
        }

        private AlgorithmEntity() { }
    }
}