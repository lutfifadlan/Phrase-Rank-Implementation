using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationRetrieval
{
    class SaveCollection
    {
        public void SaveDocumentCollection(CollectionDocument cd)
        {
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\adi-new.all");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\CACM.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\cisi.all");
            string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\CRAN.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\MED.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\NPL.ALL");
            string content = "\n.I" + contents;
            string[] contentDoc = content.Split(new string[] { "\n." }, StringSplitOptions.RemoveEmptyEntries);
            int nDoc = 0;
            for (int i = 0; i < contentDoc.Count(); i++)
            {
                if (contentDoc[i][0] == 'I')
                {
                    string[] s = contentDoc[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    cd.setDocNumber(Int32.Parse(s[1]));
                    int ind = cd.getCurrentKey();
                }
                /*
                else if (contentDoc[i][0] == 'T')
                {
                    if (contentDoc[i].Length != 1)
                    {
                        int ind = cd.getCurrentKey();
                        string s = contentDoc[i].Substring(2);
                        cd.setTitle(ind, s.ToLower());
                    }
                }*/
                else if (contentDoc[i][0] == 'W')
                {
                    if (contentDoc[i].Length > 2)
                    {
                        int ind = cd.getCurrentKey();
                        string s = contentDoc[i].Substring(2);
                        cd.setWord(ind, s.ToLower());
                        nDoc++;
                    }
                }
            }
            cd.setNTuple(nDoc);
        }
        public void SaveQueryCollection(CollectionDocument cd)
        {
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\query.text");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\QUERYAAF");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\query.text");
            string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\QUERYADG");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\QUERYABW");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\QUERYACB");
            string tempQueryFile = "\n.I" + queryFile;
            string[] contentQuery = tempQueryFile.Split(new string[] { "\n." }, StringSplitOptions.RemoveEmptyEntries);
            int nQuery = 0;
            for (int i = 0; i < contentQuery.Count(); i++)
            {
                if (contentQuery[i][0] == 'I')
                {
                    string[] s = contentQuery[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    cd.setDocNumber(Int32.Parse(s[1]));
                    int ind = cd.getCurrentKey();
                }
                else if (contentQuery[i][0] == 'W')
                {
                    if (contentQuery[i].Length > 2)
                    {
                        int ind = cd.getCurrentKey();
                        string s = contentQuery[i].Substring(2);
                        cd.setWord(ind, s.ToLower());
                        //nQuery++;
                    }
                    nQuery++;
                }
            }
            cd.setNTuple(nQuery);
        }
    }
}
