using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuffmanCodingConsole
{
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        // Fa felépítése
        public void HuffmanTreeBuild(string source)
        {
            // Frekvenciák megszámolása minden egyedi karakterre
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);     
                }

                Frequencies[source[i]]++;
            }

            // Rektordok összepárosítása a Node tömbben
            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
                Console.WriteLine("Key: " + symbol.Key + ", Frequency: " + Convert.ToDouble(symbol.Value) / source.Length );
                
                
            }

            // Elemek kombinálása
            while (nodes.Count > 1)
            {
                // Rendezett lista létrehozása (Frekvencia alapján)
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();
                
                if (orderedNodes.Count >= 2)
                {
                    // Első két elem kiválasztása
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Szülő node elkészítése az előbb kiválasztott kettő elem kombinálásával
                    Node parent = new Node()
                    {   
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    // Node listából az aktuálsan vizsgált elemek eltávolitása
                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    // Szülő elem hozzáadása a node listához
                    nodes.Add(parent);
                }
                // Root kiválasztása => Node első eleme
                this.Root = nodes.FirstOrDefault();
                
            }

        }

        // Kódolás
        public List<int> EncodeTheSource(string source)
        {
            List<int> encodedSource = new List<int>();

            // Elemekhez előállitja a megfelelő listát (0/1)
            for (int i = 0; i < source.Length; i++)
            {
                List<int> encodedSymbol = this.Root.Find(source[i], new List<int>());
                encodedSource.AddRange(encodedSymbol);
                // Console.WriteLine(source[i] + ":" + encodedSource[i]);
               

            }

            //BitArray bits = new BitArray(encodedSource.ToArray());
            
            return encodedSource;
        }

        // Dekódkolás
        public string DecodeTheBits(List<int> bits)
        {
            Node current = this.Root;
            string decoded = "";

            for (int i = 0; i < bits.Count; i++)
            {

          
                if (bits[i] == 1)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }

        // Levél vizsgálat - Dekódoláshoz kell

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }

    }
}
