namespace Utilities
{
    public static class UIUtilities
    {
        public static string GetStringFromUser(string prompt)
        {
            string? userInput = null;

            while (string.IsNullOrWhiteSpace(userInput))
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
            }
            return userInput;
        }

        public static int GetIntFromUser(string prompt)
        {
            var intFromUser = 0;

            var isInvalidInput = true;
            while (isInvalidInput)
            {
                Console.Write(prompt);
                isInvalidInput = !int.TryParse(Console.ReadLine(),
                    System.Globalization.NumberStyles.AllowLeadingWhite
                    | System.Globalization.NumberStyles.AllowTrailingWhite,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out intFromUser);
                if (isInvalidInput)
                {
                    Console.WriteLine("Must be an integer.");
                }
            }

            return intFromUser;
        }

        public static decimal GetDecimalFromUser(string prompt)
        {
            var decimalFromUser = 0.0M;
            var isInvalidInput = true;
            while (isInvalidInput)
            {
                Console.Write(prompt);
                isInvalidInput = !decimal.TryParse(Console.ReadLine(),
                    System.Globalization.NumberStyles.AllowLeadingWhite
                    | System.Globalization.NumberStyles.AllowTrailingWhite
                    | System.Globalization.NumberStyles.Currency,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out decimalFromUser);
                if (isInvalidInput)
                {
                    Console.WriteLine("Must be a number.");
                }
            }
            return decimalFromUser;
        }
    }
}
