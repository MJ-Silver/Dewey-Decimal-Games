using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prog7312
{
    

        /// <summary>
        ///    
        /// Creating a node with in a tree to store the items
        /// 
        /// </summary>
        public class Tree
        {
            public Node Root { get; set; }
            public List<Node> Nodes { get; } = new List<Node>();
            private Stack<Node> stack = new Stack<Node>();
            public LinkedList<Node> llist = new LinkedList<Node>();

            public Tree()
            {

            }
            public Tree(string key, string value)
            {
                Root = new Node(key, value, null);
            }
        /// <summary>
        /// finding a node 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
            public Node Find(Node node, string key)
            {


                if (node.Key == key)
                {
                    return node;
                }
                else
                {
                    foreach (Node child in node.Child)
                    {

                        Node foundNode = Find(child, key);
                        if (foundNode != null)
                        {
                            return foundNode;
                        }
                    }
                }
                return null;


            }


            public Tree Add(string key, string val)
            {
                stack.Peek().Insert(key, val);

                //llist.First().Insert(key,val);
                return this;
            }
            public Tree startBranch(string key, string value)
            {
                if (stack.Count == 0)
                {
                    //Node newNode = new Node(value, null);
                    Root = new Node(key, value, null);
                    Nodes.Add(Root);
                    // llist.AddFirst(Root);
                    stack.Push(Root);
                } else             
                {
                    Node newnodes = stack.Peek().Insert(key, value);
                    // Node newnodes = llist.First().Insert(key,value);
                    Nodes.Add(newnodes);
                    //llist.AddFirst(newnodes);
                    stack.Push(newnodes);
                }

                return this;
            }
            public Tree stopBranch()
            {
                stack.Pop();
                //llist.RemoveFirst();
                return this;
            }
        }

    /// <summary>
    /// node class for storing the parent and values of each node
    /// </summary>
        public class Node
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public Node Parent { get; set; }
            public List<Node> Child { get; set; }



            public Node(string key, string value, Node parent)
            {
                Value = value;
                Key = key;
                Parent = parent;
                Child = new List<Node>();
            }

            public Node Insert(string key, string value)
            {
                Node newNode = new Node(key, value, Parent = this);
                this.Child.Add(newNode);
                return newNode;
            }

            public int getHeight()
            {

                int height = 1;
                Node current = this;

                while (current != null)
                {
                    height++;
                    current = current.Parent;
                }

                return height;
            }

        }

    }

