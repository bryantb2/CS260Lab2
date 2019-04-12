using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Double_Ended_Q;
using NUnit.Framework;

namespace QueueUnitTest
{
    [TestFixture]
    public class Test
    {
        DoubleQ qDefault;
        DoubleQ qDefined;
        DoubleQ qDefined2;
        DoubleQ fullQ;
        DoubleQ fullQOdd;

        [SetUp]
        public void SetUp()
        {
            qDefault = new DoubleQ();
            qDefined = new DoubleQ(10);
            qDefined2 = new DoubleQ(10);
            fullQ = new DoubleQ("fullQueue");
            fullQOdd = new DoubleQ("oddNumbered","queue");
        }

        [Test]
        public void BasicTest()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void TestingGetSetLeft()
        {
            //test getter when array is empty (should throw an exception)
            try
            {
                int leftSideValue = qDefault.Left;
                Assert.Fail("The left getter should throw an exception for trying to get values out of an empty queue.");
            }
            catch (Exception)
            {
                Assert.Pass("The getter threw an exception for trying to get values out of an empty queue");
            }

            //setting to an empty queue
            qDefault.Left = 12;
            int getLeft = qDefault.Left;
            Assert.AreEqual(12, getLeft);

            //setting to a full queue
            fullQ.Left = 3;
            getLeft = fullQ.Left;
            Assert.AreEqual(3, getLeft);

            //setting and getting from a non-empty but non-full queue
            qDefined2.Left = 5;
            qDefined2.Left = 6;
            qDefined2.Left = 7;
            getLeft = qDefined2.Left;
            Assert.AreEqual(7, getLeft);

            //setting and getting from an object that has simulated the right setter being used first
            DoubleQ rightUsedTestQ = new DoubleQ("hasRightBeenUsed");
            rightUsedTestQ.Left = 3;
            getLeft = fullQ.Left;
            Assert.AreEqual(3, getLeft);
        }

        [Test]
        public void TestingLeftSideWrap()
        {
            //Testing to see if left side will wrap around array successfully
            //Filling queue object via for loop
            for (int i = 0; i < 5; i++)
                {
                    qDefined.Left = i;
                }
            qDefined.Left = 7; 
            qDefined.Left = 17; //this should wrap the queue
            Assert.AreEqual(true, qDefined.IsWrapped);
            int leftValue = qDefined.Left;
            Assert.AreEqual(17, leftValue);
            leftValue = qDefined.Left;
            Assert.AreEqual(7, leftValue);
            Assert.AreEqual(false, qDefined.IsWrapped);
        }


        [Test]
        public void TestingGetSetRight()
        {
            //test getter when array is empty (should throw an exception)
            //test the setter when array is empty
            try
            {
                int rightSideValue = qDefault.Right;
                Assert.Fail("The right getter should throw an exception for trying to get values out of an empty queue.");
            }
            catch (Exception)
            {
                Assert.Pass("The getter threw an exception for trying to get values out of an empty queue");
            }

            //setting to an empty queue
            qDefault.Right = 12;
            int getRight = qDefault.Right;
            Assert.AreEqual(12, getRight);

            //setting to a full queue
            fullQ.Right = 3;
            getRight = fullQ.Left;
            Assert.AreEqual(3, getRight);

            //setting and getting from a non-empty but non-full queue
            qDefined2.Right = 5;
            qDefined2.Right = 6;
            qDefined2.Right = 7;
            getRight = qDefined2.Right;
            Assert.AreEqual(7, getRight);

            //setting and getting from an object that has simulated the left setter being used first
            DoubleQ leftUsedTestQ = new DoubleQ("hasLeftBeenUsed");
            leftUsedTestQ.Right = 3;
            getRight = fullQ.Right;
            Assert.AreEqual(3, getRight);
        }

        [Test]
        public void TestingRightSideWrap()
        {
            //Testing to see if left side will wrap around array successfully
            //Filling queue object via for loop
            for (int i = 0; i < 5; i++)
            {
                qDefined.Right = i;
            }
            qDefined.Right = 7;
            qDefined.Right = 17; //this should wrap the queue
            Assert.AreEqual(true, qDefined.IsWrapped);
            int rightValue = qDefined.Right;
            Assert.AreEqual(17, rightValue);
            rightValue = qDefined.Right;
            Assert.AreEqual(7, rightValue);
            Assert.AreEqual(false, qDefined.IsWrapped);
        }

        [Test]
        public void TestingQDoubleEvenQueue()
        {
            //testing to see if the size doubles when adding to a full queue
            int preDoubleSize = fullQ.Size;
            fullQ.Right = -3;
            int postDoubleSize = fullQ.Size;
            int rightValue = fullQ.Right;
            Assert.AreEqual(postDoubleSize, (preDoubleSize * 2));
            Assert.AreEqual(-3, rightValue); //testing to see if expanded queue returns previously added value
        }

        [Test]
        public void TestingQDoubleOddQueue()
        {
            //testing to see if the size doubles when adding to a full queue with an odd number of elements
            int preDoubleSize = fullQOdd.Size;
            fullQOdd.Right = -3;
            int postDoubleSize = fullQOdd.Size;
            int rightValue = fullQOdd.Right;
            Assert.AreEqual(postDoubleSize, (preDoubleSize * 2));
            Assert.AreEqual(-3, rightValue); //testing to see if expanded queue returns previously added value
        }

        [Test]
        public void TestingWrappedQueue()
        {
            int originalSize = qDefined.Size;
            for(int i = 0; i <qDefined.Size; i++)
            {
                qDefined.Right = i;
            }
            Assert.AreEqual(true, qDefined.IsWrapped);
            qDefined.Right = -1; //this should cause the wrapped
            Assert.AreEqual(-1, qDefined.Right); //seeing if the end2 value is put in the right spot following the double
            Assert.AreEqual(false, qDefined.IsWrapped); //determining that the array is no longer wrapped
            Assert.AreEqual(qDefined.Size, (originalSize * 2)); //determing if the array doubled
        }
        [Test]
        public void TestingWrappedQueueReturnBehavior()
        {
            //this test will determine if a queue can be filled with value, wrapped, 
            //and then unwrapped with the return values in the same order
            int originalSize = qDefined.Size;
            int[] originalInput = new int[10];
            for (int i = 0; i < qDefined.Size; i++)
            {
                qDefined.Right = i;
                originalInput[i] = i;
            }
            Assert.AreEqual(true, qDefined.IsWrapped); //this should be true because end2 will wrap around to end1
            int[] finalOutput = new int[10];
            //now take queue values out and put into different array
            for (int i = 0; i < qDefined.Size; i++)
            {
                finalOutput[i] = qDefined.Right;
            }
            //this next part will reverse the ouput array into the original order
            int swapValueOne;
            int swapValueTwo;
            for (int i = 0; i < (qDefined.Size / 2); i++)
            {
                swapValueOne = finalOutput[i]; //stores first value to be swapped
                swapValueTwo = finalOutput[qDefined.Size - (i+1)]; //stores second value to be stopped
                finalOutput[i] = swapValueTwo; //moves end value to front
                finalOutput[qDefined.Size - (i + 1)] = swapValueOne;
            }
            Assert.AreNotEqual(qDefined.Size, (originalSize * 2)); //checking to make sure array did not expand
            Assert.AreEqual(originalInput, finalOutput);
            Assert.AreEqual(finalOutput[9], originalInput[9]);
            Assert.AreEqual(finalOutput[0], originalInput[0]);
        }

        [Test]
        public void IsEmptyTest()
        {
            Assert.AreEqual(true, qDefault.IsEmpty());
            Assert.AreEqual(true, qDefined.IsEmpty());
            //ADD TEST FOR WHEN FALSE
            Assert.AreNotEqual(true, fullQ);
            Assert.AreNotEqual(true, fullQOdd);
        }

        [Test]
        public void IsFullTest()
        {
            Assert.AreEqual(false, qDefault.IsFull());
            Assert.AreEqual(false, qDefined.IsFull());
            //ADD TEST FOR WHEN TRUE
            Assert.AreNotEqual(false, fullQ);
            Assert.AreNotEqual(false, fullQOdd);
        }

        [Test]
        public void TestingPrintQueue()
        {
            string output = "";
            for (int i = 0; i < qDefined.Size; i++)
            {
                qDefined.Left = i;
            }
            output = qDefined.PrintQueueLeftToRight();
            Assert.AreEqual("9 8 7 6 5 4 3 2 1 0", output);
        }
    }
}
