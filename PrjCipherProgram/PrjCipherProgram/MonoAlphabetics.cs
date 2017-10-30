using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjCipherProgram
{
    class MonoAlphabetics
    {
        public readonly int ngramLength;//define the ngram length used to score in substutution solver
        private nGrams nGram;

        public MonoAlphabetics(int ngl)
        {
            ngramLength = ngl;
            nGram = new nGrams(ngramLength);
        }



        public string Substitution(string text, int noIterations)
        {
            char[] bestKey = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] Key = bestKey;
            char buffer;
            double Score;
            double maxScore = -99e9;
            int i = 0;
            int j = 0;
            int a;
            int b;
            string bestText = text;
            string currentText = text;
            Console.WriteLine("Attempting Auto Substitution Solve \nThis may take a minute");
            while (i < noIterations)
            {
                i++;
                Console.CursorLeft = 0;
                //Randomise Key Here
                Random rand = new Random();
                j = 0;
                while (j < 100)
                {
                    j++;
                    a = rand.Next(0, 25);
                    b = rand.Next(0, 25);
                    buffer = Key[a];
                    Key[a] = Key[b];
                    Key[b] = buffer;
                }
                j = 0;
                while (j < 1000)
                {
                    j++;
                    a = rand.Next(0, 25);
                    b = rand.Next(0, 25);
                    buffer = Key[a];
                    Key[a] = Key[b];
                    Key[b] = buffer;
                    currentText = SubstituteChars(text, Key);
                    Score = nGram.Score(currentText);
                    if (Score > maxScore)
                    {
                        maxScore = Score;
                        bestKey = Key;
                        bestText = currentText;
                        j = 0;
                    }
                    else
                    {
                        Key[b] = Key[a];
                        Key[a] = buffer;
                    }
                }
                Console.Write("{0}/{1} iterations complete", i, noIterations);
            }
            Console.WriteLine("\nBest Text is: \n{0}\n\n", bestText);
            return bestText;
        }

        string SubstituteChars(string text, char[] key)
        {
            text = text.ToLower();
            text = text.Replace('a', key[0]).Replace('b', key[1]).Replace('c', key[2]).Replace('d', key[3]).Replace('e', key[4]).Replace('f', key[5]).Replace('g', key[6]);
            text = text.Replace('h', key[7]).Replace('i', key[8]).Replace('j', key[9]).Replace('k', key[10]).Replace('l', key[11]).Replace('m', key[12]).Replace('n', key[13]);
            text = text.Replace('o', key[14]).Replace('p', key[15]).Replace('q', key[16]).Replace('r', key[17]).Replace('s', key[18]).Replace('t', key[19]);
            text = text.Replace('u', key[20]).Replace('v', key[21]).Replace('w', key[22]).Replace('x', key[23]).Replace('y', key[24]).Replace('z', key[25]);

            return text;
        }

        public string polybius(string text, int noItersInSub)
        {
            string newText = string.Empty;
            string key;
            char nextChar;
            int letter = 65;
            IDictionary<string, char> letterPairs = new Dictionary<string, char>();
            Console.WriteLine("Replacing Pairs of Chars");
            for(int i = 0; i+1<text.Length; i+=2)
            {
                key = text.Substring(i, 2);
                try
                {
                    nextChar = letterPairs[key];
                    newText += nextChar;
                }
                catch
                {
                    letterPairs.Add(key, (char)letter);
                    newText += (char)letter;
                    letter++;
                    if (letter >= 91)
                    {
                        Console.WriteLine("Text contains more than 26 letter pairs.");
                        return text;
                    }
                }
            }

            Console.WriteLine("Starting Substitution of Chars.");
            newText = Substitution(newText, noItersInSub);
            return newText;
        }
    }
}
