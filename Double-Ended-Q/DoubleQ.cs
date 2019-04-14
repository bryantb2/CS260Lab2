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
        private int size = 0;

        //tracking which one was used first and if the array has wrapped
        private bool hasRightBeenUsed = false;
        private bool hasRightWrapped = false;
        private bool hasLeftBeenUsed = false;
        private bool hasLeftWrapped = false;

        //Default Constructor
        public DoubleQ()
        {
            queue = new int[100];
            size = 100;
        }

        //User-specificed Constructor
        public DoubleQ(int arraySize)
        {
            if (arraySize >= 10)
            {
                queue = new int[arraySize];
                size = arraySize;
            }
            else
            {
                throw new ArgumentException("Queue size must be at least 10");
            }
        }

        //<---------------- Beginning of Test Only-Constructors and Properties ------------->

        //constructors below enable and disable certain properties
        //to simulate either an even or uneven array that is 
        //full, has had both setters used, and had its ends 
        //set to the proper place
        public DoubleQ(string h)
        {
            //made to test even array size behavior
            queue = new int[] {0,1,2,3,4,5,6,7,8,9};
            size = 10;
            queue[0] = 5;
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

        public DoubleQ(string a, string b)
        {
            //made to test uneven array size behavior
            queue = new int[11] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            size = 11;
            queue[0] = 5;
            if (a == "hasRightBeenUsed")
            {
                hasRightBeenUsed = true;
            }
            else if (b == "hasLeftBeenUsed")
            {
                hasLeftBeenUsed = true;
            }
            end1 = 0;
            end2 = (size - 1);
        }

        //<---------------- End of Test Only-Constructors and Properties ------------->

        //Properties
        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public bool IsWrapped
        {
            get
            {
                if (hasLeftWrapped == true || hasRightWrapped == true) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
                    if ((end2 == 0 && this.IsWrapped==false) ||end1 == end2) //true if end2 terminates on the left side are repeated pops or the two eventually meet
                    {
                        end1 = 0; //resets to middle since it occupies the same spot as end2
                        end2 = 0;
                        hasLeftBeenUsed = false;
                        hasRightBeenUsed = false;
                    }
                    else if (end2 == 0) //true if the data is wrapped and end1 is on the right side
                    {
                        end2 = (this.size -1); //sets it to the end of the left side
                        hasRightWrapped = false;
                    }
                    else if (end1 < end2 || end1 > end2)//end2 is in normal position OR wrapped
                    {
                        end2--; //only decrements if the ends are not on the same value
                    }
                    return endValue;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize(); //doubles queue and handles the unwrapping of a wrapped 
                    queue[++end2] = value; //increments BEFORE setting, so as to not overwrite
                }
                else if (IsEmpty() == true)
                {
                    queue[0] = value;
                    end2 = 0;
                    end1 = 0;
                }
                //tests to see if the array has wrapped
                else if (hasRightWrapped == true)
                {
                    queue[++end2] = value;
                }
                //testing to see if current position of end2 is at the end of the Queue, since index cannot be negative!
                else if (end2 == (this.size - 1))
                {
                    end2 = 0;
                    queue[end2] = value;
                    hasRightWrapped = true;
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
                    //test if the array is empty
                        //if not, determine if the end1 is on the end of left, left, right, or end of right
                    if ((end1 == (this.size-1) && IsWrapped == false) || end1 == end2)
                    {
                        end1 = 0; //resets to beginning since it occupies the same spot as end2
                        end2 = 0;
                        hasLeftBeenUsed = false;
                        hasRightBeenUsed = false;
                    }
                    else if (end1 == (this.size - 1)) //true if the data is wrapped and end1 is on the right side
                    {
                        end1 = 0; //sets it to the end of the left side
                        hasLeftWrapped = false;
                    }
                    else if (end1 < end2 || end1 > end2)//end1 is in normal position OR wrapped
                    {
                        end1++; //only decrements if the ends are not on the same value
                    }
                    return endValue;
                }
            }
            set
            {
                if (IsFull() == true)
                {
                    DoubleQueueSize(); //handles if a wrapped or unwrapped queue is full
                    queue[--end1] = value; //decrements BEFORE setting, so as to not overwrite
                }
                else if (IsEmpty() == true)
                {
                    //the ==> end1 & end2 <== indexers are not moved to ensure that it can still access the current value
                        //situationally specific to a first time value queue
                    queue[0] = value;
                    end1 = 0;
                    end2 = 0;
                }
                //tests to see if the array has wrapped
                else if (hasLeftWrapped == true)
                {
                    queue[--end1] = value;
                }
                //testing to see if current position of end1 is at the end of the Queue, since index cannot be negative!
                else if (end1 == 0)
                {
                    end1 = (size - 1);
                    queue[end1] = value;
                    hasLeftWrapped = true;
                }
                //executes if the array is not full, not empty, has not wrapped, and the left setter has been moved away from midpoint
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
            int[] newQueue = new int[this.size *2];
            if ((0 == end1) && ((size - 1) == end2)) //can be full if both end1 AND end2 have reached the bounds of the array
            {
                for (int i = 0; i < this.size; i++)
                {
                    newQueue[i] = queue[i];
                }
                queue = newQueue;
                size = this.size * 2;
            }
            else if ((end2 + 1) == end1) //full if end2 wraps and touches end1
            {
                for (int i = end1; i < this.size; i++)
                {
                    newQueue[i] = queue[i];
                }
                for (int i = 0; i <= end2; i++)
                {
                    newQueue[i] = queue[i];
                }
                queue = newQueue;
                size = this.size * 2;
                hasLeftWrapped = false;
                hasRightWrapped = false;
            }
            end1 = 0;
            }

        public bool IsEmpty()
        {
            bool isEmpty = false;
            if (hasLeftBeenUsed == false && hasRightBeenUsed == false) //if the ends are equal, it means that there are no values
            {
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

        public string PrintQueueLeftToRight()
        {
            string output = "";
            if (IsEmpty()==true)
            {
                output = "This queue is empty, nothing to print";
                return output;
            }
            else
            {
                //check if array is wrapped around itself
                if (this.IsWrapped == true)
                {
                    //use two for loops to:
                        //print the wrapped LEFT SIDE values from end1 to (size-1)
                        //print the wrapped RIGHT SIDE values from 0 to end2
                    //left side values
                    for (int i = end1; i < size; i++)
                    {
                        output += (this.Left).ToString() + " ";
                    }
                    //right side values
                    for (int i = 0; i <= end2; i++)
                    {
                        if (i != end2)
                        {
                            output += (this.Left).ToString() + " ";
                        }
                        else
                        {
                            output += (this.Left).ToString();
                        }
                    }
                    return output;
                }
                //check if array is full, since end1 and end2 will be a predictable distance from each other
                else if(IsFull()==true)
                {
                    //if full, then end1 is on the left side and end2 is on the right side
                    for (int i = 0; i < size; i++)
                    {
                        output += (this.Left).ToString() + " ";
                    }

                    return output;
                }
                else
                {
                    throw new Exception("Error: queue could not be printed");
                }
            }
        }
    }
}

