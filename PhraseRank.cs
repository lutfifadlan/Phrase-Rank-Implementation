using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int stemCoOccur10;
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
        public void PhRankAlgorithm(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            addQtoN.AddQueryToN(cd, cq, noQuery);
            //PrintDocRel(cq,noQuery);
            MakeUniqueStemmedWord(cq, cd, noQuery);
            CreateAffinityGraph(cd, cq, noQuery);
            //PrintProbabilityMatrix();
           // PrintAffinityGraph(cq, noQuery);
            RankingTerm(cd,cq, noQuery);
            PrintChoosenTerm(noQuery);
            Clearing(cd, cq, noQuery);
        }
        public void CreateAffinityGraph(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            CreateVertexGraph();
            //Console.WriteLine("a");
            CreateEdgeGraphAndWeightingEdge(cd, cq, noQuery);
            //Console.WriteLine("b");
            MakeEdgeStemUnique(); // Delete edge with same node
            //Console.WriteLine("c");
            //CreateProbabilityMatrix(cd, cq, noQuery);
            //Console.WriteLine("d");
            RandomWalk();
            //Console.WriteLine("e");
            MakeCandidateTerm();
            //Console.WriteLine("f");
            //PrintAffinityScore();
            //WeightingVertex(cd, cq, noQuery);
            //Console.WriteLine("g");
        }
        public void RankingTerm(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {

            for (int i = 0; i < listCandidateTerm.Count; i++)
            {
                //f(x,Q) =(rank) z(x) * sigma(w(n) dari x)phi(w(n)) / n
                //termWt = termWt * ComputeFactorZ(cd, T);
                double termWt = ComputeScoreTermQuery(listCandidateTerm[i]);
                termWt = termWt * ComputeFactorZ(cd, listCandidateTerm[i]);
                listScoreCandidateTerm.Add(termWt);
                candidateTermScore.Add(listCandidateTerm[i], termWt);
            }
            //PrintRankedCandidateTerm();
            rankedCandidateTermScore = (from pair in candidateTermScore orderby pair.Value descending select pair).ToList();
            if (rankedCandidateTermScore.Count > 0)
            {
                choosenTerm = rankedCandidateTermScore.First();
                //PrintChoosenTerm(noQuery);
                // add reformulated query to new collection query
                cq.getTermQueryID().Remove(noQuery);
                //Console.WriteLine("r");
                cq.getTermQueryID().Add(noQuery, choosenTerm.Key);
            }
            else
            {
                Console.WriteLine("asup dieu");
                choosenTerm = new KeyValuePair<string[], double>();
            }
            //Console.WriteLine("add");
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
        public int ComputeNUniqueStemmedWordinDoc(CollectionDocument cd)
        {
            for(int i = 1; i < cd.getNTuple(); i++)
            {
                string s = cd.getWordDictStemmed()[i];
                string[] arrayWord = s.Split(new string[] { " ", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                string[] uniqueStemWordDocument = arrayWord.Distinct().ToArray();
                foreach (string es in uniqueStemWordDocument)
                    uniqueStem.Add(es);
            }
            return uniqueStem.Count;
        }
        /*
        public bool isAdjacent(CollectionDocument cd, string stem1, string stem2, int noPseudoRelDoc)
        {
            int tempPos1;
            int tempPos2;
            int i = 0;
            int j = 0;
            List<int> listTempPos1 = new List<int>();
            List<int> listTempPos2 = new List<int>();
            foreach (KeyValuePair<string, List<int>> kvp in cd.stemmedWordPositionDocument[noPseudoRelDoc])
            {
                if (kvp.Key == stem1)
                {
                    if (kvp.Value.Count == 1)
                    {
                        tempPos1 = kvp.Value[0];
                        listTempPos1.Add(kvp.Value[0]);
                    }
                    else
                    {
                        i = kvp.Value.Count;
                        foreach (int k in kvp.Value)
                            listTempPos1.Add(k);
                    }
                }
                else if (kvp.Key == stem2)
                {
                    if (kvp.Value.Count == 1)
                    {
                        tempPos2 = kvp.Value[0];
                        listTempPos2.Add(kvp.Value[0]);
                    }
                    else
                    {
                        j = kvp.Value.Count;
                        foreach (int k in kvp.Value)
                            listTempPos2.Add(k);
                    }
                }
            }
            foreach (int k in listTempPos1)
            {
                foreach (int m in listTempPos2)
                {
                    if ((k - m) == -1 || (k - m) == 1)
                        return true;
                }
            }
            return false;
        }*/
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
                            if (cd.listAdjacentDictionary[noQuery][j].Contains(adj))
                            {
                                //Console.WriteLine("asup");
                                edgeStem = new Edge<string>();
                                edgeStem.setNoStem1(m);
                                edgeStem.setNode1(stem[m]);
                                edgeStem.setNoStem2(k);
                                edgeStem.setNode2(stem[k]);
                                double w = WeightingEdge(cd, cq, edgeStem, edgeStem.getNode1().getData(), edgeStem.getNode2().getData(), noQuery);
                                edgeStem.setWeight(w);
                                listEdgeStem.Add(edgeStem);
                                probabilityMatrix[m, k] = w;
                                probabilityMatrix[k, m] = w;
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
        public void CountStemCoOccurrence(CollectionDocument cd, CollectionDocument cq, string word1, string word2, int noQuery)
        {
            // unordered window
            listStemCoOccur2.Clear();
            int sizeUw2,sizeUw10, sizeWindow;
            stemCoOccur2 = 0;
            stemCoOccur10 = 0;
            int stemCoOccur2inDoc = 0;
            int tempStemCoOccur2 = 0;
            //int tempNWord1 = 0;
            //int tempNWord2 = 0;
            int tempNWord1in2W = 0;
            int tempNWord2in2W = 0;
            int tempNWord1in10W = 0;
            int tempNWord2in10W = 0;
            //Console.WriteLine("word1 = {0}", word1);
            //Console.WriteLine("word2 = {0}", word2);
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                sizeWindow = cd.getSizeWindowDocument()[i];
                sizeUw2 = sizeWindow / 2;
                sizeUw10 = sizeWindow / 10;
                int j = 1;
                /*
                foreach (string s in cd.getTermDocID()[i])
                {
                    if (j % 2 == 0)
                        if (s == word1 || s == word2)
                            stemCoOccur2++;
                    if(j%10 == 0)
                        if (s == word1 || s == word2)
                            stemCoOccur10++;
                    j++;
                }*/
                stemCoOccur2inDoc = 0;
                for(int k = 0; k<cd.getTermDocID()[i].Length;k++)
                {
                    if (cd.getTermDocID()[i][k] == word1)
                    {
                        //tempNWord1++;
                        tempNWord1in2W++;
                        tempNWord1in10W++;
                    }
                    if (cd.getTermDocID()[i][k] == word2)
                    {
                        //tempNWord2++;
                        tempNWord2in2W++;
                        tempNWord2in10W++;
                    }
                    if (j%2 == 0)
                    {
                        if (tempNWord1in2W > 0 && tempNWord2in2W > 0)
                        {
                            //Console.WriteLine("edan");
                            stemCoOccur2inDoc++;
                            stemCoOccur2++;
                        }
                        tempNWord1in2W = 0;
                        tempNWord2in2W = 0;
                    }
                    if(j%10 == 0)
                    {
                        if (tempNWord1in10W > 0 && tempNWord2in10W > 0)
                            stemCoOccur10++;
                        tempNWord1in10W = 0;
                        tempNWord2in10W = 0;
                    }
                    /*
                    if (tempNWord1 > 0 && tempNWord2 > 0)
                    {
                        if (j % 2 == 0)
                            stemCoOccur2++;
                        if (j % 10 == 0)
                            stemCoOccur10++;
                        tempNWord1 = 0;
                        tempNWord2 = 0;
                    }*/
                    j++;
                }
                //tempStemCoOccur2 = stemCoOccur2;
                tempStemCoOccur2 = stemCoOccur2inDoc;
                listStemCoOccur2.Add(tempStemCoOccur2);
            }
            //listStemCoOccur2.Clear();
            
        }
        public int CountWordOccurenceIn4Window(CollectionDocument cd, string[] word)
        {
            for (int i=1; i <= cd.getNTuple(); i++)
            {
                stemCoOccur4inD = 0;
                //int sizeUw4,sizeWindow;
                int tempStemCoOccur4inD = 0;
                //sizeWindow = cd.getSizeWindowDocument()[i];
                //sizeUw4 = sizeWindow / 4;
                int j = 0;
                if (word.Length == 1)
                {
                    foreach (string s in cd.getTermDocID()[i])
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
                    foreach (string s in cd.getTermDocID()[i])
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
                    foreach (string s in cd.getTermDocID()[i])
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
        }
        public double CountProbabilityOfDocument(CollectionDocument cd, CollectionDocument cq,/* int noDoc,*/ string stemI, string stemJ, int noQuery)
        {
            // count probability of document in which stem i and j co-occur given Q
            // compute without lamda (smoothing parameter)
            /*
            int nStringDocument = cd.getTermDocID()[noDoc].Length;
            int nStringQuery = cd.getTermDocID()[0].Length;
            int nStringDocQuery = nStringDocument + nStringQuery;*/
            int occurenceStemIJAtPseudoDoc = 0;
            double probabilityStemIJ = 0;
            /*
            Console.WriteLine("nStringDocument = {0}", nStringDocument);
            Console.WriteLine("nStringQuery = {0}", nStringQuery);
            Console.WriteLine("nStringDocQuery = {0}", nStringDocQuery);*/
            /*
            int nStemIAtDoc = 0;
            int nStemIAtQuery = 0;
            int nStemJAtDoc = 0;
            int nStemJAtQuery = 0;
            /*
            double probabilityStemI = 0;
            double probabilityStemJ = 0;
            double probabilityStemIJ = 0;
            for(int i=0; i<nStringDocument; i++)
            {
                if (cd.getTermDocID()[noDoc][i] == stemI)
                    nStemIAtDoc++;
                if (cd.getTermDocID()[noDoc][i] == stemJ)
                    nStemJAtDoc++;
            }
            for(int i=0; i < nStringQuery; i++)
            {
                if (cd.getTermDocID()[0][i] == stemI)
                    nStemIAtQuery++;
                if (cd.getTermDocID()[0][i] == stemJ)
                    nStemJAtQuery++;
            }
            /*
            Console.WriteLine("nStemIAtDoc = {0}", nStemIAtDoc);
            Console.WriteLine("nStemJAtDoc = {0}", nStemJAtDoc);
            Console.WriteLine("nStemIAtQuery = {0}", nStemIAtQuery);
            Console.WriteLine("nStemJAtQuery = {0}", nStemJAtQuery);*/
            //probabilityStemI = (nStemIAtQuery/(double)nStringQuery) + (nStemIAtDoc/(double)nStringDocQuery);
            //probabilityStemJ = (nStemJAtQuery/(double)nStringQuery) + (nStemJAtDoc/(double)nStringDocQuery);
            /*
            Console.WriteLine("probabilityStemI = {0}", probabilityStemI);
            Console.WriteLine("probabilityStemJ = {0}", probabilityStemJ);*/
            //probabilityStemIJ = probabilityStemI * probabilityStemJ;
            //Console.WriteLine("probabilityStemIJ = {0}", probabilityStemIJ);
            //return probabilityStemIJ;
            int nstemI;
            int nstemJ;
          //  Console.WriteLine("noQuery = {0}", noQuery);
            //foreach (int i in cq.getListPseudoRelDoc()[noQuery])
           // Console.WriteLine("stemI = {0}", stemI);
            //Console.WriteLine("stemJ = {0}", stemJ);
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
            return probabilityStemIJ;
        }
        public double WeightingEdge(CollectionDocument cd, CollectionDocument cq, Edge<string> edge, string word1, string word2, int noQuery)
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
            
            //Console.WriteLine("word1 = {0}", word1);
            //Console.WriteLine("word2 = {0}", word2);
            //Console.WriteLine("cc");
            CountStemCoOccurrence(cd, cq, word1, word2, noQuery);
            //Console.WriteLine("cd");
            int sigmaCW2 = listStemCoOccur2.Sum();
            
            //Console.WriteLine("listStemCoOccur2.Sum() = {0}", listStemCoOccur2.Sum());
            //Console.WriteLine("stemCoOccur2 = {0}", stemCoOccur2);
            if (sigmaCW2 == 0)
                factorR = 0;
            else
                factorR = Math.Log((double)sigmaCW2 / (1 + stemCoOccur2), 2);
           // Console.WriteLine("factorR = {0}", factorR);
            /*
            foreach (int i in cq.getListPseudoRelDoc()[noQuery])
            {
                Console.WriteLine("i = {0}", i);
                double probabilityWord1And2 = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery);
                //Console.WriteLine("probabilityWord1And2 in document {1} = {0}", probabilityWord1And2, i);
                //sigmaPCW = sigmaPCW + (probabilityWord1And2 * ((lamda * stemCoOccur2) + (1 - lamda) * stemCoOccur10));
                listProbabilityStemIJ.Add(probabilityWord1And2);
            }*/
            sigmaPCW = CountProbabilityOfDocument(cd, cq, word1, word2, noQuery)  * ((lamda * stemCoOccur2) + (1 - lamda) * stemCoOccur10);
           
            //Console.WriteLine("sigmaPCW = {0}", sigmaPCW);
            double weight = factorR * sigmaPCW;
            //edge.setWeight(weight);
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
        /*
        public void CreateProbabilityMatrix(CollectionDocument cd, CollectionDocument cq, int noQuery, int k)
        {
            probabilityMatrix = new double[uniqueStemmedWord.Count,uniqueStemmedWord.Count];
            //Console.WriteLine("listEdgeStem.Count = {0}", listEdgeStem.Count);
            Console.WriteLine("uniqueStemmedWord.Count = {0}", uniqueStemmedWord.Count);
            for (int i = 0; i < uniqueStemmedWord.Count; i++)
            {
                for(int j = 0; j < uniqueStemmedWord.Count; j++)
                {
                    if (i == j)
                        probabilityMatrix[i,j] = 0;
                    else
                    {
                       /// bool boolValue = false;
                        Adjacent adj = new Adjacent();
                        List<Adjacent> listAdj = new List<Adjacent>();
                        adj.setW1(stem[i].getData());
                        adj.setW2(stem[j].getData());
                        adj.setIsAdjacent(true);
                        //Console.WriteLine("dieu");
                        listAdj.Add(adj);
                        //if (cd.listAdjacentDictionary[noQuery].ContainsValue(listAdj))
                        if (cd.listAdjacentDictionary[noQuery].ContainsValue(listAdj//[k].Contains(adj))
                        //if (isAdjacent(cd, stem[i].getData(), stem[j].getData(), cq.getListPseudoRelDoc()[noQuery][k]) == true)
                        {
                            Console.WriteLine("asup");
                            for (int m = 0; m < listEdgeStem.Count; m++)
                            {
                                if ((listEdgeStem[m].getNoStem1() == i && listEdgeStem[m].getNoStem2() == j) ||
                                    (listEdgeStem[m].getNoStem1() == j && listEdgeStem[m].getNoStem2() == i))
                                    probabilityMatrix[i, j] = listEdgeStem[m].getWeight();
                            }
                            //      boolValue = true;
                        }
                        //for (int k = 0; k < cq.getDocRelFound().Count; k++)
                        //int k = 0;
                        //while (boolValue == false)// && (k < cq.getListPseudoRelDoc()[noQuery].Count))
                        //{
                        /*
                        foreach (int k in cq.getListPseudoRelDoc()[noQuery])
                            {
                                Console.WriteLine("k = {0}", k);
                                //List<Adjacent> listAdj = new List<Adjacent>();
                                adj.setW1(stem[i].getData());
                                adj.setW2(stem[j].getData());
                                adj.setIsAdjacent(true);
                                //Console.WriteLine("dieu");
                                //listAdj.Add(adj);
                                //if (cd.listAdjacentDictionary[noQuery].ContainsValue(listAdj))
                                if (cd.listAdjacentDictionary[noQuery][k].Contains(adj))
                                //if (isAdjacent(cd, stem[i].getData(), stem[j].getData(), cq.getListPseudoRelDoc()[noQuery][k]) == true)
                                {
                                    Console.WriteLine("asup");
                                    for (int m = 0; m < listEdgeStem.Count; m++)
                                    {
                                        if ((listEdgeStem[m].getNoStem1() == i && listEdgeStem[m].getNoStem2() == j) ||
                                            (listEdgeStem[m].getNoStem1() == j && listEdgeStem[m].getNoStem2() == i))
                                            probabilityMatrix[i, j] = listEdgeStem[m].getWeight();
                                    }
                              //      boolValue = true;
                                }
                               // Console.WriteLine("teu asup");
                                //k++;
                            //}
                            //boolValue = true;
                        }*/
                     /* 
                    }
                    //listEdgeStem[j].getNode1().getData
                    //if(stem[j].getData()
                    //probabilityMatrix[i][j] =
                }
            }
           // listEdgeStem
        }*/
        public double ComputeWeightVertex(string word, CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            //s(w(n)) = w(n)f(avg) * idf(w(n))
            //w(n)f(avg) = frequency of a word w(n) in N, averaged over k+1 documents (average frequency)
            //and normalized by the maximum average frequency of any term in N
            //idf(w(n)) = log2(|C| / (1+df(w(n))); |C| = vocabulary of stemmed words in collection C
            // df(w(n)) = number of documents in C containing w(n)
            //compute frequency of word in N
            int nWord = frequencyWordInN(word, cd, cq, noQuery);
            double wNFAvg = ((double)nWord / cq.getListPseudoRelDoc()[noQuery].Count) / MaximumAverageFrequencyinN(cq, cd, noQuery);
            int C = ComputeNUniqueStemmedWordinDoc(cd);
            int dfWn;
            if (cd.getDf().ContainsKey(word))
                dfWn = cd.getDf()[word];
            else
                dfWn = 0;
            //int C = uniqueStemmedWord.Count;
            //int dfWn = dfNew[word];
            double IDFWn = Math.Log((double)C / (1 + dfWn), 2);
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
            double[,] matrixPhiJT = new double[1, stem.Length];
            for (int ind = 0; ind < stem.Length; ind++)
                matrixPhiJT[0, ind] = 0;
            arrPhiJTPlus1 = new double[stem.Length];
            arrPhiJTPlus1Query = new Dictionary<string, double>();
            double sigmaPhiIHIJ;
            int t = 0;
            bool isExceed = true;
            // pada t = 0
            for (int i = 0; i < stem.Length; i++)
            {
                int k = 0;
                sigmaPhiIHIJ = 0;
                while (k < stem.Length)
                {
                    matrixPhiJT[0, k] = 1; //edge weights are normalized to sum to one
                    //Console.WriteLine("probabilityMatrix[k, i] = {0}", probabilityMatrix[k, i]);
                    sigmaPhiIHIJ += matrixPhiJT[0, k] * probabilityMatrix[k, i];
                    matrixPhiJT[0, k] = 0;
                    k++;
                }
                //Console.WriteLine(sigmaPhiIHIJ);
                arrPhiJTPlus1[i] = sigmaPhiIHIJ;
                matrixPhiJT[0, i] = sigmaPhiIHIJ;
                if (queryStemmedWord.Contains(stem[i].getData()))
                {
                    //Console.WriteLine("oy");
                    arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
                }
            }
           // PrintAffinityScore();
            t = 1;
            while (isExceed)
            {
                //Console.WriteLine("t = {0}", t);
                //Console.WriteLine("t = {0}", t);
                for (int i = 0; i < stem.Length; i++)
                {
                    int k = 0;
                    sigmaPhiIHIJ = 0;
                    while (k < stem.Length)
                    {
                       // Console.WriteLine("probabilityMatrix[{0}, {1}] = {2}", k, i, probabilityMatrix[k, i]);
                        //matrixPhiJT[0, k] = 1; //edge weights are normalized to sum to one
                        sigmaPhiIHIJ += matrixPhiJT[0, k] * probabilityMatrix[k, i];
                        //matrixPhiJT[0, k] = 0;
                        k++;
                    }
                    arrPhiJTPlus1[i] = sigmaPhiIHIJ;
                    //Console.WriteLine("sigmaPhiIHIJ = {0}", sigmaPhiIHIJ);
                    matrixPhiJT[0, i] = sigmaPhiIHIJ;
                    if (queryStemmedWord.Contains(stem[i].getData()))
                    {
                        //Console.WriteLine("iiy");
                        if (arrPhiJTPlus1Query.ContainsKey(stem[i].getData()))
                            arrPhiJTPlus1Query.Remove(stem[i].getData());
                        arrPhiJTPlus1Query.Add(stem[i].getData(), arrPhiJTPlus1[i]);
                    }
                }
                //MakeArrPhiJTPlus1Positive();
                isExceed = false;
                int j = 0;
                for (int i = 0; i < arrPhiJTPlus1.Length; i++)
                {
                    while (j < arrPhiJTPlus1.Length)
                    {
                        if (Math.Abs(arrPhiJTPlus1[i] - arrPhiJTPlus1[j]) > 0.0001) 
                        {
                            isExceed = true;
                            break;
                        }
                        j++;
                    }

                    if (j == arrPhiJTPlus1.Length)
                        j = 0;
                }
                //isExceed = false;
                // Console.WriteLine("nNotExceed = {0}", nNotExceed);
                // if (nNotExceed < stem.Length)
                //   nNotExceed = 0;
                t++;
            }
            //Console.WriteLine("t = {0}", t);
            MakeAffinityScoreDictionary();
            MakeAffinityQueryScoreDictionary();
            //PrintAffinityScore();
        }
        public void MakeCandidateTerm()
        {
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
        public void WeightingVertex(CollectionDocument cd, CollectionDocument cq, int noQuery)
        {
            for (int j = 0; j < stem.Count(); j++)
                stem[j].setWeight(ComputeWeightVertex(stem[j].getData(), cd, cq, noQuery));
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
            // idf(w(n)) = log2(sigma(c(x(e)) / (1+c(x(e)))
            // x(e) = approximity expression such that the component words of x appear in an unordered window of size W = 4 per word
            // a term with 2 words appears in an 8-word window
            // a term with 3 words appears in a 12-word window
            // l(x) = exponential weighting factor proposed for the normalization of ngram frequencies during query segmentation
            // this factor favors longer ngrams that tend to occur less frequently in text
            // multiplication of ngram counts by l(x) enables comparison of counts for terms of varying length
            // |x| = number of words in x; l(x) = |x|^|x|
            //Console.WriteLine("word = {0}", word);
            int fXe = CountWordOccurenceIn4Window(cd, word);
            int sigmaCXe = listStemCoOccur4.Sum();
            double IDFXe;
            if (fXe == 0)
                IDFXe = 0;
            else
                IDFXe = Math.Log((double)sigmaCXe / (1 + fXe), 2);
            int lengthX = word.Length;
            int lX = (int)Math.Pow(lengthX, lengthX);
            /*
            Console.WriteLine("fXe = {0}", fXe);
            Console.WriteLine("sigmaCXe = {0}", sigmaCXe);
            Console.WriteLine("IDFXe = {0}", IDFXe);
            Console.WriteLine("lX = {0}", lX);*/
            double factorZ = fXe * IDFXe * lX;
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
            for(int i=0; i<stem.Length;i++)
                affinityScoreTerm.Add(stem[i].getData(), arrPhiJTPlus1[i]);
        }
        public void MakeAffinityQueryScoreDictionary()
        {
            for (int i = 0; i < queryStemmedWord.Count; i++)
                affinityScoreTermQuery.Add(queryStemmedWord[i], arrPhiJTPlus1Query[queryStemmedWord[i]]);
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
