using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class InvertedFileIndex
    {
        public void MakeInvertedFileIndexDocument(CollectionDocument cd)
        {
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\InvertedFileIndexDocument.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
                {
                    List<int> tempList = new List<int>();
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        if (!tempList.Contains(kvp.Value[j]))
                            tempList.Add(kvp.Value[j]);
                    }
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        stw.Write("{0} | {1} | ", kvp.Key, tempList[j]);
                        if (cd.getTFIDFperDocument()[tempList[j]].ContainsKey(kvp.Key))
                            stw.WriteLine("{0}", cd.getTFIDFperDocument()[tempList[j]][kvp.Key]);
                        else
                            stw.WriteLine("{0}", 0);
                    }
                }
            }
        }
        public void MakeInvertedFileIndexQuery(CollectionDocument cq)
        {
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\InvertedFileIndexQuery.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cq.getStructuredQueryIndex())
                {
                    List<int> tempList = new List<int>();
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        if (!tempList.Contains(kvp.Value[j]))
                            tempList.Add(kvp.Value[j]);
                    }
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        stw.Write("{0} | {1} | ", kvp.Key, tempList[j]);
                        if (cq.getTFIDFperQuery()[tempList[j]].ContainsKey(kvp.Key))
                            stw.WriteLine("{0}", cq.getTFIDFperQuery()[tempList[j]][kvp.Key]);
                        else
                            stw.WriteLine("{0}", 0);
                    }
                }
            }
        }
        public void MakeInvertedFileIndexDocumentNormalized(CollectionDocument cd)
        {
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\InvertedFileIndexDocumentNormalized.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredDocIndex())
                {
                    List<int> tempList = new List<int>();
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        if (!tempList.Contains(kvp.Value[j]))
                            tempList.Add(kvp.Value[j]);
                    }
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        stw.Write("{0} | {1} | ", kvp.Key, tempList[j]);
                        if (cd.getTFIDFperDocumentNormalized()[tempList[j]].ContainsKey(kvp.Key))
                            stw.WriteLine("{0}", cd.getTFIDFperDocumentNormalized()[tempList[j]][kvp.Key]);
                        else
                            stw.WriteLine("{0}", 0);
                    }
                }
            }
        }
        public void MakeInvertedFileIndexQueryNormalized(CollectionDocument cd)
        {
            using (StreamWriter stw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\InvertedFileIndexQueryNormalized.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getStructuredQueryIndex())
                {
                    List<int> tempList = new List<int>();
                    for (int j = 0; j < kvp.Value.Count; j++)
                    {
                        if (!tempList.Contains(kvp.Value[j]))
                            tempList.Add(kvp.Value[j]);
                    }
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        stw.Write("{0} | {1} | ", kvp.Key, tempList[j]);
                        if (cd.getTFIDFperQueryNormalized()[tempList[j]].ContainsKey(kvp.Key))
                            stw.WriteLine("{0}", cd.getTFIDFperQueryNormalized()[tempList[j]][kvp.Key]);
                        else
                            stw.WriteLine("{0}", 0);
                    }
                }
            }
        }
    }
}
