using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDirectory
{
	public class List
	{



		//void clear();/*clear dynamic memory*/
		//void push_front(T data);/*insert in the start*/
		//void insert(T value, int index);/*insert in the middle*/

		//void pop_back();/*delete last*/
		public int Size { get; set; }
		//Car& operator[] (const int index);


		private class Node
		{
			public Node pNext;
			public Car data;
			public Node(Car data, Node pNext = null)
			{
				this.data = data;
				this.pNext = pNext;
			}
		};
		private Node head;


		public void PushBack(Car data) /*insert in the end*/
		{
			if (head == null)
			{
				head = new Node(data);
			}
			else
			{
				Node current = head;

				while (current.pNext != null)
				{
					current = current.pNext;

				}
				current.pNext = new Node(data);
			}

		}
		public void RemoveAt(int index)/*delete i-element*/
		{
			if (index == 0)
			{
				Pop_Front();
			}
			else
			{
				Node previous = head;
				for (int i = 0; i < index - 1; i++)
				{
					previous = previous.pNext;
				}

				Node toDelete = previous.pNext;
				previous.pNext = toDelete.pNext;
				Size--;

			}
		}

		public void Pop_Front()/*delete 1st*/
		{
			Node temp = head;
			head = head.pNext;
			Size--;
		}


		//	template<typename T>
		//	List<T>::List()
		//	{
		//		Size = 0;
		//		head = nullptr;
		//	}

		//	template<typename T>
		//	List<T>::~List()
		//	{
		//		clear();
		//	}

		//	template<typename T>
		//void List<T>::pop_front()
		//	{
		//		Node<T>* temp = head;

		//		head = head->pNext;

		//		delete temp;

		//		Size--;

		//	}

		//	template<typename T>
		//void List<T>::push_back(T data)
		//	{
		//		if (head == nullptr)
		//		{
		//			head = new Node<T>(data);
		//		}
		//		else
		//		{
		//			Node<T>* current = this->head;

		//			while (current->pNext != nullptr)
		//			{
		//				current = current->pNext;

		//			}
		//			current->pNext = new Node<T>(data);
		//		}
		//		Size++;
		//	}

		//	template<typename T>
		//void List<T>::clear()
		//	{
		//		while (Size)
		//		{
		//			pop_front();
		//		}
		//	}

		//	template<typename T>
		//void List<T>::push_front(T data)
		//	{
		//		head = new Node<T>(data, head);
		//		Size++;


		//	}

		//	template<typename T>
		//void List<T>::insert(T data, int index)
		//	{
		//		if (index == 0)
		//		{
		//			push_front(data);
		//		}
		//		else
		//		{
		//			Node<T>* previous = this->head;

		//			for (int i = 0; i < index - 1; i++)
		//			{
		//				previous = previous->pNext;
		//			}

		//			previous->pNext = new Node<T>(data, previous->pNext);
		//			Size++;

		//		}




		//	}

		//	template<typename T>
		//void List<T>::removeAt(int index)
		//	{
		//		if (index == 0)
		//		{
		//			pop_front();
		//		}
		//		else
		//		{
		//			Node<T>* previous = this->head;
		//			for (int i = 0; i < index - 1; i++)
		//			{
		//				previous = previous->pNext;
		//			}

		//			Node<T>* toDelete = previous->pNext;
		//			previous->pNext = toDelete->pNext;
		//			delete toDelete;
		//			Size--;

		//		}

		//	}

		//	template<typename T>
		//void List<T>::pop_back()
		//	{
		//		removeAt(Size - 1);

		//	}




		//	template<typename T>
		//T& List<T>::operator[](const int index)
		//{
		//	int counter = 0;
		//	Node<T>* current = this->head;
		//	while (current!= nullptr)
		//	{
		//		if (counter == index)
		//		{
		//			return current->data;
		//		}
		//current = current->pNext;
		//counter++;

		//	}
		//}

	}
}
