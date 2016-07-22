using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class ComputingTFIDF
    {
        public void ComputeTFIDFperDocument(CollectionDocument cd)
        {
            // term yang sama pada dokumen yang berbeda belum tentu memiliki TF yang sama
            cd.TFIDFperDocument = new Dictionary<string, double>[cd.getNTuple() + 1];
            cd.TFIDFperDocument[0] = null;
            for (int i = 1; i <= cd.getNTuple(); i++)
            {
                cd.TFIDFperDocument[i] = new Dictionary<string, double>();
                foreach (KeyValuePair<string, int> kvp in cd.getNoDocTermTF()[i])
                {
                    cd.TFIDFperDocument[i].Add(kvp.Key, (kvp.Value * cd.getIDF()[kvp.Key]));
                }
            }

            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\TFIDFperDocument.txt"))
            {
                for (int i = 1; i <= cd.getNTuple(); i++)
                {
                    stw.WriteLine("Dokumen {0}", i);
                    foreach (KeyValuePair<string, double> kvp in cd.getTFIDFperDocument()[i])
                        stw.WriteLine("[TFIDF term {0} = {1}]", kvp.Key, kvp.Value);
                }
            }
        }
        public void ComputeTFIDFperQuery(CollectionDocument cd)
        {
            cd.TFIDFperQuery = new Dictionary<string, double>[cd.getNTuple() + 1];
            cd.TFIDFperQuery[0] = null;
            //for (int i = 1; i <= cd.getNTuple(); i++)
            foreach(KeyValuePair<int, string[]>kvp in cd.getTermQueryID())
            {
                cd.TFIDFperQuery[kvp.Key] = new Dictionary<string, double>();
                foreach (KeyValuePair<string, int> mvp in cd.getNoQueryTermTF()[kvp.Key])
                {
                    if (cd.getIDFQuery().ContainsKey(mvp.Key))
                        cd.TFIDFperQuery[kvp.Key].Add(mvp.Key, (mvp.Value * cd.getIDFQuery()[mvp.Key]));
                    else
                        cd.TFIDFperQuery[kvp.Key].Add(mvp.Key, (mvp.Value * 0));
                }
            }
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TFIDFperQuery.txt"))
            {
                //for (int i = 1; i <= cd.getNTuple(); i++)
                foreach(KeyValuePair<int, string[]> kvp in cd.getTermQueryID())
                {
                    stw.WriteLine("Query {0}", kvp.Key);
                    foreach (KeyValuePair<string, double> mvp in cd.getTFIDFperQuery()[kvp.Key])
                        stw.WriteLine("[TFIDF term {0} = {1}]", mvp.Key, mvp.Value);
                }
            }
        }
    }
}
