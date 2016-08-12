using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class Indexing
    {
        public void IndexingDocument(CollectionDocument cd)
        {
            int index = 0;
            int indexDoc = 1;

            foreach (KeyValuePair<int, string> kvp in cd.getWordDictStemmed())
            {
                string word = kvp.Value;
                string[] arrayWord = word.Split(new string[] { " ", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                cd.getTermDocID().Add(kvp.Key, arrayWord);//indexDoc, arrayWord);
                //indexDoc++;
            }

            index = 1;
            //for (int i = 1; i <= cd.getTermDocID().Count; i++)
            foreach(KeyValuePair<int, string> kvp in cd.getWordDictStemmed())
            {
                foreach (string s in cd.getTermDocID()[kvp.Key])
                {
                    cd.getTerm().Add(index, s);
                    index++;
                }
            }
            cd.setSortedTerm((from kv in cd.getTerm() orderby kv.Value select kv).ToList());

            //for (int i = 1; i <= cd.getTermDocID().Count; i++)
            foreach(KeyValuePair<int, string>kvp in cd.getWordDictStemmed())
            {
                for (int j = 0; j < cd.getTermDocID()[kvp.Key].Length; j++)
                {
                    if (cd.getTermDocID().ContainsKey(kvp.Key))
                    {
                        if (!cd.getWordDocNumber().ContainsKey(cd.getTermDocID()[kvp.Key].ElementAt(j)))
                            cd.getWordDocNumber().Add(cd.getTermDocID()[kvp.Key].ElementAt(j), new List<int>());
                        //      if(!wordDocNumber[cd.getTermDocID()[i].ElementAt(j)].Contains(i))
                        cd.getWordDocNumber()[cd.getTermDocID()[kvp.Key].ElementAt(j)].Add(kvp.Key);
                    }
                }
            }
            cd.setStructuredDocIndex((from entry in cd.getWordDocNumber() orderby entry.Key ascending select entry).ToList());
            // Proses mengganti term dengan indeks term (Indexing)
            index = 1;
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
            {
                cd.getIndeksTerm().Add(index, kvp.Value);
                index++;
            }
        }
        public void IndexingQuery(CollectionDocument cd)
        {
            int index = 0;
            //int indexQuery = 1;
            foreach (KeyValuePair<int, string> kvp in cd.getQueryStemmed())
            {
                string queryString = kvp.Value;
                string[] arrayWord = queryString.Split(new string[] { " ", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                cd.getTermQueryID().Add(kvp.Key, arrayWord);
                //indexQuery++;
            }

            index = 1;
          //  for (int i = 1; i <= cd.getTermQueryID().Count; i++)
           // {
                /*
                foreach (string s in cd.getTermQueryID()[i])
                {
                    cd.getTermQuery().Add(cd.getTermQueryID()[i]., s);
                    index++;
                }*/
                 
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermQueryID())
            {
                for (int j = 0; j < cd.getTermQueryID()[kvp.Key].Length; j++)
                {
                    cd.getTermQuery().Add(index, kvp.Value[j]);
                    index++;
                }
            }
            
            cd.setSortedTermQuery((from kv in cd.getTermQuery() orderby kv.Value select kv).ToList());

            //for (int i = 1; i <= cd.getTermQueryID().Count; i++)
            foreach(KeyValuePair<int, string[]>kvp in cd.getTermQueryID())
            {
                for (int j = 0; j < cd.getTermQueryID()[kvp.Key].Length; j++)
                {
                    //if (cd.getTermQueryID().ContainsKey(kvp.Key))
                    //{
                        if (!cd.getQueryDocNumber().ContainsKey(cd.getTermQueryID()[kvp.Key].ElementAt(j)))
                            cd.getQueryDocNumber().Add(cd.getTermQueryID()[kvp.Key].ElementAt(j), new List<int>());
                        cd.getQueryDocNumber()[cd.getTermQueryID()[kvp.Key].ElementAt(j)].Add(kvp.Key);
                    //}
                }
            }
            cd.setStructuredQueryIndex((from entry in cd.getQueryDocNumber() orderby entry.Key ascending select entry).ToList());

            // Proses mengganti term dengan indeks term (Indexing)
            index = 1;
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredQueryIndex())
            {
                cd.getIndeksTermQuery().Add(index, kvp.Value);
                index++;
            }
        }
    }
}
