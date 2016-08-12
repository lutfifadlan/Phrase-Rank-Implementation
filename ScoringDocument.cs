using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InformationRetrieval
{
    class ScoringDocument
    {
        public void ScoringAllDocumentRetrieved(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cq.getDocumentScore().Clear();
            cq.getDocumentRelevantScore().Clear();
            cq.getDocumentNonRelevantScore().Clear();
            cq.getRankedDocumentScore().Clear();
            cq.getRankedDocumentRelevantScore().Clear();
            cq.getRankedDocumentNonRelevantScore().Clear();
            cq.getRankedDocFound().Clear();
            cq.getRankedDocRelFound().Clear();
            cq.getRankedDocNonRelFound().Clear();
            double TFIDFQDoc = 0;
            List<double> listTFIDFDoc = new List<double>();
            List<double> listTFIDFDocRel = new List<double>();
            List<double> listTFIDFDocNonRel = new List<double>();
            foreach (int i in cq.getListNoQueryDocFound()[noQuery])//cq.getDocFound())
            {
                foreach (string termQuery in cq.getTermQueryID()[noQuery])
                {
                    if (cd.getTFIDFperDocumentNormalized()[i].ContainsKey(termQuery))
                    {
                        listTFIDFDoc.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        if (!cq.getListNoQueryDocRelFound()[noQuery].Contains(i))//(!cq.getDocRelFound().Contains(i))
                            listTFIDFDocNonRel.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        else if(cq.getListNoQueryDocRelFound()[noQuery].Contains(i)) //(cq.getDocRelFound().Contains(i))
                            listTFIDFDocRel.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                    }
                    else
                    {
                        listTFIDFDoc.Add(0);
                        listTFIDFDocNonRel.Add(0);
                        listTFIDFDocRel.Add(0);
                    }
                }
                for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                {
                    //Console.WriteLine("cq.getListTFIDFQ()[j] = {0}", cq.getDictListTFIDFQ()[noQuery][j]);//cq.getListTFIDFQ()[j]);
                    //Console.WriteLine("listTFIDFDoc[j] = {0}", listTFIDFDoc[j]);
                    TFIDFQDoc += (cq.getDictListTFIDFQ()[noQuery][j] * listTFIDFDoc[j]);
                }
                //Console.WriteLine("TFIDFQDoc = {0}", TFIDFQDoc);
                cq.getDocumentScore().Add(i, TFIDFQDoc);
                TFIDFQDoc = 0;
                if (cq.getListNoQueryDocRelFound()[noQuery].Contains(i))//(cq.getDocRelFound().Contains(i))
                {
                    for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                        TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDocRel[j]);
                    cq.getDocumentRelevantScore().Add(i, TFIDFQDoc);
                }
                else if (!cq.getListNoQueryDocRelFound()[noQuery].Contains(i)) //(!cq.getDocRelFound().Contains(i))
                {
                    for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                        TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDocNonRel[j]);
                    cq.getDocumentNonRelevantScore().Add(i, TFIDFQDoc);
                }
                TFIDFQDoc = 0;
                listTFIDFDoc = new List<double>();
                listTFIDFDocRel = new List<double>();
                listTFIDFDocNonRel = new List<double>();
            }
            cq.getSigmaDocumentRelevantScorePerQuery().Add(noQuery, cq.getDocumentRelevantScore().Sum(x=>x.Value));
            cq.getSigmaDocumentNonRelevantScorePerQuery().Add(noQuery, cq.getDocumentNonRelevantScore().Sum(x => x.Value));
            cq.setRankedDocumentScore((from pair in cq.getDocumentScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                cq.getRankedDocFound().Add(kvp.Key);
            //cq.getDictRankedDocFound().Add(noQuery, cq.getRankedDocFound());
            if (!cq.getDictRankedDocFound().ContainsKey(noQuery))
                cq.getDictRankedDocFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocFound())
                cq.getDictRankedDocFound()[noQuery].Add(i);
            cq.setRankedDocumentRelevantScore((from pair in cq.getDocumentRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentRelevantScore())
                cq.getRankedDocRelFound().Add(kvp.Key);
            //cq.getDictRankedDocRelFound().Add(noQuery, cq.getRankedDocRelFound());
            if (!cq.getDictRankedDocRelFound().ContainsKey(noQuery))
                cq.getDictRankedDocRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocRelFound())
                cq.getDictRankedDocRelFound()[noQuery].Add(i);
            cq.setRankedDocumentNonRelevantScore((from pair in cq.getDocumentNonRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentNonRelevantScore())
                cq.getRankedDocNonRelFound().Add(kvp.Key);
            //cq.getDictRankedDocNonRelFound().Add(noQuery, cq.getRankedDocNonRelFound());
            if (!cq.getDictRankedDocNonRelFound().ContainsKey(noQuery))
                cq.getDictRankedDocNonRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocNonRelFound())
                cq.getDictRankedDocNonRelFound()[noQuery].Add(i);
        }
        public void ScoringAllDocumentAfterPhRank(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cq.getDocumentScore().Clear();
            cq.getDocumentRelevantScore().Clear();
            cq.getDocumentNonRelevantScore().Clear();
            cq.getRankedDocumentScore().Clear();
            cq.getRankedDocumentRelevantScore().Clear();
            cq.getRankedDocumentNonRelevantScore().Clear();
            cq.getRankedDocFound().Clear();
            cq.getRankedDocRelFound().Clear();
            cq.getRankedDocNonRelFound().Clear();
            double TFIDFQDoc = 0;
            List<double> listTFIDFDoc = new List<double>();
            List<double> listTFIDFDocRel = new List<double>();
            List<double> listTFIDFDocNonRel = new List<double>();
            foreach (int i in cq.getListNoQueryDocFound()[noQuery])//cq.getDocFound())
            {
                foreach (string termQuery in cq.getTermQueryReformulatedID()[noQuery])//cq.getTermQueryID()[noQuery])
                {
                    if (cd.getTFIDFperDocument()[i].ContainsKey(termQuery))
                    {
                        listTFIDFDoc.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        if (!cq.getListNoQueryDocRelFound()[noQuery].Contains(i))//(!cq.getDocRelFound().Contains(i))
                            listTFIDFDocNonRel.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        else if (cq.getListNoQueryDocRelFound()[noQuery].Contains(i)) //(cq.getDocRelFound().Contains(i))
                            listTFIDFDocRel.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                    }
                    else
                    {
                        listTFIDFDoc.Add(0);
                        listTFIDFDocNonRel.Add(0);
                        listTFIDFDocRel.Add(0);
                    }
                }
                for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                    TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDoc[j]);
                cq.getDocumentScore().Add(i, TFIDFQDoc);
                TFIDFQDoc = 0;
                if (cq.getListNoQueryDocRelFound()[noQuery].Contains(i))//(cq.getDocRelFound().Contains(i))
                {
                    for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                        TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDocRel[j]);
                    cq.getDocumentRelevantScore().Add(i, TFIDFQDoc);
                }
                else if (!cq.getListNoQueryDocRelFound()[noQuery].Contains(i)) //(!cq.getDocRelFound().Contains(i))
                {
                    for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                        TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDocNonRel[j]);
                    cq.getDocumentNonRelevantScore().Add(i, TFIDFQDoc);
                }
                TFIDFQDoc = 0;
                listTFIDFDoc = new List<double>();
                listTFIDFDocRel = new List<double>();
                listTFIDFDocNonRel = new List<double>();
            }
            cq.getSigmaDocumentRelevantScorePerQuery().Add(noQuery, cq.getDocumentRelevantScore().Sum(x => x.Value));
            cq.getSigmaDocumentNonRelevantScorePerQuery().Add(noQuery, cq.getDocumentNonRelevantScore().Sum(x => x.Value));
            cq.setRankedDocumentScore((from pair in cq.getDocumentScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                cq.getRankedDocFound().Add(kvp.Key);
            //cq.getDictRankedDocFound().Add(noQuery, cq.getRankedDocFound());
            if (!cq.getDictRankedDocFound().ContainsKey(noQuery))
                cq.getDictRankedDocFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocFound())
                cq.getDictRankedDocFound()[noQuery].Add(i);
            cq.setRankedDocumentRelevantScore((from pair in cq.getDocumentRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentRelevantScore())
                cq.getRankedDocRelFound().Add(kvp.Key);
            //cq.getDictRankedDocRelFound().Add(noQuery, cq.getRankedDocRelFound());
            if (!cq.getDictRankedDocRelFound().ContainsKey(noQuery))
                cq.getDictRankedDocRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocRelFound())
                cq.getDictRankedDocRelFound()[noQuery].Add(i);
            cq.setRankedDocumentNonRelevantScore((from pair in cq.getDocumentNonRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentNonRelevantScore())
                cq.getRankedDocNonRelFound().Add(kvp.Key);
            //cq.getDictRankedDocNonRelFound().Add(noQuery, cq.getRankedDocNonRelFound());
            if (!cq.getDictRankedDocNonRelFound().ContainsKey(noQuery))
                cq.getDictRankedDocNonRelFound().Add(noQuery, new List<int>());
            foreach (int i in cq.getRankedDocNonRelFound())
                cq.getDictRankedDocNonRelFound()[noQuery].Add(i);
        }
        public void PrintAllRetrievalResult(CollectionDocument cq, int noQuery)
        {
            Console.WriteLine("noQuery = {0}", noQuery);
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                Console.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);
        }
        public void WriteAllRetrievalResult(CollectionDocument cq, int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\All Retrieval Result.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                    sw.WriteLine("[Nomor Dokumen = {0} | Score = {1}]", kvp.Key, kvp.Value);
            }
        }
        public void WriteAllRetrievalPseudoResult(CollectionDocument cq, int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Pseudo Relevant Retrieval Result.txt"))//new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\Pseudo Relevant Retrieval Result.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                    sw.WriteLine("[Nomor Dokumen = {0} | Score = {1}]", kvp.Key, kvp.Value);
            }
        }
        public void PrintRankedRelevantRetrievalResult(CollectionDocument cq, int noQuery)
        {
            Console.WriteLine("noQuery = {0}", noQuery);
            Console.Write("[");
            for (int i = 0; i < cq.getRankedDocumentRelevantScore().Count; i++)
            {
                Console.Write("{0}", cq.getRankedDocRelFound()[i]);
                if (i < (cq.getRankedDocumentRelevantScore().Count - 1))
                    Console.Write(",");
            }
            Console.WriteLine("]");
        }
    }
}
