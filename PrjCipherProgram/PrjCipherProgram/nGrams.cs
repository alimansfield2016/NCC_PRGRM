using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PrjCipherProgram
{
    class nGrams
    {
        //This value can be read from outside the class so that the program knows the operating length of the instance
        public readonly int ngramLength;

        IDictionary<string, int> ngramFrequency = new Dictionary<string, int>();
        IDictionary<string, double> ngramLogarithm = new Dictionary<string, double>();

        string[] fileLocations = new string[5]
        {
         @"C:\Users\alima\Desktop\Cipher\english_monograms.txt",
         @"C:\Users\alima\Desktop\Cipher\english_bigrams.txt",
         @"C:\Users\alima\Desktop\Cipher\english_trigrams.txt",
         @"C:\Users\alima\Desktop\Cipher\english_quadgrams.txt",
         @"C:\Users\alima\Desktop\Cipher\english_quintgrams.txt"
        };

        public nGrams(int Length)
        {
            ngramLength = Length;
            generateNgrams(fileLocations[Length - 1], Length);
        }

        void generateNgrams(string fileLocation, int ngramLength)
        {
            string lineString;
            string ngramKey;
            string intPartofLine;
            int ngramFreq;
            int lineNo = 0;
            double ngramLog;
            UInt64 numberOfNgrams = 0;
            try
            {
                using(StreamReader sr = new StreamReader(fileLocation))
                {
                    //Firstly generate the number of Ngrams in file
                    //Then generate the Freq dictionary
                    while((lineString = sr.ReadLine()) != null)
                    {
                        lineNo++;
                        intPartofLine = lineString.Substring(ngramLength + 1);
                        ngramFreq = int.Parse(intPartofLine);
                        numberOfNgrams += UInt64.Parse(intPartofLine);
                        ngramKey = lineString.Substring(0, ngramLength);
                        ngramFrequency.Add(ngramKey, ngramFreq);
                    }
                }
                foreach(string key in ngramFrequency.Keys)
                {
                    ngramLog = Math.Log10(ngramFrequency[key] / (double)numberOfNgrams);
                    ngramLogarithm.Add(key, ngramLog);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Unexpected Occurence in Line {0}", lineNo);
                Console.WriteLine(e.Message);
            }
        }

        public double Score(string textToScore)
        {
            string ngramToScore;
            double ngramScore;
            double totalScore = 0;
            for(int i = 0; i + ngramLength < textToScore.Length + 1; i++)
            {
                ngramToScore = textToScore.Substring(i, ngramLength);
                try
                {
                    ngramScore = ngramLogarithm[ngramToScore];
                    totalScore += ngramScore;
                }
                catch
                {
                    //this just catches any ngram that does not exit in the file, 
                    //for example if it contains numbers or other non alphabet 
                    //characters
                }
            }
            return totalScore;
        }
    }
}
