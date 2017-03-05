﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreOrderTraversal
{
	class BinaryTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
	{
		public BinaryTreeNode(TNode value)
		{
			Value = value;
		}
		public BinaryTreeNode<TNode> Left
		{
			get;
			set;
		}

		public BinaryTreeNode<TNode> Right
		{
			get;
			set;
		}

		public TNode Value
		{
			get;
			private set;
		}

		public int CompareTo(TNode other)
		{
			return Value.CompareTo(other);
		}

	}

	public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
	{
		private BinaryTreeNode<T> _head;

		private int _count;

		#region Добавление нового узла дерева

		public void Add(T value)
		{
			// Первый случай: дерево пустое     

			if (_head == null)
			{
				_head = new BinaryTreeNode<T>(value);
			}

			// Второй случай: дерево не пустое, поэтому применяем рекурсивный алгорит 
			//                для поиска места добавления узла        

			else
			{
				AddTo(_head, value);
			}
			_count++;
		}

		// Рекурсивный алгоритм 

		private void AddTo(BinaryTreeNode<T> node, T value)
		{
			// Первый случай: значение добавляемого узла меньше чем значение текущего. 

			if (value.CompareTo(node.Value) < 0)
			{
				// если левый потомок отсутствует - добавляем его          

				if (node.Left == null)
				{
					node.Left = new BinaryTreeNode<T>(value);
				}
				else
				{
					// повторная итерация               
					AddTo(node.Left, value);
				}
			}
			// Второй случай: значение добавляемого узла равно или больше текущего значения      
			else
			{
				// Если правый потомок отсутствует - добавлем его.          

				if (node.Right == null)
				{
					node.Right = new BinaryTreeNode<T>(value);
				}
				else
				{
					// повторная итерация

					AddTo(node.Right, value);
				}
			}
		}

		#endregion

		#region Нумератор

		public IEnumerator<T> GetEnumerator()
		{
			return PreOrderTraversal();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()

		{
			return GetEnumerator();
		}

		#endregion

		#region Количество узлов в дереве

		public int Count
		{
			get
			{
				return _count;
			}
		}

		#endregion

		#region Прямой порядок

		private IEnumerator<T> PreOrderTraversal()
		{
			if (_head != null)
			{
				var stack = new Stack<BinaryTreeNode<T>>();
				var current = _head;

				stack.Push(current);
				while (stack.Count > 0)
				{
					current = stack.Pop();
					yield return current.Value;
					if (current.Right != null)
						stack.Push(current.Right);
					if (current.Left != null)
						stack.Push(current.Left);
				}
			}
		}

		#endregion
	}

	class Program
	{
		static void Main(string[] args)
		{
			BinaryTree<int> instance = new BinaryTree<int>();

			instance.Add(8);    //                        8
			instance.Add(5);    //                      /   \
			instance.Add(12);   //                     5    12 
			instance.Add(3);    //                    / \   / \  
			instance.Add(7);    //                   3   7 10 15
			instance.Add(10);   //
			instance.Add(15);   //
			
			foreach (var item in instance)
			{
				Console.WriteLine(item);
			}

			Console.ReadKey();
		}
	}
}
