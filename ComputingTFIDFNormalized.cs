using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class ComputingTFIDFNormalized
    {
        public void NormalizeTFFIDFDocumentperTerm(CollectionDocument cd)
        {
            cd.TFIDFperDocumentNormalized = new Dictionary<string, double>[cd.getNTuple() + 1];
            cd.TFIDFperDocumentNormalized[0] = null;
            for (int i = 1; i <= cd.getNTuple(); i++)
            {
                cd.TFIDFperDocumentNormalized[i] = new Dictionary<string, double>();
                foreach (KeyValuePair<string, double> kvp in cd.getTFIDFperDocument()[i])
                    cd.TFIDFperDocumentNormalized[i].Add(kvp.Key, (kvp.Value / cd.getNormalizeFactorDocument()[i]));
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
    }
}
