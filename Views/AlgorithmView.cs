namespace H3_Symmetric_encryption.Views
{
    public static class AlgorithmView
    {
        public static string SelectAlgorithmMenu()
        {
            string[] menuOptions =
            {
                "AES CSP 128-bit",
                "AES CSP 256-bit",
                "AES Managed 128-bit",
                "AES Managed 256-bit",
                "Rijndael Managed 128-bit",
                "Rijndael Managed 256-bit",
                "DES CSP 56-bit",
                "Triple DES CSP 168-bit",
            };

            string userInput = MainView.CustomMenu(menuOptions);
            return userInput;
        }
    }
}