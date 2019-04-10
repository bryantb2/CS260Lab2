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
        
        //TEST ONLY CONSTRUCTOR!!!!!!!!!!!
        public DoubleQ(string h)
        {
            queue = new int[] {0,1,2,3,4,5,6,7,8,9};
            size = 10;
            int arrayMidPoint = (size / 2);
            queue[arrayMidPoint] = -5;
            if (h == "hasRightBeenUsed")
            {

                hasRightBeenUsed = true;
            }
            else if (h == "hasLeftBeenUsed")
            {
                hasLeftBeenUsed = true;
            }
            end1 = 0;
            end2 = (size - 1);
        }

        //User-specificed Constructor
        public DoubleQ(int arraySize)
        {
            queue = new int[arraySize]; //DO NOT FORGET DATA VALIDATION
            size = arraySize;
        }

        //MAKE ALL GET AND SET VALUES USING END1 and END2 TO FOLLOW POST INCREMENTS!!!!!
        //MUST BE CAPABLE OF IDNEXING

        //Properties
        public int Size
        {
            get
            {
                return this.size;
            }
        }

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
                    int endValue = queue[end2];
                    if (this.IsEmpty() != true)
                    {
                        end2--; //only decrements if the ends are not on the same
                    }
                    return endValue;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize();
                    //queue[++end2] = value; //increments BEFORE setting, so as to not overwrite
                    queue[++end2] = value;
                }
                else if (IsEmpty() == true)
                {
                    //the ==> end1 & end2 <== indexers are not moved to ensure that it can still access the current value
                    //finds the midpoint of ANY queue size
                    int arrayMidPoint = (size / 2);
                    queue[arrayMidPoint] = value;
                    end2 = arrayMidPoint;
                    end1 = arrayMidPoint;
                    hasRightBeenUsed = true;
                }
                else if (hasLeftBeenUsed==true && hasRightBeenUsed == false)
                {
                    end2++;
                    queue[end2] = value; 
                }
                else
                {
                    queue[++end2] = value; //increments the right and sets the element equal to the user specified value
                }
                hasRightBeenUsed = true;
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
                    int endValue = queue[end1];
                    if (this.IsEmpty() != true)
                    {
                        end1--; //only decrements if the ends are not on the same value
                    }
                    return endValue;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize();
                    queue[--end1] = value; //decrements BEFORE setting, so as to not overwrite
                }
                else if (IsEmpty() == true)
                {
                    //the ==> end1 & end2 <== indexers are not moved to ensure that it can still access the current value
                    int arrayMidPoint = (size / 2); //finds the midpoint of ANY queue size
                    queue[arrayMidPoint] = value;
                    end1 = arrayMidPoint;
                    end2 = arrayMidPoint;
                    hasLeftBeenUsed = true;
                }
                else if (hasRightBeenUsed == true && hasLeftBeenUsed == false)
                {
                    end1--;
                    queue[end1] = value;
                }
                else
                {
                    queue[--end1] = value; //decrements indexer BEFORE setting value, so as to not overwrite
                }
                hasLeftBeenUsed = true;
            }
        }

        //Methods
        private void DoubleQueueSize()
        {
            int oldArraySize = this.size;
            int oldArrayMidPoint = (oldArraySize / 2); //int truncates the value
            int newArraySize = (oldArraySize * 2);
            int newArrayMidPoint = (newArraySize / 2);
            int[] newArray = new int[newArraySize];
            int newArrayCounter = newArrayMidPoint;
            if ((0 == end1) && ((size - 1) == end2)) //can be full if both end1 AND end2 have reached the bounds of the array
            {
                //double old array size
                //find the center point of the new array
                //split the original array into two parts, left and right
                //take the left part and print it the left of the middle point on the new array
                //print right half of old array to the right of the new array middle point
                //set the new size equal to two times the previous size
                //set the new array equal to the old one
                for (int i = oldArrayMidPoint; i >= 0; i--) //copying values from the middle to the LEFT
                {
                    newArray[newArrayCounter] = queue[i]; //starting at the mid-point for each array
                    newArrayCounter--;
                }
                newArrayCounter = (newArrayMidPoint + 1); //resetting the counter
                for (int i = (oldArrayMidPoint + 1); i < oldArraySize; i++) //copying values from the middle to the RIGHT
                {
                    newArray[newArrayCounter] = queue[i]; //starting at the mid-point for each array
                    newArrayCounter++;
                }
                //determining how much the ends need to be moved in the new array
                if ((oldArraySize % 2) == 0)
                {
                    int endDisplacement = (oldArraySize / 2);
                    end2 += endDisplacement;
                    end1 += endDisplacement;
                }
                else if ((oldArraySize % 2) != 0)
                {
                    //this process is done in the event that the doubled size is uneven, requiring one end to be 
                    double endDisplacement = (oldArraySize / 2);
                    end2 += (int)(Math.Round(endDisplacement));
                    end1 -= (int)(Math.Floor(endDisplacement));
                }
                queue = newArray;
                size = newArraySize;
            }
            else if ((end2 + 1) == end1) //full if end2 wraps and touches end1
            {
                for (int i = end2; i >= 0; i--) //copying values from the middle to the LEFT
                {
                    newArray[newArrayCounter] = queue[i];
                    newArrayCounter--; //this starts at the center of the new array and works back to the left
                }
                newArrayCounter = newArrayMidPoint; //resetting the counter
                for (int i = (end1); i < size; i++) //copying values from the middle to the RIGHT
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

