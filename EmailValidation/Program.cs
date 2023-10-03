using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace EmailValidation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter an email address=");
            string text = Console.ReadLine();

            bool isValidEmail = false;
            if (!string.IsNullOrEmpty(text))
            {
                string[] parts = text.Split('@', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    string userNamePart = parts[0];
                    bool userNamePartIsValid = StartsWithLetter(userNamePart) &&
                                               EndsWithLetterOrDigit(userNamePart) &&
                                               HasOnlyValidCharactersAndNonConsecutiveSpecialChars(userNamePart);

                    string domainNamePart = parts[1];
                    bool domainNamePartIsValid = StartsWithLetter(domainNamePart) &&
                                                HasOnlyValidCharactersAndNonConsecutiveSpecialChars(domainNamePart) &&
                                                IsValidDomainSuffix(domainNamePart);

                    // the final
                    isValidEmail = userNamePartIsValid && domainNamePartIsValid;
                }
            }

            if (isValidEmail)
            {
                Console.WriteLine($"\"{text}\" is a valid email address.");
            }
            else
            {
                Console.WriteLine($"\"{text}\" is NOT a valid email address.");
            }


            bool isValidWithRegex = IsValidEmail(text);
            if (isValidWithRegex)
            {
                Console.WriteLine($"\"{text}\" is a valid email address (with regex).");
            }
            else
            {
                Console.WriteLine($"\"{text}\" is NOT a valid email address (with regex).");
            }
        }

        static bool StartsWithLetter(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                char firstCharacter = text[0];
                bool startsWithletter = char.IsLetter(firstCharacter);
                return startsWithletter;
            }

            return false;
        }

        static bool EndsWithLetterOrDigit(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                char lastCharacter = text[text.Length - 1];
                bool endsWithletterOrDigit = char.IsLetter(lastCharacter) || char.IsDigit(lastCharacter);
                return endsWithletterOrDigit;
            }

            return false;
        }

        static bool HasOnlyValidCharactersAndNonConsecutiveSpecialChars(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                for (int i = 0; i < text.Length; i++)
                {
                    char characterAtIndex = text[0];

                    bool isCharacterAtIndexSpecialChar = (characterAtIndex == '.') ||
                                                         (characterAtIndex == '-') ||
                                                         (characterAtIndex == '_');

                    bool isCharacterAtIndexAllowed = char.IsLetter(characterAtIndex) ||
                                                     char.IsDigit(characterAtIndex) ||
                                                     isCharacterAtIndexSpecialChar;

                    if (!isCharacterAtIndexAllowed)
                    {
                        return false;
                    }

                    if (i -1 >= 0)
                    {
                        char characterAtPreviousIndex = text[i - 1];
                        bool isCharacterAtPreviousIndexSpecialChar = (characterAtPreviousIndex == '.') ||
                                                                     (characterAtPreviousIndex == '-') ||
                                                                     (characterAtPreviousIndex == '_');

                        bool hasConsecutiveSpecialChar = isCharacterAtIndexSpecialChar && isCharacterAtPreviousIndexSpecialChar;
                        if (hasConsecutiveSpecialChar)
                        {
                            return false;
                        }
                    }
                    
                }

                return true;

            }

            return false;
        }

        static bool IsValidDomainSuffix(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && text.Contains('.', StringComparison.OrdinalIgnoreCase))
            {
                int lastIndexOfDot = text.LastIndexOf(".", StringComparison.OrdinalIgnoreCase);
                if (lastIndexOfDot != -1)
                {
                    string dommainSuffix = text.Substring(lastIndexOfDot + 1);
                    if (string.IsNullOrEmpty(dommainSuffix))
                    {
                        // nothing left after final dot
                        return false;
                    }
                    else
                    {
                        bool isSuffixValid = dommainSuffix.Length >= 2 && dommainSuffix.Length <= 6;
                        return isSuffixValid;
                    }
                }
            }

            return false;
        }

        static bool IsValidEmail(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                return Regex.IsMatch(text, "^(.+)@(.+)\\.(.+)$");
            }

            return false;
        }

    }
}