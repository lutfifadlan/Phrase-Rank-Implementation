using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace InformationRetrieval
{
    class RelevanceFeedback
    {
        public void FeedbackRelevantDocument(CollectionDocument cq, int i)
        {
            Console.WriteLine("Dokumen yang telah ditemukan pada query {0}", i);
            foreach (int j in cq.getListNoQueryDocFound()[i])
                Console.WriteLine(j);
            List<int> inputNoDocRel = new List<int>();
            List<int> inputNoDocNonRel = new List<int>();
            int stopSign = 0;
            int input;
            Console.WriteLine("Masukkan dokumen yang relevan");
            while ((input = Int32.Parse(Console.ReadLine())) != stopSign)
            {
             //   inputNoDocRel.Add(input);
                if(!cq.getDocRelFound().Contains(input))
                    cq.getDocRelFound().Add(input);
            }
            //cq.setDocRelFound(inputNoDocRel);
            foreach (int j in cq.getListNoQueryDocFound()[i])
                if (!inputNoDocRel.Contains(j))
                    inputNoDocNonRel.Add(j);
            cq.setDocNonRelFound(inputNoDocNonRel);
        }
        public void RochioQuery(CollectionDocument cq, int noQuery)
        {
            // Metode umpan balik
            // Q1 = alfa*Q0 + beta(sigma vektor dokumen relevan(rk)/jumlah dokumen relevan(n1)) - gamma(sigma vektor dokumen non relevan(sk) / jumlah dokumen non relevan(n2))
            // more judged documents, higher beta and gamma
            // most IR systems set gamma > beta
            // reasonable value alfa = 1, beta = 0.75, gamma = 0.15
            //double qNol = cq.getWeightQuery().Sum(x => x.Value);
            cq.TFIDFperQueryNormalizedRochio[noQuery] = new Dictionary<string, double>();
            foreach (string q in cq.getTermQueryID()[noQuery])
            {
                double t0 = cq.getTFIDFperQueryNormalized()[noQuery][q];
                double rk = cq.getSigmaDocumentRelevantScorePerQuery()[noQuery]; //cq.getDocumentRelevantScore().Sum(x => x.Value);
                int n1 = cq.getListNoQueryDocRelFound()[noQuery].Count;//cq.getDocRelFound().Count;
                double sk = cq.getSigmaDocumentNonRelevantScorePerQuery()[noQuery];//cq.getDocumentNonRelevantScore().Sum(x => x.Value);
                int n2 = cq.getListNoQueryDocNonRelFound()[noQuery].Count;//cq.getDocNonRelFound().Count;
                double beta = 0.75;
                double gamma = 0.15;
                double t1 = t0 + (beta * (rk / n1)) - (gamma * (sk / n2));
                if(!cq.TFIDFperQueryNormalizedRochio[noQuery].ContainsKey(q))
                    cq.TFIDFperQueryNormalizedRochio[noQuery].Add(q, t1);
            }
            cq.getSigmaDocumentRelevantScorePerQuery().Remove(noQuery);
            cq.getSigmaDocumentNonRelevantScorePerQuery().Remove(noQuery);
            /*
            double qNol = cq.getWeightQueryDict()[noQuery];
            double rk = cq.getDocumentRelevantScore().Sum(x => x.Value);
            int n1 = cq.getListNoQueryDocRelFound()[noQuery].Count;//cq.getDocRelFound().Count;
            double sk = cq.getDocumentNonRelevantScore().Sum(x => x.Value);
            int n2 = cq.getListNoQueryDocNonRelFound()[noQuery].Count;//cq.getDocNonRelFound().Count;
            double beta = 0.1;
            double gamma = 0.2;
            double qOne = qNol + (beta * (rk / n1)) - (gamma * (sk / n2));
            //cq.setWeightListQuery(qOne);*/
            //cq.getWeightQueryDict().Remove(noQuery);
            //cq.getWeightQueryDict().Add(noQuery, qOne);
        }
        public void WriteTFIDFperQueryNormalizedRochio(CollectionDocument cq, int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TFIDFperQueryNormalizedRochio.txt"))
            {
                foreach (KeyValuePair<string, double> vp in cq.TFIDFperQueryNormalizedRochio[noQuery])
                    sw.WriteLine("TFIDFNormalizedRochio term {0} = {1}", vp.Key, vp.Value);
            }
        }
        public void PseudoRelevantDocument(CollectionDocument cq, int noQuery, int maxK)
        {
            int k = 0;
            List<int> pseudoRelDoc = new List<int>();
            if (cq.getDictRankedDocFound()[noQuery].Count == 0) { }//cq.getListNoQueryDocFound()[noQuery].Count == 0) { }//getRankedDocFound().Count == 0) { }
            else
            {
                while (k < maxK)
                {
                    //if (cq.getDictRankedDocFound()[noQuery].Count == k)//cq.getListNoQueryDocFound()[noQuery].Count == k)//cq.getRankedDocFound().Count == k)
                    //    break;
                    if (!pseudoRelDoc.Contains(cq.getDictRankedDocFound()[noQuery][k]))
                        pseudoRelDoc.Add(cq.getDictRankedDocFound()[noQuery][k]);//cq.getRankedDocFound()[k]);
                    k++;
                }
                //cq.setDocRelFound(pseudoRelDoc);
                cq.getListNoQueryDocRelFound().Remove(noQuery);
                if (!cq.getListNoQueryDocRelFound().ContainsKey(noQuery))
                    cq.getListNoQueryDocRelFound().Add(noQuery, new List<int>());
                foreach (int i in pseudoRelDoc)
                    cq.getListNoQueryDocRelFound()[noQuery].Add(i);
            }
        }
        public void WritePseudoRelevantDocument(CollectionDocument cq)
        {
            using (StreamWriter sw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Pseudo Relevant Document.txt"))
            {
                foreach (KeyValuePair<int, List<int>> kvp in cq.getListNoQueryDocRelFound())
                {
                    sw.WriteLine("Nomor Query = {0}", kvp.Key);
                    sw.WriteLine("Pseudo Relevant Document :");
                    for(int i = 0; i < kvp.Value.Count; i++)
                    {
                        sw.Write("{0}", kvp.Value[i]);
                        if(i < kvp.Value.Count - 1)
                            sw.Write(", ");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
