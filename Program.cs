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
      

        static void Main(string[] args)
        {


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
