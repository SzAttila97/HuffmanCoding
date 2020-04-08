using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCodingConsole
{
    public class Node
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        // Keresés a Nodeokban
        public List<int> Find(char symbol, List<int> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {  
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<int> left = null;
                List<int> right = null;

                if (Left != null)
                {
                    List<int> leftPath = new List<int>();
                    leftPath.AddRange(data);
                    leftPath.Add(0);
                    left = Left.Find(symbol, leftPath);                    
                }

                if (Right != null)
                {
                    List<int> rightPath = new List<int>();
                    rightPath.AddRange(data);
                    rightPath.Add(1);
                    right = Right.Find(symbol, rightPath);                   
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
        
    }
}
