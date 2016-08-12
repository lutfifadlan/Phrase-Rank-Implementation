using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InformationRetrieval
{
    class RetrieveCollection
    {
        public void Retrieval(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cq.getDocFound().Clear();
            cq.getRankedDocFound().Clear();
            cq.getDocRelFound().Clear();
            cq.getRankedDocRelFound().Clear();
            cq.getDocNonRelFound().Clear();
            cq.getRankedDocNonRelFound().Clear();
            cq.getMatchedQueryDoc().Clear();
            int nQRelevant = 0;
            string[] query = cq.getTermQueryID()[noQuery];
            if (cq.getRld().ContainsKey(noQuery))
                nQRelevant = cq.getRld()[noQuery].Count();
            else
                nQRelevant = 0;
            List<List<int>> noDoc = new List<List<int>>();
            string termMatchedQuery;
            foreach (string s in query)
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
                {
                    if (s == kvp.Key)
                    {
                        termMatchedQuery = s;
                        for (int i = 0; i < kvp.Value.Count; i++)
                        {
                            if (!cq.getDocFound().Contains(kvp.Value[i]))
                                cq.getDocFound().Add(kvp.Value[i]);
                            if (nQRelevant > 0)
                            {
                                for (int j = 0; j < nQRelevant; j++)
                                {
                                    if (kvp.Value[i] == cq.getRld()[noQuery].ElementAt(j))
                                    {
                                        if (!cq.getDocRelFound().Contains(kvp.Value[i]))
                                            cq.getDocRelFound().Add(kvp.Value[i]);
                                    }
                                    else
                                    {
                                        if (!cq.getDocNonRelFound().Contains(kvp.Value[i]))
                                            cq.getDocNonRelFound().Add(kvp.Value[i]);
                                    }
                                }
                            }
                        }
                        noDoc.Add(kvp.Value);
                        if (!cq.getMatchedQueryDoc().ContainsKey(termMatchedQuery))
                            cq.getMatchedQueryDoc().Add(termMatchedQuery, noDoc);
                    }
                }
                noDoc = new List<List<int>>();
            }
            if (!cq.getListNoQueryDocFound().ContainsKey(noQuery))
                cq.getListNoQueryDocFound().Add(noQuery, new List<int>());
            foreach(int i in cq.getDocFound())
                cq.getListNoQueryDocFound()[noQuery].Add(i);
            if (!cq.getListNoQueryDocRelFound().ContainsKey(noQuery))
                cq.getListNoQueryDocRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getDocRelFound())
                cq.getListNoQueryDocRelFound()[noQuery].Add(i);
            if (!cq.getListNoQueryDocNonRelFound().ContainsKey(noQuery))
                cq.getListNoQueryDocNonRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getDocNonRelFound())
                cq.getListNoQueryDocNonRelFound()[noQuery].Add(i);
        }
        public void WriteDocumentRetrievalResult(CollectionDocument cq, int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Document Retrieval Result.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                sw.WriteLine("Dokumen relevan yang ditemukan:");
                for (int i = 0; i < cq.getDocRelFound().Count; i++)
                {
                    sw.Write("{0}", cq.getDocRelFound()[i]);
                    if (i < cq.getDocRelFound().Count - 1)
                        sw.Write(", ");
                }
                sw.WriteLine();
            }
        }
        public void RetrievalPhRank(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cq.getDocFound().Clear();
            cq.getRankedDocFound().Clear();
            cq.getDocRelFound().Clear();
            cq.getRankedDocRelFound().Clear();
            cq.getDocNonRelFound().Clear();
            cq.getRankedDocNonRelFound().Clear();
            cq.getMatchedQueryDoc().Clear();
            int nQRelevant = 0;
            string[] query = cq.getTermQueryReformulatedID()[noQuery];//cq.getTermQueryID()[noQuery];
            //foreach (string s in query)
            //  Console.Write(s);
            if (cq.getRld().ContainsKey(noQuery))
                nQRelevant = cq.getRld()[noQuery].Count();
            else
                nQRelevant = 0;
            //Console.WriteLine("nQRelevant = {0}", nQRelevant);
            List<List<int>> noDoc = new List<List<int>>();
            string termMatchedQuery;
            foreach (string s in query)
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
                {
                    if (s == kvp.Key)
                    {
                        termMatchedQuery = s;
                        for (int i = 0; i < kvp.Value.Count; i++)
                        {
                            if (!cq.getDocFound().Contains(kvp.Value[i]))
                                cq.getDocFound().Add(kvp.Value[i]);
                            if (nQRelevant > 0)
                            {
                                for (int j = 0; j < nQRelevant; j++)
                                {
                                    if (kvp.Value[i] == cq.getRld()[noQuery].ElementAt(j))
                                    {
                                        if (!cq.getDocRelFound().Contains(kvp.Value[i]))
                                            cq.getDocRelFound().Add(kvp.Value[i]);
                                    }
                                    else
                                    {
                                        if (!cq.getDocNonRelFound().Contains(kvp.Value[i]))
                                            cq.getDocNonRelFound().Add(kvp.Value[i]);
                                    }
                                }
                            }
                        }
                        noDoc.Add(kvp.Value);
                        if (!cq.getMatchedQueryDoc().ContainsKey(termMatchedQuery))
                            cq.getMatchedQueryDoc().Add(termMatchedQuery, noDoc);
                    }
                }
                noDoc = new List<List<int>>();
            }
            if (!cq.getListNoQueryDocFound().ContainsKey(noQuery))
                cq.getListNoQueryDocFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getDocFound())
                cq.getListNoQueryDocFound()[noQuery].Add(i);
            if (!cq.getListNoQueryDocRelFound().ContainsKey(noQuery))
                cq.getListNoQueryDocRelFound().Add(noQuery, new List<int>());
            //cq.getListNoQueryDocRelFound().Add(noQuery, cq.getDocRelFound());
            foreach (int i in cq.getDocRelFound())
                cq.getListNoQueryDocRelFound()[noQuery].Add(i);
            if (!cq.getListNoQueryDocNonRelFound().ContainsKey(noQuery))
                cq.getListNoQueryDocNonRelFound().Add(noQuery, new List<int>());
            //cq.getListNoQueryDocNonRelFound().Add(noQuery, cq.getDocNonRelFound());
            foreach (int i in cq.getDocNonRelFound())
                cq.getListNoQueryDocNonRelFound()[noQuery].Add(i);
        }
    }
}
