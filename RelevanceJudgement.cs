using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class RelevanceJudgement
    {
        public void InputRelevanceJudgement(CollectionDocument cq, string fileName)
        {
            string[] relevanceJudgement = File.ReadAllLines(fileName);
            //string relevanceJudgment = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\qrels.text");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\qrels.text");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\QRELSAAH");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\qrels.text");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\QRELSADE");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\QRELSABT");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\QRELSACA");
            string[] rlj;
            List<string[]> listRlj = new List<string[]>();

            if (fileName == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempo.qrel")
            {
                foreach (string s in relevanceJudgement)
                {
                    rlj = new string[2];
                    rlj = s.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    //Console.WriteLine(rlj[0]);
                    //Console.WriteLine(rlj[1]);
                    string[] rld = new string[2];
                    rld[0] = rlj[0];
                    foreach (KeyValuePair<int, string> kvp in cq.getNoDocTempo())
                    {
                        if (kvp.Value == rlj[1])
                        {
                            rld[1] = kvp.Key.ToString();
                            //Console.WriteLine("rld[1] = {0}", rld[1]);
                            break;
                        }
                    }
                    //rld[1] = rlj[1];
                    //Console.WriteLine(rld[0]);
                    //Console.WriteLine(rld[1]);
                    listRlj.Add(rld);
                }
            }
            else
            {
                foreach (string s in relevanceJudgement)
                {
                    rlj = new string[2];
                    rlj = s.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    //Console.WriteLine(rlj[0]);
                    //Console.WriteLine(rlj[1]);
                    string[] rld = new string[2];
                    rld[0] = rlj[0];
                    rld[1] = rlj[1];
                    //Console.WriteLine(rld[0]);
                    //Console.WriteLine(rld[1]);
                    listRlj.Add(rld);
                }
            }
            //Console.WriteLine("listRlj.Count = {0}", listRlj.Count);
            
            //string[] rlv = relevanceJudgment.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            //string[] rlvArray = rlv.Where(str => str != "0.000000" && str != "0").ToArray();
            //int j = 1;
            //int r = 0;
            for(int i=0; i < listRlj.Count; i++)
            {
                if (!cq.getRld().ContainsKey(Int32.Parse(listRlj[i][0])))
                    cq.getRld().Add(Int32.Parse(listRlj[i][0]), new List<int>());
                //Console.WriteLine("Int32.Parse(listRlj[i][0]) = {0}", Int32.Parse(listRlj[i][0]));
                //Console.WriteLine("Int32.Parse(listRlj[i][1]) = {0}", Int32.Parse(listRlj[i][1]));
                cq.getRld()[Int32.Parse(listRlj[i][0])].Add(Int32.Parse(listRlj[i][1]));
            }
            //PrintRld(cq);
            /*
            int ij = 1;
            int ir = 0;
            while (ir < rlvArray.Length)
            {
                if (!cq.getRld().ContainsKey(Int32.Parse(rlvArray[ir])))
                    cq.getRld().Add(Int32.Parse(rlvArray[ir]), new List<int>());
                cq.getRld()[Int32.Parse(rlvArray[ir])].Add(Int32.Parse(rlvArray[ij]));
                ij = ij + 2;
                ir = ir + 2;
            }*/
        }
        public void WriteRld(CollectionDocument cq)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\relevenceJudgement.txt"))
            {
                foreach (KeyValuePair<int, List<int>> kvp in cq.getRld())
                {
                    sw.WriteLine("Query = {0}", kvp.Key);
                    sw.WriteLine("Dokumen Relevan:");
                    foreach (int i in kvp.Value)
                        sw.Write("{0} ", i);
                    sw.WriteLine();
                }
            }
        }
    }
}
