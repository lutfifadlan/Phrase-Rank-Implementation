using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InformationRetrieval
{
    class Vertex<T> 
    {
        private T data;
        private double weight;
        //private LinkedList<Vertex<T>> neighbors;
        public T getData() { return data; }
        ///public LinkedList<Vertex<T>> getNeighbors() { return neighbors;}
        public void setData(T _data) { data = _data; }
        //public void setNeighbors(LinkedList<Vertex<T>> _neighbors) { neighbors = _neighbors; }
        public double getWeight() { return weight; }
        public void setWeight(double _weight) { weight = _weight; }
    }
    class Edge<T> 
    {
        private Vertex<T> node1;
        private Vertex<T> node2;
        private int noStem1;
        private int noStem2;
        private double weight;
        public Vertex<T> getNode1() { return node1; }
        public Vertex<T> getNode2() { return node2; }
        public int getNoStem1() { return noStem1; }
        public int getNoStem2() { return noStem2; }
        public double getWeight() { return weight; }
        public void setNode1(Vertex<T> _node1) { node1 = _node1; }
        public void setNode2(Vertex<T> _node2) { node2 = _node2; }
        public void setNoStem1(int _noStem1) { noStem1 = _noStem1; }
        public void setNoStem2(int _noStem2) { noStem2 = _noStem2; }
        public void setWeight(double _weight) { weight = _weight; }
    }
    class PhraseRank : Adjacent
    {
        // variable
        public List<int> listPseudoDocRelWithoutQ = new List<int>();
        public string[] stemmedWord;
        public List<string> uniqueStemmedWord = new List<string>();
        public List<string> queryStemmedWord = new List<string>();
        public Dictionary<string, List<int>> wordDocNumberNew = new Dictionary<string, List<int>>();
        public Dictionary<string, int> dfNew = new Dictionary<string, int>();
        public HashSet<string> uniqueStem = new HashSet<string>();
        public Dictionary<int, string[]> stemmedWordDocRelDict = new Dictionary<int, string[]>();
        public Dictionary<Edge<string>, double> weightEdgeDict = new Dictionary<Edge<string>, double>();
        public Vertex<string>[] stem;
        public Edge<string> edgeStem;
        public List<Edge<string>> listEdgeStem = new List<Edge<string>>();
        public int stemCoOccur2;
        public Dictionary<int, int> stemCoOccur2Dict = new Dictionary<int, int>();
        public int stemCoOccur10;
        public Dictionary<int, int> stemCoOccur10Dict = new Dictionary<int, int>();
        public Dictionary<string, int> stemCoOccur4Dict = new Dictionary<string, int>();
        public Dictionary<string, int> stemCoOccur8Dict = new Dictionary<string, int>();
        public Dictionary<string, int> stemCoOccur12Dict = new Dictionary<string, int>();
        public Dictionary<string, int> stemCoOccur16Dict = new Dictionary<string, int>();
        public Dictionary<string, int> stemCoOccur20Dict = new Dictionary<string, int>();
        public Dictionary<string, int> stemCoOccur24Dict = new Dictionary<string, int>();
        public int stemCoOccur4inD;
        public List<int> listStemCoOccur2 = new List<int>();
        public List<int> listStemCoOccur4 = new List<int>();
        public double[,] probabilityMatrix;
        public double[] arrPhiJTPlus1;
        public Dictionary<string,double> arrPhiJTPlus1Query;
        public Dictionary<string, double> affinityScoreTerm = new Dictionary<string, double>();
        public Dictionary<string, double> affinityScoreTermQuery = new Dictionary<string, double>();
        public Dictionary<string, double> candidateTerm = new Dictionary<string, double>();
        public Dictionary<string[], double> candidateTermScore = new Dictionary<string[], double>();
        public List<string[]> listCandidateTerm = new List<string[]>();
        public List<double> listScoreCandidateTerm = new List<double>();
        public List<KeyValuePair<string[], double>> rankedCandidateTermScore = new List<KeyValuePair<string[], double>>();
        public KeyValuePair<string[], double> choosenTerm = new KeyValuePair<string[], double>();
        // getter
        public string[] getStemmedWord() { return stemmedWord; }
        public List<string> getUniqueStemmedWord() { return uniqueStemmedWord; }
        public List<string> getQueryStemmedWord() { return queryStemmedWord; }
        public Dictionary<string, List<int>> getWordDocNumberNew() { return wordDocNumberNew; }
        public Dictionary<string, int> getDfNew() { return dfNew; }
        public HashSet<string> getUniqueStem() { return uniqueStem; }
        public Dictionary<int, string[]> getStemmedWordDocRelDict() { return stemmedWordDocRelDict; }
        public Dictionary<Edge<string>, double> getWeightEdgeDict() { return weightEdgeDict; }
        public Vertex<string>[] getStem() { return stem; }
        public Edge<string> getEdgeStem() { return edgeStem; }
        public List<Edge<string>> getListEdgeStem() { return listEdgeStem; }
        public int getStemCoOccur2() { return stemCoOccur2; }
        public int getStemCoOccur10() { return stemCoOccur10; }
        public int getStemCoOccur4inD() { return stemCoOccur4inD; }
        public List<int> getListStemCoOccur2() { return listStemCoOccur2; }
        public List<int> getListStemCoOccur4() { return listStemCoOccur4; }
        public double[,] getProbabilityMatrix() { return probabilityMatrix; }
        public double[] getArrPhiJTPlus1() { return arrPhiJTPlus1; }
        public Dictionary<string, double> getArrPhiJTPlus1Query() { return arrPhiJTPlus1Query; }
        public Dictionary<string, double> getAffinityScoreTerm() { return affinityScoreTerm; }
        public Dictionary<string, double> getAffinityScoreTermQuery() { return affinityScoreTermQuery; }
        public Dictionary<string, double> getCandidateTerm() { return candidateTerm; }
        public Dictionary<string[], double> getCandidateTermScore() { return candidateTermScore; }
        public List<string[]> getListCandidateTerm() { return listCandidateTerm; }
        public List<double> getListScoreCandidateTerm() { return listScoreCandidateTerm; }
        public List<KeyValuePair<string[], double>> getRankedCandidateTermScore() { return rankedCandidateTermScore; }
        public KeyValuePair<string[], double> getChoosenTerm() { return choosenTerm; }
        //private List<int> listStemCoOccur10 = new List<int>();
        // Create matrix probability (hij) / transition probability/ edge weight vi & vj
        // Path walk = square(probability matrix H = hij with size n = number of unique vertex in G
        // probability hij = lij if vi and vj connectied else hij = 0
        // PhRank capture query context with affinity graph constructed from stopped, stemmed, pseudo-relevant doc
        // Vertex = unique stemmed words (stems)
        // Edges connect stem that are adjacent in the processed pseudo relevant set
        // N = pseudo relevan document + query
        // cij = count of stem co-occurrence in windows (size 2 and 10) in N
        AddingQueryToN addQtoN = new AddingQueryToN();
        public void PhRankAlgorithmSF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            //PrintDocRel(cq,noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraphSF(cd, cq, noQuery);
            //PrintProbabilityMatrix();
           // PrintAffinityGraph(cq, noQuery);
            RankingTerm(cd,cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgorithmRF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraphRF(cd, cq, noQuery);
            RankingTerm(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgorithmZF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraph(cd, cq, noQuery);
            RankingTermZF(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgoritmSRZT(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraph(cd, cq, noQuery);
            RankingTerm(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgorithmSF6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            //PrintDocRel(cq,noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraphSF6Term(cd, cq, noQuery);
            //PrintProbabilityMatrix();
            // PrintAffinityGraph(cq, noQuery);
            RankingTerm(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgorithmRF6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraphRF6Term(cd, cq, noQuery);
            RankingTerm(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            WriteChoosenTerm(noQuery);
            PrintChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgorithmZF6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraph(cd, cq, noQuery);
            RankingTermZF(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void PhRankAlgoritmSRZT6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            WriteUniqueStemmedWord(noQuery);
            CreateAffinityGraph6Term(cd, cq, noQuery);
            RankingTerm(cd, cq, noQuery);
            WriteRankedCandidateTermScore(noQuery);
            PrintChoosenTerm(noQuery);
            WriteChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        // NIAP lebih besar jika tidak menggunakan random walk
        public void CreateAffinityGraphSF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            CreateEdgeGraphAndWeightingEdge(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
                sw.WriteLine("Nomor Query = {0}", noQuery);
            RandomWalk();
            WriteNormalizeProbabilityMatrix(noQuery);
            //MakeAffinityScoreDictionary();
            //MakeAffinityQueryScoreDictionary();
            MakeCandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
            //Console.WriteLine("f");
            //PrintAffinityScore();
            //WeightingVertex(cd, cq, noQuery);
            //Console.WriteLine("g");
        }
        public void CreateAffinityGraphSF6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            CreateEdgeGraphAndWeightingEdge(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node
            RandomWalk();
            WriteNormalizeProbabilityMatrix(noQuery);
            Make6CandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
        }
        public void CreateAffinityGraphRF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            //WeightingVertex(cd, cq, noQuery);
            //WriteWeightVertex(noQuery);
            CreateEdgeGraphAndWeightingEdgeRF(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
                sw.WriteLine("Nomor Query = {0}", noQuery);
            RandomWalk();
            WriteNormalizeProbabilityMatrix(noQuery);
            WeightingVertex(cd, cq, noQuery);
            WriteWeightVertex(noQuery);
            //MakeWeightVertexAsAffinityScore();
            CombineFactorSToVertexWeight();
            MakeCandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
        }
        public void CreateAffinityGraphRF6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            //WeightingVertex(cd, cq, noQuery);
            //WriteWeightVertex(noQuery);
            CreateEdgeGraphAndWeightingEdgeRF(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node
            //MakeWeightVertexAsAffinityScore();
            //CombineFactorSToVertexWeight();
            RandomWalk();
            WriteNormalizeProbabilityMatrix(noQuery);
            WeightingVertex(cd, cq, noQuery);
            WriteWeightVertex(noQuery);
            //MakeWeightVertexAsAffinityScore();
            CombineFactorSToVertexWeight();
            Make6CandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
        }
        public void CreateAffinityGraph(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            //WeightingVertex(cd, cq, noQuery);
            //WriteWeightVertex(noQuery);
            CreateEdgeGraphAndWeightingEdge(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node 
            //MakeWeightVertexAsAffinityScore();
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
                sw.WriteLine("Nomor Query = {0}", noQuery);
            RandomWalk();
            WriteNormalizeProbabilityMatrix(noQuery);
            WeightingVertex(cd, cq, noQuery);
            WriteWeightVertex(noQuery);
            //MakeWeightVertexAsAffinityScore();
            CombineFactorSToVertexWeight();
            //CombineFactorSToVertexWeight();
            MakeCandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
        }
        public void CreateAffinityGraph6Term(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            WeightingVertex(cd, cq, noQuery);
            WriteWeightVertex(noQuery);
            CreateEdgeGraphAndWeightingEdge(cd, cq, noQuery);
            WriteProbabilityMatrix(noQuery);
            WriteWeightEdge(noQuery);
            MakeEdgeStemUnique(); // Delete edge with same node
            //MakeWeightVertexAsAffinityScore();
            //CombineFactorSToVertexWeight();
            RandomWalk();
            WeightingVertex(cd, cq, noQuery);
            WriteWeightVertex(noQuery);
            //MakeWeightVertexAsAffinityScore();
            CombineFactorSToVertexWeight();
            Make6CandidateTerm();
            WriteRankedAffinityScoreQueryDictionary(noQuery);
            WriteCandidateTerm(noQuery);
        }
        public void RankingTerm(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            for (int i = 0; i < listCandidateTerm.Count; i++)
            {
                //f(x,Q) =(rank) z(x) * sigma(w(n) dari x)phi(w(n)) / n
                double termWt = ComputeScoreTermQuery(listCandidateTerm[i]);
                termWt = termWt * ComputeFactorZ(cd, listCandidateTerm[i]);
                if (termWt == 0) { }
                else
                {
                    listScoreCandidateTerm.Add(termWt);
                    candidateTermScore.Add(listCandidateTerm[i], termWt);
                }
            }
            rankedCandidateTermScore = (from pair in candidateTermScore orderby pair.Value descending select pair).ToList();
            if (rankedCandidateTermScore.Count > 0)
            {
                choosenTerm = rankedCandidateTermScore.First();
                cq.getTermQueryReformulatedID().Add(noQuery, choosenTerm.Key);
            }
            else
            {
                choosenTerm = new KeyValuePair<string[], double>(cq.getTermQueryID()[noQuery], 0);
                cq.getTermQueryReformulatedID().Add(noQuery, choosenTerm.Key);
            }
                //choosenTerm = new KeyValuePair<string[], double>();
        }
        public void RankingTermZF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {

            for (int i = 0; i < listCandidateTerm.Count; i++)
            {
                //f(x,Q) =(rank) z(x) * sigma(w(n) dari x)phi(w(n)) / n
                double termWt = ComputeScoreTermQuery(listCandidateTerm[i]);
                if (termWt == 0) { }
                else
                {
                    listScoreCandidateTerm.Add(termWt);
                    candidateTermScore.Add(listCandidateTerm[i], termWt);
                }
            }
            rankedCandidateTermScore = (from pair in candidateTermScore orderby pair.Value descending select pair).ToList();
            if (rankedCandidateTermScore.Count > 0)
            {
                choosenTerm = rankedCandidateTermScore.First();
                cq.getTermQueryReformulatedID().Add(noQuery, choosenTerm.Key);
            }
            else
            {
                choosenTerm = new KeyValuePair<string[], double>(cq.getTermQueryID()[noQuery], 0);
                cq.getTermQueryReformulatedID().Add(noQuery, choosenTerm.Key);
            }
                // choosenTerm = new KeyValuePair<string[], double>();
        }
        public void WriteRankedCandidateTermScore(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Ranked Candidate Term Score.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                foreach (KeyValuePair<string[], double> kvp in rankedCandidateTermScore)
                {
                    for (int i = 0; i < kvp.Key.Length; i++)
                        sw.Write("{0} ", kvp.Key[i]);
                    sw.WriteLine("| Candidate Term Score = {0}", kvp.Value);
                }
            }
        }
        public void MakeUniqueStemmedWord(CollectionDocument cq, CollectionDocument cd, int noQuery)
        {
            ///int j = 0;
            //cq.getListNoQueryDocFound[]
            //Console.WriteLine("noQuery = {0}", noQuery);
            //Console.WriteLine("u");
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])//cq.getDocRelFound())
            {
                //Console.WriteLine(i);
                stemmedWord = cd.getTermDocID()[i].Distinct().ToArray();
                //Console.WriteLine("stemmedWord = {0}", stemmedWord);
                if(!stemmedWordDocRelDict.ContainsKey(i))
                    stemmedWordDocRelDict.Add(i, stemmedWord);
            }
            foreach(KeyValuePair<int, string[]> kvp in stemmedWordDocRelDict)
            {
                if(kvp.Key == 0)
                {
                    foreach (string stemWord in kvp.Value)
                    {
                        if (!queryStemmedWord.Contains(stemWord))
                            queryStemmedWord.Add(stemWord); // list of unique stem word for vertex
                    }
                }
                foreach(string stemWord in kvp.Value)
                {
                    if (!uniqueStemmedWord.Contains(stemWord))
                        uniqueStemmedWord.Add(stemWord); // list of unique stem word for vertex
                }
            }
        }
        public void WriteUniqueStemmedWord(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Unique Stemmed Word.txt"))
            {
                List<string> temp = new List<string>(uniqueStemmedWord);
                temp.Sort();
                sw.WriteLine("No Query = {0}", noQuery);
                sw.WriteLine("Kata unik yang sudah di-stem:");
                for(int i = 0; i < temp.Count; i++)
                {
                    sw.Write("{0}", temp[i]);
                    if (i < temp.Count - 1)
                        sw.Write(", ");
                }
                sw.WriteLine();
            }
        }
        public int ComputeNUniqueStemmedWordinDoc(CollectionDocument cd)
        {
            //for(int i = 1; i <= cd.getNTuple(); i++)
            //foreach(KeyValuePair<int,string[]>kvp in cd.getTermDocID())
            foreach(KeyValuePair<int, string>kvp in cd.getWordDictStemmed())
            {
                string s = kvp.Value;//cd.getWordDictStemmed()[kvp.Key];
                //Console.WriteLine("s = {0}",s);
                string[] arrayWord = s.Split(new string[] { " ", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                string[] uniqueStemWordDocument = arrayWord.Distinct().ToArray();
                foreach (string es in uniqueStemWordDocument)
                    uniqueStem.Add(es);
            }
            return uniqueStem.Count;
        }
        public void CreateVertexGraph()
        {
            stem = new Vertex<string>[uniqueStemmedWord.Count];
            // create node graph with string value
            int i = 0;
            foreach (string s in uniqueStemmedWord)
            {
                stem[i] = new Vertex<string>();
                stem[i].setData(s);
                i++;
            }
        }   
        public void CreateEdgeGraphAndWeightingEdge(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            probabilityMatrix = new double[uniqueStemmedWord.Count, uniqueStemmedWord.Count];
            //Console.WriteLine("jumlah pseudo doc rel = {0}", cq.getListPseudoRelDoc()[noQuery].Count);
            //Console.WriteLine("noQuery = {0}", noQuery);
            foreach (int j in cq.getListPseudoRelDoc()[noQuery])
            {
                //Console.WriteLine("Dokumen Relevan = {0}", j);
                for (int m = 0; m < uniqueStemmedWord.Count; m++)
                {
                    string tempS1 = uniqueStemmedWord[m];
                    for (int k = 0; k < uniqueStemmedWord.Count; k++)
                    {
                        if (m == k)
                            probabilityMatrix[m, k] = 0;
                        else
                        {
                            string tempS2 = uniqueStemmedWord[k];
                            //Console.WriteLine("tempS1 = {0}", tempS1);
                            //Console.WriteLine("tempS2 = {0}", tempS2);
                            Adjacent adj = new Adjacent();
                            adj.setW1(tempS1);
                            adj.setW2(tempS2);
                            adj.setIsAdjacent(true);
                            if (j == 0)
                            {
                                if (cq.getAdjacentQuery()[noQuery].Contains(adj))//cd.listAdjacentDictionary[noQuery][j].Contains(adj))
                                {
                                    //Console.WriteLine("tempS1 = {0}", tempS1);
                                    //Console.WriteLine("tempS2 = {0}", tempS2);
                                    //Console.WriteLine("asup");
                                    edgeStem = new Edge<string>();
                                    edgeStem.setNoStem1(m);
                                    edgeStem.setNode1(stem[m]);
                                    edgeStem.setNoStem2(k);
                                    edgeStem.setNode2(stem[k]);
                                    double w = WeightingEdge(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery, j);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = w;
                                    probabilityMatrix[k, m] = w;
                                }
                            }
                            else
                            {
                                if (cd.getAdjacentDocument()[j].Contains(adj))//cd.listAdjacentDictionary[noQuery][j].Contains(adj))
                                {
                                    //Console.WriteLine("asup");
                                    //Console.WriteLine("tempS1 = {0}", tempS1);
                                    //Console.WriteLine("tempS2 = {0}", tempS2);
                                    edgeStem = new Edge<string>();
                                    edgeStem.setNoStem1(m);
                                    edgeStem.setNode1(stem[m]);
                                    edgeStem.setNoStem2(k);
                                    edgeStem.setNode2(stem[k]);
                                    double w = WeightingEdge(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery, j);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = w;
                                    probabilityMatrix[k, m] = w;
                                }
                            }
                        }
                        //edgeStem.getWeight();
                        //probabilityMatrix[k, m] = w;
                        //Console.WriteLine("probabilityMatrix[{0}, {1}] = {2}", m, k, probabilityMatrix[m, k]);
                        /*
                        for (int indStem = 0; indStem < stem.Length; indStem++)
                        {
                            if (stem[indStem].getData() == tempS1)
                            {
                                edgeStem.setNoStem1(indStem);
                                edgeStem.setNode1(stem[indStem]);
                            }
                            if (stem[indStem].getData() == tempS2)
                            {
                                edgeStem.setNoStem2(indStem);
                                edgeStem.setNode2(stem[indStem]);
                            }
                            if (edgeStem.getNode1() != null && edgeStem.getNode2() != null)
                            {
                                if (!listEdgeStem.Contains(edgeStem))
                                {
                                    double w = WeightingEdge(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = edgeStem.getWeight();
                                    Console.WriteLine("probabilityMatrix[{0}, {1}] = {2}", m, k, probabilityMatrix[m, k]);
                                }
                            }
                        }*/

                    }
                }
            }
        }
        public void CreateEdgeGraphAndWeightingEdgeRF(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            probabilityMatrix = new double[uniqueStemmedWord.Count, uniqueStemmedWord.Count];
            //Console.WriteLine("jumlah pseudo doc rel = {0}", cq.getListPseudoRelDoc()[noQuery].Count);
            foreach (int j in cq.getListPseudoRelDoc()[noQuery])
            {
                //Console.WriteLine("Dokumen Relevan = {0}", j);
                for (int m = 0; m < uniqueStemmedWord.Count; m++)
                {
                    string tempS1 = uniqueStemmedWord[m];
                    for (int k = 0; k < uniqueStemmedWord.Count; k++)
                    {
                        if (m == k)
                            probabilityMatrix[m, k] = 0;
                        else
                        {
                            string tempS2 = uniqueStemmedWord[k];
                            //Console.WriteLine("tempS1 = {0}", tempS1);
                            //Console.WriteLine("tempS2 = {0}", tempS2);
                            Adjacent adj = new Adjacent();
                            adj.setW1(tempS1);
                            adj.setW2(tempS2);
                            adj.setIsAdjacent(true);
                            if (j == 0)
                            {
                                if (cq.getAdjacentQuery()[noQuery].Contains(adj))//cd.listAdjacentDictionary[noQuery][j].Contains(adj))
                                {
                                    //Console.WriteLine("asup");
                                    edgeStem = new Edge<string>();
                                    edgeStem.setNoStem1(m);
                                    edgeStem.setNode1(stem[m]);
                                    edgeStem.setNoStem2(k);
                                    edgeStem.setNode2(stem[k]);
                                    double w = WeightingEdgeRF(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery, j);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = w;
                                    probabilityMatrix[k, m] = w;
                                }
                            }
                            else
                            {
                                if (cd.getAdjacentDocument()[j].Contains(adj))//cd.listAdjacentDictionary[noQuery][j].Contains(adj))
                                {
                                    //Console.WriteLine("asup");
                                    edgeStem = new Edge<string>();
                                    edgeStem.setNoStem1(m);
                                    edgeStem.setNode1(stem[m]);
                                    edgeStem.setNoStem2(k);
                                    edgeStem.setNode2(stem[k]);
                                    double w = WeightingEdgeRF(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery, j);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = w;
                                    probabilityMatrix[k, m] = w;
                                }
                            }
                        }
                        //edgeStem.getWeight();
                        //probabilityMatrix[k, m] = w;
                        //Console.WriteLine("probabilityMatrix[{0}, {1}] = {2}", m, k, probabilityMatrix[m, k]);
                        /*
                        for (int indStem = 0; indStem < stem.Length; indStem++)
                        {
                            if (stem[indStem].getData() == tempS1)
                            {
                                edgeStem.setNoStem1(indStem);
                                edgeStem.setNode1(stem[indStem]);
                            }
                            if (stem[indStem].getData() == tempS2)
                            {
                                edgeStem.setNoStem2(indStem);
                                edgeStem.setNode2(stem[indStem]);
                            }
                            if (edgeStem.getNode1() != null && edgeStem.getNode2() != null)
                            {
                                if (!listEdgeStem.Contains(edgeStem))
                                {
                                    double w = WeightingEdge(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery);
                                    edgeStem.setWeight(w);
                                    listEdgeStem.Add(edgeStem);
                                    probabilityMatrix[m, k] = edgeStem.getWeight();
                                    Console.WriteLine("probabilityMatrix[{0}, {1}] = {2}", m, k, probabilityMatrix[m, k]);
                                }
                            }
                        }*/

                    }
                }
            }
        }
        public void MakeEdgeStemUnique()
        {
           // Console.WriteLine("listEdgeStem.Count = {0}", listEdgeStem.Count);
            for(int i = 0; i < listEdgeStem.Count; i++)
            {
                if(listEdgeStem[i].getNode1().getData() == listEdgeStem[i].getNode2().getData())
                    listEdgeStem.RemoveAt(i);
                for (int j = 0; j < listEdgeStem.Count; j++)
                {
                    if ((listEdgeStem[i].getNode1().getData() == listEdgeStem[j].getNode2().getData()) &&
                        (listEdgeStem[j].getNode1().getData() == listEdgeStem[i].getNode2().getData()))
                        listEdgeStem.RemoveAt(j);
                }
            }
        }
        public void CountStemCoOccurrenceW2(CollectionDocument cd, CollectionDocument cq, string word1, string word2, int noQuery)
        {
            // unordered window
            //Console.WriteLine("i = {0}", word1);
            //Console.WriteLine("j = {0}", word2);
            listStemCoOccur2.Clear();
            stemCoOccur2 = 0;
            int stemCoOccur2inDoc = 0;
            int tempStemCoOccur2 = 0;
            int tempNWord1in2W = 0;
            int tempNWord2in2W = 0;
            stemCoOccur2Dict.Clear();
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                stemCoOccur2inDoc = 0;
                int j = 1;
                for(int k = 0; k<cd.getTermDocID()[i].Length;k++)
                {
                    if (cd.getTermDocID()[i][k] == word1)
                        tempNWord1in2W++;
                    if (cd.getTermDocID()[i][k] == word2)
                        tempNWord2in2W++;
                    if (j%2 == 0)
                    {
                        if (tempNWord1in2W > 0 && tempNWord2in2W > 0)
                        {
                            stemCoOccur2inDoc++;
                            //Console.WriteLine("masuk");
                            //Console.WriteLine("stemCoOccur2inDoc = {0}", stemCoOccur2inDoc);
                            stemCoOccur2++;
                        }
                        tempNWord1in2W = 0;
                        tempNWord2in2W = 0;
                        if (k == cd.getTermDocID()[i].Length - 1)
                            break;
                        else
                            k--;
                        j = 0;
                    }
                    j++;
                }
                tempStemCoOccur2 = stemCoOccur2inDoc;
                //Console.WriteLine("Nomor Dokumen = {0}", i);
                //Console.WriteLine("stemCoOccur2inDoc = {0}", stemCoOccur2inDoc);
                listStemCoOccur2.Add(tempStemCoOccur2);
                stemCoOccur2Dict.Add(i, stemCoOccur2inDoc);
            }
            //Console.WriteLine("kelar");
            //listStemCoOccur2.Clear();
        }
        public void CountStemCoOccurrenceW10(CollectionDocument cd, CollectionDocument cq, string word1, string word2, int noQuery)
        {
            // unordered window
            stemCoOccur10 = 0;
            int stemCoOccur10inDoc = 0;
            int tempStemCoOccur10 = 0;
            int tempNWord1in10W = 0;
            int tempNWord2in10W = 0;
            stemCoOccur10Dict.Clear();
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                stemCoOccur10inDoc = 0;
                int j = 1;
                for (int k = 0; k < cd.getTermDocID()[i].Length; k++)
                {
                    if (cd.getTermDocID()[i][k] == word1)
                        tempNWord1in10W++;
                    if (cd.getTermDocID()[i][k] == word2)
                        tempNWord2in10W++;
                    if (j % 10 == 0)
                    {
                        if (tempNWord1in10W > 0 && tempNWord2in10W > 0)
                        {
                            stemCoOccur10inDoc++;
                            stemCoOccur10++;
                        }
                        tempNWord1in10W = 0;
                        tempNWord2in10W = 0;
                        if (k == cd.getTermDocID()[i].Length - 1)
                            break;
                        else
                            k = k - 9;
                        j = 0;
                    }
                    j++;
                }
                tempStemCoOccur10 = stemCoOccur10inDoc;
                stemCoOccur10Dict.Add(i, stemCoOccur10inDoc);
            }
        }
        /*
        public void CountStemCoOccurrenceW4(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur4inDoc = 0;
            int tempNWordin4W = 0;
            stemCoOccur4Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur4inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin4W++;
                        if (j % 4 == 0)
                        {
                            if (tempNWordin4W > 0)
                                stemCoOccur4inDoc++;
                            tempNWordin4W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 3;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur4Dict.Add(s, stemCoOccur4inDoc);
            }
        }*/
        public void CountStemCoOccurrenceW4(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur4inDoc = 0;
            int tempNWordin4W = 0;
            stemCoOccur4Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur4inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin4W++;
                        if (j % 4 == 0)
                        {
                            if (tempNWordin4W > 0)
                                stemCoOccur4inDoc++;
                            tempNWordin4W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 3;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur4Dict.Add(s, stemCoOccur4inDoc);
            }
        }
        public void CountStemCoOccurrenceW8(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur8inDoc = 0;
            int tempNWordin8W = 0;
            stemCoOccur8Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur8inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin8W++;
                        if (j % 8 == 0)
                        {
                            if (tempNWordin8W > 0)
                                stemCoOccur8inDoc++;
                            tempNWordin8W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 7;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur8Dict.Add(s, stemCoOccur8inDoc);
            }
        }
        public void CountStemCoOccurrenceW12(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur12inDoc = 0;
            int tempNWordin12W = 0;
            stemCoOccur12Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur12inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin12W++;
                        if (j % 12 == 0)
                        {
                            if (tempNWordin12W > 0)
                                stemCoOccur12inDoc++;
                            tempNWordin12W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 11;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur12Dict.Add(s, stemCoOccur12inDoc);
            }
        }
        public void CountStemCoOccurrenceW16(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur16inDoc = 0;
            int tempNWordin16W = 0;
            stemCoOccur4Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur16inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin16W++;
                        if (j % 16 == 0)
                        {
                            if (tempNWordin16W > 0)
                                stemCoOccur16inDoc++;
                            tempNWordin16W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 15;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur16Dict.Add(s, stemCoOccur16inDoc);
            }
        }
        public void CountStemCoOccurrenceW20(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur20inDoc = 0;
            int tempNWordin20W = 0;
            stemCoOccur20Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur20inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin20W++;
                        if (j % 20 == 0)
                        {
                            if (tempNWordin20W > 0)
                                stemCoOccur20inDoc++;
                            tempNWordin20W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 19;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur20Dict.Add(s, stemCoOccur20inDoc);
            }
        }
        public void CountStemCoOccurrenceW24(CollectionDocument cd, CollectionDocument cq)
        {
            // unordered window
            int stemCoOccur24inDoc = 0;
            int tempNWordin24W = 0;
            stemCoOccur24Dict.Clear();
            foreach (string s in cq.getUniqueQueryTerm())
            {
                stemCoOccur24inDoc = 0;
                foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
                {
                    int j = 1;
                    for (int k = 0; k < kvp.Value.Length; k++)
                    {
                        if (kvp.Value[k] == s)
                            tempNWordin24W++;
                        if (j % 24 == 0)
                        {
                            if (tempNWordin24W > 0)
                                stemCoOccur24inDoc++;
                            tempNWordin24W = 0;
                            if (k == kvp.Value.Length - 1)
                                break;
                            else
                                k = k - 23;
                            j = 0;
                        }
                        j++;
                    }
                }
                stemCoOccur24Dict.Add(s, stemCoOccur24inDoc);
            }
        }
        /*
        public int CountWordsCoOccurrenceW4(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordsCoOccur4inDoc = 0;
            int tempNWordin4W = 0;
            wordsCoOccur4inDoc = 0;
            string s = word[0];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s)
                        tempNWordin4W++;
                    if (j % 4 == 0)
                    {
                        if (tempNWordin4W > 0)
                            wordsCoOccur4inDoc++;
                        tempNWordin4W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 3;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordsCoOccur4inDoc;
        }
        public int CountWordsCoOccurrenceW8(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordsCoOccur8inDoc = 0;
            int tempNWord1in8W = 0;
            int tempNWord2in8W = 0;
            string s1 = word[0];
            string s2 = word[1];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s1)
                        tempNWord1in8W++;
                    if (kvp.Value[k] == s2)
                        tempNWord2in8W++;
                    if (j % 8 == 0)
                    {
                        if (tempNWord1in8W > 0 && tempNWord2in8W > 0)
                            wordsCoOccur8inDoc++;
                        tempNWord1in8W = 0;
                        tempNWord2in8W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 7;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordsCoOccur8inDoc;
        }
        public int CountWordsCoOccurrenceW12(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordsCoOccur12inDoc = 0;
            int tempNWord1in12W = 0;
            int tempNWord2in12W = 0;
            int tempNWord3in12W = 0;
            string s1 = word[0];
            string s2 = word[1];
            string s3 = word[2];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s1)
                        tempNWord1in12W++;
                    if (kvp.Value[k] == s2)
                        tempNWord2in12W++;
                    if (kvp.Value[k] == s3)
                        tempNWord3in12W++;
                    if (j % 12 == 0)
                    {
                        if (tempNWord1in12W > 0 && tempNWord2in12W > 0 && tempNWord3in12W > 0)
                            wordsCoOccur12inDoc++;
                        tempNWord1in12W = 0;
                        tempNWord2in12W = 0;
                        tempNWord3in12W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 11;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordsCoOccur12inDoc;
        }
        public int CountWordsCoOccurrenceW16(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordCoOccur16inDoc = 0;
            int tempNWord1in16W = 0;
            int tempNWord2in16W = 0;
            int tempNWord3in16W = 0;
            int tempNWord4in16W = 0;
            string s1 = word[0];
            string s2 = word[1];
            string s3 = word[2];
            string s4 = word[3];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s1)
                        tempNWord1in16W++;
                    if (kvp.Value[k] == s2)
                        tempNWord2in16W++;
                    if (kvp.Value[k] == s3)
                        tempNWord3in16W++;
                    if (kvp.Value[k] == s4)
                        tempNWord4in16W++;
                    if (j % 16 == 0)
                    {
                        if (tempNWord1in16W > 0 && tempNWord2in16W > 0 && tempNWord3in16W > 0 && tempNWord4in16W > 0)
                            wordCoOccur16inDoc++;
                        tempNWord1in16W = 0;
                        tempNWord2in16W = 0;
                        tempNWord3in16W = 0;
                        tempNWord4in16W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 15;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordCoOccur16inDoc;
        }
        public int CountWordsCoOccurrenceW20(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordCoOccur20inDoc = 0;
            int tempNWord1in20W = 0;
            int tempNWord2in20W = 0;
            int tempNWord3in20W = 0;
            int tempNWord4in20W = 0;
            int tempNWord5in20W = 0;
            string s1 = word[0];
            string s2 = word[1];
            string s3 = word[2];
            string s4 = word[3];
            string s5 = word[4];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s1)
                        tempNWord1in20W++;
                    if (kvp.Value[k] == s2)
                        tempNWord2in20W++;
                    if (kvp.Value[k] == s3)
                        tempNWord3in20W++;
                    if (kvp.Value[k] == s4)
                        tempNWord4in20W++;
                    if (kvp.Value[k] == s5)
                        tempNWord5in20W++;
                    if (j % 20 == 0)
                    {
                        if (tempNWord1in20W > 0 && tempNWord2in20W > 0 && tempNWord3in20W > 0 && tempNWord4in20W > 0 && tempNWord5in20W > 0)
                            wordCoOccur20inDoc++;
                        tempNWord1in20W = 0;
                        tempNWord2in20W = 0;
                        tempNWord3in20W = 0;
                        tempNWord4in20W = 0;
                        tempNWord5in20W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 19;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordCoOccur20inDoc;
        }
        public int CountWordsCoOccurrenceW24(CollectionDocument cd, string[] word)
        {
            // unordered window
            int wordCoOccur24inDoc = 0;
            int tempNWord1in24W = 0;
            int tempNWord2in24W = 0;
            int tempNWord3in24W = 0;
            int tempNWord4in24W = 0;
            int tempNWord5in24W = 0;
            int tempNWord6in24W = 0;
            string s1 = word[0];
            string s2 = word[1];
            string s3 = word[2];
            string s4 = word[3];
            string s5 = word[4];
            string s6 = word[5];
            foreach (KeyValuePair<int, string[]> kvp in cd.getTermDocID().Skip(1))
            {
                int j = 1;
                for (int k = 0; k < kvp.Value.Length; k++)
                {
                    if (kvp.Value[k] == s1)
                        tempNWord1in24W++;
                    if (kvp.Value[k] == s2)
                        tempNWord2in24W++;
                    if (kvp.Value[k] == s3)
                        tempNWord3in24W++;
                    if (kvp.Value[k] == s4)
                        tempNWord4in24W++;
                    if (kvp.Value[k] == s5)
                        tempNWord5in24W++;
                    if (kvp.Value[k] == s6)
                        tempNWord6in24W++;
                    if (j % 24 == 0)
                    {
                        if (tempNWord1in24W > 0 && tempNWord2in24W > 0 && tempNWord3in24W > 0 && tempNWord5in24W > 0 && tempNWord5in24W > 0 && tempNWord6in24W > 0)
                            wordCoOccur24inDoc++;
                        tempNWord1in24W = 0;
                        tempNWord2in24W = 0;
                        tempNWord3in24W = 0;
                        tempNWord4in24W = 0;
                        tempNWord5in24W = 0;
                        tempNWord6in24W = 0;
                        if (k == kvp.Value.Length - 1)
                            break;
                        else
                            k = k - 23;
                        j = 0;
                    }
                    j++;
                }
            }
            return wordCoOccur24inDoc;
        }*/
        /*
        public int CountWordOccurenceIn4Window(CollectionDocument cd, string[] word)
        {
            //for (int i=1; i <= cd.getNTuple(); i++)
            foreach(KeyValuePair<int, string[]>kvp in cd.getTermDocID())//cd.getWordDictStemmed())//cd.getTermDocID())
            {
                stemCoOccur4inD = 0;
                //int sizeUw4,sizeWindow;
                int tempStemCoOccur4inD = 0;
                //sizeWindow = cd.getSizeWindowDocument()[i];
                //sizeUw4 = sizeWindow / 4;
                int j = 0;
                if (word.Length == 1)
                {
                    foreach (string s in kvp.Value)//cd.getTermDocID()[kvp.Key])//i])
                    {
                        if (j % 4 == 0)
                        {
                            for (int k = 0; k < word.Length; k++)
                            {
                                if (s == word[k])
                                    stemCoOccur4inD++;
                            }
                        }
                        j++;
                    }
                }
                else if (word.Length == 2)
                {
                    foreach (string s in kvp.Value)//cd.getTermDocID()[kvp.Key])//[i])
                    {
                        if (j % 8 == 0)
                        {
                            for (int k = 0; k < word.Length; k++)
                            {
                                if (s == word[k])
                                    stemCoOccur4inD++;
                            }
                        }
                        j++;
                    }
                }
                else if (word.Length == 3)
                {
                    foreach (string s in kvp.Value)//cd.getTermDocID()[kvp.Key])//[i])
                    {
                        if (j % 12 == 0)
                        {
                            for (int k = 0; k < word.Length; k++)
                            {
                                if (s == word[k])
                                    stemCoOccur4inD++;
                            }
                        }
                        j++;
                    }
                }
                tempStemCoOccur4inD = stemCoOccur4inD;
                listStemCoOccur4.Add(tempStemCoOccur4inD);
            }
            return listStemCoOccur4.Sum();
        }*/
        
        public int CountWordOccurenceIn4Window(string[] word)
        {
            stemCoOccur4inD = 0;
            if (word.Length == 1)
            {
                if (!stemCoOccur4Dict.ContainsKey(word[0]))
                    stemCoOccur4Dict.Add(word[0], 0);
                stemCoOccur4inD = stemCoOccur4Dict[word[0]];
            }
            else if (word.Length == 2)
            {
                if (!stemCoOccur8Dict.ContainsKey(word[0]))
                    stemCoOccur8Dict.Add(word[0], 0);
                if (!stemCoOccur8Dict.ContainsKey(word[1]))
                    stemCoOccur8Dict.Add(word[1], 0);
                stemCoOccur4inD = stemCoOccur8Dict[word[0]] + stemCoOccur8Dict[word[1]];
            }
            else if (word.Length == 3)
            {
                if (!stemCoOccur12Dict.ContainsKey(word[0]))
                    stemCoOccur12Dict.Add(word[0], 0);
                if (!stemCoOccur12Dict.ContainsKey(word[1]))
                    stemCoOccur12Dict.Add(word[1], 0);
                if (!stemCoOccur12Dict.ContainsKey(word[2]))
                    stemCoOccur12Dict.Add(word[2], 0);
                stemCoOccur4inD = stemCoOccur12Dict[word[0]] + stemCoOccur12Dict[word[1]] + stemCoOccur12Dict[word[2]];
            }
            else if (word.Length == 4)
            {
                if (!stemCoOccur16Dict.ContainsKey(word[0]))
                    stemCoOccur16Dict.Add(word[0], 0);
                if (!stemCoOccur16Dict.ContainsKey(word[1]))
                    stemCoOccur16Dict.Add(word[1], 0);
                if (!stemCoOccur16Dict.ContainsKey(word[2]))
                    stemCoOccur16Dict.Add(word[2], 0);
                if (!stemCoOccur16Dict.ContainsKey(word[3]))
                    stemCoOccur16Dict.Add(word[3], 0);
                //foreach (string s in word)
                  //  Console.WriteLine(s);
                stemCoOccur4inD = stemCoOccur16Dict[word[0]] + stemCoOccur16Dict[word[1]] + stemCoOccur16Dict[word[2]] + stemCoOccur16Dict[word[3]];
            }
            else if (word.Length == 5)
            {
                if (!stemCoOccur20Dict.ContainsKey(word[0]))
                    stemCoOccur20Dict.Add(word[0], 0);
                if (!stemCoOccur20Dict.ContainsKey(word[1]))
                    stemCoOccur20Dict.Add(word[1], 0);
                if (!stemCoOccur20Dict.ContainsKey(word[2]))
                    stemCoOccur20Dict.Add(word[2], 0);
                if (!stemCoOccur20Dict.ContainsKey(word[3]))
                    stemCoOccur20Dict.Add(word[3], 0);
                if (!stemCoOccur20Dict.ContainsKey(word[4]))
                    stemCoOccur20Dict.Add(word[4], 0);
                stemCoOccur4inD = stemCoOccur20Dict[word[0]] + stemCoOccur20Dict[word[1]] + stemCoOccur20Dict[word[2]] + stemCoOccur20Dict[word[3]] + stemCoOccur20Dict[word[4]];
            }
            else if (word.Length == 6)
            {
                if (!stemCoOccur24Dict.ContainsKey(word[0]))
                    stemCoOccur24Dict.Add(word[0], 0);
                if (!stemCoOccur24Dict.ContainsKey(word[1]))
                    stemCoOccur24Dict.Add(word[1], 0);
                if (!stemCoOccur24Dict.ContainsKey(word[2]))
                    stemCoOccur24Dict.Add(word[2], 0);
                if (!stemCoOccur24Dict.ContainsKey(word[3]))
                    stemCoOccur24Dict.Add(word[3], 0);
                if (!stemCoOccur24Dict.ContainsKey(word[4]))
                    stemCoOccur24Dict.Add(word[4], 0);
                if (!stemCoOccur24Dict.ContainsKey(word[5]))
                    stemCoOccur24Dict.Add(word[5], 0);
                stemCoOccur4inD = stemCoOccur24Dict[word[0]] + stemCoOccur24Dict[word[1]] + stemCoOccur24Dict[word[2]] + stemCoOccur24Dict[word[3]] + stemCoOccur24Dict[word[4]] + stemCoOccur24Dict[word[5]];
            }
            return stemCoOccur4inD;
        }
        /*
        public int CountWordOccurenceIn4Window(CollectionDocument cd, string[] word)
        {
            stemCoOccur4inD = 0;
            if (word.Length == 1)
                stemCoOccur4inD = CountWordsCoOccurrenceW4(cd, word);
            else if (word.Length == 2)
                stemCoOccur4inD = CountWordsCoOccurrenceW8(cd, word);
            else if (word.Length == 3)
                stemCoOccur4inD = CountWordsCoOccurrenceW12(cd, word);
            else if (word.Length == 4)
                stemCoOccur4inD = CountWordsCoOccurrenceW16(cd, word);
            else if (word.Length == 5)
                stemCoOccur4inD = CountWordsCoOccurrenceW20(cd, word);
            else if (word.Length == 6)
                stemCoOccur4inD = CountWordsCoOccurrenceW24(cd, word);
            return stemCoOccur4inD;
        }*/
        public double CountProbabilityOfDocument(CollectionDocument cd, CollectionDocument cq,/* int noDoc,*/ string stemI, string stemJ, int noQuery)
        {
            // count probability of document in which stem i and j co-occur given Q
            // compute without lamda (smoothing parameter)
            int occurenceStemIJAtPseudoDoc = 0;
            double probabilityStemIJ = 0;
            int nstemI;
            int nstemJ;
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                nstemI = 0;
                nstemJ = 0;
                for (int j = 0; j < cd.getTermDocID()[i].Length; j++)
                {
                    if (cd.getTermDocID()[i][j] == stemI)
                        nstemI++;
                    if (cd.getTermDocID()[i][j] == stemJ)
                        nstemJ++;
                    if (nstemI > 0 && nstemJ > 0)
                    {
                        occurenceStemIJAtPseudoDoc++;
                        j = cd.getTermDocID()[i].Length+1;
                        //break;
                    }
                }
            }
           // Console.WriteLine("occurenceStemIJAtPseudoDoc = {0}", occurenceStemIJAtPseudoDoc);
            probabilityStemIJ = (double)occurenceStemIJAtPseudoDoc / (cq.getListPseudoRelDoc()[noQuery].Count/* - 1*/);
            //Console.WriteLine("probabilityStemIJ = {0}", probabilityStemIJ);
            return probabilityStemIJ;
        }
        public double WeightingEdge(CollectionDocument cd, CollectionDocument cq, Edge<string> edge, string word1, string word2, int noQuery, int pseudoRelDoc)
        {
            //l(i,j) = r * sigma(p(dk|Q) * (lamda(c(i,j)w2) + (1-lamda)(c(i,j)w10); sigma dk from N; lamda = 0.6
            //r = tfidf that confirm importance of connection between i and j in N
            //p(dk|Q) = probability of document in which stem i & j co-occur given Q
            //c(i,j)= counts of stem co-occurrence in windows of size 2 and 10 in N
            // tf component already accoounted for (lamda(c(i,j)w2) + (1-lamda)(c(i,j)w10)
            // r(i,j) = log2(sigma(c(i,j)w2 / (1+c(i,j)w2)); sigma ij from N
            List<double> listProbabilityStemIJ = new List<double>();
            double lamda = 0.6;
            double factorR = 0;
            double sigmaPCW = 0;
            int stemCoOccur2inRelDoc = 0;
            int stemCoOccur10inRelDoc = 0;
            //Console.WriteLine("word1 = {0}", word1);
            //Console.WriteLine("word2 = {0}", word2);
            //Console.WriteLine("cc");
            //CountStemCoOccurrence(cd, cq, word1, word2, noQuery);
            CountStemCoOccurrenceW2(cd, cq, word1, word2, noQuery);
            CountStemCoOccurrenceW10(cd, cq, word1, word2, noQuery);
            stemCoOccur2inRelDoc = stemCoOccur2Dict[pseudoRelDoc];
            stemCoOccur10inRelDoc = stemCoOccur10Dict[pseudoRelDoc];
            //Console.WriteLine("cd");
            int sigmaCW2 = listStemCoOccur2.Sum();
            
            //Console.WriteLine("sigmaCW2 = {0}", listStemCoOccur2.Sum());
            //Console.WriteLine("stemCoOccur2 = {0}", stemCoOccur2);
            //Console.WriteLine("stemCoOccur2inRelDoc = {0}", stemCoOccur2inRelDoc);
            if (sigmaCW2 == 0)
                factorR = 0;
            else
                factorR = Math.Log((double)sigmaCW2 / (1 + stemCoOccur2inRelDoc), 2);
            //factorR = Math.Log((double)sigmaCW2 / (1 + stemCoOccur2), 2);
            //Console.WriteLine("factorR = {0}", factorR);
            /*
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                Console.WriteLine("i = {0}", i);
                double probabilityWord1And2 = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery);
                //Console.WriteLine("probabilityWord1And2 in document {1} = {0}", probabilityWord1And2, i);
                //sigmaPCW = sigmaPCW + (probabilityWord1And2 * ((lamda * stemCoOccur2) + (1 - lamda) * stemCoOccur10));
                listProbabilityStemIJ.Add(probabilityWord1And2);
            }*/
            //sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery)  * ((lamda * stemCoOccur2) + (1 - lamda) * stemCoOccur10);
            //sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery) * ((lamda * stemCoOccur2inRelDoc) + (1 - lamda) * stemCoOccur10inRelDoc);
            sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery) * (lamda * stemCoOccur2inRelDoc) + CountProbabilityOfDocument(cd, cq, word1, word2, noQuery)* (1 - lamda) * stemCoOccur10inRelDoc;
            //Console.WriteLine("sigmaPCW = {0}", sigmaPCW);
            double weight = factorR * sigmaPCW;
            //Console.WriteLine("word 1 = {0}, word 2 = {1}, weight = {2}", word1, word2, weight);
            //edge.setWeight(weight);
            //return Math.Abs(weight);
            return weight;
            /*
            foreach(int i in cq.getDocRelFound())
            {
                if (i == 0)
                    Console.WriteLine("Weight Query {0} = {1}", i, cq.listTFIDFQ.Sum());
                else
                    Console.WriteLine("Score Dokumen {0} = {1}", i, cq.getDocumentScore()[i]);
            }*/
        }
        public double WeightingEdgeRF(CollectionDocument cd, CollectionDocument cq, Edge<string> edge, string word1, string word2, int noQuery, int pseudoRelDoc)
        {
            List<double> listProbabilityStemIJ = new List<double>();
            double lamda = 0.6;
            double sigmaPCW = 0;
            int stemCoOccur2inRelDoc = 0;
            int stemCoOccur10inRelDoc = 0;
            //CountStemCoOccurrence(cd, cq, word1, word2, noQuery);
            CountStemCoOccurrenceW2(cd, cq, word1, word2, noQuery);
            CountStemCoOccurrenceW10(cd, cq, word1, word2, noQuery);
            stemCoOccur2inRelDoc = stemCoOccur2Dict[pseudoRelDoc];
            stemCoOccur10inRelDoc = stemCoOccur10Dict[pseudoRelDoc];
            //sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery) * ((lamda * stemCoOccur2) + (1 - lamda) * stemCoOccur10);
            sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery) * ((lamda * stemCoOccur2inRelDoc) + (1 - lamda) * stemCoOccur10inRelDoc);
            double weight = sigmaPCW;
            //return Math.Abs(weight);
            return weight;

        }
        public double ComputeWeightVertex(string word, CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            //s(w(n)) = w(n)f(avg) * idf(w(n))
            //w(n)f(avg) = frequency of a word w(n) in N, averaged over k+1 documents (average frequency)
            //and normalized by the maximum average frequency of any term in N
            //idf(w(n)) = log2(|C| / (1+df(w(n))); |C| = vocabulary of stemmed words in collection C
            // df(w(n)) = number of documents in C containing w(n)
            //compute frequency of word in N
            int nWord = frequencyWordInN(word, cd, cq, noQuery);
            double wNFAvg = /*affinityScoreTerm[word] */ ((double)nWord / cq.getListPseudoRelDoc()[noQuery].Count) / MaximumAverageFrequencyinN(cq, cd, noQuery); // dikali score atau ngga / ditambah?
            int C = ComputeNUniqueStemmedWordinDoc(cd);
            int dfWn;
            if (cd.getDf().ContainsKey(word))
                dfWn = cd.getDf()[word];
            else
                dfWn = 0;
            //int C = uniqueStemmedWord.Count;
            //int dfWn = dfNew[word];
            double IDFWn = Math.Log((double)C / (1 + dfWn), 2);
            //Console.WriteLine("noQuery = {0}", noQuery);
            //Console.WriteLine("IDFWn = {0}", IDFWn);
            double sWn = wNFAvg * IDFWn;
            return sWn;
        }
        public void RandomWalk()
        {
            //affinity scores compute recursively
            //phi^t(j) = affinity score associated with v(j) at time t
            //phi^t+1(j) = sum of scores for each v(i) connected to v(j), weighted by possibility
            // of choosing v(j) as the next step on the path from v(i)
            //phi^t+1(j) = sigma(i) phi(i) * h(i,j)
            //some minimal likelihood that a path from v(i) at time t will randomly step to some v(j) at time t+1
            // that maybe unconnected to v(i) -> defined to be the uniform probability vector u = (1/n)
            //corresponding factor reflects the likelihood that a path will follow the structure of edges in G
            // damping factor alfa control the balance between them:
            // phi^t+1 = alfa*phi^t*H + (1-alfa)*u
            //--------------------------------------------------------------------------------------------------
            //Edge weights are normalized to sum to one and phi(j) = affinity score of stem associated with v(j)
            //phi(j) indicates importance of stem in query context
            //Iteration of the walk ceases when difference in score at any vertex doesn't exceed 0.0001
            double[,] phiJT = new double[1, stem.Length];
            //double[,] phiJT = new double[stem.Length, 1];
            double[,] dotResult = new double[1, stem.Length];
            //double[,] dotResult = new double[stem.Length, 1];
            double[,] dotProbMatrix = new double[stem.Length, stem.Length];
            arrPhiJTPlus1 = new double[stem.Length];
            double[] tempArrPhiJTPlus1 = new double[stem.Length];
            for (int ind = 0; ind < stem.Length; ind++)
            {
                //phiJT[0, ind] = 0;
                //phiJT[0, ind] = 1;
                phiJT[0, ind] = (double) 1 / stem.Length; // because phiij = phiji -> chain symmetric -> stationary distribution is uniform
                //Console.WriteLine("phiJT[0, ind] = {0}", phiJT[0, ind]);
                //phiJT[ind, 0] = 0;
                dotResult[0, ind] = 0;
                //dotResult[ind, 0] = 0;
                arrPhiJTPlus1[ind] = 0;
            }
            arrPhiJTPlus1Query = new Dictionary<string, double>();
            //double alfa = 0.85;
            //double u = (double)1 / stem.Length;
            double sigmaPhiIHIJ = 0;
            double[] tempSigmaPhiIHIJ = new double[stem.Length];
            int t = 0;
            bool isExceed = true;
            bool isInfinite = false;
            // pada t = 0
            //using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
            //sw.WriteLine("t = {0}", t);
            // normalized row to sum to one
            double[] factor = new double[stem.Length];
            double sumOfNumber;
            for(int i = 0; i <stem.Length; i++)
            {
                sumOfNumber = 0;
                for(int j = 0; j<stem.Length; j++)
                {
                    //sumOfNumber += probabilityMatrix[j, i]; 
                    sumOfNumber += probabilityMatrix[i, j];
                }
                factor[i] = (double) 1 / sumOfNumber;
            }
            for(int i = 0; i< stem.Length; i++)
            {
                for(int j = 0; j<stem.Length; j++)
                    probabilityMatrix[i, j] = probabilityMatrix[i, j] * factor[i];
                //probabilityMatrix[i, j] = probabilityMatrix[i, j] * factor[i];
                //probabilityMatrix[j, i] = probabilityMatrix[j, i] * factor[i];
            }
            // write normalize probability matrix
            for (int i = 0; i < stem.Length; i++)
            {
                //Console.WriteLine("phiJT = {0}", phiJT[0,i]);
                //sigmaPhiIHIJ = 0;
                //phiJT[0, i] = 1;
                //phiJT[i, 0] = 1;
                dotResult[0, i] = 0;
                //dotResult[i, 0] = 0;
                List<double> listDotResult = new List<double>();
                for (int j = 0; j <stem.Length; j++)
                {
                    //dotProbMatrix[i, j] = probabilityMatrix[i, j];
                    //dotProbMatrix[k, i] += probabilityMatrix[k, i];
                    //dotResult[0, i] +=  probabilityMatrix[k, i];
                    //dotResult[0, i] += phiJT[0,i]*probabilityMatrix[j, i];
                    dotResult[0, i] += phiJT[0, j] * probabilityMatrix[j, i];
                    //dotResult[0, i] += phiJT[0, j] * probabilityMatrix[i, j];
                    /*
                    if (probabilityMatrix[j, i] == 0) { }
                    else
                        listDotResult.Add(phiJT[0, i] * probabilityMatrix[j, i]);*/
                    //dotResult[0, j] = phiJT[0, i] * probabilityMatrix[j, i];
                    //dotResult[0, i] += matrixPhiJT[0, j] * probabilityMatrix[i, j];
                    //dotResult[0, i] += probabilityMatrix[i, j];
                    //dotResult[0, i] += probabilityMatrix[j, i];
                }
                //dotResult[0, i] = probabilityMatrix[i, k];
                //phiJT[0, i] = 0;
                //phiJT[i, 0] = 0;
                //Random ran = new Random();
                //int r = ran.Next(listDotResult.Count);
                //double phiJTPlus1 = dotResult[0,ran.Next(0, dotResult.Length)];
                //double phiJTPlus1 = listDotResult[r];
                //arrPhiJTPlus1[i] = phiJTPlus1;
                //Console.WriteLine(dotResult[0, i]);
                arrPhiJTPlus1[i] = dotResult[0, i];
                //arrPhiJTPlus1[i] = dotResult[i, 0];
                if (queryStemmedWord.Contains(stem[i].getData()))
                    arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
                //using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PhiJt.txt"))
                  //  sw.WriteLine("arrPhiJTPlus1[{0}] = {1}", i, arrPhiJTPlus1[i]);
            }
            while (isExceed && !isInfinite && t < 15)
            {
                /*
                for (int ia = 0; ia < stem.Length; ia++)
                {
                    for (int ib = 0; ib < stem.Length; ib++)
                    {
                        double temp = 0;
                        for (int ic = 0; ic < stem.Length; ic++)
                            temp+= dotProbMatrix[ia, ic] * dotProbMatrix[ib, ic];
                        dotProbMatrix[ia, ib] = temp;
                    }
                }*/
                // pij(2) = (sigma k =1 sampai r(jumlah baris)) pik * pkj
                /*
                for (int j = 0; j < stem.Length; j++)
                {
                    for (int i = 0; i < stem.Length; i++)
                    {
                        double temp = 0;
                        for (int k = 0; k < stem.Length; k++)
                        {
                            //temp += dotProbMatrix[i, k] * dotProbMatrix[k, j];
                        }
                        dotProbMatrix[i, j] = temp;
                    }
                }*/
                /*
                for(int i=0; i<stem.Length; i++)
                {
                    double temp = 0;
                    for(int j=0; j<stem.Length; j++)
                    {
                        temp += arrPhiJTPlus1[j] * probabilityMatrix[j, i];
                    }
                    tempArrPhiJTPlus1[i] = temp;
                    arrPhiJTPlus1[i] = temp;
                }*/
                double minusInfinity = -1.0 / 0.0;
                //Console.WriteLine("t = {0}", t);
                t++;
                //using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
                  //  sw.WriteLine("t = {0}", t);
                //Console.WriteLine("t = {0}", t);
                for (int i = 0; i < stem.Length; i++)
                {
                    tempSigmaPhiIHIJ[i] = arrPhiJTPlus1[i];
                    //phiJT[0, i] = 1;
                    dotResult[0, i] = 0;
                    //dotResult[i, 0] = 0;
                    List<double> listDotResult = new List<double>();
                    for (int j = 0; j < stem.Length; j++)
                    {
                        //dotResult[0, i] += matrixPhiJT[0, k] * dotProbMatrix[i, k];
                        dotResult[0, i] += arrPhiJTPlus1[j] * probabilityMatrix[j,i];
                        /*
                        if (probabilityMatrix[j, i] == 0) { }
                        else
                            listDotResult.Add(arrPhiJTPlus1[j] * probabilityMatrix[j, i]);*/
                            //dotResult[0, j] = arrPhiJTPlus1[j] * probabilityMatrix[j, i];
                        //dotResult[i, 0] += arrPhiJTPlus1[j] * probabilityMatrix[i, j];
                    }
                    //Random ran = new Random();
                    //double phiJTPlus1 = dotResult[0, ran.Next(0, dotResult.Length)];
                    //int r = ran.Next(listDotResult.Count);
                    //double phiJTPlus1 = dotResult[0,ran.Next(0, dotResult.Length)];
                    //double phiJTPlus1 = listDotResult[r];
                    //sigmaPhiIHIJ = phiJTPlus1;
                    //matrixPhiJT[0, i] = 0;
                    sigmaPhiIHIJ = dotResult[0, i];
                    //sigmaPhiIHIJ = dotResult[i, 0];
                    if (!Double.IsInfinity(sigmaPhiIHIJ) && sigmaPhiIHIJ != minusInfinity && !Double.IsNaN(sigmaPhiIHIJ))
                    {
                        arrPhiJTPlus1[i] = sigmaPhiIHIJ;
                        //Console.WriteLine(arrPhiJTPlus1[i]);
                        //using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
                          //sw.WriteLine("arrPhiJTPlus1[{0}] = {1}", i, arrPhiJTPlus1[i]);
                        if (queryStemmedWord.Contains(stem[i].getData()))
                        {
                            if (arrPhiJTPlus1Query.ContainsKey(stem[i].getData()))
                                arrPhiJTPlus1Query.Remove(stem[i].getData());
                            arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
                        }
                    }
                    else
                    {
                        for (int p = 0; p < stem.Length; p++)
                        {
                            arrPhiJTPlus1[p] = tempSigmaPhiIHIJ[p];
                            if (queryStemmedWord.Contains(stem[p].getData()))
                            {
                                if (arrPhiJTPlus1Query.ContainsKey(stem[p].getData()))
                                    arrPhiJTPlus1Query.Remove(stem[p].getData());
                                arrPhiJTPlus1Query.Add(stem[p].getData(), arrPhiJTPlus1[p]);
                            }
                        }
                        isInfinite = true;
                    }
                    if (isInfinite)
                        break;
                }
                int m = 0;
                if (!isInfinite)
                {
                    isExceed = false;
                    for (int i = 0; i < arrPhiJTPlus1.Length; i++)
                    {
                        while (m < arrPhiJTPlus1.Length)
                        {
                            if (Math.Abs(arrPhiJTPlus1[i] - arrPhiJTPlus1[m]) > 0.0001)
                            {
                                isExceed = true;
                                break;
                            }
                            m++;
                            if (isExceed == true)
                                break;
                        }
                        if (isExceed == true)
                            break;
                        if (m == arrPhiJTPlus1.Length)
                            m = 0;
                    }
                }
                else
                    break;
            }
            //using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\PHIJt.txt"))
              //  sw.WriteLine("t = {0}", t);
            Console.WriteLine("t = {0}", t); // t = 2 -> affinity score = 0 semua, t ratusan 300,700 -> NaN
            MakeAffinityScoreDictionary();
            MakeAffinityQueryScoreDictionary();
        }    
        public void MakeWeightVertexAsAffinityScore()
        {
            affinityScoreTerm.Clear();
            affinityScoreTermQuery.Clear();
            arrPhiJTPlus1 = new double[stem.Length];
            arrPhiJTPlus1Query = new Dictionary<string, double>();
            for (int i=0; i < stem.Length; i++)
            {
                arrPhiJTPlus1[i] = stem[i].getWeight();
                if (queryStemmedWord.Contains(stem[i].getData()))
                    arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
            }
            MakeAffinityScoreDictionary();
            MakeAffinityQueryScoreDictionary();
        }
        public void CombineFactorSToVertexWeight()
        {
            //double minusInfinity = -1.0 / 0.0;
            for (int i = 0; i < stem.Length; i++)
            {
                affinityScoreTerm.Remove(stem[i].getData());
                affinityScoreTermQuery.Remove(stem[i].getData());
                arrPhiJTPlus1[i] = arrPhiJTPlus1[i] + stem[i].getWeight(); // ga tau bener apa ngga -> combine ditambah apa dikali?
                if (queryStemmedWord.Contains(stem[i].getData()))
                {
                    //Console.WriteLine("iiy");
                    if (arrPhiJTPlus1Query.ContainsKey(stem[i].getData()))
                        arrPhiJTPlus1Query.Remove(stem[i].getData());
                    //if((!Double.IsInfinity(arrPhiJTPlus1[i]) && arrPhiJTPlus1[i] != minusInfinity))
                    arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
                }
            }
            MakeAffinityScoreDictionary();
            MakeAffinityQueryScoreDictionary();
        }
        public void MakeCandidateTerm()
        {
            /*
            foreach (var item in arrPhiJTPlus1Query.Where(kvp => kvp.Value == 0).ToList())
            {
                arrPhiJTPlus1Query.Remove(item.Key);
            }*/
            var arrPhiJTPlus1QueryRanked = (from pair in arrPhiJTPlus1Query orderby pair.Value descending select pair).ToList();
            foreach (KeyValuePair<string, double> kvp in arrPhiJTPlus1QueryRanked)
            {
                // Console.WriteLine("string {0} = value {1}", kvp.Key, kvp.Value);
                if (arrPhiJTPlus1QueryRanked.Count > 2)
                {
                    if (candidateTerm.Count < 3)
                        candidateTerm.Add(kvp.Key, kvp.Value);
                }
                else
                    candidateTerm.Add(kvp.Key, kvp.Value);
            }
            //PrintCandidateTerm();
            string[] T = new string[candidateTerm.Count];
            int i = 0;
            // make combination candidate term (1 - 3  word)
            foreach (KeyValuePair<string, double> kvp in candidateTerm)
            {
               // Console.WriteLine("oy");
                T[i] = kvp.Key;
               // Console.WriteLine("T[i] = {0}", T[i]);
                i++;
            }
            //Console.WriteLine("ea");
            if (candidateTerm.Count > 2)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term3 = new string[1];
                term3[0] = T[2];
                string[] term12 = term1.Concat(term2).ToArray();
                string[] term13 = term1.Concat(term3).ToArray();
                string[] term23 = term2.Concat(term3).ToArray();
                string[] term33 = term12.Concat(term3).ToArray();
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term3);
                listCandidateTerm.Add(term12);
                listCandidateTerm.Add(term13);
                listCandidateTerm.Add(term23);
                listCandidateTerm.Add(term33);
            }
            else if(candidateTerm.Count == 2)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term12 = term1.Concat(term2).ToArray();
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term12);
            }
            else if(candidateTerm.Count == 1)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                listCandidateTerm.Add(term1);
            }
        }
        public void WriteCandidateTerm(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Candidate Term.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                foreach (KeyValuePair<string, double> kvp in candidateTerm)
                    sw.WriteLine("{0}", kvp.Key);
                sw.WriteLine("Kombinasi kata :");
                for (int i = 0; i < listCandidateTerm.Count; i++)
                {
                    for (int j = 0; j < listCandidateTerm[i].Length; j++)
                        sw.Write("{0} ", listCandidateTerm[i][j]);
                    sw.WriteLine();
                }
            }
        }
        public void Make6CandidateTerm()
        {
            /*
            foreach (var item in arrPhiJTPlus1Query.Where(kvp => kvp.Value == 0).ToList())
            {
                arrPhiJTPlus1Query.Remove(item.Key);
            }*/
            var arrPhiJTPlus1QueryRanked = (from pair in arrPhiJTPlus1Query orderby pair.Value descending select pair).ToList();
            foreach (KeyValuePair<string, double> kvp in arrPhiJTPlus1QueryRanked)
            {
                if (arrPhiJTPlus1QueryRanked.Count > 5)
                {
                    if (candidateTerm.Count < 6)
                        candidateTerm.Add(kvp.Key, kvp.Value);
                }
                else
                    candidateTerm.Add(kvp.Key, kvp.Value);
            }
            string[] T = new string[candidateTerm.Count];
            int i = 0;
            foreach (KeyValuePair<string, double> kvp in candidateTerm)
            {
                T[i] = kvp.Key;
                i++;
            }
            if (candidateTerm.Count > 5)
            {
                // 1 kata
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term3 = new string[1];
                term3[0] = T[2];
                string[] term4 = new string[1];
                term4[0] = T[3];
                string[] term5 = new string[1];
                term5[0] = T[4];
                string[] term6 = new string[1];
                term6[0] = T[5];
                // 2 kata
                string[] term12 = term1.Concat(term2).ToArray();
                string[] term13 = term1.Concat(term3).ToArray();
                string[] term14 = term1.Concat(term4).ToArray();
                string[] term15 = term1.Concat(term5).ToArray();
                string[] term16 = term1.Concat(term6).ToArray();
                string[] term23 = term2.Concat(term3).ToArray();
                string[] term24 = term2.Concat(term4).ToArray();
                string[] term25 = term2.Concat(term5).ToArray();
                string[] term26 = term2.Concat(term6).ToArray();
                string[] term34 = term3.Concat(term4).ToArray();
                string[] term35 = term3.Concat(term5).ToArray();
                string[] term36 = term3.Concat(term6).ToArray();
                string[] term45 = term4.Concat(term5).ToArray();
                string[] term46 = term4.Concat(term6).ToArray();
                string[] term56 = term5.Concat(term6).ToArray();
                // 3 kata
                string[] term123 = term12.Concat(term3).ToArray();
                string[] term124 = term12.Concat(term4).ToArray();
                string[] term125 = term12.Concat(term5).ToArray();
                string[] term126 = term12.Concat(term6).ToArray();
                string[] term134 = term13.Concat(term4).ToArray();
                string[] term135 = term13.Concat(term5).ToArray();
                string[] term136 = term13.Concat(term6).ToArray();
                string[] term145 = term14.Concat(term5).ToArray();
                string[] term146 = term14.Concat(term6).ToArray();
                string[] term156 = term15.Concat(term6).ToArray();
                string[] term234 = term23.Concat(term4).ToArray();
                string[] term235 = term23.Concat(term5).ToArray();
                string[] term236 = term23.Concat(term6).ToArray();
                string[] term245 = term24.Concat(term5).ToArray();
                string[] term246 = term24.Concat(term6).ToArray();
                string[] term256 = term25.Concat(term6).ToArray();
                string[] term345 = term34.Concat(term5).ToArray();
                string[] term346 = term34.Concat(term6).ToArray();
                string[] term356 = term35.Concat(term6).ToArray();
                string[] term456 = term45.Concat(term6).ToArray();
                // 4 kata
                string[] term1234 = term123.Concat(term4).ToArray();
                string[] term1235 = term123.Concat(term5).ToArray();
                string[] term1236 = term123.Concat(term6).ToArray();
                string[] term1345 = term134.Concat(term5).ToArray();
                string[] term1346 = term134.Concat(term6).ToArray();
                string[] term1356 = term135.Concat(term6).ToArray();
                string[] term1456 = term145.Concat(term6).ToArray();
                string[] term2345 = term234.Concat(term5).ToArray();
                string[] term2346 = term234.Concat(term6).ToArray();
                string[] term2356 = term235.Concat(term6).ToArray();
                string[] term2456 = term245.Concat(term6).ToArray();
                string[] term3456 = term345.Concat(term6).ToArray();
                // 5 kata
                string[] term12345 = term1234.Concat(term5).ToArray();
                string[] term12346 = term1234.Concat(term6).ToArray();
                string[] term23456 = term2345.Concat(term6).ToArray();
                // 6 kata
                string[] term123456 = term12345.Concat(term6).ToArray();
                // 1 kata
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term3);
                listCandidateTerm.Add(term4);
                listCandidateTerm.Add(term5);
                listCandidateTerm.Add(term6);
                // 2 kata
                listCandidateTerm.Add(term12);
                listCandidateTerm.Add(term13);
                listCandidateTerm.Add(term14);
                listCandidateTerm.Add(term15);
                listCandidateTerm.Add(term16);
                listCandidateTerm.Add(term23);
                listCandidateTerm.Add(term24);
                listCandidateTerm.Add(term25);
                listCandidateTerm.Add(term26);
                listCandidateTerm.Add(term34);
                listCandidateTerm.Add(term35);
                listCandidateTerm.Add(term36);
                listCandidateTerm.Add(term45);
                listCandidateTerm.Add(term46);
                listCandidateTerm.Add(term56);
                // 3 kata
                listCandidateTerm.Add(term123);
                listCandidateTerm.Add(term124);
                listCandidateTerm.Add(term125);
                listCandidateTerm.Add(term126);
                listCandidateTerm.Add(term134);
                listCandidateTerm.Add(term135);
                listCandidateTerm.Add(term136);
                listCandidateTerm.Add(term145);
                listCandidateTerm.Add(term146);
                listCandidateTerm.Add(term156);
                listCandidateTerm.Add(term234);
                listCandidateTerm.Add(term235);
                listCandidateTerm.Add(term236);
                listCandidateTerm.Add(term245);
                listCandidateTerm.Add(term246);
                listCandidateTerm.Add(term256);
                listCandidateTerm.Add(term345);
                listCandidateTerm.Add(term346);
                listCandidateTerm.Add(term356);
                listCandidateTerm.Add(term456);
                // 4 kata
                listCandidateTerm.Add(term1234);
                listCandidateTerm.Add(term1235);
                listCandidateTerm.Add(term1236);
                listCandidateTerm.Add(term1345);
                listCandidateTerm.Add(term1346);
                listCandidateTerm.Add(term1356);
                listCandidateTerm.Add(term1456);
                listCandidateTerm.Add(term2345);
                listCandidateTerm.Add(term2346);
                listCandidateTerm.Add(term2356);
                listCandidateTerm.Add(term2456);
                listCandidateTerm.Add(term3456);
                // 5 kata
                listCandidateTerm.Add(term12345);
                listCandidateTerm.Add(term12346);
                listCandidateTerm.Add(term23456);
                // 6 kata
                listCandidateTerm.Add(term123456);
            }
            else if (candidateTerm.Count == 5)
            {
                // 1 kata
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term3 = new string[1];
                term3[0] = T[2];
                string[] term4 = new string[1];
                term4[0] = T[3];
                string[] term5 = new string[1];
                term5[0] = T[4];
                // 2 kata
                string[] term12 = term1.Concat(term2).ToArray();
                string[] term13 = term1.Concat(term3).ToArray();
                string[] term14 = term1.Concat(term4).ToArray();
                string[] term15 = term1.Concat(term5).ToArray();
                string[] term23 = term2.Concat(term3).ToArray();
                string[] term24 = term2.Concat(term4).ToArray();
                string[] term25 = term2.Concat(term5).ToArray();
                string[] term34 = term3.Concat(term4).ToArray();
                string[] term35 = term3.Concat(term5).ToArray();
                string[] term45 = term4.Concat(term5).ToArray();
                // 3 kata
                string[] term123 = term12.Concat(term3).ToArray();
                string[] term124 = term12.Concat(term4).ToArray();
                string[] term125 = term12.Concat(term5).ToArray();
                string[] term134 = term13.Concat(term4).ToArray();
                string[] term135 = term13.Concat(term5).ToArray();
                string[] term145 = term14.Concat(term5).ToArray();
                string[] term234 = term23.Concat(term4).ToArray();
                string[] term235 = term23.Concat(term5).ToArray();
                string[] term245 = term24.Concat(term5).ToArray();
                string[] term345 = term34.Concat(term5).ToArray();
                // 4 kata
                string[] term1234 = term123.Concat(term4).ToArray();
                string[] term1235 = term123.Concat(term5).ToArray();
                string[] term1345 = term134.Concat(term5).ToArray();
                string[] term2345 = term234.Concat(term5).ToArray();
                // 5 kata
                string[] term12345 = term1234.Concat(term5).ToArray();
                // 1 kata
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term3);
                listCandidateTerm.Add(term4);
                listCandidateTerm.Add(term5);
                // 2 kata
                listCandidateTerm.Add(term12);
                listCandidateTerm.Add(term13);
                listCandidateTerm.Add(term14);
                listCandidateTerm.Add(term15);
                listCandidateTerm.Add(term23);
                listCandidateTerm.Add(term24);
                listCandidateTerm.Add(term25);
                listCandidateTerm.Add(term34);
                listCandidateTerm.Add(term35);
                listCandidateTerm.Add(term45);
                // 3 kata
                listCandidateTerm.Add(term123);
                listCandidateTerm.Add(term124);
                listCandidateTerm.Add(term125);
                listCandidateTerm.Add(term134);
                listCandidateTerm.Add(term135);
                listCandidateTerm.Add(term145);
                listCandidateTerm.Add(term234);
                listCandidateTerm.Add(term235);
                listCandidateTerm.Add(term245);
                listCandidateTerm.Add(term345);
                // 4 kata
                listCandidateTerm.Add(term1234);
                listCandidateTerm.Add(term1235);
                listCandidateTerm.Add(term1345);
                listCandidateTerm.Add(term2345);
                // 5 kata
                listCandidateTerm.Add(term12345);
            }
            else if (candidateTerm.Count == 4)
            {
                // 1 kata
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term3 = new string[1];
                term3[0] = T[2];
                string[] term4 = new string[1];
                term4[0] = T[3];
                // 2 kata
                string[] term12 = term1.Concat(term2).ToArray();
                string[] term13 = term1.Concat(term3).ToArray();
                string[] term14 = term1.Concat(term4).ToArray();
                string[] term23 = term2.Concat(term3).ToArray();
                string[] term24 = term2.Concat(term4).ToArray();
                string[] term34 = term3.Concat(term4).ToArray();
                // 3 kata
                string[] term123 = term12.Concat(term3).ToArray();
                string[] term124 = term12.Concat(term4).ToArray();
                string[] term134 = term13.Concat(term4).ToArray();
                string[] term234 = term23.Concat(term4).ToArray();
                // 4 kata
                string[] term1234 = term123.Concat(term4).ToArray();
                // 1 kata
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term3);
                listCandidateTerm.Add(term4);
                // 2 kata
                listCandidateTerm.Add(term12);
                listCandidateTerm.Add(term13);
                listCandidateTerm.Add(term14);
                listCandidateTerm.Add(term23);
                listCandidateTerm.Add(term24);
                listCandidateTerm.Add(term34);
                // 3 kata
                listCandidateTerm.Add(term123);
                listCandidateTerm.Add(term124);
                listCandidateTerm.Add(term134);
                listCandidateTerm.Add(term234);
                // 4 kata
                listCandidateTerm.Add(term1234);
            }
            else if (candidateTerm.Count == 3)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term3 = new string[1];
                term3[0] = T[2];
                string[] term12 = term1.Concat(term2).ToArray();
                string[] term13 = term1.Concat(term3).ToArray();
                string[] term23 = term2.Concat(term3).ToArray();
                string[] term33 = term12.Concat(term3).ToArray();
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term3);
                listCandidateTerm.Add(term12);
                listCandidateTerm.Add(term13);
                listCandidateTerm.Add(term23);
                listCandidateTerm.Add(term33);
            }
            else if (candidateTerm.Count == 2)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                string[] term2 = new string[1];
                term2[0] = T[1];
                string[] term12 = term1.Concat(term2).ToArray();
                listCandidateTerm.Add(term1);
                listCandidateTerm.Add(term2);
                listCandidateTerm.Add(term12);
            }
            else if (candidateTerm.Count == 1)
            {
                string[] term1 = new string[1];
                term1[0] = T[0];
                listCandidateTerm.Add(term1);
            }
        }
        public void WeightingVertex(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            for (int j = 0; j < stem.Count(); j++)
                stem[j].setWeight(ComputeWeightVertex(stem[j].getData(), cd, cq, noQuery));
        }
        public void WriteWeightVertex(int noQuery)
        {
            SortedDictionary<string, double> tempDict = new SortedDictionary<string, double>();
            for (int i = 0; i < stem.Length; i++)
                tempDict.Add(stem[i].getData(), stem[i].getWeight());
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Weight Node factor s.txt"))
            {
                sw.WriteLine("No Query = {0}", noQuery);
                foreach(KeyValuePair<string, double> kvp in tempDict)
                    sw.WriteLine("Node {0} | Weight Vertex = {1}", kvp.Key, kvp.Value);
            }
        }
        public double ComputeScoreTermQuery(string[] term)
        {
            double S = 0;
            foreach (string w in term)
                S += affinityScoreTermQuery[w];
            return (S / term.Length);
        }
        public double ComputeFactorZ(CollectionDocument cd, string[] word)
        {
            //To avoid a bias towards longer terms, a term x is scored by averaging the affinity scores
            // for its component words {w1,[..wn]}
            // term rank is determined by the average score multiplied by a factor z(x)
            // that represents the degree to which the term is discriminative in a collection:
            // z(x) = f(x(e)) * idf(x(e)) * l(x); f(x(e)) = frequency of x(e) in C; idf(x(e)) is defined analogously to idf(w(n))
            // r(i,j) = log2(sigma(c(i,j)w2 / (1+c(i,j)w2)); sigma ij from N
            // idf(w(n) = log2(panjang C / (1 + dfwn))
            // idf(w(n)) = log2(sigma(c(x(e)) / (1+c(x(e)))
            // x(e) = approximity expression such that the component words of x appear in an unordered window of size W = 4 per word
            // a term with 2 words appears in an 8-word window
            // a term with 3 words appears in a 12-word window
            // l(x) = exponential weighting factor proposed for the normalization of ngram frequencies during query segmentation
            // this factor favors longer ngrams that tend to occur less frequently in text
            // multiplication of ngram counts by l(x) enables comparison of counts for terms of varying length
            // |x| = number of words in x; l(x) = |x|^|x|
            //Console.WriteLine("word = {0}", word);
            int fXe = CountWordOccurenceIn4Window(word);
            int C = ComputeNUniqueStemmedWordinDoc(cd);
            int tempDf = 0;
            foreach (string s in word)
            {
                if (!cd.getDf().ContainsKey(s)) { }
                else
                    tempDf += cd.getDf()[s];
            }
            int sigmaDfX = tempDf;
            //int sigmaCXe = listStemCoOccur4.Sum();
            double IDFXe;
            /*
            if (fXe == 0)
                IDFXe = 0;
            else*/
            IDFXe = Math.Log((double)C / (1 + sigmaDfX), 2);
            //IDFXe = Math.Log((double)C / (1 + fXe), 2);
            //IDFXe = Math.Log((double)sigmaCXe / (1 + fXe), 2);
            int lengthX = word.Length;
            int lX = (int)Math.Pow(lengthX, lengthX);
            /*
            Console.WriteLine("fXe = {0}", fXe);
            Console.WriteLine("IDFXe = {0}", IDFXe);
            Console.WriteLine("lX = {0}", lX);*/
            /*foreach (string s in word)
                Console.Write("{0} ", s);
            Console.WriteLine();
            Console.WriteLine("fXe = {0}", fXe);*/
            double factorZ = fXe * IDFXe * lX;
            //Console.WriteLine("factor Z = {0}", factorZ);
            return factorZ;
        }
        public int frequencyWordInN(string word, CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            int nWord = 0;
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                for (int j = 0; j < cd.getTermDocID()[i].Count(); j++)
                {
                    if (word == cd.getTermDocID()[i][j])
                        nWord++;
                }
            }
            return nWord;
        }
        public int MaximumAverageFrequencyinN(CollectionDocument cq, CollectionDocument cd, int noQuery)
        {
            List<int>[] wordFrequency = new List<int>[cq.getListPseudoRelDoc()[noQuery].Count];
            int j = 0;
            foreach(int i in cq.getListPseudoRelDoc()[noQuery])
            {
                wordFrequency[j] = new List<int>();
                foreach (KeyValuePair<string, int> kvp in cd.getNoDocTermTF()[i])
                    wordFrequency[j].Add(kvp.Value);
                j++;
            }
            List<int> maxFrequency = new List<int>();
            for(int i=0; i < cq.getListPseudoRelDoc()[noQuery].Count; i++)
                maxFrequency.Add(wordFrequency[i].Max());
            return (maxFrequency.Sum() / cq.getListPseudoRelDoc()[noQuery].Count);
        }
        public double SumWeightEdge()
        {
            List<double> listWeight = new List<double>();
            for (int i = 0; i < listEdgeStem.Count; i++)
                listWeight.Add(listEdgeStem[i].getWeight());
            return listWeight.Sum();
        }
        public void MakeArrPhiJTPlus1Positive()
        {
            double[] temp = new double[arrPhiJTPlus1.Length];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = arrPhiJTPlus1[i] * (-1);
            arrPhiJTPlus1 = temp;
        }
        public void MakeAffinityScoreDictionary()
        {
            for (int i = 0; i < stem.Length; i++)
            {
                //Console.WriteLine("arrPhiJTPlus1[{0}] = {1}", i, arrPhiJTPlus1[i]);
                affinityScoreTerm.Add(stem[i].getData(), arrPhiJTPlus1[i]);
            }
        }
        public void MakeAffinityQueryScoreDictionary()
        {
            for (int i = 0; i < queryStemmedWord.Count; i++)
            {
                if (arrPhiJTPlus1Query.ContainsKey(queryStemmedWord[i]))
                {
                   // if (arrPhiJTPlus1Query[queryStemmedWord[i]] == 0) { }
                    //else
                        affinityScoreTermQuery.Add(queryStemmedWord[i], arrPhiJTPlus1Query[queryStemmedWord[i]]);
                }
            }
        }
        public void WriteRankedAffinityScoreQueryDictionary(int noQuery)
        {
            /*
            foreach (var item in arrPhiJTPlus1Query.Where(kvp => kvp.Value == 0).ToList())
            {
                arrPhiJTPlus1Query.Remove(item.Key);
            }*/
            var arrPhiJTPlus1QueryRanked = (from pair in arrPhiJTPlus1Query orderby pair.Value descending select pair).ToList();
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Ranked Affinity Query Score.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                foreach (KeyValuePair<string, double> kvp in arrPhiJTPlus1QueryRanked)
                    sw.WriteLine("Node {0} | Affinity Score = {1}", kvp.Key, kvp.Value);
            }
        }   
        public void PrintWeightEdge()
        {
            int j = 1;
            for (int i = 0; i < listEdgeStem.Count; i++)
            {
                Console.WriteLine("{3} Weight Edge ({0},{1}) = {2}", listEdgeStem[i].getNode1().getData(), listEdgeStem[i].getNode2().getData(), listEdgeStem[i].getWeight(), j);
                j++;
            }
        }
        public void WriteWeightEdge(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Weight Edge.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                for (int i = 0; i < listEdgeStem.Count; i++)
                    sw.WriteLine("Edge ({0},{1}) | Weight Edge = {2}", listEdgeStem[i].getNode1().getData(), listEdgeStem[i].getNode2().getData(), listEdgeStem[i].getWeight());                   
            }
        }
        public void PrintWeightVertex()
        {
            int j = 1;
            for (int i = 0; i < stem.Count(); i++)
            {
                Console.WriteLine("{2} Weight Vertex ({0}) = {1}", stem[i].getData(), stem[i].getWeight(), j);
                j++;
            }
        }
        public void PrintAffinityGraph(CollectionDocument cq, int noQuery)
        {
            Console.WriteLine("Top-k pseudo relevant document:");
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
                Console.WriteLine(i);
            Console.WriteLine("Jumlah node = {0}", stem.Length);
            Console.WriteLine("Node Graph: ");
            string[] arrString = new string[stem.Length];
            for (int i = 0; i < stem.Length; i++)
                arrString[i] = stem[i].getData();
            // for (int i=0;i<stem.Length;i++)
            // Console.Write("{0} ", stem[i].getData());
            //Array.Sort(arrString);
            for (int i = 0; i < arrString.Length; i++)
                Console.WriteLine("{0} {1}", i+1, arrString[i]);
            Console.WriteLine();
            Console.WriteLine("Jumlah edge = {0}", listEdgeStem.Count);
            Console.WriteLine("Edge Graph:");
            for(int i = 0; i < listEdgeStem.Count; i++)
                Console.WriteLine("{0} {1} {2}", i+1, listEdgeStem[i].getNode1().getData(), listEdgeStem[i].getNode2().getData());
        }
        public void PrintProbabilityMatrix()
        {
            for(int i = 0; i < uniqueStemmedWord.Count; i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < uniqueStemmedWord.Count; j++)
                {
                    Console.Write("{0}", probabilityMatrix[i, j]/*.ToString("0.###")*/);
                    if (j < (uniqueStemmedWord.Count - 1))
                        Console.Write(", ");
                }
                Console.WriteLine("]");
            }
        }
        public void WriteProbabilityMatrix(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Matrix Probability.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                for (int i = 0; i < uniqueStemmedWord.Count; i++)
                {
                    sw.Write("[ ");
                    for (int j = 0; j < uniqueStemmedWord.Count; j++)
                    {
                        sw.Write("{0}", probabilityMatrix[i, j]);
                        if (j < (uniqueStemmedWord.Count - 1))
                            sw.Write(", ");
                    }
                    sw.WriteLine("]");
                }
            }
        }
        public void WriteNormalizeProbabilityMatrix(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Normalized Matrix Probability.txt"))
            {
                sw.WriteLine("Nomor Query = {0}", noQuery);
                for (int i = 0; i < uniqueStemmedWord.Count; i++)
                {
                    sw.Write("[ ");
                    for (int j = 0; j < uniqueStemmedWord.Count; j++)
                    {
                        sw.Write("{0}", probabilityMatrix[i, j]);
                        if (j < (uniqueStemmedWord.Count - 1))
                            sw.Write(", ");
                    }
                    sw.WriteLine("]");
                }
            }
        }
        public void PrintUniqueStemmedWordInN()
        {
            string[] arrString = new string[uniqueStemmedWord.Count];
            arrString = uniqueStemmedWord.ToArray();
            //Array.Sort(arrString);
            int i = 1;
            foreach (string s in arrString)
            {
                Console.WriteLine("{0} {1}", i, s);
                i++;
            }
        }
        public void PrintUniqueStemmedWordInDoc()
        {
            foreach (string s in uniqueStem)
                Console.WriteLine(s);
        }
        public void PrintAffinityScore(double[] arrPhiJTPlus1_)
        {
            for (int i = 0; i < arrPhiJTPlus1_.Length; i++)
                Console.WriteLine("arrPhiJTPlus1[{0}] = {1}", i, arrPhiJTPlus1_[i]);
        }
        public void PrintCandidateTerm()
        {
            foreach(KeyValuePair<string,double>kvp in candidateTerm)
                Console.WriteLine("Term {0} , Score = {1}", kvp.Key, kvp.Value);
        }
        public void PrintRankedCandidateTerm()
        {
            foreach (KeyValuePair<string[], double> kvp in rankedCandidateTermScore)
            {
                Console.Write("Candidate term = ");
                foreach (string s in kvp.Key)
                {
                    Console.Write("{0} ", s);
                }
                Console.WriteLine(", Score = {0}", kvp.Value);
            }
        }
        public void PrintChoosenTerm(int noQuery)
        {
            Console.Write("Term yang dipilih untuk query {0}: ", noQuery);
            if (choosenTerm.Equals(default(KeyValuePair<string[], double>)))
                Console.WriteLine(" ");
            else
            {
                foreach (string s in choosenTerm.Key)
                    Console.Write("{0} ", s);
                Console.WriteLine();
            }
        }
        public void WriteChoosenTerm(int noQuery)
        {
            using (StreamWriter sw = File.AppendText(@"C: \Users\Mochamad Lutfi F\Documents\Visual Studio 2015\Projects\ConsoleApplication11\output\Query\Reformulated Query.txt"))
            {
                sw.Write("Term yang dipilih untuk query {0}: ", noQuery);
                if (choosenTerm.Equals(default(KeyValuePair<string[], double>)))
                    sw.WriteLine(" ");
                else
                {
                    foreach (string s in choosenTerm.Key)
                        sw.Write("{0} ", s);
                    sw.WriteLine();
                }
            }
        }
        public void PrintDocRel(CollectionDocument cq, int noQuery)
        {
            Console.Write("Dokumen relevan : ");
            for (int i = 0; i < cq.getListPseudoRelDoc()[noQuery].Count; i++)
                Console.Write("{0} ", cq.getListPseudoRelDoc()[noQuery][i]);
            Console.WriteLine();
        }
        public void Clearing(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            cq.getDocRelFound().Remove(0);
            cd.getTermDocID().Remove(0);
            cq.getListPseudoRelDoc()[noQuery].Remove(0);
            cq.getListPseudoRelDocWithoutQ().Clear();
            uniqueStemmedWord.Clear();
            queryStemmedWord.Clear();
            wordDocNumberNew.Clear();
            dfNew.Clear();
            uniqueStem.Clear();
            stemmedWordDocRelDict.Clear();
            weightEdgeDict.Clear();
            listEdgeStem.Clear();
            listStemCoOccur2.Clear();
            listStemCoOccur4.Clear();
            arrPhiJTPlus1Query.Clear();
            affinityScoreTerm.Clear();
            affinityScoreTermQuery.Clear();
            candidateTerm.Clear();
            candidateTermScore.Clear();
            listCandidateTerm.Clear();
            listScoreCandidateTerm.Clear();
            rankedCandidateTermScore.Clear();
            choosenTerm = new KeyValuePair<string[], double>();
        }
    }
}
