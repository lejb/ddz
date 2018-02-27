using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Logic.Core.CardPoint;
using static Logic.Core.CardSuit;
using static Logic.Core.Cards;

namespace Logic.Tests
{
    [TestClass]
    public class CardComparisonTests
    {
        [TestMethod]
        public void ComparePoint()
        {
            Assert.IsTrue(_3 - _4 < 0);
            Assert.IsTrue(_5 - _4 > 0);
            Assert.IsTrue(_3 - _3 == 0);
            Assert.IsFalse(_3 - _4 > 0);
        }

        [TestMethod]
        public void CompareSuit()
        {
            Assert.IsTrue(Spade - Heart < 0);
            Assert.IsTrue(Club - Heart > 0);
            Assert.IsTrue(Heart - Heart == 0);
            Assert.IsFalse(Spade - Heart > 0);
        }

        [TestMethod]
        public void CompareCard()
        {
            Assert.IsTrue(S3 - S4 < 0);
            Assert.IsTrue(S4 - S3 > 0);
            Assert.IsTrue(S3 - S3 == 0);
            Assert.IsFalse(S3 - S4 > 0);
            Assert.IsTrue(S3 - C3 == 0);
            Assert.IsTrue(C3 - S3 == 0);
            Assert.IsTrue(JO - jo > 0);
            Assert.IsTrue(JO - JO == 0);
            Assert.IsTrue(JO - SK > 0);
        }
    }
}
