using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InformationRetrieval
{
    class SaveCollection
    {
        public void SaveDocumentCollection(CollectionDocument cd, CollectionDocument cq, string fileName)
        {
            string contents = File.ReadAllText(fileName);
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\adi-new.all");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\CACM.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\cisi.all");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\CRAN.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\MED.ALL");
            //string contents = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\NPL.ALL");
            /*
            if (fileName == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempo lama")
            {
                //int nMatch = Regex.Matches(contents, @"TEMPO-.{6}-\d{3,4}").Count;
                MatchCollection noDocPrev = Regex.Matches(contents, @"TEMPO-.{6}-\d{3,5}");
                Console.WriteLine("mc.Count = {0}", noDocPrev.Count);
                //Match mt = Regex.Match(contents, @"(TEMPO-.{6}-\d{3,4})");
                //Console.WriteLine("Matched = {0}", mt.Value);
                //string tempcontents = Regex.Replace(contents, @"(TEMPO-.{6}-\d{3,4})", ".I 1");
                File.WriteAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\NoDocTempo.txt", string.Empty);
                //string[] tempContent = new string[noDocPrev.Count + 1];
                for (int i = 0; i < noDocPrev.Count; i++)
                {
                    int j = i + 1;
                    //Console.WriteLine("noDocPrev[{0}] = {1}", i, noDocPrev[i]);
                    Console.WriteLine("j = {0}", j);
                    string noDoc = ".I " + j.ToString();
                    string pattern = @"(TEMPO-.{6}-\d{3,5})";
                    Regex rgx = new Regex(pattern);
                    //rgx.Match(contents);
                    //Console.WriteLine("noDoc = {0}", noDoc);
                    //contents = Regex.Replace(contents, noDocPrev[i].ToString(), noDoc);
                    //contents = Regex.Replace(contents, @"(TEMPO-.{6}-\d{3,4}?)", noDoc);
                    Match m = rgx.Match(contents);
                    using (StreamWriter sw = File.AppendText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\NoDocTempo.txt"))
                    {
                        sw.WriteLine("{0} {1}", j, m.Value);
                    }
                    contents = rgx.Replace(contents, noDoc, 1);
                    //Match m = Regex.Match(contents, @"TEMPO-.{6}-\d{3,4}", 1);
                    //Match m = rgx.Match(contents);
                    //cd.getNoDocTempo().Add(j, m.Value);
                    
                    // mt = mt.NextMatch();
                    //Console.WriteLine("matched = {0}", mt.Value);
                    //mt.NextMatch();
                    //contents = Regex.Replace(contents, @"(TEMPO-.{6}-\d{3,4})", noDoc);
                    //Console.WriteLine("Replace berhasil");
                }

                string removeDOC1 = Regex.Replace(contents, @"<DOC>", "");
                string removeDOCID1 = Regex.Replace(removeDOC1, @"<DOCID>", "");
                string removeDOCID2 = Regex.Replace(removeDOCID1, @"</DOCID>", "");
                string replaceTITLE = Regex.Replace(removeDOCID2, @"<TITLE>", ".T" + "\n");
                string removeTITLE= Regex.Replace(replaceTITLE, @"</TITLE>", "");
                string replaceTEXT = Regex.Replace(removeTITLE, @"<TEXT>", ".W");
                string removeTEXT = Regex.Replace(replaceTEXT, @"</TEXT>", "");
                string removeDOC2 = Regex.Replace(removeTEXT, @"</DOC>", "");
                contents = removeDOC2;
            }
            using (StreamWriter sw = new StreamWriter(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempo"))
            {
                sw.Write(contents);
            }*/
            if (fileName == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt")
            {
                string[] noDocTempo = File.ReadAllLines(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\NoDocTempo.txt");
                int noDoc;
                foreach (string s in noDocTempo)
                {
                    string[] temp = new string[2];
                    temp = s.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    noDoc = Int32.Parse(temp[0]);
                    cq.getNoDocTempo().Add(noDoc, temp[1]);
                }
            }
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
                    nDoc++;
                }
                else if (contentDoc[i][0] == 'W')
                {
                    if (contentDoc[i].Length > 2)
                    {
                        int ind = cd.getCurrentKey();
                        string s = contentDoc[i].Substring(2);
                        cd.setWord(ind, s/*s.ToLower()*/);
                        //nDoc++;
                    }
                    //nDoc++;
                }
            }
            cd.setNTuple(nDoc);
        }
        public void SaveQueryCollection(CollectionDocument cd, string fileName)
        {
            string queryFile = File.ReadAllText(fileName);
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\query.text");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\QUERYAAF");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\CISI\query.text");
            //string queryFile = File.ReadAllText(@"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\QUERYADG");
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
                    nQuery++;
                }
                else if (contentQuery[i][0] == 'W')
                {
                    if (contentQuery[i].Length > 2)
                    {
                        int ind = cd.getCurrentKey();
                        string s = contentQuery[i].Substring(2);
                        cd.setWord(ind, s/*.ToLower()*/);
                        //nQuery++;
                    }
                    //nQuery++;
                }
            }
            cd.setNTuple(nQuery);
        }
    }
}
