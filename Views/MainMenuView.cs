namespace H3_Symmetric_encryption.Views
{
    public static class MainMenuView
    {
        public static string SelectMainMenuOption()
        {
            string[] menuOptions =
            {
                "Test encryption performance",
                "View test results",
            };

            string userInput = MainView.CustomMenu(menuOptions);
            return userInput;
        }
    }
}