using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class MatchedDocument
    {
        public void PrintMatchedQuery(CollectionDocument cq)
        {
            foreach (KeyValuePair<string, List<List<int>>> kvp in cq.getMatchedQueryDoc())
            {
                int j = 0;
                Console.Write("[");
                Console.Write("{0}", kvp.Key);

                Console.Write("|");
                for (int i = 0; i < kvp.Value[j].Count; i++)
                {
                    Console.Write("{0}", kvp.Value[j].ElementAt(i));
                    if (i < (kvp.Value[j].Count - 1))
                        Console.Write(", ");
                }
                Console.WriteLine("]");
            }
        }
        public void SaveMatchDocument(CollectionDocument cq)
        {
            cq.getMatchedDocument().Clear();
            foreach (KeyValuePair<string, List<List<int>>> kvp in cq.getMatchedQueryDoc())
                foreach (List<int> li in kvp.Value)
                    foreach (int i in li)
                        if (!cq.getMatchedDocument().Contains(i))
                            cq.getMatchedDocument().Add(i);
        }
    }
}
