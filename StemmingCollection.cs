using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class StemmingCollection
    {
        public void StemmingDocument(CollectionDocument cd)
        {
            StemmingTool stemmer = new StemmingTool();
            int index = 1;
            //int a = 10875;
            //Console.WriteLine(a);
            //   stemmedWordPosition = new Dictionary<string, List<int>>[cd.getNTuple() + 1];
            //  stemmedWordPosition[0] = null;
            /*
              while (index <= cd.getNTuple())
              {
                  string word = cd.getWordDictStopped()[index];
                  //Console.WriteLine(index);
                  //Console.WriteLine(word);
                  string stemmedWord = stemmer.Stemming(word);
                  cd.getWordDictStemmed().Add(index,stemmedWord);
                  index++;
              }*/
            foreach (KeyValuePair<int, string> kvp in cd.getWordDictStopped())
            {
                //Console.WriteLine(kvp.Key);
                string word = cd.getWordDictStopped()[kvp.Key];
                cd.getWordDictStemmed().Add(kvp.Key, stemmer.Stemming(word));
            }
        }
        public void StemmingQuery(CollectionDocument cd)
        {
            StemmingTool stemmer = new StemmingTool();
            /*
            int index = 1;
            while (index <= cd.getNTuple())
            {
                string word = cd.getQueryDictStopped()[index];
                //cd.getQueryStemmed().Add(index, stemmer.Stemming(word));
                cd.getQueryStemmed().Add(index, stemmer.Stemming(word));
                index++;
            }*/
            foreach(KeyValuePair<int, string>kvp in cd.getQueryDictStopped())
            {
                string word = cd.getQueryDictStopped()[kvp.Key];
                cd.getQueryStemmed().Add(kvp.Key, stemmer.Stemming(word));
            }
        }
        /*
        public void StemmingWord(CollectionDocument cd)
        {
            Stemmer sw = new Stemmer();
            int index = 1;
            while (index <= cd.getNTuple())
            {
                StringBuilder sb = new StringBuilder();
                string word = cd.getWordDictStopped()[index];
                char[] w = new char[word.Length];
                int i = 0;
                int j = 0;
                while (i < (word.Length))
                {
                    char alphabetWord = word[i];
                    i++;
                    if (Char.IsLetter(alphabetWord))
                    {
                        w[j] = alphabetWord;
                        j++;
                    }
                    else if (!Char.IsLetter(alphabetWord))
                    {
                        for (int c = 0; c < j; c++)
                            sw.add(w[c]);
                        sw.stem();
                        String u;
                        u = sw.ToString();
                        sb.Append(u).Append(" ");
                        j = 0;
                    }
                    if (i == word.Length)
                    {
                        for (int c = 0; c < j; c++)
                            sw.add(w[c]);
                        sw.stem();
                        String u;
                        u = sw.ToString();
                        sb.Append(u);
                    }

                }
                cd.getWordDictStemmed().Add(index, sb.ToString().Trim());
                index++;
            }
        }*/
    }
}
