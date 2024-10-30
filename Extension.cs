namespace LevenshteinAlgorithm
{
    public static class Extension
    {
        private static readonly char[] separator = [
                                                    ' ',   // Space
                                                    ',',   // Comma
                                                    '.',   // Period
                                                    '!',   // Exclamation mark
                                                    '?',   // Question mark
                                                    ';',   // Semicolon
                                                    ':',   // Colon
                                                    '-',   // Hyphen
                                                    '_',   // Underscore
                                                    '(',   // Open parenthesis
                                                    ')',   // Close parenthesis
                                                    '[',   // Open bracket
                                                    ']',   // Close bracket
                                                    '{',   // Open curly brace
                                                    '}',   // Close curly brace
                                                    '\n',  // Newline
                                                    '\r',  // Carriage return
                                                    '\t',  // Tab
                                                    '\f',  // Form feed
                                                    '\v',  // Vertical tab
                                                    '/',   // Slash
                                                    '\\',  // Backslash
                                                    '"',   // Double quote
                                                    '\'',  // Single quote
                                                    '|',   // Pipe
                                                    '<',   // Less than
                                                    '>',   // Greater than
                                                    '@',   // At symbol
                                                    '#',   // Hash
                                                    '$',   // Dollar sign
                                                    '%',   // Percent sign
                                                    '^',   // Caret
                                                    '&',   // Ampersand
                                                    '*',   // Asterisk
                                                    '+',   // Plus
                                                    '=',   // Equals
                                                    '`',   // Backtick
                                                    '~'    // Tilde
                                                ];

        //Check if similar word exists in a statement
        public static bool DoesStatementContainSimilarWords(this string statement, string wordToBeSerached)
        {
            try
            {
                var words = statement.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (word.AreStringsSimilar(wordToBeSerached))
                        return true;
                }
            }
            catch { }

            return false;
        }

        //Compare two strings for similarity
        public static bool AreStringsSimilar(this string firstString, string secondString)
        {
            try
            {
                var formattedFirstString = firstString.ToUpperWithoutSpaces();
                var formattedSecondString = secondString.ToUpperWithoutSpaces();

                if (formattedFirstString == formattedSecondString)
                    return true;

                if (string.IsNullOrEmpty(formattedFirstString) || string.IsNullOrEmpty(formattedSecondString))
                    return false;

                return formattedFirstString.CompareStringsAlgorithm(formattedSecondString);
            }
            catch
            {
                return false;
            }
        }

        //LEVENSHTEIN ALGORITHM checks if two strings are similar
        //Similarity is validated based on the length of each string 
        //Matrix is formed to compare the strings and their match, and how many addition/deletion/substituion are needed of each character to macth these two 
        private static bool CompareStringsAlgorithm(this string firstString, string secondString)
        {
            int maxLength = Math.Max(firstString.Length, secondString.Length);
            decimal percentageOfLongerString = 0.3m; //30% of the longer string length
            int threshold = (int)Math.Round(maxLength * percentageOfLongerString); //maximum value of number of edits required to make strings same

            var len1 = firstString.Length;
            var len2 = secondString.Length;
            var matrix = new int[len1 + 1, len2 + 1];

            for (int s1 = 0; s1 <= len1; s1++)
                matrix[s1, 0] = s1;

            for (int s2 = 0; s2 <= len2; s2++)
                matrix[0, s2] = s2;

            for (int s1 = 1; s1 <= len1; s1++)
            {
                for (int s2 = 1; s2 <= len2; s2++)
                {
                    int cost = firstString[s1 - 1] == secondString[s2 - 1] ? 0 : 1;

                    matrix[s1, s2] = Math.Min(Math.Min(
                        matrix[s1 - 1, s2] + 1,         // Deletion(Removing a character,  to make strings similar)
                        matrix[s1, s2 - 1] + 1),        // Insertion(Adding a character, to make strings similar)
                        matrix[s1 - 1, s2 - 1] + cost); // Substitution(Changing a character, to make strings similar)
                }
            }

            var distance = matrix[len1, len2];//number of edits required to make strings same
            return distance <= threshold;
        }

        //Remove string spaces and make it CAPS
        //https://stackoverflow.com/questions/6219454/efficient-way-to-remove-all-whitespace-from-string
        private static string ToUpperWithoutSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var spacesRemoved = new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
            return spacesRemoved.ToUpper();
        }
    }
}
