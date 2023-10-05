using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace EmailValidation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter an email address=");
            string text = Console.ReadLine();

            bool isValidEmail = EmailValidator.IsEmailValidManual(text);
            if (isValidEmail)
            {
                Console.WriteLine($"\"{text}\" is a valid email address.");
            }
            else
            {
                Console.WriteLine($"\"{text}\" is NOT a valid email address.");
            }


            bool isValidWithRegex = EmailValidator.IsEmailValidRegex(text);
            if (isValidWithRegex)
            {
                Console.WriteLine($"\"{text}\" is a valid email address (with regex).");
            }
            else
            {
                Console.WriteLine($"\"{text}\" is NOT a valid email address (with regex).");
            }
        }
    }
}