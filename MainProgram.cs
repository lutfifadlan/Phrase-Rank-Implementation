using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
//using System.Windows;
using System.Windows.Forms;

namespace InformationRetrieval
{
    public class Adjacent : IEquatable<Adjacent>
    {
        private string w1;
        private string w2;
        private bool isAdjacent = false;
        public string getW1() { return w1; }
        public string getW2() { return w2; }
        public bool getIsAdjacent() { return isAdjacent; }
        public bool Equals(Adjacent adj1)
        {
            if ((this.getW1() == adj1.getW1()) && (this.getW2() == adj1.getW2()) &&
                (this.getIsAdjacent() == adj1.getIsAdjacent()))
                return true;
            else
                return false;
        }
        public void setW1(string _w1) { w1 = _w1; }
        public void setW2(string _w2) { w2 = _w2; }
        public void setIsAdjacent(bool _isAdjacent) { isAdjacent = _isAdjacent; }
    }
    public class CollectionDocument
    {
        private Dictionary<int, string> word = new Dictionary<int, string>();
        private SortedDictionary<int, string> wordDictStopped = new SortedDictionary<int, string>();
        private SortedDictionary<int, string> queryDictStopped = new SortedDictionary<int, string>();
        private SortedDictionary<int, string> wordDictStemmed = new SortedDictionary<int, string>();
        private SortedDictionary<int, string> queryStemmed = new SortedDictionary<int, string>();
        private SortedDictionary<int, List<int>> rld = new SortedDictionary<int, List<int>>();
        private SortedDictionary<int, string> term = new SortedDictionary<int, string>(); // list term pada dokumen
        private SortedDictionary<int, string> termQuery = new SortedDictionary<int, string>(); // list term pada query
        private SortedDictionary<int, string[]> termDocID = new SortedDictionary<int, string[]>(); // list kumpulan term tiap dokumen
        private SortedDictionary<int, string[]> termQueryID = new SortedDictionary<int, string[]>(); // list kumpulan term tiap query
        private SortedDictionary<int, string[]> termQueryReformulatedID = new SortedDictionary<int, string[]>();
        //private SortedDictionary<int, string[]> termQueryIDAfterRanked = new SortedDictionary<int, string[]>();
        private SortedDictionary<string, List<int>> wordDocNumber = new SortedDictionary<string, List<int>>(); // list term tiap dokumen beserta list dokumen yang memiliki term tersebut
        private SortedDictionary<string,List<int>> queryDocNumber = new SortedDictionary<string, List<int>>(); // list term tiap query beserta list query yang memiliki term tersebut
        private List<KeyValuePair<int, string>> sortedTerm = new List<KeyValuePair<int, string>>(); //  list term pada dokumen yang telah diurut sesuai abjad
        private List<KeyValuePair<int, string>> sortedTermQuery = new List<KeyValuePair<int, string>>(); //  list term pada query yang telah diurut sesuai abjad
        private List<KeyValuePair<string, List<int>>> structuredDocIndex = new List<KeyValuePair<string, List<int>>>(); // list term tiap dokumen yang telah diurut berdasarkan abjad beserta list dokumen yang memiliki term tersebut 
        private List<KeyValuePair<string, List<int>>> structuredQueryIndex = new List<KeyValuePair<string, List<int>>>(); // list term tiap query yang telah diurut berdasarkan abjad beserta list dokumen yang memiliki term tersebut                                                                                                                   
        private Dictionary<int, List<int>> indeksTerm = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> indeksTermQuery = new Dictionary<int, List<int>>();
        private Dictionary<int, int> sizeWindowDocument = new Dictionary<int, int>();
        private Dictionary<int, int> sizeWindowQuery = new Dictionary<int, int>();
        private Dictionary<string, int> termFrequency = new Dictionary<string, int>();
        private Dictionary<string, double> logarithmicTF = new Dictionary<string, double>();
        private Dictionary<string, int> df = new Dictionary<string, int>();
        private Dictionary<string, double> IDF = new Dictionary<string, double>();
        private Dictionary<string, int> termFrequencyQuery = new Dictionary<string, int>();
        private Dictionary<string, double> logarithmicTFQuery = new Dictionary<string, double>();
        private Dictionary<string, int> dfQuery = new Dictionary<string, int>();
        private Dictionary<string, double> IDFQuery = new Dictionary<string, double>();
        private Dictionary<int, double> normalizeFactorDocument = new Dictionary<int, double>();
        private Dictionary<int, double> normalizeFactorQuery = new Dictionary<int, double>();
        private Dictionary<int, List<double>> normalizedTFIDFperDocument = new Dictionary<int, List<double>>();
        private Dictionary<int, List<double>> normalizedTFIDFperQuery = new Dictionary<int, List<double>>();
        private Dictionary<string, List<List<int>>> matchedQueryDoc = new Dictionary<string, List<List<int>>>();
        private List<int> matchedDocument = new List<int>();
        private Dictionary<int, double> documentScore = new Dictionary<int, double>();
        private Dictionary<string, double> weightQuery = new Dictionary<string, double>();
        private Dictionary<int, double> weightQueryDict = new Dictionary<int, double>();
        private double weightListQuery = 0;
        private Dictionary<int, double> documentRelevantScore = new Dictionary<int, double>();
        private Dictionary<int, double> sigmaDocumentRelevantScorePerQuery = new Dictionary<int, double>();
        private Dictionary<int, double> documentNonRelevantScore = new Dictionary<int, double>();
        private Dictionary<int, double> sigmaDocumentNonRelevantScorePerQuery = new Dictionary<int, double>();
        private List<KeyValuePair<int, double>> rankedDocumentScore = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> rankedDocumentNonRelevantScore = new List<KeyValuePair<int, double>>();
        private List<KeyValuePair<int, double>> rankedDocumentRelevantScore = new List<KeyValuePair<int, double>>();
        private int nQRel = 0; // jumlah dokumen yang relevan 
        private int nAllDocFound = 0; // jumlah seluruh dokumen yang ditemukan
        private List<int> docFound = new List<int>(); // list seluruh dokumen yang ditemukan (termasuk yang tidak relevan)
        private Dictionary<string, List<int>> listQueryDocFound = new Dictionary<string, List<int>>(); // list term query beserta no. dokumen yang telah ditemukan yang mengandung term tersebut
        private Dictionary<int, List<int>> listNoQueryDocFound = new Dictionary<int, List<int>>(); //list no.query beserta no.dokumen yang telah ditemukan pada query tersebut
        private Dictionary<int, List<int>> listNoQueryDocRelFound = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> listNoQueryDocNonRelFound = new Dictionary<int, List<int>>();
        private List<int> docRelFound = new List<int>(); // list seluruh dokumen relevan yang ditemukan
        private List<int> docNonRelFound = new List<int>();
        private List<int> rankedDocRelFound = new List<int>(); // list seluruh dokumen relevan yang ditemukan yang telah di-ranking
        private List<int> rankedDocNonRelFound = new List<int>();
        private List<int> rankedDocFound = new List<int>(); // list seluruh dokumen yang ditemukan yang telah di-ranking
        private Dictionary<int, List<int>> dictRankedDocFound = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> dictRankedDocRelFound = new Dictionary<int, List<int>>();
        private Dictionary<int, List<int>> dictRankedDocNonRelFound = new Dictionary<int, List<int>>();
        private Dictionary<int, double> recallDict = new Dictionary<int, double>();
        private Dictionary<int, double> precisionDict = new Dictionary<int, double>();
        private double recallRetrieval = 0;
        private double precisionRetrieval = 0;
        private Dictionary<int, double> interpolatedPrecision = new Dictionary<int, double>();
        private Dictionary<double, List<double>> recallPrecision = new Dictionary<double, List<double>>();
        private double interpolatedAveragePrecision = 0;
        private double nonInterpolatedAveragePrecision = 0;
        private Dictionary<int, double> docRelevantPrecision = new Dictionary<int, double>();
        private Dictionary<int, List<int>> listPseudoRelDoc = new Dictionary<int, List<int>>();
        private Dictionary<int, List<double>> dictListTFIDFQ = new Dictionary<int, List<double>>();
        private List<int> listPseudoRelDocWithoutQ = new List<int>();
        private int currentKey = 0;
        private int nTuple = 0;
        public Dictionary<int, List<Adjacent>>[] listAdjacentDictionary;// = new Dictionary<int, List<Adjacent>>();
        public Dictionary<string, List<int>>[] stemmedWordPositionDocument;
        public Dictionary<string, List<int>>[] stemmedWordPositionQuery;
        public Dictionary<string, int>[] noDocTermTF; // array dictionary untuk term frequency tiap dokumen
        public Dictionary<string, int>[] noQueryTermTF; // array dictionary untuk term frequency tiap query
        public Dictionary<string, double>[] TFIDFperDocument;
        public Dictionary<string, double>[] TFIDFperDocumentNormalized;
        public Dictionary<string, double>[] TFIDFperQuery;
        public Dictionary<string, double>[] TFIDFperQueryNormalized;
        public Dictionary<string, double>[] TFIDFperQueryNormalizedRochio;
        public List<double> listTFIDFQ = new List<double>();
        // GETTER
        public Dictionary<int, string> getWordDictionary() { return word; }
        public string getWord(int index) { return word[index]; }
        public SortedDictionary<int, string> getWordDictStopped() { return wordDictStopped; }
        public SortedDictionary<int, string> getQueryDictStopped() { return queryDictStopped; }
        public SortedDictionary<int, string> getWordDictStemmed() { return wordDictStemmed; }
        public SortedDictionary<int, string> getQueryStemmed() { return queryStemmed; }
        public SortedDictionary<int, List<int>> getRld() { return rld; }
        public SortedDictionary<int, string> getTerm() { return term; }
        public SortedDictionary<int, string> getTermQuery() { return termQuery; }
        public SortedDictionary<int, string[]> getTermDocID() { return termDocID; }
        public SortedDictionary<int, string[]> getTermQueryID() { return termQueryID; }
        public SortedDictionary<int, string[]> getTermQueryReformulatedID() { return termQueryReformulatedID; }
        public SortedDictionary<string, List<int>> getWordDocNumber() { return wordDocNumber; }
        public SortedDictionary<string, List<int>> getQueryDocNumber() { return queryDocNumber; }
        public List<KeyValuePair<int, string>> getSortedTerm() { return sortedTerm; }
        public List<KeyValuePair<string, List<int>>> getStructuredDocIndex() { return structuredDocIndex; }
        public List<KeyValuePair<string, List<int>>> getStructuredQueryIndex() { return structuredQueryIndex; }
        public Dictionary<int, List<int>> getIndeksTerm() { return indeksTerm;  }
        public Dictionary<int, List<int>> getIndeksTermTitle() { return indeksTerm; }
        public Dictionary<int, List<int>> getIndeksTermQuery() { return indeksTermQuery; }
        public Dictionary<int, int> getSizeWindowDocument() { return sizeWindowDocument; }
        public Dictionary<int, int> getSizeWindowQuery() { return sizeWindowQuery; }
        public Dictionary<string, int> getTermFrequency() { return termFrequency; }
        public Dictionary<string, int>[] getNoDocTermTF() { return noDocTermTF; }
        public Dictionary<string, int>[] getNoQueryTermTF() { return noQueryTermTF; }
        public Dictionary<string, double> getLogarithmicTF() { return logarithmicTF; }
        public Dictionary<string, int> getDf() { return df; }
        public Dictionary<string, double> getIDF() { return IDF; }
        public Dictionary<string, int> getTermFrequencyQuery() { return termFrequencyQuery; }
        public Dictionary<string, double> getLogarithmicTFQuery() { return logarithmicTFQuery; }
        public Dictionary<string, int> getDfQuery() { return dfQuery; }
        public Dictionary<string, double> getIDFQuery() { return IDFQuery; }
        public Dictionary<string, double>[] getTFIDFperDocument() { return TFIDFperDocument; }
        public Dictionary<string, double>[] getTFIDFperDocumentNormalized() { return TFIDFperDocumentNormalized; }
        public Dictionary<string, double>[] getTFIDFperQuery() { return TFIDFperQuery; }
        public Dictionary<string, double>[] getTFIDFperQueryNormalized() { return TFIDFperQueryNormalized; }
        public Dictionary<int, double> getNormalizeFactorDocument() { return normalizeFactorDocument; }
        public Dictionary<int, double> getNormalizeFactorQuery() { return normalizeFactorQuery; }
        public Dictionary<int, List<double>> getNormalizedTFIDFperDocument() { return normalizedTFIDFperDocument; }
        public Dictionary<int, List<double>> getNormalizedTFIDFperQuery() { return normalizedTFIDFperQuery; }
        public Dictionary<string, List<List<int>>> getMatchedQueryDoc() { return matchedQueryDoc; }
        public List<int> getMatchedDocument() { return matchedDocument; }
        public Dictionary<int, double> getDocumentScore() { return documentScore; }
        public Dictionary<string, double> getWeightQuery() { return weightQuery; }
        public Dictionary<int, double> getWeightQueryDict() { return weightQueryDict; }
        public List<double> getListTFIDFQ() { return listTFIDFQ; }
        public Dictionary<int, List<double>> getDictListTFIDFQ() { return dictListTFIDFQ; }
        public Dictionary<int, double> getDocumentRelevantScore() { return documentRelevantScore; }
        public Dictionary<int, double> getSigmaDocumentRelevantScorePerQuery() { return sigmaDocumentRelevantScorePerQuery; }
        public Dictionary<int, double> getDocumentNonRelevantScore() { return documentRelevantScore; }
        public Dictionary<int, double> getSigmaDocumentNonRelevantScorePerQuery() { return sigmaDocumentNonRelevantScorePerQuery; }
        public List<KeyValuePair<int, double>> getRankedDocumentScore() { return rankedDocumentScore; }
        public List<KeyValuePair<int, double>> getRankedDocumentRelevantScore() { return rankedDocumentRelevantScore; }
        public List<KeyValuePair<int, double>> getRankedDocumentNonRelevantScore() { return rankedDocumentNonRelevantScore; }
        public int getNQRel() { return nQRel; }
        public int getNAllDocFound() { return nAllDocFound; }
        public List<int> getDocRelFound() { return docRelFound; } 
        public List<int> getDocNonRelFound() { return docNonRelFound; }
        public List<int> getDocFound() { return docFound; }
        public Dictionary<string, List<int>> getListQueryDocFound() { return listQueryDocFound; }
        public Dictionary<int, List<int>> getListNoQueryDocFound() { return listNoQueryDocFound; }
        public Dictionary<int, List<int>> getListNoQueryDocRelFound() { return listNoQueryDocRelFound; }
        public Dictionary<int, List<int>> getListNoQueryDocNonRelFound() { return listNoQueryDocNonRelFound; }
        public List<int> getRankedDocFound() { return rankedDocFound; }
        public List<int> getRankedDocRelFound() { return rankedDocRelFound; }
        public List<int> getRankedDocNonRelFound() { return rankedDocNonRelFound; }
        public Dictionary <int, List<int>> getDictRankedDocFound() { return dictRankedDocFound; }
        public Dictionary<int, List<int>> getDictRankedDocRelFound() { return dictRankedDocRelFound; }
        public Dictionary<int, List<int>> getDictRankedDocNonRelFound() { return dictRankedDocNonRelFound; }
        public Dictionary<int, double> getRecallDict() { return recallDict; }
        public Dictionary<int, double> getPrecisionDict() { return precisionDict; }
        public Dictionary<double, List<double>> getRecallPrecision() { return recallPrecision; }
        public double getRecallRetrieval() { return recallRetrieval; }
        public double getPrecisionRetrieval() { return precisionRetrieval; }
        public Dictionary<int, double> getInterpolatedPrecision() { return interpolatedPrecision; }
        public double getInterpolatedAveragePrecision() { return interpolatedAveragePrecision; }
        public double getNonInterpolatedAveragePrecision() { return nonInterpolatedAveragePrecision; }
        public Dictionary<int, double> getDocRelevantPrecision() {return docRelevantPrecision; }
        public Dictionary<int, List<int>> getListPseudoRelDoc() { return listPseudoRelDoc; }
        public List<int> getListPseudoRelDocWithoutQ() { return listPseudoRelDocWithoutQ; }
        //public Dictionary<int, List<Adjacent>>[] getListAdjacentDictionary() { return listAdjacentDictionary; }
        public int getCurrentKey() { return currentKey; }
        public int getNTuple() { return nTuple; }
        // SETTER
        public void setNTuple(int n) { nTuple = n; }
        public void setDocNumber(int key) { currentKey = key; }
        public void setWord(int index, string _word) { word.Add(index, _word); }
        public void setSortedTerm(List<KeyValuePair<int, string>> st) { sortedTerm = st; }
        public void setSortedTermQuery(List<KeyValuePair<int, string>> stq) { sortedTermQuery = stq; }
        public void setStructuredDocIndex(List<KeyValuePair<string, List<int>>> sdi) { structuredDocIndex = sdi; }
        public void setStructuredQueryIndex(List<KeyValuePair<string, List<int>>> sqi) { structuredQueryIndex = sqi; }
        public void setNoDocTermTF(Dictionary<string, int>[] dtf) { noDocTermTF = dtf; }
        public void setNAllDocFound(int n) { nAllDocFound = n; }
        public void setRankedDocumentScore(List<KeyValuePair<int, double>> docScore) { rankedDocumentScore = docScore; }
        public void setRankedDocumentRelevantScore(List<KeyValuePair<int, double>> docRelScore) { rankedDocumentRelevantScore = docRelScore; }
        public void setRankedDocumentNonRelevantScore(List<KeyValuePair<int, double>> docNonRelScore) { rankedDocumentNonRelevantScore = docNonRelScore; }
        public void setInterpolatedAveragePrecision(double IAP) { interpolatedAveragePrecision = IAP; }
        public void setNonInterpolatedAveragePrecision(double NIAP) { nonInterpolatedAveragePrecision = NIAP; }
        public void setWeightQuery(Dictionary<string, double> wQ) { weightQuery = wQ; }
        public void setWeightListQuery(double wlq) { weightListQuery = wlq; }
        public void setDocRelFound(List<int> dRF)
        {
            docRelFound.Clear();
            int tempCount = docRelFound.Count;
            //for (int i = 0; i < tempCount; i++)
              //  docRelFound.
            for (int i = 0; i < dRF.Count; i++)
                docRelFound.Add(dRF[i]);
        }
        public void setDocNonRelFound(List<int> dNRF) {
            docNonRelFound.Clear();
            //docNonRelFound = dNRF;
            int tempCount = docNonRelFound.Count;
           // for (int i = 0; i < tempCount; i++)
             //   docNonRelFound.RemoveAt(i);
            for (int i = 0; i < dNRF.Count; i++)
                docNonRelFound.Add(dNRF[i]);
        }
        public void setRecallRetrieval(double _recall) { recallRetrieval = _recall; }
        public void setPrecisionRetrieval(double _precision) { precisionRetrieval = _precision; }
        public void setListPseudoRelDocWithoutQ(List<int> _listPseudoRelDocWithoutQ) { listPseudoRelDocWithoutQ = _listPseudoRelDocWithoutQ; }
    }
}

namespace InformationRetrieval
{
    class MainProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new InformationRetrievalSystem());
            //InformationRetrievalSystem form = new InformationRetrievalSystem();
            //Form1 Form = new Form1();
            //Form.Show();
            //Form.Close();
            /*
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
            List<double> allRecallRetrieval = new List<double>();
            List<double> allPrecisionRetrieval = new List<double>();
            List<double> allInterpolatedAveragePrecision = new List<double>();
            List<double> allNonInterpolatedAveragePrecision = new List<double>();
            collDoc.setWord(0, null);
            collQuery.setWord(0, null);
            //saveColl.SaveDocumentCollection(collDoc); // Menyimpan koleksi dokumen
            saveColl.SaveQueryCollection(collQuery); // Menyimpan kumpulan query
            removeStop.RemoveStopWordDocumentDictionary(collDoc);
            removeStop.RemoveStopWordQueryDictionary(collQuery);
            relJudge.InputRelevanceJudgement(collQuery);
            stemColl.StemmingDocument(collDoc);
            stemColl.StemmingQuery(collQuery);
            indexer.IndexingDocument(collDoc);
            positioningTerm.CreatePositionDocument(collDoc);
            saveIndexing.SaveDocumentIndexingToFile(collDoc);
            indexer.IndexingQuery(collQuery);
            positioningTerm.CreatePositionQuery(collQuery);
            saveIndexing.SaveQueryIndexingToFile(collQuery);
            weightCollection.WeightingDocument(collDoc);
            weightCollection.WeightingQuery(collQuery); 
            computeTFIDF.ComputeTFIDFperDocument(collDoc);
            computeTFIDF.ComputeTFIDFperQuery(collQuery);
            computeNormalizationFactor.ComputeNormalizeFactorDocument(collDoc);
            computeNormalizationFactor.ComputeNormalizeFactorQuery(collQuery);
            computeTFIDFNormalized.NormalizeTFFIDFDocumentperTerm(collDoc);
            computeTFIDFNormalized.NormalizeTFFIDFQueryperTerm(collQuery);
            invertedFile.MakeInvertedFileIndexDocument(collDoc);
            invertedFile.MakeInvertedFileIndexQuery(collQuery);
            invertedFile.MakeInvertedFileIndexDocumentNormalized(collDoc);
            invertedFile.MakeInvertedFileIndexQueryNormalized(collQuery);*/

            //for (int i = 1; i<=collQuery.getNTuple(); i++)
            /*
            foreach(KeyValuePair<int, string[]>kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Query = {0}", kvp.Key);
                retrieval.Retrieval(collDoc, collQuery, kvp.Key);
                //Console.WriteLine("edan");
                weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
                //Console.WriteLine("Score Dokumen {0}", i);
                //scoring.PrintAllRetrievalResult(collQuery);
                evaluation.Evaluation(collQuery);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
                //Console.WriteLine("yeah");
            }
            Console.WriteLine("Hasil Convensional Information Retrieval");
            Console.WriteLine("Average Recall Query Collection = {0}", (double)allRecallRetrieval.Sum() / (double)collQuery.getTermQueryID().Count);
            Console.WriteLine("Average Precision Query Collection = {0}", (double)allPrecisionRetrieval.Sum() / (double)collQuery.getTermQueryID().Count);
            Console.WriteLine("Average Interpolated Average Precision Query Collection = {0}", (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count);
            Console.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getTermQueryID().Count);
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();
            collQuery.getListNoQueryDocFound().Clear();
            */
            // Pseudo Relevance Feedback
            //for (int i = 1; i <= collQuery.getNTuple(); i++)
            /*
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                Console.WriteLine("Query = {0}", kvp.Key);
                retrieval.Retrieval(collDoc, collQuery, kvp.Key);
                //relFeedBack.FeedbackRelevantDocument(collQuery, i);
                //relFeedBack.PseudoRelevantDocument(collQuery);
                relFeedBack.RochioQuery(collQuery);
                weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key); // Ranking semua dokumen berdasarkan skor
                relFeedBack.PseudoRelevantDocument(collQuery);
                // foreach (int j in collQuery.getDocRelFound())
                //   Console.WriteLine("Dokumen = {0}", j);
                if (!collQuery.getListPseudoRelDoc().ContainsKey(kvp.Key))
                    collQuery.getListPseudoRelDoc().Add(kvp.Key, new List<int>());
                foreach(int j in collQuery.getDocRelFound())
                    collQuery.getListPseudoRelDoc()[kvp.Key].Add(j);
                evaluation.Evaluation(collQuery);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
              //  collQuery.PrintEvaluation(collQuery, i);
                // buat top k document dengan priority queue - > heap
            }
            
            Console.WriteLine();
            Console.WriteLine("Hasil Pseudo Relevance Feedback");
            Console.WriteLine("Average Recall Query Collection = {0}", (double)allRecallRetrieval.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Precision Query Collection = {0}", (double)allPrecisionRetrieval.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Interpolated Average Precision Query Collection = {0}", (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getNTuple());
            
            allRecallRetrieval.Clear();
            allPrecisionRetrieval.Clear();
            allInterpolatedAveragePrecision.Clear();
            allNonInterpolatedAveragePrecision.Clear();

            //phRank.PrintDocRel(collQuery, i);
            collQuery.getListNoQueryDocFound().Clear();
            //int noQuery = 2;
            //phRank.PrintDocRel(collQuery);
            isAdj.isWordAdjacent(collDoc, collQuery);

            */
            //Phrase Rank
            /*
            List<int> listNoQuery = new List<int>();
            foreach (KeyValuePair<int, string[]> pair in collQuery.getTermQueryID())
                listNoQuery.Add(pair.Key);

            Console.WriteLine("PhRank algorithm");
            //for (int i = 1; i <= collQuery.getNTuple(); i++)
            //foreach(KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID()) -> di ubah pas phase rank
            foreach(int i in listNoQuery)
            {
                Console.WriteLine("Query = {0}", i);
                phRank.PhRankAlgorithm(collDoc, collQuery, i);
            }
            collQuery.getListNoQueryDocFound().Clear();
            //phRank.PhRankAlgorithm(collDoc, collQuery, noQuery);
            //for (int i = 1; i <= collQuery.getNTuple(); i++)
            foreach (KeyValuePair<int, string[]> kvp in collQuery.getTermQueryID())
            {
                retrieval.Retrieval(collDoc, collQuery, kvp.Key);
                weightCollection.MakeWeightQueryToList(collQuery, kvp.Key);
                scoring.ScoringAllDocumentRetrieved(collDoc, collQuery, kvp.Key);
                //Console.WriteLine("Score Dokumen {0}", i);
                //scoring.PrintAllRetrievalResult(collQuery);
                evaluation.Evaluation(collQuery);
                allRecallRetrieval.Add(collQuery.getRecallRetrieval());
                allPrecisionRetrieval.Add(collQuery.getPrecisionRetrieval());
                allInterpolatedAveragePrecision.Add(collQuery.getInterpolatedAveragePrecision());
                allNonInterpolatedAveragePrecision.Add(collQuery.getNonInterpolatedAveragePrecision());
            }
            Console.WriteLine();
            Console.WriteLine("Hasil Reformulasi Query dengan PhRank");
            Console.WriteLine("Average Recall Query Collection = {0}", (double)allRecallRetrieval.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Precision Query Collection = {0}", (double)allPrecisionRetrieval.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Interpolated Average Precision Query Collection = {0}", (double)allInterpolatedAveragePrecision.Sum() / (double)collQuery.getNTuple());
            Console.WriteLine("Average Non Interpolated Average Precision Query Collection = {0}", (double)allNonInterpolatedAveragePrecision.Sum() / (double)collQuery.getNTuple());
            */
            //Close Main
        }
    }            
}



