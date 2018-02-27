using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using Logic.DDZ;
using static Logic.DDZ.Tools;
using static Logic.Core.CardPoint;
using static Logic.Core.CardSuit;
using static Logic.Core.Cards;

namespace Logic.Tests
{
    [TestClass]
    public class DDZLogicTests
    {
        private DDZLogic logic = new DDZLogic();
        private List<Card> next;

        private void Init()
        {
            logic.Reset();
        }

        private void Test(bool expected)
        {
            Assert.AreEqual(expected, logic.Accept(next));
        }

        private void Pass(bool expected)
        {
            Assert.AreEqual(expected, logic.Accept(new List<Card>()));
        }

        [TestMethod]
        public void DDZLogicBasic()
        {
            logic = new DDZLogic();
            next = new List<Card>() { C5 }; Test(true);
            next = new List<Card>() { S6 }; Test(true);
            next = new List<Card>() { JO }; Test(true);
            next = new List<Card>() { C5 }; Test(false);
            next = new List<Card>() { jo }; Test(false);
            Pass(true);
            Pass(true);
            Pass(false);
            next = new List<Card>() { JO }; Test(true);
            next = new List<Card>() { C3, S3, D3, H3 }; Test(true);
            next = new List<Card>() { JO, jo }; Test(true);
            next = new List<Card>() { JO, jo }; Test(false);
            Init();
            Pass(false);
            next = new List<Card>() { jo, C3 }; Test(false);
            next = new List<Card>() { C3, S3, D3, H4, C5, D4, C4, D6 }; Test(true);
            next = new List<Card>() { C3, S3, D3, H4, C5, D4, C4, D6 }; Test(false);
            next = new List<Card>() { C5, S5, D7, H4, C5, D4, C4, D6 }; Test(true);
            Pass(true);
            next = new List<Card>() { C3, S3, D3, H3 }; Test(true);
            next = new List<Card>() { C5, S5, D5, H5 }; Test(true);
            next = new List<Card>() { JO }; Test(false);
        }
    }
}
