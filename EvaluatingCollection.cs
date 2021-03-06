﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InformationRetrieval
{
    class EvaluatingCollection
    {
        public void Evaluation(CollectionDocument cq, int noQuery)
        {
            //Console.WriteLine(noQuery);
            cq.getRecallDict().Clear();
            cq.getPrecisionDict().Clear();
            cq.getRecallPrecision().Clear();
            cq.getDocRelevantPrecision().Clear();
            cq.getInterpolatedPrecision().Clear();
            double recall = 0;
            double precision = 0;
            int nDocRelFound = 0;
            int nDoc = 0; // jumlah dokumen yang ditemukan
            foreach (int i in cq.getDictRankedDocFound()[noQuery])//cq.getRankedDocFound())
            {
                nDoc++;
                foreach (int j in cq.getListNoQueryDocRelFound()[noQuery])//cq.getDocRelFound())
                    if (i == j)
                        nDocRelFound++;
                if (nDocRelFound == 0) //|| nDoc == 0)
                {
                    recall = 0;
                    precision = 0;
                }
                else if (cq.getListNoQueryDocRelFound()[noQuery].Count == 0)//cq.getDocRelFound().Count == 0)
                    recall = 0;
                else
                {
                    recall = (double)nDocRelFound / (double)cq.getListNoQueryDocRelFound()[noQuery].Count;//cq.getDocRelFound().Count;
                    precision = (double)nDocRelFound / (double)nDoc;
                    // Console.WriteLine("recall = {0}", recall);
                    //Console.WriteLine("precision = {0}", precision);
                }
                cq.getRecallDict().Add(i, recall);
                cq.getPrecisionDict().Add(i, precision);
                if (!cq.getRecallPrecision().ContainsKey(recall))
                    cq.getRecallPrecision().Add(recall, new List<double>());
                cq.getRecallPrecision()[recall].Add(precision);
            }
            cq.setRecallRetrieval(recall);
            cq.setPrecisionRetrieval(precision);
            foreach (int j in cq.getListNoQueryDocRelFound()[noQuery])//cq.getDocRelFound())
                cq.getDocRelevantPrecision().Add(j, cq.getPrecisionDict()[j]);

            foreach (KeyValuePair<int, double> kvp in cq.getRecallDict())
            {
                double tempRecall = kvp.Value;
                double tempPrecision = cq.getPrecisionDict()[kvp.Key];
                foreach (KeyValuePair<double, List<double>> lrp in cq.getRecallPrecision())
                {
                    foreach (double p in cq.getRecallPrecision()[lrp.Key])
                    {
                        if (lrp.Key > tempRecall)
                        {
                            if (p > tempPrecision)
                            {
                                if (!cq.getInterpolatedPrecision().ContainsKey(kvp.Key))
                                    cq.getInterpolatedPrecision().Add(kvp.Key, p);
                                break;
                            }
                        }
                    }
                }
                if (!cq.getInterpolatedPrecision().ContainsKey(kvp.Key))
                    cq.getInterpolatedPrecision().Add(kvp.Key, tempPrecision);
            }
            if (cq.getInterpolatedPrecision().Count == 0)
                cq.setInterpolatedAveragePrecision(0);
            else
                cq.setInterpolatedAveragePrecision(cq.getInterpolatedPrecision().Sum(x => x.Value) / cq.getInterpolatedPrecision().Count);
            if (cq.getListNoQueryDocRelFound()[noQuery].Count()==0)//cq.getDocRelFound().Count == 0)
                cq.setNonInterpolatedAveragePrecision(0);
            else
                cq.setNonInterpolatedAveragePrecision(cq.getDocRelevantPrecision().Sum(x => x.Value) / cq.getListNoQueryDocRelFound()[noQuery].Count());//cq.getDocRelFound().Count);
        }
        public void PrintEvaluation(CollectionDocument cq, int i)
        {
            /*
            Console.WriteLine("Recall");
            foreach(KeyValuePair<int, double> kvp in cq.getRecallDict())
                Console.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);
            Console.WriteLine("Precision");
            foreach (KeyValuePair<int, double> kvp in cq.getPrecisionDict())
                Console.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);*/
            Console.WriteLine("Query ke-{0}", i);
            Console.WriteLine("Recall Retrieval = {0}", cq.getRecallRetrieval());
            Console.WriteLine("Precision Retrieval = {0}", cq.getPrecisionRetrieval());
            /*
            Console.WriteLine("Interpolated Average Precision");
            foreach(KeyValuePair<int, double> kvp in cq.getInterpolatedPrecision())
                Console.WriteLine("[{0} | {1}]", kvp.Key, kvp.Value);*/
            Console.WriteLine("Non Interpolated Precision = {0}", cq.getNonInterpolatedAveragePrecision());
        }
        public void WriteStandardEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\Standard Evaluation.txt"))
            {
                stw.WriteLine(rm);
                stw.WriteLine("Average Recall Query Collection = {0}", rc);
                stw.WriteLine("Average Precision Query Collection = {0}", pr);
                stw.WriteLine("Average Interpolated Average Precision Query Collection = {0}", iap);
                stw.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", niap);
            }
        }
        public void WritePseudoRelEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PRF Evaluation.txt"))
            {
                stw.WriteLine(rm);
                stw.WriteLine("Average Recall Query Collection = {0}", rc);
                stw.WriteLine("Average Precision Query Collection = {0}", pr);
                stw.WriteLine("Average Interpolated Average Precision Query Collection = {0}", iap);
                stw.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", niap);
            }
        }
        public void WritePhRankEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PhRank ZF Evaluation.txt"))
            {
                stw.WriteLine(rm);
                stw.WriteLine("Average Recall Query Collection = {0}", rc);
                stw.WriteLine("Average Precision Query Collection = {0}", pr);
                stw.WriteLine("Average Interpolated Average Precision Query Collection = {0}", iap);
                stw.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", niap);
            }
        }
    }
}
