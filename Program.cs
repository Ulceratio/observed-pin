using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        //class A
        //{
        //    public virtual void Method()
        //    {
        //        Console.WriteLine("MethodA");
        //    }
        //}
        //class B : A
        //{
        //    public override void Method()
        //    {
        //        Console.WriteLine("MethodB");
        //    }
        //}
        //class C : B
        //{
        //    public new void Method()
        //    {
        //        Console.WriteLine("MethodC");
        //    }
        //}


        static void Main(string[] args)
        {

            //BruteForce bruteForce = new BruteForce();
            //bruteForce.Start("19");
            //bruteForce.Start("369");

            Trie trie = new Trie();

            string observed = "369";
            


            Stopwatch watch = Stopwatch.StartNew();

            watch.Stop();


            watch = Stopwatch.StartNew();

            var GivenSymbols = (from el in observed.ToCharArray() select GetReplacements(el)).ToList();

            foreach (var item in GivenSymbols)
            {
                trie.Add(item.Where(el => el != 'z'));
            }

            var res = trie.Print().ToList();

            watch.Stop();

            Console.WriteLine("Done. Took {0} seconds", (double)watch.ElapsedMilliseconds / 1000.0);

            //var res = trie.Print().ToList();

            res.AsParallel().ForAll(el => Console.WriteLine(el));

            //Console.WriteLine(res.Count());

            Console.ReadKey();
        }

        public static List<string> GetPINs(string observed)
        {
            return null;
        }

        //class BruteForce
        //{
        //    #region Dop 
        //    private class Symbol : ICloneable
        //    {
        //        #region Fields

        //        public List<Symbol> PossibleReplacements { get => _possibleReplacements;
        //            private set
        //            {
        //                _possibleReplacements = value;
        //                foreach (var item in _possibleReplacements)
        //                {
        //                    item.PreviousSymbol = PreviousSymbol;
        //                }
        //            }
        //        }

        //        public char IntraSymbol { get; set; }
        //        public char ForTrySymbol { get; set; }

        //        private Symbol _nextSymbol;
        //        public Symbol NextSymbol
        //        {
        //            get => _nextSymbol;
        //            set
        //            {
        //                _nextSymbol = value;
        //                PossibleReplacements = (from el in GetReplacements(_nextSymbol.IntraSymbol) orderby el where el != 'z' select new Symbol(el)).ToList();
        //            }
        //        }

        //        public Symbol PreviousSymbol;
        //        private List<Symbol> _possibleReplacements;

        //        #endregion

        //        public Symbol(char IntraSymbol)
        //        {
        //            this.IntraSymbol = IntraSymbol;
        //            //PossibleReplacements = (from el in GetReplacements() orderby el where el != 'z' select new Symbol(el)).ToList();
        //        }

        //        public Symbol()
        //        {
        //            IntraSymbol = 'z';
        //        }

        //        #region Methods

        //        public void AddToChain(Symbol symbol)
        //        {
        //            if (PossibleReplacements == null)
        //            {
        //                PossibleReplacements = (from el in GetReplacements(symbol.IntraSymbol) orderby el where el != 'z' select new Symbol(el)).ToList();
        //            }
        //            else
        //            {
        //                foreach (var item in PossibleReplacements)
        //                {
        //                    if (item.NextSymbol != null)
        //                    {
        //                        item.AddToChain(symbol);
        //                    }
        //                    else
        //                    {
        //                        item.PreviousSymbol = this;
        //                        item.NextSymbol = symbol;
        //                        item.ToString();
        //                    }
        //                }
        //            }
        //        }

        //        private IEnumerable<char> GetReplacements(char InputSymbol)
        //        {
        //            var tempForECLock = ECLock();

        //            bool exit = false;

        //            for (int i = 0; i < 4 && exit == false; i++)
        //            {
        //                for (int j = 0; j < 3 && exit == false; j++)
        //                {
        //                    if (tempForECLock[i, j] == InputSymbol)
        //                    {
        //                        for (int k = -1; k <= 1; k += 2)
        //                        {
        //                            if (i + k >= 0 && i + k < 4)
        //                            {
        //                                yield return tempForECLock[i + k, j];
        //                            }
        //                            if (j + k >= 0 && j + k < 3)
        //                            {
        //                                yield return tempForECLock[i, j + k];
        //                            }
        //                        }
        //                        yield return InputSymbol;
        //                        exit = true;
        //                    }
        //                }
        //            }
        //        }

        //        public override string ToString()
        //        {
        //            string result = "";
        //            Symbol symbol = this;
        //            if (NextSymbol == null)
        //            {
        //                while (symbol.PreviousSymbol != null)
        //                {
        //                    result += symbol.IntraSymbol;
        //                    symbol = symbol.PreviousSymbol;
        //                }
        //                result += symbol.IntraSymbol;
        //                return new string(result.Reverse().ToArray());
        //            }
        //            else
        //            {
        //                return IntraSymbol.ToString();
        //            }
        //        }

        //        public IEnumerable<string> Print()
        //        {
        //            if(NextSymbol != null || PreviousSymbol == null)
        //            {
        //                foreach (var item in PossibleReplacements)
        //                {
        //                    item.Print().ToList();
        //                }
        //            }
        //            else
        //            {
        //                yield return ToString();
        //            }
        //        }

        //        public object Clone()
        //        {
        //            return new Symbol() { IntraSymbol = IntraSymbol };
        //        }

        //        #endregion
        //    }
        //    #endregion

        //    #region Fields

        //    private List<Symbol> symbols = new List<Symbol>();

        //    private string Observed;

        //    #endregion

        //    public BruteForce()
        //    {
        //    }

        //    public BruteForce(string Observed)
        //    {
        //        this.Observed = Observed;
        //    }

        //    public void Start(string observed)
        //    {
        //        Queue<Symbol> symbols = new Queue<Symbol>();
        //        var GivenSymbols =(from el in observed.ToCharArray().AsParallel() select new Symbol(el)).ToList();
        //        var root = new Symbol();
        //        foreach (var item in GivenSymbols)
        //        {
        //            root.AddToChain(item);
        //        }

        //        var res = root.Print().ToList();
        //    }

        //}

        class TrieElement
        {
            #region Fields
            public TrieElement ParentTrie { get; set; }

            public char Symbol { get; private set; }

            public List<TrieElement> ChildTries = new List<TrieElement>();

            public bool HasChilds { get; set; }
            #endregion

            #region Constructors
            public TrieElement()
            {
            }

            public TrieElement(char symbol)
            {
                Symbol = symbol;
            }
            #endregion

            #region Methods
            public void AddChilds(IEnumerable<char> Childs,TrieElement ParentElement)
            {
                if(HasChilds)
                {

                    for (int i = 0; i < ChildTries.Count; i++)
                    {
                        ChildTries[i].AddChilds(Childs, this);
                    }

                }
                else
                {
                    ChildTries = (from el in Childs.AsParallel() select new TrieElement(el)).ToList();
                    ChildTries.AsParallel().ForAll(el => el.ParentTrie = this);
                    HasChilds = true;
                }
            }

            public IEnumerable<string> Print()
            {
                if (HasChilds)
                {
                    foreach (var item in ChildTries)
                    {
                        foreach (var item1 in item.Print())
                        {
                            yield return item1;
                        }
                    }
                }
                else
                {
                    string resultStr = "";
                    TrieElement trieElement = this;
                    while (trieElement.ParentTrie != null)
                    {
                        resultStr += trieElement.Symbol;
                        trieElement = trieElement.ParentTrie;
                    }
                    yield return ToString();
                }
            }

            public override string ToString()
            {
                string resultStr = "";
                TrieElement trieElement = this;
                while (trieElement.ParentTrie != null)
                {
                    resultStr += trieElement.Symbol;
                    trieElement = trieElement.ParentTrie;
                }
                return new string(resultStr.Reverse().ToArray());
            }
            #endregion
        }

        class Trie
        {
            
            #region Fields

            public TrieElement SourceOfTrie { get; set; }

            #endregion

            #region Consructors
            public Trie()
            {
                SourceOfTrie = new TrieElement('z');
            }

            public Trie(TrieElement sourceOfTrie)
            {
                SourceOfTrie = sourceOfTrie;
            }
            #endregion

            #region Methods
            public void Add(IEnumerable<char> input)
            {
                SourceOfTrie.AddChilds(input,SourceOfTrie);
            }

            public IEnumerable<string> Print()
            {
                return SourceOfTrie.Print();
            }

                #endregion
            }

        public static char[,] ECLock()
        {
            return new char[,] { { '1' , '2' , '3' },
                                { '4' , '5' , '6' },
                                { '7' , '8' , '9' },
                                { 'z' , '0' , 'z' },
            };
        }

        public static IEnumerable<char> GetReplacements(char InputSymbol)
        {
            var tempForECLock = ECLock();

            bool exit = false;

            for (int i = 0; i < 4 && exit == false; i++)
            {
                for (int j = 0; j < 3 && exit == false; j++)
                {
                    if (tempForECLock[i, j] == InputSymbol)
                    {
                        for (int k = -1; k <= 1; k += 2)
                        {
                            if (i + k >= 0 && i + k < 4)
                            {
                                yield return tempForECLock[i + k, j];
                            }
                            if (j + k >= 0 && j + k < 3)
                            {
                                yield return tempForECLock[i, j + k];
                            }
                        }
                        yield return InputSymbol;
                        exit = true;
                    }
                }
            }
        }

    }

}
