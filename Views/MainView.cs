namespace H3_Symmetric_encryption.Views
{
    internal static class MainView
    {
        internal static void CustomOutput(string message)
        {
            Console.WriteLine(message);
        }

        internal static string CustomMenu(string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                ConsoleColor consoleColor = i % 2 == 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
                Console.ForegroundColor = consoleColor;

                Console.WriteLine($"{i + 1}. {menuItems[i]}");
            }

            Console.ForegroundColor = ConsoleColor.White;

            string userInput = Console.ReadLine() ?? "";
            return userInput;
        }

        internal static string GetUserInputWithTitle(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine() ?? "";
        }

        internal static string GetUserInput()
        {
            return Console.ReadLine() ?? "";
        }
    }
}