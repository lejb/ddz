using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using static Logic.DDZ.Tools;
using static Logic.Core.Cards;

namespace Logic.Tests
{
    [TestClass]
    public class DisplayOrderTests
    {
        private List<Card> cards, expected;

        private void Test()
        {
            Assert.IsTrue(ListCompare(expected, cards.DisplayOrder()));
        }

        [TestMethod]
        public void DisplayOrder()
        {
            cards = new List<Card>() { C5, C3, S5, jo, S6, C6, D5, H3, D3, JO, H5, C2, D2 };
            expected = new List<Card>() { S5, H5, C5, D5, H3, C3, D3, C2, D2, S6, C6, JO, jo };
            Test();
        }
    }
}
