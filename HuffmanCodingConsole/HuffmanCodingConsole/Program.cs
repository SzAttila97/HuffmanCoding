using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Globalization;

namespace HuffmanCodingConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            string path = @"C:\Users\Attila\Desktop\input.txt";


            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);

            /*Console.WriteLine("Please enter the string:");
            string input = Console.ReadLine();*/
            HuffmanTree huffmanTree = new HuffmanTree();



            // HuffmanTreeBuild the Huffman tree
            huffmanTree.HuffmanTreeBuild(readText);

            

            
            // Encode
            List<int> encoded = huffmanTree.EncodeTheSource(readText);
            //List<int> encoded = huffmanTree.EncodeTheSource("a");
            //encodedEach = huffmanTree.EncodeTheSource(letters[i].ToString());


            Console.Write("\nEncoded: ");
            for (int i = 0; i < encoded.Count; i++)
            {
                Console.Write((encoded[i]) + "");
                
            };

            // Encode each character in the string
            Char[] letters = readText.ToCharArray();


            Dictionary<string, List<int>> asd = new Dictionary<string, List<int>>();
            Console.WriteLine("\n");
            for (int i = 0; i < letters.Length; i++)
            {
               
                List<int> encodedEach = huffmanTree.EncodeTheSource(letters[i].ToString());                            
                if (!asd.ContainsKey(letters[i].ToString()))
                {
                    asd.Add(letters[i].ToString(), encodedEach);
                }
                    
            }

            // Key-value párok
            foreach (KeyValuePair<string, List<int>> symbol in asd)
            {
                var strings =
            symbol.Value.Select(i => i.ToString(CultureInfo.InvariantCulture))
                .Aggregate((s1, s2) => s1 + s2);
                Console.WriteLine("Key : {0}, Value : {1}", symbol.Key, strings);
            }

            // Hatásfok
            double H = 0;
            for (int i = 0; i < huffmanTree.ps.Count; i++)
            {
                H += -huffmanTree.ps[i] * Math.Log2(huffmanTree.ps[i]);

            }
            Console.Write("\nH: " + H + "\n");

            CreateTheOutputTxt(encoded, huffmanTree.ps, H, asd);

            // Decode
            string decoded = huffmanTree.DecodeTheBits(encoded);

            Console.WriteLine("Decoded: " + decoded);
        
            Console.ReadLine();       
        }

        public static void CreateTheOutputTxt(List<int> code, List<Double> p, double H, Dictionary<string, List<int>> asd)
        {
            TextWriter tw = new StreamWriter(@"C:\Users\Attila\Desktop\output.txt");
            tw.Write("Encoded: ");
            for (int i = 0; i < code.Count; i++)
            {
                tw.Write(code[i]);
            }
            for (int i = 0; i < p.Count; i++)
            {
               tw.Write("\np" + (i+1) + ": " + p[i]);
            }
            tw.WriteLine("\nH (hatásfok): " + H);
            foreach (KeyValuePair<string, List<int>> symbol in asd)
            {
                var strings =
            symbol.Value.Select(i => i.ToString(CultureInfo.InvariantCulture))
                .Aggregate((s1, s2) => s1 + s2);
                tw.WriteLine("Key : {0}, Value : {1}", symbol.Key, strings);
            }
            Console.WriteLine("\nOutput file is done!");
            tw.Close();
            Console.WriteLine();

        }

    }
}
