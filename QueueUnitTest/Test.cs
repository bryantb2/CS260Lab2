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

        [SetUp]
        public void SetUp()
        {
            qDefault = new DoubleQ();
            qDefined = new DoubleQ(10);
            qDefined2 = new DoubleQ(10);
            fullQ = new DoubleQ("fullQueue");
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
            //test the setter when array is empty
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

            //setting and getting from an object that has simulated the right setter being used first
            DoubleQ leftUsedTestQ = new DoubleQ("hasLeftBeenUsed");
            leftUsedTestQ.Right = 3;
            getRight = fullQ.Right;
            Assert.AreEqual(3, getRight);
        }

        [Test]
        public void TestingQDouble()
        {
            //testing to see if the size doubles when adding to a full queue
            int preDoubleSize = fullQ.Size;
            fullQ.Right = -3;
            int postDoubleSize = fullQ.Size;
            int rightValue = fullQ.Right;
            Assert.AreEqual(postDoubleSize, (preDoubleSize * 2));
            Assert.AreEqual(-3, rightValue); //testing to see if expanded queue returns previously added value
        }

        /*
        [Test]
        public void IsEmptyTest()
        {
            Assert.AreEqual(true, qDefault.IsEmpty());
            Assert.AreEqual(true, qDefined.IsEmpty());
            //ADD TEST FOR WHEN FALSE
        }

        [Test]
        public void IsFullTest()
        {
            Assert.AreEqual(false, qDefault.IsFull());
            Assert.AreEqual(false, qDefined.IsFull());
            //ADD TEST FOR WHEN TRUE
        }*/
    }
}
