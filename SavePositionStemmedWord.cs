using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class SavePositionStemmedWord
    {
        public void CreatePositionDocument(CollectionDocument cd)
        {
            string[] arrStemmedWord;
            cd.stemmedWordPositionDocument = new Dictionary<string, List<int>>[cd.getNTuple() + 1];
            cd.stemmedWordPositionDocument[0] = null;
            for(int i=1; i<=cd.getNTuple(); i++)
            {
                cd.stemmedWordPositionDocument[i] = new Dictionary<string, List<int>>();
                arrStemmedWord = cd.getTermDocID()[i];
                List<int> listPosition = new List<int>();
                int j = 0;
                foreach(string word in arrStemmedWord)
                {
                    if (!cd.stemmedWordPositionDocument[i].ContainsKey(word))
                        cd.stemmedWordPositionDocument[i].Add(word, new List<int>());
                    cd.stemmedWordPositionDocument[i][word].Add(j);
                    listPosition.Add(j);
                    j++;
                }
                cd.getSizeWindowDocument().Add(i, listPosition.Count);
            }
        }
        public void CreatePositionQuery(CollectionDocument cq)
        {
            string[] arrStemmedWord;
            cq.stemmedWordPositionQuery = new Dictionary<string, List<int>>[cq.getNTuple() + 1];
            cq.stemmedWordPositionQuery[0] = null;
            //for (int i = 1; i <= cq.getNTuple(); i++)
            //Console.WriteLine("cq.getNTuple() = {0}", cq.getNTuple());
            foreach(KeyValuePair<int, string[]>kvp in cq.getTermQueryID())
            {
                cq.stemmedWordPositionQuery[kvp.Key] = new Dictionary<string, List<int>>();
                arrStemmedWord = cq.getTermQueryID()[kvp.Key];
                List<int> listPosition = new List<int>();
                int j = 0;
                foreach (string word in arrStemmedWord)
                {
                    if (!cq.stemmedWordPositionQuery[kvp.Key].ContainsKey(word))
                        cq.stemmedWordPositionQuery[kvp.Key].Add(word, new List<int>());
                    cq.stemmedWordPositionQuery[kvp.Key][word].Add(j);
                    listPosition.Add(j);
                    j++;
                }
                cq.getSizeWindowQuery().Add(kvp.Key, listPosition.Count);
            }
        }
        public void PrintPositionDocument(CollectionDocument cd)
        {
            for (int i = 1; i < cd.getNTuple(); i++)
            {
                foreach(KeyValuePair<string, List<int>> kvp in cd.stemmedWordPositionDocument[i])
                {
                    Console.Write("term {0}, position = ", kvp.Key);
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        Console.Write("{0}", kvp.Value[j]);
                        if (j < (kvp.Value.Count - 1))
                            Console.Write(",");
                    }
                    Console.WriteLine();
                }
            }
        }
        public void PrintPositionQuery(CollectionDocument cq)
        {
            for (int i = 1; i < cq.getNTuple(); i++)
            {
                foreach (KeyValuePair<string, List<int>> kvp in cq.stemmedWordPositionQuery[i])
                {
                    Console.Write("term {0}, position = ", kvp.Key);
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        Console.Write("{0}", kvp.Value[j]);
                        if (j < (kvp.Value.Count - 1))
                            Console.Write(",");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
