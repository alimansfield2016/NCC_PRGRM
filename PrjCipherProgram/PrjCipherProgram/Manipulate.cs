using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrjCipherProgram
{
    class Manipulate
    {

        public static string Sub(string text)
        {
            string charToSwap1;
            string charToSwap2;
            string newText = text;
            Console.Write("What two Characters would you like to replace in text : ");
            charToSwap1 = Console.ReadKey().KeyChar.ToString().ToUpper();
            Console.Write(" and ");
            charToSwap2 = Console.ReadKey().KeyChar.ToString().ToUpper();
            if (charToSwap1 != charToSwap2)
            {
                newText = newText.Replace(charToSwap1, ".").Replace(charToSwap2, charToSwap1).Replace(".", charToSwap2);
                Console.WriteLine("\nCharacters \"{0}\" and \"{1}\" have been swapped", charToSwap1, charToSwap2);
                Console.Write("Would you like to see modified text? Y/N : ");
                if (Console.ReadKey().KeyChar == ('y' | 'Y'))
                {
                    Console.WriteLine();
                    Console.WriteLine(newText);
                }
                else Console.WriteLine();
                Console.Write("Would you like to update CipherText? Y/N : ");
                if (Console.ReadKey().KeyChar == ('y' | 'Y'))
                {
                    Console.WriteLine();
                    return newText;
                }
                else
                {
                    Console.WriteLine();
                    return text;
                }
            }
            else
            {
                Console.WriteLine("Characters Must be Different");
                return text;
            }

        }

    }
}
