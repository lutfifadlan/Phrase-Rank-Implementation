using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class SaveIndexingToFile
    {
        public void SaveDocumentIndexingToFile(CollectionDocument cd)
        {
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\DocNumberWithTermDoc.txt"))
            {
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID())
                    foreach (string s in kvp.Value)
                        stw.WriteLine("[{0} {1}]", kvp.Key, s);
            }
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\TermDocWithDocNumber.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getWordDocNumber())
                    foreach (int s in kvp.Value)
                        stw.WriteLine("[{0} {1}]", kvp.Key, s);
            }
            /*
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Document\UniqueTermDocument.txt"))
            {
                int index = 1;
                foreach (string s in cd.getUniqueTerm())
                {
                    stw.WriteLine("[{0} {1}]", index, s);
                    index++;
                }
            }*/
        }
        public void SaveQueryIndexingToFile(CollectionDocument cd)
        {
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\QueryNumberWithTermQuery.txt"))
            { 
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermQueryID())
                    foreach (string s in kvp.Value)
                        stw.WriteLine("[{0} {1}]", kvp.Key, s);
            }
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TermQueryWithDocNumber.txt"))
            {
                foreach (KeyValuePair<string, List<int>> kvp in cd.getQueryDocNumber())
                    foreach (int s in kvp.Value)
                        stw.WriteLine("[{0} {1}]", kvp.Key, s);
            }
            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\UniqueTermQuery.txt"))
            {
                int index = 1;
                foreach (string s in cd.getUniqueQueryTerm())
                {
                    stw.WriteLine("[{0} {1}]", index, s);
                    index++;
                }
            }
        }
    }
}
