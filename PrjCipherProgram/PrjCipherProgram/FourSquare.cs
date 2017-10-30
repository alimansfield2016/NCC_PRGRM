using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjCipherProgram
{
    class FourSquare
    {
        Random rand = new Random();

        char[] key1 = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] key2 = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();

        public string crack(int iters, string text)
        {
            char[] textChars = text.ToCharArray();
            int length = text.Length;
            string result = string.Empty;
            Console.WriteLine("Running FourSquare Crack \nThis could take a few minutes");

            int i = 0;
            double score, maxscore = -99e99;

            while (i < iters)
            {
                i++;
                score = foursquareCrack(text);
                if(score > maxscore)
                {
                    maxscore = score;
                    Console.WriteLine("Best score so far:{0:F}, on iteration {1}", score, i);
                    Console.WriteLine("Key is : {0:s}, {1:s}", key1, key2);
                    result = foursquareDecipher(key1.ToString(), key2.ToString(), text);
                    Console.WriteLine("PlainText is : {0:s}", result);
                }
            }

            return result.ToString();
        }

        char[] swapTwoLetters(char[] key)
        {
            int i = rand.Next(24);
            int j = rand.Next(24);
            char buffer = key[i];
            key[i] = key[j];
            key[j] = buffer;
            return key;
        }

        double foursquareCrack(string text)
        {
            int count;
            nGrams Trigram = new nGrams(3);
            double T;
            string deciphered = string.Empty;
            char[] testKey1, testKey2 = new char[25];
            char[] maxKey1, maxKey2 = new char[25];
            double prob, dF, maxscore, score;
            double bestScore;
            maxKey1 = key1;
            maxKey2 = key2;
            deciphered = foursquareDecipher(maxKey1.ToString(), maxKey2.ToString(), text);
            maxscore = Trigram.Score(deciphered.ToString());
            bestScore = maxscore;
            for(T = 20; T>=0; T -= 0.1)
            {
                for(count = 0; count < 10000; count++)
                {
                    testKey1 = maxKey1;
                    testKey2 = maxKey2;
                    if (count % 2 == 0)
                    {
                        testKey1 = swapTwoLetters(testKey1);
                    }
                    else
                    {
                        testKey2 = swapTwoLetters(testKey2);
                    }
                    deciphered = foursquareDecipher(testKey1.ToString(), testKey2.ToString(), text);
                    score = Trigram.Score(deciphered.ToString());
                    dF = score - maxscore;
                    if (dF >= 0)
                    {
                        maxscore = score;
                        maxKey1 = testKey1;
                        maxKey2 = testKey2;
                    }
                    else if (T > 0)
                    {
                        prob = Math.Log10(1.0 * dF / T);
                        if(prob > 1.0 * rand.Next(100) / 100)
                        {
                            maxscore = score;
                            maxKey1 = testKey1;
                            maxKey2 = testKey2;
                        }
                    }
                    if(maxscore > bestScore)
                    {
                        bestScore = maxscore;
                        key1 = maxKey1;
                        key2 = maxKey2;
                    }
                }
                Console.WriteLine("Complete {0:0} passes",200 - T * 10);
            }
            return bestScore;
        }

        string foursquareDecipher(string _key1, string _key2, string text)
        {
            string result = string.Empty;
            int i;
            char[] characters = new char[2];
            int a_ind, b_ind;
            int a_row, b_row;
            int a_col, b_col;
            string alph = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

            for(i = 0; i < (text.Length/2) *2 -1; i += 2)
            {
                characters[0] = text[i];
                characters[1] = text[i + 1];
                a_ind = _key1.IndexOf(characters[0]);
                Console.Write("Key1: ");
                Console.WriteLine( _key1);
                Console.WriteLine("A = \"{0}\" at {1}", characters[0], a_ind);
                b_ind = _key2.IndexOf(characters[1]);
                Console.Write("Key2: ");
                Console.WriteLine( _key2);
                Console.WriteLine("A = \"{0}\" at {1}", characters[1], b_ind);
                a_row = a_ind / 5;
                b_row = b_ind / 5;
                a_col = a_ind % 5;
                b_col = b_ind % 5;
                result += alph[5 * a_row + b_col];
                result += alph[5 * b_row + a_col];
            }

            return result;
        }

        char[] shuffleKey(char[] key)
        {
            int i, j;
            char buffer;
            for(i = 24; i>=1; i--)
            {
                j = rand.Next() % (i + 1);
                buffer = key[j];
                key[j] = key[i];
                key[i] = buffer;
            }
            return key;
        }
    }
}
