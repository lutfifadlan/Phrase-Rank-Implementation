using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class Weighting
    {
        public void WeightingDocument(CollectionDocument cd)
        {
            // term frequency = kemunculan term dalam SATU dokumen
            // Hitung term freqeuncy dari term pada suatu dokumen
            // Buat list dokumen dengan term frequency masing2 term
            // raw tf
            cd.noDocTermTF = new Dictionary<string, int>[cd.getNTuple() + 1];
            cd.noDocTermTF[0] = null;
            Dictionary<string, int> termTF = new Dictionary<string, int>();
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID())
            {
                cd.noDocTermTF[kvp.Key] = new Dictionary<string, int>();
                foreach (string word in kvp.Value)
                {
                    if (termTF.ContainsKey(word))
                        termTF[word] += 1;
                    else
                        termTF.Add(word, 1);
                }
                foreach (KeyValuePair<string, int> tempkvp in termTF)
                    cd.noDocTermTF[kvp.Key].Add(tempkvp.Key, tempkvp.Value);
                termTF.Clear();
            }

            /* 
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
            {
                int nterm = 1;
                int templv = kvp.Value[0];
                for (int i = 1; i < kvp.Value.Count; i++)
                {
                    if (templv == kvp.Value[i])
                        nterm++;
                    else
                        templv = kvp.Value[i];
                }
                termFrequency.Add(kvp.Key, nterm);
            }*/

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\TermFrequencyDocument.txt"))
            {
                for (int i = 1; i <= cd.getNTuple(); i++)
                {
                    stw.WriteLine("Dokumen {0}", i);
                    foreach (KeyValuePair<string, int> kvp in cd.getNoDocTermTF()[i])
                        stw.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);
                }
            }

            // Logharitmic tf

            foreach (KeyValuePair<string, int> kvp in cd.getTermFrequency())
                cd.getLogarithmicTF().Add(kvp.Key, (1 + Math.Log10(kvp.Value)));

            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
            {
                int ndoc = kvp.Value.Count;
                List<int> temp = new List<int>();
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (!temp.Contains(kvp.Value[i]))
                        temp.Add(kvp.Value[i]);
                    //    Console.WriteLine(kvp.Value[i]);
                }
                // Console.WriteLine(temp.Count);
                cd.getDf().Add(kvp.Key, temp.Count());
                temp.Clear();
            }
            // IDF
            // make idf for every term
            int N = cd.getTermDocID().Count;
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
            {
                //    Console.WriteLine(kvp.Key);
                double temp = Math.Log10((double)N / (double)cd.getDf()[kvp.Key]);
                if (!Double.IsNaN(temp))
                    cd.getIDF().Add(kvp.Key, temp);
                else
                    cd.getIDF().Add(kvp.Key, 0);
            }

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\IDFDocument.txt"))
            {
                foreach (KeyValuePair<string, double> kvp in cd.getIDF())
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }
        }
        public void WeightingQuery(CollectionDocument cd)
        {
            cd.noQueryTermTF = new Dictionary<string, int>[cd.getNTuple() + 1];
            cd.noQueryTermTF[0] = null;
            Dictionary<string, int> termTF = new Dictionary<string, int>();
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermQueryID())
            {
                cd.noQueryTermTF[kvp.Key] = new Dictionary<string, int>();
                foreach (string word in kvp.Value)
                {
                    if (termTF.ContainsKey(word))
                        termTF[word] += 1;
                    else
                        termTF.Add(word, 1);
                }
                foreach (KeyValuePair<string, int> tempkvp in termTF)
                    cd.noQueryTermTF[kvp.Key].Add(tempkvp.Key, tempkvp.Value);
                termTF.Clear();
            }

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TermFrequencyQuery.txt"))
            {
                //for (int i = 1; i <= cd.getNTuple(); i++)
                foreach(KeyValuePair<int, string[]> kvp in cd.getTermQueryID())
                {
                    stw.WriteLine("Dokumen {0}", kvp.Key);
                    foreach (KeyValuePair<string, int> mvp in cd.getNoQueryTermTF()[kvp.Key])
                        stw.WriteLine("[{0} | {1}]", mvp.Key, mvp.Value);
                }
            }

            // Logharitmic tf
            foreach (KeyValuePair<string, int> kvp in cd.getTermFrequencyQuery())
                cd.getLogarithmicTFQuery().Add(kvp.Key, (1 + Math.Log10(kvp.Value)));

            //IDF Query
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredQueryIndex())
            {
                int ndoc = kvp.Value.Count;
                List<int> temp = new List<int>();
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    if (!temp.Contains(kvp.Value[i]))
                        temp.Add(kvp.Value[i]);
                }
                cd.getDfQuery().Add(kvp.Key, temp.Count());
            }

            int N = cd.getNTuple();
            foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredQueryIndex())
            {
                double temp = Math.Log10((double)N / (double)cd.getDfQuery()[kvp.Key]);
                if (!Double.IsNaN(temp))
                    cd.getIDFQuery().Add(kvp.Key, temp);
                else
                    cd.getIDFQuery().Add(kvp.Key, 0);
            }
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\IDFQuery.txt"))
            {
                foreach (KeyValuePair<string, double> kvp in cd.getIDFQuery())
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }
        }
        /*
        public void WeightingQuery(List<string> q)
        {
            foreach (string sq in q)
            {
                int nterm = 1;
                string temps = q[0];
                for (int i = 1; i < q.Count; i++)
                {
                    if (temps == q[i])
                        nterm++;
                    else
                        temps = q[i];
                }
                if(!termFrequencyQuery.ContainsKey(sq))
                termFrequencyQuery.Add(sq, nterm);
            }
            

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TermFrequencyQuery.txt"))
            {
                foreach (KeyValuePair<string, int> kvp in termFrequencyQuery)
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }

            // Logharitmic tf
            foreach (KeyValuePair<string, int> kvp in termFrequencyQuery)
                logarithmicTFQuery.Add(kvp.Key, (1 + Math.Log10(kvp.Value)));
            //IDF
            foreach (string sq in q)
            {
                int ndoc = q.Count;
                string templv = q[0];
                for (int i = 1; i < q.Count; i++)
                {
                    if (templv == q[i])
                        ndoc--;
                    templv = q[i];
                }
              //  dfQuery.Add(kvp.Key, ndoc);
            }
           // int NQuery = cd.getTermQueryID().Count;
            foreach (KeyValuePair<string, int> kvp in termFrequencyQuery)
            {
             //   IDFQuery.Add(kvp.Key, Math.Log10(NQuery / dfQuery[kvp.Key]));
            }

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\IDFQuery.txt"))
            {
                foreach (KeyValuePair<string, double> kvp in IDFQuery)
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }
        }*/
        public void MakeWeightQueryToList(CollectionDocument cq, int noQuery)
        {
            cq.getListTFIDFQ().Clear();
            cq.getWeightQuery().Clear();
            foreach (string termQuery in cq.getTermQueryID()[noQuery])
            {
                cq.listTFIDFQ.Add(cq.getTFIDFperQueryNormalized()[noQuery][termQuery]);
                if (!cq.getWeightQuery().ContainsKey(termQuery))
                    cq.getWeightQuery().Add(termQuery, cq.getTFIDFperQueryNormalized()[noQuery][termQuery]);
            }
            cq.setWeightListQuery(cq.getListTFIDFQ().Sum());
        }
    }
}
