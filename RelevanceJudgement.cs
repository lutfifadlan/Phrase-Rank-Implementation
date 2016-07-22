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
        public void InputRelevanceJudgement(CollectionDocument cq)
        {
            //string relevanceJudgment = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\qrels.text");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\qrels.text");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\QRELSAAH");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\qrels.text");
            string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\QRELSADE");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\QRELSABT");
            //string[] relevanceJudgment = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\QRELSACA");
            string[] rlj;
            List<string[]> listRlj = new List<string[]>();
            foreach (string s in relevanceJudgment)
            {
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
            //Console.WriteLine("listRlj.Count = {0}", listRlj.Count);
            
            //string[] rlv = relevanceJudgment.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            //string[] rlvArray = rlv.Where(str => str != "0.000000" && str != "0").ToArray();
            int j = 1;
            int r = 0;
            for(int i=0; i < listRlj.Count; i++)
            {
                if (!cq.getRld().ContainsKey(Int32.Parse(listRlj[i][0])))
                    cq.getRld().Add(Int32.Parse(listRlj[i][0]), new List<int>());
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
        public void PrintRld(CollectionDocument cd)
        {
            foreach(KeyValuePair<int, List<int>>kvp in cd.getRld())
            {
                Console.WriteLine("Query = {0}", kvp.Key);
                Console.WriteLine("Dokumen Relevan:");
                foreach (int i in kvp.Value)
                    Console.Write("{0} ", i);
                Console.WriteLine();
            }
        }
    }
}
