using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace HuffmanCodingConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\Attila\Desktop\test.txt";

            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);

            /*Console.WriteLine("Please enter the string:");
            string input = Console.ReadLine();*/
            HuffmanTree huffmanTree = new HuffmanTree();



            // HuffmanTreeBuild the Huffman tree
            huffmanTree.HuffmanTreeBuild(readText);

            // Encode
            List<int> encoded = huffmanTree.EncodeTheSource(readText);
        
            Console.Write("Encoded: ");
            for (int i = 0; i < encoded.Count; i++)
            {
                Console.Write((encoded[i]) + "");
                
            };

            CreateTheOutputTxt(encoded);

            // Decode
            string decoded = huffmanTree.DecodeTheBits(encoded);

            Console.WriteLine("Decoded: " + decoded);
        
            Console.ReadLine();       
        }

        public static void CreateTheOutputTxt(List<int> code)
        {
            TextWriter tw = new StreamWriter(@"C:\Users\Attila\Desktop\output.txt");
            for (int i = 0; i < code.Count; i++)
            {
                tw.Write(code[i]);
            }
            Console.WriteLine("\nOutput file is done!");
            tw.Close();
            Console.WriteLine();

        }
    }
}
