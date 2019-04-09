using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Double_Ended_Q
{
    public class DoubleQ
    {
        //Class Fields
        private int end1; //left side of the queue
        private int end2; //right side of the queue
        private int[] queue;
        private int valueCount = 0;
        private int size = 0;

        //tracking which one was used first
        private bool hasRightBeenUsed = false;
        private bool hasLeftBeenUsed = false;

        //Default Constructor
        public DoubleQ()
        {
            queue = new int[100];
            size = 100;
        }

        //User-specificed Constructor
        public DoubleQ(int arraySize)
        {
            queue = new int[arraySize]; //DO NOT FORGET DATA VALIDATION
            size = arraySize;
        }

        //MAKE ALL GET AND SET VALUES USING END1 and END2 TO FOLLOW POST INCREMENTS!!!!!

        //Properties
        public int Right
        {
            get
            {
                if (IsEmpty() == true)
                {
                    throw new Exception("Queue is empty, cannot get values");
                }
                else
                {
                    return end2;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize();
                    queue[++end2] = value;
                }
                else if (IsEmpty() == true)
                {
                    int arrayMidPoint = size % 2;
                    queue[arrayMidPoint] = value;
                    end2 = arrayMidPoint + 1;
                    end1 = arrayMidPoint - 1;
                    hasRightBeenUsed = true;
                }
                else if (hasLeftBeenUsed==true)
                {
                    queue[end2] = value;
                    end2++;
                }
                else
                {
                    queue[++end2] = value; //increments the right and sets the element equal to the user specified value
                }
            }
        }

        public int Left
        {
            get
            {
                if (IsEmpty() == true)
                {
                    throw new Exception("Queue is empty, cannot get values");
                }
                else
                {
                    return end1;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize();
                    queue[--end1] = value;
                }
                else if (IsEmpty() == true)
                {
                    int arrayMidPoint = size % 2;
                    queue[arrayMidPoint] = value;
                    end1 = arrayMidPoint - 1;
                    end2 = arrayMidPoint + 1;
                    hasLeftBeenUsed = true;
                }
                else if (hasRightBeenUsed == true)
                {
                    queue[end1] = value;
                    end1++;
                }
                else
                {
                    queue[--end1] = value; //increments the right and sets the element equal to the user specified value
                }
                hasLeftBeenUsed = true;
            }
        }

        //Methods
        private void DoubleQueueSize()
        {
            int oldArraySize = size;
            int oldArrayMidPoint = oldArraySize % 2; //int truncates the value
            int newArraySize = size * 2;
            int newArrayMidPoint = newArraySize % 2;
            int newArrayCounter = newArrayMidPoint;
            int[] newArray = new int[newArraySize];
            if ((0 == end1) && (size - 1) == end2) //can be full if both end1 AND end2 have reached the bounds of the array
            {
                //double old array size
                //find the center point of the new array
                //split the original array into two parts, left and right
                //take the left part and print it the left of the middle point on the new array
                //print right half of old array to the right of the new array middle point
                //set the new size equal to two times the previous size
                //set the new array equal to the old one
                for (int i = oldArrayMidPoint; i >= 0; i++) //copying values from the middle to the LEFT
                {
                    newArray[newArrayCounter] = queue[i];
                    newArrayCounter--;
                }
                newArrayCounter = newArrayMidPoint; //resetting the counter
                for (int i = (oldArrayMidPoint + 1); i < oldArraySize; i++) //copying values from the middle to the RIGHT
                {
                    newArray[newArrayCounter] = queue[i];
                    newArrayCounter++;
                }
                queue = newArray;
                size = newArraySize;
            }
            else if ((end2 + 1) == end1) //can be full if ends wrap and touch
            {
                for (int i = end2; i >= 0; i--) //copying values from the middle to the LEFT
                {
                    newArray[newArrayCounter] = queue[i];
                    newArrayCounter--; //this starts at the center of the new array and works back to the left
                }
                newArrayCounter = newArrayMidPoint; //resetting the counter
                for (int i = (end2 + 1); i < size; i++) //copying values from the middle to the RIGHT
                {
                    newArray[newArrayCounter] = queue[i];
                    newArrayCounter++; //this starts at the center of the new array and works towards the RIGHT
                }
                queue = newArray;
                size = newArraySize;
            }
            else
            {
                throw new Exception("Error in the doubling of the array, array not full");
            }
        }

        public bool IsEmpty()
        {
            bool isEmpty = false;
            if (end1 == end2) //if the ends are equal, it means that there are no values
            {
                hasRightBeenUsed = false; //if it is empty, reeset has right/left been used
                hasLeftBeenUsed = false;
                return isEmpty = true;
            }
            return isEmpty;
        }

        public bool IsFull() //pretest method!!!
        {
            bool isFull = false;
            if (((0 == end1) && (size - 1) == end2) || (end2+1)==end1) //can be full if both end1 AND end2 have reached the bounds of the array; also, if the head is behind the tail
            {
                return isFull = true;
            }
            else
            {
                return isFull;
            }
        }
    }
}

