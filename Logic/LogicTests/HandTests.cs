using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using static Logic.Core.Cards;

namespace Logic.Tests
{
    [TestClass]
    public class HandTests
    {
        [TestMethod]
        public void Hand()
        {
            var test = new List<Card>() { C3, H4, S4, JO, SK, jo, C4, D4, S3, C3, H4 };
            var expect = new List<Card>() { S3, C3, C3, S4, H4, H4, C4, D4, SK, jo, JO };

            Hand hand = new Hand(test);
            for (int i = 0; i < test.Count; i++)
            {
                Assert.AreEqual(expect[i], hand.SortedCards[i]);
                Assert.AreEqual(test[i], hand.OriginalCards[i]);
            }
        }

        [TestMethod]
        public void HandEmpty()
        {
            var test = new List<Card>();
            Hand hand = new Hand(test);
            Assert.AreEqual(0, hand.OriginalCards.Count);
            Assert.AreEqual(0, hand.SortedCards.Count);
        }
    }
}
