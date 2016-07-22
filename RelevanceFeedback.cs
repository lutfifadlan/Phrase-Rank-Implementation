using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void RochioQuery(CollectionDocument cq)
        {
            // Metode umpan balik
            // Q1 = alfa*Q0 + beta(sigma vektor dokumen relevan(rk)/jumlah dokumen relevan(n1)) - gamma(sigma vektor dokumen non relevan(sk) / jumlah dokumen non relevan(n2))
            // more judged documents, higher beta and gamma
            // most IR systems set gamma > beta
            // reasonable value alfa = 1, beta = 0.75, gamma = 0.15
            double qNol = cq.getWeightQuery().Sum(x => x.Value);
            double rk = cq.getDocumentRelevantScore().Sum(x => x.Value);
            int n1 = cq.getDocRelFound().Count;
            double sk = cq.getDocumentNonRelevantScore().Sum(x => x.Value);
            int n2 = cq.getDocNonRelFound().Count;
            double beta = 0.1;
            double gamma = 0.2;
            double qOne = qNol + (beta * (rk / n1)) - (gamma * (sk / n2));
            cq.setWeightListQuery(qOne);
        }
        public void PseudoRelevantDocument(CollectionDocument cq)
        {
            int k = 0;
            List<int> pseudoRelDoc = new List<int>();
            if (cq.getRankedDocFound().Count == 0) { }
            else
            {
                while (k < 20)
                {
                    if (cq.getRankedDocFound().Count == k)
                        break;
                    if (!pseudoRelDoc.Contains(cq.getRankedDocFound()[k]))
                        pseudoRelDoc.Add(cq.getRankedDocFound()[k]);
                    k++;
                }
                cq.setDocRelFound(pseudoRelDoc);
            }
        }
    }
}
