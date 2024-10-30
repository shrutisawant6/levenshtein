namespace LevenshteinAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteColoredLine("┌──────────────────────────────────┐", ConsoleColor.Cyan);
            WriteColoredLine("│    Validate string similarity    │", ConsoleColor.Blue);
            WriteColoredLine("└──────────────────────────────────┘", ConsoleColor.Cyan);
            WriteColoredLine(" * Enter ESC to closed the console.", ConsoleColor.Cyan);

            bool keepLooping = true;
            while (keepLooping)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter the first string");
                    string firstString = Console.ReadLine();


                    Console.WriteLine();
                    Console.WriteLine("Enter the second string");
                    string secondString = Console.ReadLine();


                    bool isSimilar = firstString.AreStringsSimilar(secondString);

                    Console.WriteLine();
                    if (isSimilar)
                        WriteColoredLine("Strings are similar!", ConsoleColor.Green);
                    else
                        WriteColoredLine("Strings are not similar.", ConsoleColor.Yellow);

                }
                catch (Exception)
                {
                    WriteColoredLine("Check the entered text and try again!", ConsoleColor.Red);
                }
                WriteColoredLine("───────────────────────────────────", ConsoleColor.Cyan);

                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    keepLooping = false;
            }
        }

        private static void WriteColoredLine(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.WriteLine("");
            Console.ResetColor();
        }
    }
}
