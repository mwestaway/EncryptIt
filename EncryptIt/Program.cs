using System;
using System.Linq;

namespace EncryptIt
{
    class Program
    {
        static char[] randomKey;
        static char[] validKey = new char[26];
        static string plaintext;
        static string cipherMessage;
        static string alphaKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        static void Main(string[] args)
        {
            int menuOption;

            while (true)
            {
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1) Encrypt a message");
                Console.WriteLine("2) Decrypt a message");
                Console.WriteLine("3) Exit program");

                menuOption = int.Parse(Console.ReadLine());

                if (menuOption == 1)
                {
                    // Ask user for a message to be encrypted
                    Console.WriteLine("\nPlease enter a message to be encrypted: ");
                    plaintext = Console.ReadLine();

                    // Generate and display random key for encryption
                    RandomKeyGen();

                    // Convert plaintext into ciphertext
                    // Display ciphertext
                    DisplayCipher();
                }
                else if (menuOption == 2)
                {
                    // Prompt user to enter a message to decrypt
                    Console.WriteLine("\nPlease enter a message to decrypt: ");
                    cipherMessage = Console.ReadLine();

                    // Prompt user for key used to encrypt plaintext
                    // Valid key provided
                    GetKey();

                    // Decrypt and display plaintext
                    DisplayPlaintext(cipherMessage);
                }
                else if (menuOption == 3)
                {
                    // Exits program
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }

        // Generate random key
        static char[] RandomKeyGen()
        {
            int index = 0;
            char randomChar;
            randomKey = new char[26];

            while (index < randomKey.Length)
            {
                // Generate random char
                Random rand = new Random();
                //randomNum = rand.Next(0, 26);
                randomChar = (char)('A' + rand.Next(0, 26));

                // Check if char is in key array
                if (!Array.Exists(randomKey, element => element == randomChar))
                {
                    // If char is not in key, add char to key at index i
                    randomKey[index] = randomChar;
                    index++;
                }
            }

            // Diplay key used for cipher
            Console.Write("\nKey:");
            Console.WriteLine(string.Join("", randomKey) + "\n");

            return randomKey;
        }


        // Convert plaintext to ciphertext and display message
        static void DisplayCipher()
        {
            // Convert plaintext into ciphertext - needs to be the same length (same amount of memory required)
            char[] ciphertext = new char[plaintext.Length];

            for (int j = 0; j < plaintext.Length; j++)
            {
                // Maintains plaintext's uppercasing
                if (char.IsUpper(plaintext[j]))
                {
                    int k = plaintext[j] - 65; // ASCII value for A = 65
                    ciphertext[j] = char.ToUpper(randomKey[k]);
                }
                // Maintains plaintext's lowercasing
                else if (char.IsLower(plaintext[j]))
                {
                    int k = plaintext[j] - 97; // ASCII value for a = 97
                    ciphertext[j] = char.ToLower(randomKey[k]);
                }
                // Maintains plaintext's numbers and punctuation 
                else
                {
                    ciphertext[j] = plaintext[j];
                }
            }

            // Display ciphertext
            Console.WriteLine("Encrypted message: ");
            Console.WriteLine(string.Join("", ciphertext) + "\n");
        }


        // Get and validate key used to encrypt message
        static char[] GetKey()
        {
            string key;
            bool duplicate;

            while (true)
            {
                // Prompt user to enter a key
                Console.WriteLine("\nPlease enter key used to encrypt message: ");
                key = Console.ReadLine().ToUpper();

                // Validate key
                // Check for duplicates - key cannot contain duplicates
                duplicate = false;

                for (int i = 0; i < key.Length; i++)
                {
                    int counter = key.Count(c => (c == key[i]));

                    if (counter > 1)
                    {
                        Console.WriteLine("\nInvalid key. Key must not contain duplicate letters.");
                        duplicate = true;
                        break;
                    }
                }

                // Check key length - if no duplicates present
                if (key.Length == 26 && !duplicate)
                {
                    // Check if key contains only letters
                    if (key.Any(char.IsDigit))
                    {
                        Console.WriteLine("\nInvalid key. Key must contain only letters.");
                    }
                    else
                    {
                        // Valid key
                        validKey = key.ToUpper().ToCharArray();
                        break;
                    }
                }
                if (key.Length != 26)
                {
                    Console.WriteLine("Invalid key. Key must contain exactly 26 letters.");
                }
            }

            return validKey;
        }


        // Display plaintext
        static void DisplayPlaintext(string cipherMessage)
        {

            char[] plainMessage = new char[cipherMessage.Length];

            for (int i = 0; i < cipherMessage.Length; i++)
            {
                int index = Array.FindIndex(validKey, element => element == char.ToUpper(cipherMessage[i]));

                // Maintain uppercase
                if (char.IsUpper(cipherMessage[i]))
                {
                    plainMessage[i] = alphaKey.ToUpper()[index];
                }
                // Maintain lowercase
                else if (char.IsLower(cipherMessage[i]))
                {
                    plainMessage[i] = alphaKey.ToLower()[index];
                }
                // Maintain number and punctuation
                else
                {
                    plainMessage[i] = cipherMessage[i];
                }
            }

            // Display decrypted message
            Console.WriteLine("\nDecrypted message: ");
            Console.WriteLine(string.Join("", plainMessage) + "\n");
        }
    }
}


