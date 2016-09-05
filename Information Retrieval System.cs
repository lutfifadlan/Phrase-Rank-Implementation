using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace InformationRetrieval
{
    public partial class InformationRetrievalSystem : Form
    {
        CollectionDocument collDoc = new CollectionDocument();
        CollectionDocument collQuery = new CollectionDocument();
        CollectionDocument collReformulatedQuery = new CollectionDocument();
        SaveCollection saveColl = new SaveCollection();
        StemmingCollection stemColl = new StemmingCollection();
        RemoveStopWords removeStop = new RemoveStopWords();
        RelevanceJudgement relJudge = new RelevanceJudgement();
        Indexing indexer = new Indexing();
        SavePositionStemmedWord positioningTerm = new SavePositionStemmedWord();
        SaveIndexingToFile saveIndexing = new SaveIndexingToFile();
        Weighting weightCollection = new Weighting();
        ComputingTFIDF computeTFIDF = new ComputingTFIDF();
        ComputingNormalizationFactor computeNormalizationFactor = new ComputingNormalizationFactor();
        ComputingTFIDFNormalized computeTFIDFNormalized = new ComputingTFIDFNormalized();
        InvertedFileIndex invertedFile = new InvertedFileIndex();
        RetrieveCollection retrieval = new RetrieveCollection();
        ScoringDocument scoring = new ScoringDocument();
        EvaluatingCollection evaluation = new EvaluatingCollection();
        RelevanceFeedback relFeedBack = new RelevanceFeedback();
        Is2WordAdjacent isAdj = new Is2WordAdjacent();
        PhraseRank phRank = new PhraseRank();
        string retrievalMethod;
        double recallCollection;
        double precisionCollection;
        double IAPColletion;
        double NIAPCollection;
        List<double> allRecallRetrieval = new List<double>();
        List<double> allPrecisionRetrieval = new List<double>();
        List<double> allInterpolatedAveragePrecision = new List<double>();
        List<double> allNonInterpolatedAveragePrecision = new List<double>();
        string dokumenADI = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\ADI\adi.all";
        string dokumenCACM = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CACM\CACM.ALL";
        string dokumenCISI = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CISI\cisi.all";
        string dokumenCRAN = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\CRAN\CRAN.ALL";
        string dokumenMED = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\MED\MED.ALL";
        string dokumenNPL = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\NPL\NPL.ALL";
        string dokumenTEMPO = @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt";
        public InformationRetrievalSystem()
        {
            InitializeComponent();
        }
        public string getRetrievalMethod()
        {
            return retrievalMethod;
        }
        public double getRecallCollection()
        {
            return recallCollection;
        }
        public double getPrecisionCollection()
        {
            return precisionCollection;
        }
        public double getIAPCollection()
        {
            return IAPColletion;
        }
        public double getNIAPCollection()
        {
            return NIAPCollection;
        }
        private void btnDocumentFile_Click(object sender, EventArgs e)
        {
            textBoxDocFile.MaxLength = 300;
            collDoc.setWord(0, null);
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textBoxDocFile.Text = file;
                try
                {
                    //if(file == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt")
                    saveColl.SaveDocumentCollection(collDoc,collQuery,file);
                }
                catch (IOException) { }
            }
            /*
            if (textBoxDocFile.Text == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt")
            {
                Console.WriteLine("Menyimpan Dokumen TEMPO yang sudah di-stem");
                MessageBox.Show("Dokumen TEMPO berhasil disimpan", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
                foreach (KeyValuePair<int, string> kvp in collDoc.getWordDictionary().Skip(1).ToList())
                {
                    Console.WriteLine(kvp.Key);
                    //Console.WriteLine(kvp.Value);
                    //char[] delimiters = new char[] { '\n' };
                    //string[] valueWithoutEnter = kvp.Value.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);//delimiters, StringSplitOptions.RemoveEmptyEntries); 
                    string[] valueWithoutEnter = kvp.Value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    collDoc.getWordDictStemmed().Add(kvp.Key, valueWithoutEnter[1]);
                    stemColl.WriteOutputStemmingDocument(collDoc);
                }
                //MessageBox.Show("Dokumen TEMPO berhasil disimpan");
                MessageBox.Show("Dokumen TEMPO berhasil disimpan", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
            }*/
        }
        private void btnQueryFile_Click(object sender, EventArgs e)
        {
            textBoxQueryFile.MaxLength = 300;
            collQuery.setWord(0, null);
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textBoxQueryFile.Text = file;
                try
                {
                    saveColl.SaveQueryCollection(collQuery, file);
                }
                catch (IOException) { }
            }
            /*
            if (textBoxDocFile.Text == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt")
            {
                Console.WriteLine("Menyimpan Query TEMPO yang sudah di-stem");
                foreach (KeyValuePair<int, string> kvp in collQuery.getWordDictionary().Skip(1).ToList())
                {
                    //Console.WriteLine(kvp.Key);
                    //char[] delimiters = new char[] { '\n' };
                    //string[] valueWithoutEnter = kvp.Value.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);//delimiters, StringSplitOptions.RemoveEmptyEntries);
                    string[] valueWithoutEnter = kvp.Value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    collQuery.getQueryStemmed().Add(kvp.Key, valueWithoutEnter[1]);
                    stemColl.WriteOutputStemmingQuery(collQuery);
                }
            }*/
        }
        private void btnRelevanceJudgement_Click(object sender, EventArgs e)
        {
            textBoxRelevanceJudgement.MaxLength = 300;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                textBoxRelevanceJudgement.Text = file;
                try
                {
                    relJudge.InputRelevanceJudgement(collQuery, file);
                }
                catch (IOException) { }
            }
        }      
        private void btnStopWords_Click(object sender, EventArgs e)
        {
            removeStop.RemoveStopWordDocumentDictionary(collDoc);
            removeStop.RemoveStopWordQueryDictionary(collQuery);
            MessageBox.Show("Stop words telah dihilangkan");
        }
        private void btnStemming_Click(object sender, EventArgs e)
        {
            stemColl.StemmingDocument(collDoc);
            stemColl.StemmingQuery(collQuery);
            MessageBox.Show("Stemming berhasil dilakukan");
        }
        private void btnIndexing_Click(object sender, EventArgs e)
        {
            indexer.IndexingDocument(collDoc);
            positioningTerm.CreatePositionDocument(collDoc);
            saveIndexing.SaveDocumentIndexingToFile(collDoc);
            indexer.IndexingQuery(collQuery);
            positioningTerm.CreatePositionQuery(collQuery);
            saveIndexing.SaveQueryIndexingToFile(collQuery);
            MessageBox.Show("Indexing berhasil dilakukan");
        }
        private void btnWeighting_Click(object sender, EventArgs e)
        {
            weightCollection.WeightingDocument(collDoc);
            weightCollection.WeightingQuery(collQuery);
            computeTFIDF.ComputeTFIDFperDocument(collDoc);
            computeTFIDF.ComputeTFIDFperQuery(collQuery, collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorDocument(collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorQuery(collQuery);
            computeTFIDFNormalized.NormalizeTFFIDFDocumentperTerm(collDoc);
            computeTFIDFNormalized.NormalizeTFFIDFQueryperTerm(collQuery);
            computeTFIDFNormalized.WriteTFIDFperDocumentNormalized(collDoc);
            computeTFIDFNormalized.WriteTFIDFperQueryNormalized(collQuery);
            MessageBox.Show("Pembobotan kata berhasil dilakukan");
        }
        private void btnInvertedFile_Click(object sender, EventArgs e)
        {
            invertedFile.MakeInvertedFileIndexDocument(collDoc);
            invertedFile.MakeInvertedFileIndexQuery(collQuery);
            invertedFile.MakeInvertedFileIndexDocumentNormalized(collDoc);
            invertedFile.MakeInvertedFileIndexQueryNormalized(collQuery);
            MessageBox.Show("Inverted File Index telah berhasil dibuat");
        }
        private void btnRetrieval_Click(object sender, EventArgs e)
        {
            /*
            removeStop.RemoveStopWordDocumentDictionary(collDoc);
            removeStop.RemoveStopWordQueryDictionary(collQuery);
            //MessageBox.Show("Stop words telah dihilangkan");
            stemColl.StemmingDocument(collDoc);
            stemColl.StemmingQuery(collQuery);
            //MessageBox.Show("Stemming berhasil dilakukan");
            indexer.IndexingDocument(collDoc);
            positioningTerm.CreatePositionDocument(collDoc);
            saveIndexing.SaveDocumentIndexingToFile(collDoc);
            indexer.IndexingQuery(collQuery);
            positioningTerm.CreatePositionQuery(collQuery);
            saveIndexing.SaveQueryIndexingToFile(collQuery);
            //MessageBox.Show("Indexing berhasil dilakukan");
            weightCollection.WeightingDocument(collDoc);
            weightCollection.WeightingQuery(collQuery);
            computeTFIDF.ComputeTFIDFperDocument(collDoc);
            computeTFIDF.ComputeTFIDFperQuery(collQuery);
            computeNormalizationFactor.ComputeNormalizeFactorDocument(collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorQuery(collQuery);
            computeTFIDFNormalized.NormalizeTFFIDFDocumentperTerm(collDoc);
            computeTFIDFNormalized.NormalizeTFFIDFQueryperTerm(collQuery);
            //MessageBox.Show("Pembobotan kata berhasil dilakukan");
            invertedFile.MakeInvertedFileIndexDocument(collDoc);
            invertedFile.MakeInvertedFileIndexQuery(collQuery);
            invertedFile.MakeInvertedFileIndexDocumentNormalized(collDoc);
            invertedFile.MakeInvertedFileIndexQueryNormalized(collQuery);
            //MessageBox.Show("Inverted File Index telah berhasil dibuat");
            // ----------------------------------------------------------
            */
            collQuery.getListNoQueryDocFound().Clear();
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Retrieve Query {0}", kvp.Key);
                retrieval.Retrieval(collDoc, collQuery, kvp.Key);
            }
            MessageBox.Show("Retrieval berhasil dilakukan");
        }
        private void btnEvaluation_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Evaluasi Query {0}", kvp.Key);
                weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
                evaluation.Evaluation(collQuery, kvp.Key);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
            }
            retrievalMethod = "Hasil Convensional Information Retrieval";
            recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            WriteStandardEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            ShowRetrievalResult formRetrievalResult = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            formRetrievalResult.Show();
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();
        }
        private void btnPseudoRelFB_Click(object sender, EventArgs e)
        {
            /*
            collQuery.getDictRankedDocFound().Clear();
            collQuery.getDictRankedDocRelFound().Clear();
            collQuery.getDictRankedDocNonRelFound().Clear(); */
            //collQuery.TFIDFperQueryNormalizedRochio = new Dictionary<string, double>[collQuery.getNTuple()+1];//collQuery.getTermQueryID().Count+1];
            int k = Convert.ToInt32(textBoxK.Text);
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Pseudo Relevance Feedback Query {0}", kvp.Key);
                relFeedBack.PseudoRelevantDocument(collQuery, kvp.Key, k);
                relFeedBack.WritePseudoRelevantDocument(collQuery, kvp.Key);
                if (!collQuery.getListPseudoRelDoc().ContainsKey(kvp.Key))
                    collQuery.getListPseudoRelDoc().Add(kvp.Key, new List<int>());
                foreach (int j in collQuery.getListNoQueryDocRelFound()[kvp.Key])//collQuery.getDocRelFound())
                    collQuery.getListPseudoRelDoc()[kvp.Key].Add(j);

            }
            MessageBox.Show("Pseudo Relevance Feedback berhasil dilakukan");
            /*
            relFeedBack.PseudoRelevantDocument(collQuery,kvp.Key, k);
            if (!collQuery.getListPseudoRelDoc().ContainsKey(kvp.Key))
                collQuery.getListPseudoRelDoc().Add(kvp.Key, new List<int>());
            foreach (int j in collQuery.getListNoQueryDocRelFound()[kvp.Key])//collQuery.getDocRelFound())
                collQuery.getListPseudoRelDoc()[kvp.Key].Add(j);
            relFeedBack.RochioQuery(collQuery, kvp.Key);
            weightCollection.MakeNewWeightQueryToList(collQuery, kvp.Key);
            collQuery.getDictRankedDocFound()[kvp.Key].Clear();
            collQuery.getDictRankedDocRelFound()[kvp.Key].Clear();
            collQuery.getDictRankedDocNonRelFound()[kvp.Key].Clear();
            scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
            evaluation.Evaluation(collQuery, kvp.Key);
            allRecallRetrieval.Add(collQuery.getRecallRetrieval());
            allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
            allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
            allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
        }
        retrievalMethod = "Hasil Pseudo Relevance Feedback";
        recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
        precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
        IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
        NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
        WritePseudoRelEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
        ShowRetrievalResult formRetrievalResult = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
        formRetrievalResult.Show();
        allRecallRetrieval.Clear();
        allPrecisionRetrieval.Clear();
        allInterpolatedAveragePrecision.Clear();
        allNonInterpolatedAveragePrecision.Clear();*/
        }
        private void btnPhraseRank_Click(object sender, EventArgs e)
        {
            
            // RETRIEVAL
            
            //Console.WriteLine("masuk");
            //MessageBox.Show("Dokumen TEMPO berhasil disimpan", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
            if (textBoxDocFile.Text == @"C:\Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\test collection\TEMPO\tempoStemmed.txt")
            {
                Console.WriteLine("Menyimpan Dokumen TEMPO yang sudah di-stem");
                foreach (KeyValuePair<int, string> kvp in collDoc.getWordDictionary().Skip(1).ToList())
                {
                    Console.WriteLine(kvp.Key);
                    //Console.WriteLine(kvp.Value);
                    //char[] delimiters = new char[] { '\n' };
                    //string[] valueWithoutEnter = kvp.Value.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);//delimiters, StringSplitOptions.RemoveEmptyEntries); 
                    string[] valueWithoutEnter = kvp.Value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    collDoc.getWordDictStemmed().Add(kvp.Key, valueWithoutEnter[1]);
                    stemColl.WriteOutputStemmingDocument(collDoc);
                }
                //MessageBox.Show("Koleksi Dokumen TEMPO berhasil disimpan");
                //MessageBox.Show("Dokumen TEMPO berhasil disimpan", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);  // MB_TOPMOST
                Console.WriteLine("Menyimpan Query TEMPO yang sudah di-stem");
                foreach (KeyValuePair<int, string> kvp in collQuery.getWordDictionary().Skip(1).ToList())
                {
                    //Console.WriteLine(kvp.Key);
                    //char[] delimiters = new char[] { '\n' };
                    //string[] valueWithoutEnter = kvp.Value.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);//delimiters, StringSplitOptions.RemoveEmptyEntries);
                    string[] valueWithoutEnter = kvp.Value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    collQuery.getQueryStemmed().Add(kvp.Key, valueWithoutEnter[1]);
                    stemColl.WriteOutputStemmingQuery(collQuery);
                }
            }
            else
            {
                removeStop.RemoveStopWordDocumentDictionary(collDoc);
                removeStop.RemoveStopWordQueryDictionary(collQuery);
                //MessageBox.Show("Stop words telah dihilangkan");
                stemColl.StemmingDocument(collDoc);
                stemColl.WriteOutputStemmingDocument(collDoc);
                stemColl.StemmingQuery(collQuery);
                stemColl.WriteOutputStemmingQuery(collQuery);
            }
            Console.WriteLine("Stemming berhasil dilakukan");
            //MessageBox.Show("Stemming berhasil dilakukan");
            indexer.IndexingDocument(collDoc);
            positioningTerm.CreatePositionDocument(collDoc);
            saveIndexing.SaveDocumentIndexingToFile(collDoc);
            indexer.IndexingQuery(collQuery);
            positioningTerm.CreatePositionQuery(collQuery);
            saveIndexing.SaveQueryIndexingToFile(collQuery);
            Console.WriteLine("Indexing berhasil dilakukan");
            //MessageBox.Show("Indexing berhasil dilakukan");
            weightCollection.WeightingDocument(collDoc);
            weightCollection.WeightingQuery(collQuery);
            computeTFIDF.ComputeTFIDFperDocument(collDoc);
            computeTFIDF.ComputeTFIDFperQuery(collQuery, collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorDocument(collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorQuery(collQuery);
            computeTFIDFNormalized.NormalizeTFFIDFDocumentperTerm(collDoc);
            computeTFIDFNormalized.NormalizeTFFIDFQueryperTerm(collQuery);
            computeTFIDFNormalized.WriteTFIDFperDocumentNormalized(collDoc);
            computeTFIDFNormalized.WriteTFIDFperQueryNormalized(collQuery);
            Console.WriteLine("Pembobotan kata berhasil dilakukan");
            //MessageBox.Show("Pembobotan kata berhasil dilakukan");
            invertedFile.MakeInvertedFileIndexDocument(collDoc);
            invertedFile.MakeInvertedFileIndexQuery(collQuery);
            invertedFile.MakeInvertedFileIndexDocumentNormalized(collDoc);
            invertedFile.MakeInvertedFileIndexQueryNormalized(collQuery);
            Console.WriteLine("Inverted File index berhasil dibuat");
            //MessageBox.Show("Inverted File Index telah berhasil dibuat");
            // ----------------------------------------------------------
            collQuery.getListNoQueryDocFound().Clear();
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Document Retrieval Result.txt", string.Empty);
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Retrieve Query {0}", kvp.Key);
                retrieval.Retrieval(collDoc, collQuery, kvp.Key);
                retrieval.WriteDocumentRetrievalResult(collQuery, kvp.Key);
            }
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\All Retrieval Result.txt", string.Empty);
            //File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Pseudo Relevant Retrieval Result.txt", string.Empty);
            // EVALUATION
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Evaluasi Query {0}", kvp.Key);
                weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
                // write score result
                scoring.WriteAllRetrievalResult(collQuery, kvp.Key);
                evaluation.Evaluation(collQuery, kvp.Key);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
            }
            retrievalMethod = "Hasil Convensional Information Retrieval";
            recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            WriteStandardEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            ShowRetrievalResult formRetrievalResult = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            formRetrievalResult.Show();
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();
            // PSEUDO RELEVANCE FEEDBACK
            //collQuery.TFIDFperQueryNormalizedRochio = new Dictionary<string, double>[collQuery.getNTuple() + 1];//collQuery.getTermQueryID().Count+1];
            int k = Convert.ToInt32(textBoxK.Text);
            //File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\TFIDFperQueryNormalizedRochio.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Pseudo Relevant Document.txt", string.Empty);
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                relFeedBack.PseudoRelevantDocument(collQuery, kvp.Key, k);
                relFeedBack.WritePseudoRelevantDocument(collQuery, kvp.Key);
                if (!collQuery.getListPseudoRelDoc().ContainsKey(kvp.Key))
                    collQuery.getListPseudoRelDoc().Add(kvp.Key, new List<int>());
                foreach (int j in collQuery.getListNoQueryDocRelFound()[kvp.Key])//collQuery.getDocRelFound())
                    collQuery.getListPseudoRelDoc()[kvp.Key].Add(j);
            }
            /*
            relFeedBack.RochioQuery(collQuery, kvp.Key);
            relFeedBack.WriteTFIDFperQueryNormalizedRochio(collQuery, kvp.Key);
            weightCollection.MakeNewWeightQueryToList(collQuery, kvp.Key);
            collQuery.getDictRankedDocFound()[kvp.Key].Clear();
            collQuery.getDictRankedDocRelFound()[kvp.Key].Clear();
            collQuery.getDictRankedDocNonRelFound()[kvp.Key].Clear();
            scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
            // write pseudo retrieval result
            scoring.WriteAllRetrievalPseudoResult(collQuery, kvp.Key);
            evaluation.Evaluation(collQuery, kvp.Key);
            allRecallRetrieval.Add(collQuery.getRecallRetrieval());
            allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
            allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
            allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());*/

            /*
            retrievalMethod = "Hasil Pseudo Relevance Feedback";
            recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            WritePseudoRelEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            ShowRetrievalResult formPseudoRel = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            formPseudoRel.Show();
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear(); */
            // PHRANK
            //Console.WriteLine("oy");
            collQuery.getDictRankedDocFound().Clear();
            collQuery.getDictRankedDocRelFound().Clear();
            collQuery.getDictRankedDocNonRelFound().Clear();
            collQuery.getDictListTFIDFQ().Clear();
            collQuery.getSigmaDocumentRelevantScorePerQuery().Clear();
            collQuery.getSigmaDocumentNonRelevantScorePerQuery().Clear();
            collQuery.getListNoQueryDocFound().Clear();
            collQuery.getListNoQueryDocRelFound().Clear();
            collQuery.getListNoQueryDocNonRelFound().Clear();
            //isAdj.isWordAdjacent(collDoc, collQuery);
            // SEQUENTIAL DEPENDENCE MODEL
            isAdj.isWordDocumentAdjacent(collDoc);
            isAdj.isWordQueryAdjacent(collQuery);
            List<int> listNoQuery = new List<int>();
            phRank = new PhraseRank();
            if ((radioButton3Term.Checked || (!radioButton3Term.Checked && !radioButton6Term.Checked)) && !radioButton6Term.Checked)
            {
                Console.WriteLine("Count each word occurrence in query in windows with size 4");
                phRank.CountStemCoOccurrenceW4(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 8");
                phRank.CountStemCoOccurrenceW8(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 12");
                phRank.CountStemCoOccurrenceW12(collDoc, collQuery);
            }
            else if (radioButton6Term.Checked && !radioButton3Term.Checked)
            {
                Console.WriteLine("Count each word occurrence in query in windows with size 4");
                phRank.CountStemCoOccurrenceW4(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 8");
                phRank.CountStemCoOccurrenceW8(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 12");
                phRank.CountStemCoOccurrenceW12(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 16");
                phRank.CountStemCoOccurrenceW16(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 20");
                phRank.CountStemCoOccurrenceW20(collDoc, collQuery);
                Console.WriteLine("Count each word occurrence in query in windows with size 24");
                phRank.CountStemCoOccurrenceW24(collDoc, collQuery);
            }
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Unique Stemmed Word.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Weight Node factor s.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Weight Edge.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Matrix Probability.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Normalized Matrix Probability.txt", string.Empty);
            //File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Affinity Score.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Candidate Term.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Ranked Affinity Query Score.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Ranked Candidate Term Score.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Reformulated Query.txt", string.Empty);
            File.WriteAllText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt", string.Empty);
            foreach (KeyValuePair<int, string[]> pair in collQuery.getTermQueryID())
            {
                listNoQuery.Add(pair.Key);
            }
            if ((radioButton3Term.Checked || (!radioButton3Term.Checked && !radioButton6Term.Checked)) && !radioButton6Term.Checked)
            {
                if (btnS.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmSF(collDoc, collQuery, i);
                }
                else if (btnR.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmRF(collDoc, collQuery, i);
                }
                else if (btnZ.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmZF(collDoc, collQuery, i);
                }
                else if (!btnS.Checked && !btnR.Checked && !btnZ.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgoritmSRZT(collDoc, collQuery, i);
                }
                else
                {
                    MessageBox.Show("Pilihan tidak tersedia");
                    Application.Exit();
                    Environment.Exit(1);
                }
            }
            else if (radioButton6Term.Checked && !radioButton3Term.Checked)
            {
                if (btnS.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmSF6Term(collDoc, collQuery, i);
                }
                else if (btnR.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmRF6Term(collDoc, collQuery, i);
                }
                else if (btnZ.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgorithmZF6Term(collDoc, collQuery, i);
                }
                else if (!btnS.Checked && !btnR.Checked && !btnZ.Checked)
                {
                    foreach (int i in listNoQuery)
                        phRank.PhRankAlgoritmSRZT6Term(collDoc, collQuery, i);
                }
                else
                {
                    MessageBox.Show("Pilihan tidak tersedia");
                    Application.Exit();
                    Environment.Exit(1);
                }
            }
            else
                MessageBox.Show("Hanya dapat memilih salah satu jumlah kandidat term");
            //MessageBox.Show("Algoritma Phrase Rank berhasil dijalankan");
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryReformulatedID())
            {
                Console.WriteLine("Retrieve Query Phrase Rank {0}", kvp.Key);
                retrieval.RetrievalPhRank(collDoc, collQuery, kvp.Key);
            }
            Console.WriteLine();
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryReformulatedID())//collQuery.getTermQueryID())
            {
                Console.WriteLine("Evaluasi Query Phrase Rank {0}", kvp.Key);
                weightCollection.MakeWeightQueryToListAfterPhRank(collQuery, kvp.Key);
                scoring.ScoringAllDocumentAfterPhRank(collDoc, collQuery, kvp.Key);
                evaluation.Evaluation(collQuery, kvp.Key);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
            }
            retrievalMethod = "Hasil Evaluasi dengan Algoritma Phrase Rank";
            recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            WritePhRankEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            ShowRetrievalResult formPhRankResult = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            formPhRankResult.TopMost = true;
            formPhRankResult.Show();
            //formRetrievalResult.ShowDialog();
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();
            isAdj = new Is2WordAdjacent();
            phRank = new PhraseRank();
            collQuery.getTermQueryReformulatedID().Clear();
            allRecallRetrieval = new List<double>();
            allPrecisionRetrieval = new List<double>();
            allInterpolatedAveragePrecision = new List<double>();
            allNonInterpolatedAveragePrecision = new List<double>();   
        }
        private void btnRetTermPhRank_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryReformulatedID())
            {
                Console.WriteLine("Retrieval Reformulated Query {0}", kvp.Key);
                retrieval.RetrievalPhRank(collDoc, collQuery, kvp.Key);
            }
            MessageBox.Show("Retrieval term PhRank berhasil dilakukan");
        }
        private void btnEvalTermPhRank_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryReformulatedID())//collQuery.getTermQueryID())
            {
                //retrieval.Retrieval(collDoc, collQuery, kvp.Key);
                //retrieval.RetrievalPhRank(collDoc, collQuery, kvp.Key);
                //weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                Console.WriteLine("Evaluasi Reformulated Query {0}", kvp.Key);
                weightCollection.MakeWeightQueryToListAfterPhRank(collQuery, kvp.Key);
                //scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key); // Ranking semua dokumen berdasarkan skor
                scoring.ScoringAllDocumentAfterPhRank(collDoc, collQuery, kvp.Key);
                evaluation.Evaluation(collQuery, kvp.Key);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
            }
            retrievalMethod = "Hasil Evaluasi dengan Algoritma Phrase Rank";
            recallCollection = (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            precisionCollection = (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count;
            IAPColletion = (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            NIAPCollection = (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count;
            WritePhRankEvaluationToFile(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            ShowRetrievalResult formRetrievalResult = new ShowRetrievalResult(retrievalMethod, recallCollection, precisionCollection, IAPColletion, NIAPCollection);
            formRetrievalResult.Show();
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();
            /*
            collDoc = new CollectionDocument();
            collQuery = new CollectionDocument();
            collReformulatedQuery = new CollectionDocument();
            saveColl = new SaveCollection();
            stemColl = new StemmingCollection();
            removeStop = new RemoveStopWords();
            relJudge = new RelevanceJudgement();
            indexer = new Indexing();
            positioningTerm = new SavePositionStemmedWord();
            saveIndexing = new SaveIndexingToFile();
            weightCollection = new Weighting();
            computeTFIDF = new ComputingTFIDF();
            computeNormalizationFactor = new ComputingNormalizationFactor();
            computeTFIDFNormalized = new ComputingTFIDFNormalized();
            invertedFile = new InvertedFileIndex();*/
            //retrieval = new RetrieveCollection();
            //scoring = new ScoringDocument();
            //evaluation = new EvaluatingCollection();
            //relFeedBack = new RelevanceFeedback();
            /*
            isAdj = new Is2WordAdjacent();
            phRank = new PhraseRank();
            collQuery.getTermQueryReformulatedID().Clear();
            allRecallRetrieval = new List<double>();
            allPrecisionRetrieval = new List<double>();
            allInterpolatedAveragePrecision = new List<double>();
            allNonInterpolatedAveragePrecision = new List<double>();*/
        }
        public void WriteWithStreamWriter(StreamWriter stw, string rm, double rc, double pr, double iap, double niap)
        {
            stw.WriteLine(rm);
            stw.WriteLine("Average Recall Query Collection = {0}", rc);
            stw.WriteLine("Average Precision Query Collection = {0}", pr);
            stw.WriteLine("Average Interpolated Average Precision Query Collection = {0}", iap);
            stw.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", niap);
        }
        public void WriteStandardEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            if (textBoxDocFile.Text == dokumenADI)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if (textBoxDocFile.Text == dokumenCACM)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if(textBoxDocFile.Text == dokumenCISI)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if (textBoxDocFile.Text == dokumenCRAN)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if (textBoxDocFile.Text == dokumenMED)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if (textBoxDocFile.Text == dokumenNPL)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
            else if (textBoxDocFile.Text == dokumenTEMPO)
            {
                using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\Standard Evaluation.txt"))
                    WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
            }
        }
        public void WritePseudoRelEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            if (textBoxDocFile.Text == dokumenADI)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
            else if (textBoxDocFile.Text == dokumenCACM)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
            else if (textBoxDocFile.Text == dokumenCISI)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
            else if (textBoxDocFile.Text == dokumenCRAN)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
            else if (textBoxDocFile.Text == dokumenMED)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
            else if (textBoxDocFile.Text == dokumenNPL)
            {
                if (Convert.ToInt32(textBoxK.Text) == 1)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 1\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 2)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 2\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 3)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 3\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 5)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 5\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 10)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 10\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
                else if (Convert.ToInt32(textBoxK.Text) == 20)
                {
                    using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 20\PRF Evaluation.txt"))
                        WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                }
            }
        }
        public void WritePhRankEvaluationToFile(string rm, double rc, double pr, double iap, double niap)
        {
            if (btnS.Checked)
            {
                if (textBoxDocFile.Text == dokumenADI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if(radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCACM)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 2\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 5\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 10\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 20\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenCISI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCRAN)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenMED)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 2\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 5\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 10\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 20\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenNPL)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 2\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 5\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 10\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 20\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenTEMPO)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 1\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 3\PhRankSF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\3 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\6 term\PhRankSF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
            }
            else if (btnR.Checked)
            {
                if (textBoxDocFile.Text == dokumenADI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCACM)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 2\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 5\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 10\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 20\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenCISI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCRAN)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenMED)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 2\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 5\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 10\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 20\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenNPL)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 2\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 5\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 10\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 20\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenTEMPO)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 1\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 3\PhRankRF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\3 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\6 term\PhRankRF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
            }
            else if (btnZ.Checked)
            {
                if (textBoxDocFile.Text == dokumenADI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCACM)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 2\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 5\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 10\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 20\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenCISI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCRAN)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenMED)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 2\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 5\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 10\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 20\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenNPL)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 2\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 5\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 10\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 20\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenTEMPO)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 1\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 3\PhRankZF Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\3 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\6 term\PhRankZF Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
            }
            else if(!btnS.Checked && !btnR.Checked && !btnZ.Checked)
            {
                if (textBoxDocFile.Text == dokumenADI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 2\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 5\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 10\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\ADI\k = 20\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCACM)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 2\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 5\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 10\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CACM\k = 20\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenCISI)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 2\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 5\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 10\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CISI\k = 20\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenCRAN)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 2\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 5\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 10\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\CRAN\k = 20\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
                else if (textBoxDocFile.Text == dokumenMED)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 2\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 5\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 10\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\MED\k = 20\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenNPL)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 2\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 5\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 10\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\NPL\k = 20\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                }
                else if (textBoxDocFile.Text == dokumenTEMPO)
                {
                    if (Convert.ToInt32(textBoxK.Text) == 1)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 1\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 2)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 2\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 3)
                    {
                        using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 3\PhRankSRZT Evaluation.txt"))
                            WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 5)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 5\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 10)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 10\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                    else if (Convert.ToInt32(textBoxK.Text) == 20)
                    {
                        if (radioButton3Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\3 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                        else if (radioButton6Term.Checked)
                        {
                            using (StreamWriter stw = new StreamWriter(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Evaluation Result\TEMPO\k = 20\6 term\PhRankSRZT Evaluation.txt"))
                                WriteWithStreamWriter(stw, rm, rc, pr, iap, niap);
                        }
                    }
                }
            }
        }

    }
}
