using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using Logic.DDZ;
using Logic.DDZ.Types;
using static Logic.Core.Cards;
using Microsoft.CSharp;

namespace Logic.Tests
{
    [TestClass]
    public class DDZComparisonTests
    {
        private List<Card> cards1;
        private List<Card> cards2;

        private void Test(int expectedResult)
        {
            DDZClassifier classifier = new DDZClassifier();
            HandType type1 = classifier.Classify(new CompressedCardList(new Hand(cards1)));
            HandType type2 = classifier.Classify(new CompressedCardList(new Hand(cards2)));
            int compareResult = HandType.Compare(type1, type2);
            if (compareResult < 0) compareResult = -1;
            if (compareResult > 0) compareResult = 1;
            Assert.AreEqual(expectedResult, compareResult);
        }

        [TestMethod]
        public void Comparison_1_2_3()
        {
            cards1 = new List<Card>() { C3 };
            cards2 = new List<Card>() { S3 }; Test(0);
            cards2 = new List<Card>() { H3 }; Test(0);
            cards2 = new List<Card>() { C2 }; Test(-1);
            cards2 = new List<Card>() { C4 }; Test(-1);

            cards1 = new List<Card>() { jo };
            cards2 = new List<Card>() { jo }; Test(0);
            cards2 = new List<Card>() { JO }; Test(-1);
            cards2 = new List<Card>() { C4 }; Test(1);
            cards2 = new List<Card>() { HA }; Test(1);
            cards2 = new List<Card>() { C2 }; Test(1);
            cards2 = new List<Card>() { C4, C5 }; Test(0);
            cards2 = new List<Card>() { C5, C5 }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C5, S5 };
            cards2 = new List<Card>() { H5, D5 }; Test(0);
            cards2 = new List<Card>() { H6, C6 }; Test(-1);
            cards2 = new List<Card>() { C3, S3 }; Test(1);
            cards2 = new List<Card>() { C5, JO }; Test(0);
            cards2 = new List<Card>() { C2 }; Test(0);
            cards2 = new List<Card>() { C4, C5 }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4 };
            cards2 = new List<Card>() { H4, D4, D4 }; Test(0);
            cards2 = new List<Card>() { C4, S4 }; Test(0);
            cards2 = new List<Card>() { D4 }; Test(0);
            cards2 = new List<Card>() { C5, S5, D5 }; Test(-1);
            cards2 = new List<Card>() { C2, S2, D2 }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3 }; Test(1);
        }

        [TestMethod]
        public void Comparison_BasicBombs()
        {
            cards1 = new List<Card>() { C4, S4, D4, H4 };
            cards2 = new List<Card>() { C4, S4, D4, H4 }; Test(0);
            cards2 = new List<Card>() { C2, S2, D2, H2 }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3, H3 }; Test(1);
            cards2 = new List<Card>() { CA, SA, DA }; Test(1); 
            cards2 = new List<Card>() { CA, SA }; Test(1);
            cards2 = new List<Card>() { CA }; Test(1);
            cards2 = new List<Card>() { jo, JO }; Test(-1);
            cards2 = new List<Card>() { C4, S5 }; Test(0);

            cards1 = new List<Card>() { jo, JO };
            cards2 = new List<Card>() { C4, S5 }; Test(0);
            cards2 = new List<Card>() { C4, S5, JO }; Test(0);
            cards2 = new List<Card>() { jo, JO }; Test(0);
            cards2 = new List<Card>() { C2, S2, D2, H2 }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3 }; Test(1);
            cards2 = new List<Card>() { CA, SA }; Test(1);
            cards2 = new List<Card>() { CA }; Test(1);

            cards1 = new List<Card>() { C3 };                         
            cards2 = new List<Card>() { C4, S4, D4, H4 }; Test(0);
            cards2 = new List<Card>() { jo, JO }; Test(0);
            cards1 = new List<Card>() { C3, C4 };
            cards2 = new List<Card>() { C4, S4, D4, H4 }; Test(0);
            cards2 = new List<Card>() { jo, JO }; Test(0);
            cards1 = new List<Card>() { C3, C4, D4 };
            cards2 = new List<Card>() { C4, S4, D4, H4 }; Test(0);
            cards2 = new List<Card>() { jo, JO }; Test(0);
            cards1 = new List<Card>() { C3, C4, D6, S5 };
            cards2 = new List<Card>() { C4, S4, D4, H4 }; Test(0);
            cards2 = new List<Card>() { jo, JO }; Test(0);
        }

        [TestMethod]
        public void Comparison_n_plus_p()
        {
            cards1 = new List<Card>() { C4, S4, D4, jo };
            cards2 = new List<Card>() { C4, S4, D4, JO }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, C5 }; Test(0);
            cards2 = new List<Card>() { C5, S5, D5, JO }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3, JO }; Test(1);
            cards2 = new List<Card>() { C4, S4, D3, JO }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4, CA, SA };
            cards2 = new List<Card>() { C4, S4, D4, C3, S3 }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, C5, S6 }; Test(0);
            cards2 = new List<Card>() { C5, S5, D5, JO, JO }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C4, S4, D4, JO }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4, H4, SA, C2 };
            cards2 = new List<Card>() { C4, S4, D4, H4, C3, S3 }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, H4, C2, jo }; Test(0);
            cards2 = new List<Card>() { C5, S5, D5, H5, JO, JO }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3, H3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C4, S4, D4, JO, jo }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, JO }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4, H4, SA, CA, H7, C7 };
            cards2 = new List<Card>() { C4, S4, D4, H4, jo, jo, C3, S3 }; Test(0);
            cards2 = new List<Card>() { C5, S5, D5, H5, C3, H3, JO, JO }; Test(-1);
            cards2 = new List<Card>() { C3, S3, D3, H3, jo, jo, JO, JO }; Test(1);
            cards2 = new List<Card>() { C4, S4, D4, H4, C2, jo }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, JO, jo }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4, JO }; Test(0);
            cards2 = new List<Card>() { C4, S4, D4 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4, H4 };
            cards2 = new List<Card>() { C3, S3, D3, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, H3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, H3, jo, jo, JO, JO }; Test(1);

            cards1 = new List<Card>() { jo, JO };
            cards2 = new List<Card>() { C3, S3, D3, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, H3, JO, JO }; Test(1);
            cards2 = new List<Card>() { C3, S3, D3, H3, jo, jo, JO, JO }; Test(1);
        }

        [TestMethod]
        public void Comparison_Straights()
        {
            cards1 = new List<Card>() { C8, S4, D7, H6, H5 };
            cards2 = new List<Card>() { C4, S8, D7, H6, C5 }; Test(0);
            cards2 = new List<Card>() { C5, S7, D8, S6, D9 }; Test(-1);
            cards2 = new List<Card>() { C3, S4, D5, H6, H7 }; Test(1);
            cards2 = new List<Card>() { C4, S8, D7, H6, C5, H9 }; Test(0);

            cards1 = new List<Card>() { C8, S8, D7, H6, H6, D7 };
            cards2 = new List<Card>() { C7, S8, D7, H6, C8, H6 }; Test(0);
            cards2 = new List<Card>() { C7, S7, D8, S9, D9, H8 }; Test(-1);
            cards2 = new List<Card>() { C5, S6, D5, H6, H7, D7 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H6, C8, H6, H9, H9 }; Test(0);

            cards1 = new List<Card>() { C6, S7, D7, H6, H6, D7 };
            cards2 = new List<Card>() { C7, S6, D7, H6, C7, H6 }; Test(0);
            cards2 = new List<Card>() { C7, S7, D8, S8, D7, H8 }; Test(-1);
            cards2 = new List<Card>() { C5, S6, D5, H6, H5, D6 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H7, C8, H8, H9, H9, D9 }; Test(0);

            cards1 = new List<Card>() { C4, S4, D4, H4 };
            cards2 = new List<Card>() { C4, S8, D7, H6, C5, H9 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H6, C8, H6, H9, H9 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H7, C8, H8, H9, H9, D9 }; Test(1);

            cards1 = new List<Card>() { jo, JO };
            cards2 = new List<Card>() { C4, S8, D7, H6, C5, H9 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H6, C8, H6, H9, H9 }; Test(1);
            cards2 = new List<Card>() { C7, S8, D7, H7, C8, H8, H9, H9, D9 }; Test(1);
        }

        [TestMethod]
        public void Comparison_Planes()
        {
            cards1 = new List<Card>() { C4, S4, D5, H5, H5, D4, D7, S8 };
            cards2 = new List<Card>() { C4, S4, D5, H5, H5, D4, jo, JO }; Test(0);
            cards2 = new List<Card>() { C6, S6, D5, H5, H5, D6, D7, S8 }; Test(-1);
            cards2 = new List<Card>() { C4, S4, D3, H3, H3, D4, D7, S8 }; Test(1);
            cards2 = new List<Card>() { CJ, SJ, DQ, HQ, HQ, DJ, DK, SK, HK, jo, JO, D9 }; Test(0);
            cards1 = new List<Card>() { C4, S4, D5, H5, H5, D4, D7, C7, H8, S8 };
            cards2 = new List<Card>() { C4, S4, D5, H5, H5, D4, DK, SQ, HK, HQ }; Test(0);
            cards2 = new List<Card>() { C6, S6, D5, H5, H5, D6, D7, C7, H8, S8 }; Test(-1);
            cards2 = new List<Card>() { C4, S4, D3, H3, H3, D4, D7, C7, H8, S8 }; Test(1);
            cards2 = new List<Card>() { CJ, SJ, DQ, HQ, HQ, DJ, DK, SK, HK, H7, S8, D9, H9, D8, S7 }; Test(0);
        }
    }
}
