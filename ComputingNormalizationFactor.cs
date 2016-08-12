using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class ComputingNormalizationFactor
    {
        public void ComputeNormalizeFactorDocument(CollectionDocument cd)
        {
            double sigmaPowTFIDF;
            double normalizeFactor = 0;
            //for (int i = 1; i <= cd.getNTuple(); i++)
            foreach(KeyValuePair<int, string>kvp in cd.getWordDictStemmed())
            {
                sigmaPowTFIDF = 0;
                foreach (KeyValuePair<string, double> vp in cd.getTFIDFperDocument()[kvp.Key])
                    sigmaPowTFIDF += Math.Pow(vp.Value, 2);
                normalizeFactor = Math.Sqrt(sigmaPowTFIDF);
                cd.getNormalizeFactorDocument().Add(kvp.Key, normalizeFactor);
            }
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\NormalizeFactorDocument.txt"))
            {
                foreach (KeyValuePair<int, double> kvp in cd.getNormalizeFactorDocument())
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }
        }
        public void ComputeNormalizeFactorQuery(CollectionDocument cq)
        {
            double sigmaPowTFIDF;
            double normalizeFactor = 0;
            foreach(KeyValuePair<int, string[]>kvp in cq.getTermQueryID())
            //for (int i = 1; i <= cq.getNTuple(); i++)
            {
                sigmaPowTFIDF = 0;
                foreach (KeyValuePair<string, double> mvp in cq.getTFIDFperQuery()[kvp.Key])
                    sigmaPowTFIDF += Math.Pow(mvp.Value, 2);
                normalizeFactor = Math.Sqrt(sigmaPowTFIDF);
                cq.getNormalizeFactorQuery().Add(kvp.Key, normalizeFactor);
            }
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\NormalizeFactorQuery.txt"))
            {
                foreach (KeyValuePair<int, double> kvp in cq.getNormalizeFactorQuery())
                    stw.WriteLine("[{0} {1}]", kvp.Key, kvp.Value);
            }
        }
    }
}
