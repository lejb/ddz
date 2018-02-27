using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Core;
using Logic.DDZ;
using Logic.DDZ.Types;
using static Logic.Core.Cards;

namespace Logic.Tests
{
    [TestClass]
    public class DDZClassificationTests
    {
        private List<Card> cards;

        private void Test(Type expectedType)
        {
            DDZClassifier classifier = new DDZClassifier();
            HandType result = classifier.Classify(new CompressedCardList(new Hand(cards)));
            Assert.IsInstanceOfType(result, expectedType);
        }

        [TestMethod]
        public void Classify_1_2_3()
        {
            cards = new List<Card>() { C3 }; Test(typeof(DDZ_1));
            cards = new List<Card>() { jo }; Test(typeof(DDZ_1));
            cards = new List<Card>() { JO }; Test(typeof(DDZ_1));
            cards = new List<Card>() { C3, D3 }; Test(typeof(DDZ_2));
            cards = new List<Card>() { S3, D3 }; Test(typeof(DDZ_2));
            cards = new List<Card>() { HA, HA }; Test(typeof(DDZ_2));
            cards = new List<Card>() { C3, D3, S3 }; Test(typeof(DDZ_3));
            cards = new List<Card>() { H3, D3, S3 }; Test(typeof(DDZ_3));
            cards = new List<Card>() { CK, DK, DK }; Test(typeof(DDZ_3));
            cards = new List<Card>() { C3, S4 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, jo }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, JO }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S4 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CK, D3, S4 }; Test(typeof(ErrorType));
        }

        [TestMethod]
        public void Classify_Bombs()
        {
            cards = new List<Card>() { C3, D3, S3, H3 }; Test(typeof(DDZ_Bomb));
            cards = new List<Card>() { C3, C3, C3, C3 }; Test(typeof(DDZ_Bomb));
            cards = new List<Card>() { jo, JO }; Test(typeof(DDZ_SuperBomb));
            cards = new List<Card>() { C3, D3, S3, D3, C4 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, D3, C3 }; Test(typeof(ErrorType));
            cards = new List<Card>() { jo, JO, jo, JO }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, jo, JO }; Test(typeof(ErrorType));
        }

        [TestMethod]
        public void Classify_n_plus_p()
        {
            cards = new List<Card>() { C3, D3, S3, H4 }; Test(typeof(DDZ_3_1));
            cards = new List<Card>() { C3, D3, S3, JO }; Test(typeof(DDZ_3_1));
            cards = new List<Card>() { C3, D3, S3, JO, jo }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, C5, S6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, jo, jo }; Test(typeof(DDZ_3_2));
            cards = new List<Card>() { C3, D3, S3, C2, D2 }; Test(typeof(DDZ_3_2));
            cards = new List<Card>() { C3, D3, S3, H3, H4, C5 }; Test(typeof(DDZ_4_2));
            cards = new List<Card>() { C3, D3, S3, H3, JO, jo }; Test(typeof(DDZ_4_2));
            cards = new List<Card>() { C3, D3, S3, H3, H4, C4 }; Test(typeof(DDZ_4_2));
            cards = new List<Card>() { C3, D3, S3, C3, S6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, C3, S6, C7, S8 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, H3, H4, C4, D8, H8 }; Test(typeof(DDZ_4_2_2));
            cards = new List<Card>() { C3, D3, S3, H3, JO, JO, jo, jo }; Test(typeof(DDZ_4_2_2));
            cards = new List<Card>() { C3, D3, S3, C3, S6, C7, S8, C9 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, C3, S6, C6, S6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D3, S3, C3, S6, C6, S6, D6 }; Test(typeof(ErrorType));
        }

        [TestMethod]
        public void Classify_Straights()
        {
            cards = new List<Card>() { C3, D4, S5, H6, C7 }; Test(typeof(DDZ_Straight));
            cards = new List<Card>() { C0, DJ, SQ, HK, CA }; Test(typeof(DDZ_Straight));
            cards = new List<Card>() { C3, D4, S5, H6, C7, C8, C9, S0, SJ, DQ, HK, SA }; Test(typeof(DDZ_Straight));
            cards = new List<Card>() { C3, HK, S0, SJ, S5, H6, C7, SA, D4, C9, C8, DQ }; Test(typeof(DDZ_Straight));
            cards = new List<Card>() { CJ, DQ, SK, HA, C2 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C2, D3, S4, H5, C6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S5, H5, C6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C4, D5, S6, H7, C9 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C4, D4, S5, H6, C7 }; Test(typeof(ErrorType));
            cards = new List<Card>() { D4, S5, H6, C7 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H5, C4, D5 }; Test(typeof(DDZ_Straight2));
            cards = new List<Card>() { CQ, DA, SQ, HK, CA, HK }; Test(typeof(DDZ_Straight2));
            cards = new List<Card>() { C3, D4, S5, H6, C7, C8, C9, S0, SJ, DQ, HK, SA,
                C3, HK, S0, SJ, S5, H6, C7, SA, D4, C9, C8, DQ}; Test(typeof(DDZ_Straight2));
            cards = new List<Card>() { C4, D4, S5, H5 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C2, D2, S3, H4, C3, C4 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CK, DK, SA, HA, C2, C2 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CK, DK, SA, HA, JO, jo }; Test(typeof(ErrorType));
            cards = new List<Card>() { C6, C6, S3, H4, C3, C4 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4 }; Test(typeof(DDZ_Plane0));
            cards = new List<Card>() { CK, DA, SA, HK, CA, HK }; Test(typeof(DDZ_Plane0));
            cards = new List<Card>() { C3, D4, S5, H6, C7, C8, C9, S0, SJ, DQ, HK, SA,
                C3, HK, S0, SJ, S5, H6, C7, SA, D4, C9, C8, DQ,
                C3, HK, S0, SJ, S5, H6, C7, SA, D4, C9, C8, DQ }; Test(typeof(DDZ_Plane0));
            cards = new List<Card>() { C2, D2, S2, H3, C3, C3 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C2, D2, SA, HA, CA, C2 }; Test(typeof(ErrorType));
            cards = new List<Card>() { jo, jo, jo, JO, JO, JO }; Test(typeof(ErrorType));
        }

        [TestMethod]
        public void Classify_Planes()
        {
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D6 }; Test(typeof(DDZ_Plane1));
            cards = new List<Card>() { CK, DA, SA, HK, CA, HK, jo, JO }; Test(typeof(DDZ_Plane1));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5 }; Test(typeof(DDZ_Plane1));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C7, C8, C9 }; Test(typeof(DDZ_Plane1));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C8, C8, C9 }; Test(typeof(DDZ_Plane1));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, H6, H7 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C7, C8 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C5 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CQ, DA, SQ, HQ, CA, DA, H5, D6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, D6, C6 }; Test(typeof(DDZ_Plane2));
            cards = new List<Card>() { CK, DA, SA, HK, CA, HK, jo, jo, JO, JO }; Test(typeof(DDZ_Plane2));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C8, C8, C9, H9, D2, S2 }; Test(typeof(DDZ_Plane2));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, H6, H7, H5, H6, H7 }; Test(typeof(ErrorType));
            cards = new List<Card>() { C3, D4, S3, H3, C4, D4, H5, D5, H5, C7, C7, C8, C8 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CQ, DA, SQ, HQ, CA, DA, H5, D6, H5, D6 }; Test(typeof(ErrorType));
            cards = new List<Card>() { CQ, SQ, HQ, H5, D6, H5, D6 }; Test(typeof(ErrorType));
        }
    }
}
