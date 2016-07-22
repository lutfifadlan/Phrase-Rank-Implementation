using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class AddingQueryToN
    {
        public void AddQueryToN(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cd.getTermDocID().Remove(0);
            //Console.WriteLine("ea1");
            cd.getTermDocID().Add(0, cq.getTermQueryID()[noQuery]);
            //Console.WriteLine("ea2");
            cq.getDocRelFound().Add(0);
            cq.setListPseudoRelDocWithoutQ(cq.getListPseudoRelDoc()[noQuery]);
            cq.getListPseudoRelDoc()[noQuery].Add(0);
            cd.stemmedWordPositionDocument[0] = new Dictionary<string, List<int>>();
            cd.stemmedWordPositionDocument[0] = cq.stemmedWordPositionQuery[noQuery];
            cd.getSizeWindowDocument()[0] = cq.getSizeWindowQuery()[noQuery];
            // Compute term frequency query in new N
            cd.noDocTermTF[0] = new Dictionary<string, int>();
            Dictionary<string, int> termTF = new Dictionary<string, int>();
            foreach (string word in cd.getTermDocID()[0])
            {
                //Console.WriteLine("ea3");
                if (termTF.ContainsKey(word))
                    termTF[word] += 1;
                else
                    termTF.Add(word, 1);
            }
            foreach (KeyValuePair<string, int> tempkvp in termTF)
                cd.noDocTermTF[0].Add(tempkvp.Key, tempkvp.Value);
            //Console.WriteLine("ea4");
            termTF.Clear();
            // Indexing
            /*
            foreach (int i in cq.getDocRelFound())
            {
                for (int j = 0; j < cd.getTermDocID()[i].Length; j++)
                {
                    if (!wordDocNumberNew.ContainsKey(cd.getTermDocID()[i].ElementAt(j)))
                        wordDocNumberNew.Add(cd.getTermDocID()[i].ElementAt(j), new List<int>());
                    wordDocNumberNew[cd.getTermDocID()[i].ElementAt(j)].Add(i);
                }
            }
            // Compute df w(n)
            foreach (KeyValuePair<string, List<int>> kvp in wordDocNumberNew)
            {
                int ndoc = kvp.Value.Count;
                List<int> temp = new List<int>();
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (!temp.Contains(kvp.Value[i]))
                        temp.Add(kvp.Value[i]);
                }
                dfNew.Add(kvp.Key, temp.Count());
                temp.Clear();
            }*/
        }
        /*
        public bool isAdjacent(CollectionDocument cd, CollectionDocument cq, int noPseudoRelDoc)
        {
            int tempPos1;
            int tempPos2;
            //int i = 0;
            //int j = 0;
            string s1, s2;
            List<int> listTempPos1 = new List<int>();
            List<int> listTempPos2 = new List<int>();
            //foreach(int i in cq.getDocRelFound())
            for(int i=0; i<cq.getTermQueryID().Count; i++)

            for (int j = 0; j < cd.getTermDocID()[i].Length; j++)
            {
                if (j == cd.getTermDocID()[i].Length - 1) { }
                else
                {
                    s1 = cd.getTermDocID()[i][j];
                    s2 = cd.getTermDocID()[i][j + 1];
                    cd.getAdjacentDictionary()
                }
            }
            
        }
        */
    }
}
