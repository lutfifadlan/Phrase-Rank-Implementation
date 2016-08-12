using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace InformationRetrieval
{
    class ComputingTFIDFNormalized
    {
        public void NormalizeTFFIDFDocumentperTerm(CollectionDocument cd)
        {
            cd.TFIDFperDocumentNormalized = new Dictionary<string, double>[cd.getNTuple() + 1];
            cd.TFIDFperDocumentNormalized[0] = null;
            //for (int i = 1; i <= cd.getNTuple(); i++)
            foreach(KeyValuePair<int, string>kvp in cd.getWordDictStemmed())
            {
                cd.TFIDFperDocumentNormalized[kvp.Key] = new Dictionary<string, double>();
                foreach (KeyValuePair<string, double> vp in cd.getTFIDFperDocument()[kvp.Key])
                    cd.TFIDFperDocumentNormalized[kvp.Key].Add(vp.Key, (vp.Value / cd.getNormalizeFactorDocument()[kvp.Key]));
            }
        }
        public void NormalizeTFFIDFQueryperTerm(CollectionDocument cq)
        {
            cq.TFIDFperQueryNormalized = new Dictionary<string, double>[cq.getNTuple() + 1];
            cq.TFIDFperQueryNormalized[0] = null;
            //for (int i = 1; i <= cq.getNTuple(); i++)
            foreach(KeyValuePair<int, string[]> kvp in cq.getTermQueryID())
            {
                cq.TFIDFperQueryNormalized[kvp.Key] = new Dictionary<string, double>();
                foreach (KeyValuePair<string, double> mvp in cq.getTFIDFperQuery()[kvp.Key])
                    cq.TFIDFperQueryNormalized[kvp.Key].Add(mvp.Key, (mvp.Value / cq.getNormalizeFactorQuery()[kvp.Key]));
            }
        }
        public void WriteTFIDFperDocumentNormalized(CollectionDocument cd)
        {
            using (StreamWriter sw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\TFIDFperDocumentNormalized.txt"))
            {
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID())
                {
                    sw.WriteLine("Nomor Dokumen = {0}", kvp.Key);
                    foreach (KeyValuePair<string, double> vp in cd.TFIDFperDocumentNormalized[kvp.Key])
                        sw.WriteLine("[TFIDFNormalized term {0} = {1}]", vp.Key, vp.Value);
                }
            }
        }
        public void WriteTFIDFperQueryNormalized(CollectionDocument cq)
        {
            using (StreamWriter sw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TFIDFperQueryNormalized.txt"))
            {
                foreach (KeyValuePair<int, string[]> kvp in cq.getTermQueryID())
                {
                    sw.WriteLine("Nomor Query = {0}", kvp.Key);
                    foreach (KeyValuePair<string, double> vp in cq.TFIDFperQueryNormalized[kvp.Key])
                        sw.WriteLine("[TFIDFNormalized term {0} = {1}]", vp.Key, vp.Value);
                }
            }
        }
    }
}
