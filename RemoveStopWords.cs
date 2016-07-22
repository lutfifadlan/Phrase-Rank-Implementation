using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class RemoveStopWords
    {
        public string removeStopWords(string _word)
        {
            //Gimana buat nanganin word kayak u.s. ? 
            // abbreviation di dokumen adi:
            // tic, a. d. i, nasa, kwic, i. e., m. d., IS, r, ddc, sdi, bscp, itek
            string[] stopWords = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\stopwords.txt");
            Dictionary<string, bool> stopWord = new Dictionary<string, bool>();
            foreach (string s in stopWords)
                stopWord.Add(s, true);
            //char[] delimiters = new char[] { ' ', ',', ';', '.', '-', '(', ')', ':', '/' };
            char[] delimiters = new char[]
            {
                ' ',
                ',',
                ';',
                '.',
                '-',
                '\n',
                '\'',
                ':',
                '(',
                ')',
                '/',
                '?',
                '\t',
                '[',
                ']',
                '"',
                '%',
                '=',
                '>',
                '<',
                '*',
                '#',
                '@',
                '!',
                '$',
                '{',
                '}',
                '^',
                '&',
                '+'
            };
            string[] words = _word.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();
            foreach (string currentWord in words)
            {
                if (!stopWord.ContainsKey(currentWord))
                    builder.Append(currentWord).Append(' ');
            }
            return builder.ToString().Trim();
        }
        public void RemoveStopWordDocumentDictionary(CollectionDocument cd)
        {
            int index = 1;
            foreach (KeyValuePair<int, string> kvp in cd.getWordDictionary().Skip(1).ToList())
            {
                string stoppedWord = removeStopWords(kvp.Value);
                //cd.getWordDictStopped().Add(index, stoppedWord);
                cd.getWordDictStopped().Add(kvp.Key, stoppedWord);
                //index++;
            }
        }
        public void RemoveStopWordQueryDictionary(CollectionDocument cd)
        {
            int index = 1;
            foreach (KeyValuePair<int, string> kvp in cd.getWordDictionary().Skip(1).ToList())
            {
                string stoppedWord = removeStopWords(kvp.Value);
                //cd.getQueryDictStopped().Add(index, stoppedWord);
                cd.getQueryDictStopped().Add(kvp.Key, stoppedWord);
                index++;
            }
        }
    }
}
