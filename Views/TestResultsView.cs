namespace H3_Symmetric_encryption.Views
{
    public static class TestResultsView
    {
        public static string ShowEncryptionOrDecryptionMenu()
        {
            string[] menuOptions =
            {
                "Show encryption results",
                "Show decryption results"
            };

            string userInput = MainView.CustomMenu(menuOptions);
            return userInput;
        }

        public static void RenderPerformanceTable(string tableName, List<string> headers, List<List<string>> rows)
        {
            MainView.CustomOutput($"\n{tableName}");
            MainView.CustomOutput(new string('-', tableName.Length));

            // Render headers
            string headerRow = string.Join(" | ", headers);
            MainView.CustomOutput(headerRow);
            MainView.CustomOutput(new string('-', headerRow.Length));

            // Render rows
            foreach (var row in rows)
            {
                string rowData = string.Join(" | ", row);
                MainView.CustomOutput(rowData);
            }

            MainView.CustomOutput(new string('-', headerRow.Length));
        }
    }
}