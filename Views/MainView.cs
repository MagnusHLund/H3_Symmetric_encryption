namespace H3_Symmetric_encryption.Views
{
    public static class MainView
    {
        public static void CustomOutput(string message)
        {
            Console.WriteLine(message);
        }

        public static void CustomMenu(string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                ConsoleColor consoleColor = i % 2 == 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.ForegroundColor = consoleColor;

                Console.WriteLine($"{i + 1}. {menuItems[i]}");
            }
        }

        public static string GetUserInputWithTitle(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine() ?? "";
        }

        public static string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }
    }
}