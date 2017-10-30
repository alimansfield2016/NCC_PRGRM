using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrjCipherProgram
{
    class Program
    {

        /// <summary>
        /// NOTE - Quintgram Dictionanry takes 500MB of memory to function
        /// </summary>

        public static string cipherText = "";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Cipher Cracking Program");
            getCMD();
            shutdown();
        }

        static void getCMD()
        {
            Boolean alive = true;
            string userInput;
            string cmd3chars;
            while (alive)
            {
                cmd3chars = "";
                cmdPrompt();
                userInput = Console.ReadLine() + "   ";
                for (int i = 0; i < 3; i++)
                {
                    cmd3chars = cmd3chars + userInput[i];
                }
                //               Console.WriteLine(cmd3chars, userInput);        is used for debuging to show the string going into the case statement
                switch (cmd3chars.ToLower())
                {
                    case "shu":
                    case "sd ":
                        alive = false;
                        break;

                    case "hel":
                        help();
                        break;

                    case "cle":
                        Console.Clear();
                        break;

                    case "loa":
                    case "ld ":
                    case "lda":
                        loadCipherText();
                        break;

                    case "rem":
                        remove(userInput);
                        break;

                    case "rep":
                        replace();
                        break;

                    case "ana":
                        analyze.analyze_();
                        break;

                    case "asc":
                        AsciiCharacters();
                        break;

                    case "sho":
                        Console.WriteLine(cipherText);
                        break;

                    case "upp":
                        Console.WriteLine("CipherText converted to Upper-Case");
                        cipherText = cipherText.ToUpper();
                        break;

                    case "low":
                        Console.WriteLine("CipherText converted to Lower-Case");
                        cipherText = cipherText.ToLower();
                        break;

                    case "fit":
                        if (userInput.ToUpper().Contains("-QUINT"))
                        {
                            Console.WriteLine("Initialising Quintgrams \nThis may take a few seconds");
                            nGrams Quintgram = new nGrams(5);
                            Console.WriteLine("Quintgram Fitness is {0:F}", Quintgram.Score(cipherText));
                            Quintgram = null;
                        }
                        else if (userInput.ToUpper().Contains("-TRI"))
                        {
                            Console.WriteLine("Initialising Trigrams \nThis may take a few seconds");
                            nGrams Trigram = new nGrams(3);
                            Console.WriteLine("Trigram Fitness is {0:F}", Trigram.Score(cipherText));
                            Trigram = null;
                        }
                        else
                        {
                            Console.WriteLine("Initialising Quadgrams \nThis may take a few seconds");
                            nGrams Quadgram = new nGrams(4);
                            Console.WriteLine("Quadgram Fitness is {0:F}", Quadgram.Score(cipherText));
                            Quadgram = null;
                        }
                        break;

                    case "cra":
                        crack();
                        break;
                }
                
            }
        }

        public static void cmdPrompt(params string[] Bits)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("CipherCracker");
            for(int i = 0; i<Bits.Length; i++)
            {
                Console.Write('\\');
                Console.Write(Bits[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(':');
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write('>');
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void shutdown()
        {
            Console.WriteLine("\nTerminating Program");
            System.Threading.Thread.Sleep(1000);
        }

        static void help()
        {
            Console.WriteLine("Help    - Shows this Menu");
            Console.WriteLine("Clear   - Clears the terminal");
            Console.WriteLine("Load    - Load CipherText into memory");
            Console.WriteLine("Remove  - Remove Spaces and other Punctuation from CipherText");
            Console.WriteLine("Replace - Replace a specific character with another Unicode character");
            Console.WriteLine("Analyze - Analyze text for letter frequency, letter pairs etc");
            Console.WriteLine("Show    - Prints out the active cipherText");
            Console.WriteLine("Upper   - Convert CipherText to Purely Upper-Case");
            Console.WriteLine("Fitness - Analyze Fitness of Cipher Text");
            Console.WriteLine("Shutdown- Terminates the Program Safely");
        }

        static void loadCipherText()
        {
            bool reading = true;
            ConsoleKeyInfo userInputKey;
            int noEnters = 0;
            Console.Write("Are you sure you want to load Cipher Text\nThis will overwrite any existing text in memory\nY/N : ");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper() == "Y")
            {
                Console.WriteLine("Please input Cipher Text:");
                cipherText = "";
                while (reading)
                {
                    userInputKey = Console.ReadKey();
                    if (userInputKey.Key == ConsoleKey.Enter)
                    {
                        if (noEnters == 0)
                        {
                            cipherText += (char)10;
                            Console.WriteLine();
                        }
                        noEnters++;
                        if (noEnters == 3) reading = false;
                    }
                    else
                    {
                        cipherText += userInputKey.KeyChar;
                        noEnters = 0;
                    }
                }
            }
        }

        /// <summary>
        /// removes all spacing or punctuation from the ciphertext. does not effect any of the characters
        /// </summary>
        static void remove(string parameters)
        {
            bool notDone = true;
            int i = 0;
            int[,] charsParams;
            charsParams = new int[5, 2] { { 32, 48 }, { 10, 10 }, { 58, 64 }, { 91, 96 }, { 123, 126 } };
            bool all;
            all = parameters.Contains(@"-a");
            if (all)
            {
                Console.WriteLine("Remove All Punctuation? (esc to cancel)");
                if (Console.ReadKey().Key == ConsoleKey.Escape) return;
            }

            while (notDone && i < 5)
            {
                notDone = remove_2(charsParams[i, 0], charsParams[i, 1], all);
                i++;
            }
            Console.Write("Would you like to see Modified text? Y/N : ");
            if (Console.ReadLine().ToUpper() == "Y") Console.WriteLine(cipherText);

        }

        static bool remove_2(int startAt, int endAt, bool all)
        {
            ConsoleKey userInput = System.ConsoleKey.Y;
            string userInputString = "Y";
            for (int j = startAt; j < endAt + 1; j++)
            {
                userInputString = "Y";
                if (!all)
                {
                    Console.Write("Remove all \"{0}\"? Y/N : ", (char)j);
                    userInput = Console.ReadKey().Key;
                    userInputString = userInput.ToString().ToUpper();
                    Console.WriteLine();
                }
                if (userInput == ConsoleKey.Escape)
                {
                    return false;
                }
                else if (userInputString == "Y")
                {
                    cipherText = cipherText.Replace(((char)j).ToString(), string.Empty);
                }
            }
            return true;
        }

        static void replace()
        {
            ConsoleKeyInfo userInput;
            string userInputString;
            char charToReplace;
            int numberOfChar;

            bool inputValid = false;
            Console.Write("Which character would you like to replace? (Press F1 for more options) : ");
            userInput = Console.ReadKey();
            Console.WriteLine();
            if (userInput.Key == ConsoleKey.F1)
            {
                Console.Write("Would you like ASCII or Unicode Character set? A/U : ");
                userInput = Console.ReadKey();
                Console.WriteLine();
                if (userInput.KeyChar == 'A')
                {
                    do
                    {
                        Console.Write("Which ASCII character would you like to replace? (0-255) : ");
                        userInputString = Console.ReadLine();
                        inputValid = int.TryParse(userInputString, out numberOfChar);
                        charToReplace = (char)numberOfChar;
                    } while (!inputValid | numberOfChar > 255);
                }
                else
                {
                    //     do
                    //     {
                    //         Console.Write("Which Unicode character would you like to replace? 0x0000 - FFFF : ");
                    //         userInputString = Console.ReadLine();
                    //         inputValid = int.TryParse(userInputString.ToString(),System.Globalization.NumberStyles.HexNumber out numberOfChar);
                    ////         charToReplace  ;
                    //     } while (!inputValid | numberOfChar > 255);

                }
            }
            else
            {
                charToReplace = userInput.KeyChar;
                Console.Write("What Character would you like to replace it with? (F1 for nothing) : ");
                if((userInput=Console.ReadKey()).Key == ConsoleKey.F1)
                {
                    cipherText = cipherText.Replace(charToReplace.ToString(), string.Empty);
                }
                else
                {
                    cipherText = cipherText.Replace(charToReplace.ToString(), userInput.ToString());
                }
            }
        }


        static void AsciiCharacters()
        {
            for (int i = 0; i < 256; i++)
            {
                Console.WriteLine("{0}, {1}", i, (char)i);
            }
        }

        static void crack()
        {
            string cmd;
            bool cracking = true;
            int l;
            while (cracking)
            {
                Program.cmdPrompt("Crack");
                cmd = Console.ReadLine();
                if (cmd.Length < 3) l = 0;
                else l = 3;
                switch (cmd.Substring(0, l).ToUpper())
                {
                    case "AUT":
                        autoCrack(cipherText, cmd);
                        break;

                    case "SUB":
                        cipherText = Manipulate.Sub(cipherText);
                        break;

                    case "EXI":
                    case "QUI":
                        cracking = false;
                        break;
                }
            }
        }

        static void autoCrack(string text, string parameters)
        {
            int noiters = 100;
            int ngLength = 2;
            if (parameters.Contains("-sub"))
            {
                if(parameters.Contains("-iter"))
                {
                    Console.Write("How many Iterations (default 100) : ");
                    int.TryParse(Console.ReadLine(), out noiters);
                }
                if (parameters.Contains("-gram"))
                {
                    Console.Write("What length of NGram do you want to use - longer ngrams take longer. \n1 - 5, default 2 : ");
                    int.TryParse(Console.ReadLine(), out ngLength);
                }
                MonoAlphabetics mono = new MonoAlphabetics(ngLength);
                cipherText = mono.Substitution(text, noiters);

            }
            else if (parameters.Contains("-poly"))
            {
                if (parameters.Contains("-iter"))
                {
                    Console.Write("How many Iterations (default 100) : ");
                    int.TryParse(Console.ReadLine(), out noiters);
                }
                if (parameters.Contains("-gram"))
                {
                    Console.Write("What length of NGram do you want to use - longer ngrams take longer. \n1 - 5, default 2 : ");
                    int.TryParse(Console.ReadLine(), out ngLength);
                }
                MonoAlphabetics mono = new MonoAlphabetics(ngLength);
                cipherText = mono.polybius(text, noiters);

            }
            else if(parameters.ToLower().Contains("-four") | parameters.ToLower().Contains("-fs"))
            {
                FourSquare fs = new FourSquare();

                cipherText = fs.crack(1000, cipherText);
            }
        }
    }
}
