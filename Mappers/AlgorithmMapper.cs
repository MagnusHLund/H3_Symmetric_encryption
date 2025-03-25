using H3_Symmetric_encryption.Entities;

namespace H3_Symmetric_encryption.Mappers
{
    public static class AlgorithmMapper
    {
        private readonly static Dictionary<int, AlgorithmEntity> _algorithmEntities = new Dictionary<int, AlgorithmEntity>
        {
            {1, new AlgorithmEntity(1, "AES CPS 128 bit", 128)},
            {2, new AlgorithmEntity(2, "AES CPS 192 bit", 192)},
            {3, new AlgorithmEntity(3, "AES CPS 256 bit", 256)},
            {4, new AlgorithmEntity(4, "AES Managed 128 bit", 128)},
            {5, new AlgorithmEntity(5, "AES Managed 192 bit", 192)},
            {6, new AlgorithmEntity(6, "AES Managed 256 bit", 256)},
            {7, new AlgorithmEntity(7, "Rijndael 128 bit", 128)},
            {8, new AlgorithmEntity(8, "Rijndael 192 bit", 192)},
            {9, new AlgorithmEntity(9, "Rijndael 256 bit", 256)},
            {10, new AlgorithmEntity(10, "DES 64 bit", 64)},
            {11, new AlgorithmEntity(11, "TripleDES 192 bit", 192)}
        };

        public static List<AlgorithmEntity> GetAllAlgorithms()
        {
            return _algorithmEntities.Values.ToList();
        }

        public static AlgorithmEntity GetAlgorithmById(int algorithmId)
        {
            return _algorithmEntities[algorithmId];
        }
    }
}