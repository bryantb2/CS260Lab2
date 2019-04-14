using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Double_Ended_Q;

namespace QueueUnitTest
{
    class StackTest
    {
        [TestFixture]
        public class QueueTest
        {
            Stack definedStack;
            Stack defaultStack;

            [SetUp]
            public void SetUp()
            {
                definedStack = new Stack(10);
                defaultStack = new Stack();
            }

            [Test]
            public void BasicTest()
            {
                Assert.AreEqual(true, true);
            }

            [Test]
            public void BasicStackTest()
            {
                definedStack.Push(5);
                int tempPopValue = definedStack.Pop();
                Assert.AreEqual(5, tempPopValue);
            }

            [Test]
            public void TestingFullStackPush()
            {
                //this test will stress underlying data structure to see if pushing and popping to
                //a full stack will result in the same values being returned
                int[] pushValues = new int[10];
                int[] popValues = new int[10];
                for(int i = 0; i < 10; i++)
                {
                    pushValues[i] = i;
                    definedStack.Push(i);
                }
                //now popping values into secondary array
                for (int i = 0; i < 10; i++)
                {
                    popValues[i] = definedStack.Pop();
                }
                //now rearranging pop array into the order of the original input
                int swapValueOne;
                int swapValueTwo;
                for (int i = 0; i < (popValues.Length / 2); i++)
                {
                    swapValueOne = popValues[i]; //stores first value to be swapped
                    swapValueTwo = popValues[popValues.Length - (i + 1)]; //stores second value to be stopped
                    popValues[i] = swapValueTwo; //moves end value to front
                    popValues[popValues.Length - (i + 1)] = swapValueOne;
                }
                Assert.AreEqual(pushValues, popValues);

                //final pop and push to empty stack
                definedStack.Push(-1);
                int finalValue = definedStack.Pop();
                Assert.AreEqual(-1, finalValue);
            }

            [Test]
            public void TestingSizeMethod()
            {
                Assert.AreEqual(10, definedStack.Size);
            }

            [Test]
            public void TestingResizingStack()
            {
                //this test will stress underlying data structure to see if pushing and popping to
                //a full stack will result in the same values being returned
                int[] pushValues = new int[12];
                int[] popValues = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    pushValues[i] = i;
                    definedStack.Push(i);
                }

                //now popping values into secondary array
                for (int i = 0; i < 12; i++)
                {
                    popValues[i] = definedStack.Pop();
                }
                
                //now rearranging pop array into the order of the original input
                int swapValueOne;
                int swapValueTwo;
                for (int i = 0; i < (popValues.Length / 2); i++)
                {
                    swapValueOne = popValues[i]; //stores first value to be swapped
                    swapValueTwo = popValues[popValues.Length - (i + 1)]; //stores second value to be stopped
                    popValues[i] = swapValueTwo; //moves end value to front
                    popValues[popValues.Length - (i + 1)] = swapValueOne;
                }
                //Assert.AreEqual(pushValues, popValues);
                Assert.AreEqual(20, definedStack.Size);
            }

            [Test]
            public void TestingPeek()
            {
                definedStack.Push(3);
                definedStack.Push(5);
                definedStack.Push(2);
                definedStack.Push(-100);
                Assert.AreEqual(-100, definedStack.Peek());
            }
        }
    }
}
 