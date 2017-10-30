using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjCipherProgram
{
    class analyze
    {
        public static void analyze_()
        {
            bool graphing = true;
            int i = 0;
            int[,] charFreq_ = new int[256, 2];
            int posNextHighestFreq_;
            int totalChars = Program.cipherText.Length;
            for (i = 0; i < 256; i++)
            {
                charFreq_[i, 0] = charFreq(i);
                charFreq_[i, 1] = 0;
            }
            i = 0;
            Console.WriteLine("character Frequency Analysis");
            Console.WriteLine("BoxTop");
            Console.WriteLine(" | Character     | Frequency     | %             | Graph         ");
            do
            {
                posNextHighestFreq_ = posNextHighestFreq(charFreq_);
                charFreq_[posNextHighestFreq_, 1] = 1;
                if (charFreq_[posNextHighestFreq_, 0] > 0)
                {
                    printCharStats(posNextHighestFreq_, charFreq_[posNextHighestFreq_, 0]);
                }
                else graphing = false;
            } while (graphing && i < 256);
        }

        static int charFreq(int asciiChar)
        {
            int charCount = 0;
            for (int i = 0; i < Program.cipherText.Length; i++)
            {
                if (Program.cipherText[i] == (char)asciiChar) charCount++;
            }
            return charCount;
        }

        static int posNextHighestFreq(int[,] Frequencies)
        {
            int highestFreq = -1;
            int pos = -1;
            for (int i = 0; i < 256; i++)
            {
                if ((Frequencies[i, 0] > highestFreq) && (Frequencies[i, 1] == 0))
                {
                    highestFreq = Frequencies[i, 0];
                    pos = i;
                }
            }
            return pos;
        }

        static void printCharStats(int posChar, int charFreq)
        {
            double charpercent = (1.0 * charFreq / Program.cipherText.Length) * 100.0;
            Console.Write(" | {0} {1}{2}", (char)posChar, (char)9, (char)9);
            Console.Write(" | {0} {1}{2}", charFreq, (char)9, (char)9);
            Console.Write(" | {0:0.0}% {1}", charpercent, (char)9);
            Console.Write(" | {0} {1}", graphBar(charpercent), (char)9);

            Console.WriteLine();

        }

        static string graphBar(double percent)
        {
            int whole;
            double remainder;
            int remainderInt;
            string graph = string.Empty;
            remainder = percent % 1;
            remainderInt = (int)(remainder / 0.5);
            whole = (int)percent / 1;
            for (int i = 0; i < whole; i++)
            {
                graph += "\u2588";
            }
            if (remainderInt == 1) graph += "\u258c";
            return graph;
        }
    }
}
