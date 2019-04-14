using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Double_Ended_Q
{
    
    public class Stack
    {
        //Class Fields
        private int lastItem;
        private int valueCount = 0;
        private DoubleQ stack = null;

        //Default Constructor
        public Stack()
        {
            //creates a default double ended queue with a size
            //of 100
            stack = new DoubleQ();
        }

        //User-specificed Constructor
        public Stack(int stackSize)
        {
            if (stackSize >= 10)
            {
                stack = new DoubleQ(stackSize);
            }
            else
            {
                throw new ArgumentException("Stack size must be at least 10");
            }
        }

        //Properties
        public int Size
        {
            get
            {
                return stack.Size;
            }
        }

        //Methods
        public void Push(int item)
        {
            //to push an item to the stack, use the right setter property of the double ended queue
            try
            {
                stack.Right = item;
                valueCount++;
                lastItem = item;
            }
            catch (Exception)
            {
                throw new Exception("An error has occured in the underlying data structure");
            }
        }

        public int Peek()
        {
            //to peek an item, return the lastItem variable
            if (valueCount >= 1)
            {
                return lastItem;
            }
            else
            {
                throw new Exception("Stack is empty");
            }
        }

        public int Pop()
        {
            //to pop an item, use right getter property to store and remove the stack value
            try
            {
                int popValue = stack.Right;
                valueCount--;
                return popValue;
            }
            catch (Exception)
            {
                throw new Exception("Cannot pop from an empty stack");
            }
        }















    }
    
}
