using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class Is2WordAdjacent
    {
        public void isWordAdjacent(CollectionDocument cd, CollectionDocument cq)
        {
            string s1, s2;
            cd.listAdjacentDictionary = new Dictionary<int, List<Adjacent>>[cq.getNTuple() +1];
            cd.listAdjacentDictionary[0] = null;
            //for (int i = 1; i <= cq.getTermQueryID().Count; i++)
            foreach(KeyValuePair<int, string[]>kvp in cq.getTermQueryID())
            {
                cd.getTermDocID().Remove(0);
                cd.getTermDocID().Add(0, cq.getTermQueryID()[kvp.Key]);
                cd.listAdjacentDictionary[kvp.Key] = new Dictionary<int, List<Adjacent>>();

                //for (int j = 0; j < cd.getTermDocID().Count; j++)
               foreach (KeyValuePair<int, string[]> vp in cd.getTermDocID())
               {
                    List<Adjacent> listAdj = new List<Adjacent>();
                    for (int k = 0; k < vp.Value.Length;k++)//cd.getTermDocID()[j].Length; k++)
                    {
                        if (k == (vp.Value.Length - 1)) { }//cd.getTermDocID()[j].Length - 1) { }
                        else
                        {
                            Adjacent adj = new Adjacent();
                            s1 = vp.Value[k];//cd.getTermDocID()[j][k];
                            s2 = vp.Value[k + 1];//cd.getTermDocID()[j][k + 1];
                            adj.setW1(s1);
                            adj.setW2(s2);
                            adj.setIsAdjacent(true);
                            listAdj.Add(adj);
                        }
                    }
                    cd.listAdjacentDictionary[kvp.Key].Add(vp.Key, listAdj);//j, listAdj);
                }
                
            }
        }
    }
}
