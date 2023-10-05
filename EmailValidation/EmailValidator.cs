using System.Text.RegularExpressions;

namespace EmailValidation
{
    public static class EmailValidator
    {
        public static bool IsEmailValidManual(string email)
        {
            bool isValidEmail = false;
            if (!string.IsNullOrEmpty(email))
            {
                string[] parts = email.Split('@', StringSplitOptions.RemoveEmptyEntries);
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

            return isValidEmail;
        }

        public static bool IsEmailValidRegex(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return Regex.IsMatch(email, "^(.+)@(.+)\\.(.+)$");
            }

            return false;
        }

        // ---------------------- Helper methods ------------------------------

        private static bool StartsWithLetter(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                char firstCharacter = text[0];
                bool startsWithletter = char.IsLetter(firstCharacter);
                return startsWithletter;
            }

            return false;
        }

        private static bool EndsWithLetterOrDigit(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                char lastCharacter = text[text.Length - 1];
                bool endsWithletterOrDigit = char.IsLetter(lastCharacter) || char.IsDigit(lastCharacter);
                return endsWithletterOrDigit;
            }

            return false;
        }

        private static bool HasOnlyValidCharactersAndNonConsecutiveSpecialChars(string text)
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

                    if (i - 1 >= 0)
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

        private static bool IsValidDomainSuffix(string text)
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
    }
}
