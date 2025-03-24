namespace H3_Symmetric_encryption.Entities
{
    public class AlgorithmEntity
    {
        public int AlgorithmId { get; set; }
        public string Name { get; set; }
        public ushort Bits { get; set; }

        public List<AlgorithmPerformanceEntity> AlgorithmPerformanceEntities { get; set; }

        public AlgorithmEntity(int algorithmId, string name, ushort bits)
        {
            AlgorithmId = algorithmId;
            Name = name;
            Bits = bits;
        }

        public AlgorithmEntity(string name, ushort bits)
        {
            Name = name;
            Bits = bits;
        }

        private AlgorithmEntity() { }
    }
}