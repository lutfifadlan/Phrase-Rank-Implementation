﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (int i in cq.getDocFound())
            {
                foreach (string termQuery in cq.getTermQueryID()[noQuery])
                {
                    if (cd.getTFIDFperDocument()[i].ContainsKey(termQuery))
                    {

                        listTFIDFDoc.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        if (!cq.getDocRelFound().Contains(i))
                            listTFIDFDocNonRel.Add(cd.getTFIDFperDocumentNormalized()[i][termQuery]);
                        else if (cq.getDocRelFound().Contains(i))
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
                if (cq.getDocRelFound().Contains(i))
                {
                    for (int j = 0; j < cq.getListTFIDFQ().Count; j++)
                        TFIDFQDoc += (cq.getListTFIDFQ()[j] * listTFIDFDocRel[j]);
                    cq.getDocumentRelevantScore().Add(i, TFIDFQDoc);
                }
                else if (!cq.getDocRelFound().Contains(i))
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
            cq.setRankedDocumentScore((from pair in cq.getDocumentScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                cq.getRankedDocFound().Add(kvp.Key);
            cq.setRankedDocumentRelevantScore((from pair in cq.getDocumentRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentRelevantScore())
                cq.getRankedDocRelFound().Add(kvp.Key);
            cq.setRankedDocumentNonRelevantScore((from pair in cq.getDocumentNonRelevantScore() orderby pair.Value descending select pair).ToList());
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentNonRelevantScore())
                cq.getRankedDocNonRelFound().Add(kvp.Key);
        }
        public void PrintAllRetrievalResult(CollectionDocument cq)
        {
            foreach (KeyValuePair<int, double> kvp in cq.getRankedDocumentScore())
                Console.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);
        }
        public void PrintRankedRelevantRetrievalResult(CollectionDocument cq)
        {
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
