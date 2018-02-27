using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using static Logic.Core.Cards;
using static Logic.Core.CardPoint;

namespace Logic.Tests
{
    [TestClass]
    public class CompressionTests
    {
        [TestMethod]
        public void Compression()
        {
            var test = new List<Card>() { C3, H4, S4, JO, SK, jo, C4, D4, S3, C3, H4 };
            var expectPoints = new List<CardPoint>() { K, joker, JOKER, _3, _4 };
            var expectCounts = new List<int>() { 1, 1, 1, 3, 5 };

            CompressedCardList compressedList = new CompressedCardList(new Hand(test));
            for (int i = 0; i < expectPoints.Count; i++)
            {
                Assert.AreEqual(expectPoints[i], compressedList[i].Point);
                Assert.AreEqual(expectCounts[i], compressedList[i].Count);
                Assert.AreEqual(expectPoints[i], compressedList.SortedPointList[i]);
                Assert.AreEqual(expectCounts[i], compressedList.SortedCount[i]);
            }
        }
    }
}
