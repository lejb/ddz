using System.Collections.Generic;
using static Logic.Core.CardPoint;
using static Logic.Core.CardSuit;

namespace Logic.Core
{
    public static class Cards
    {
        public static Card SA = Spade & A;
        public static Card S2 = Spade & _2;
        public static Card S3 = Spade & _3;
        public static Card S4 = Spade & _4;
        public static Card S5 = Spade & _5;
        public static Card S6 = Spade & _6;
        public static Card S7 = Spade & _7;
        public static Card S8 = Spade & _8;
        public static Card S9 = Spade & _9;
        public static Card S0 = Spade & _10;
        public static Card SJ = Spade & J;
        public static Card SQ = Spade & Q;
        public static Card SK = Spade & K;
        public static Card HA = Heart & A;
        public static Card H2 = Heart & _2;
        public static Card H3 = Heart & _3;
        public static Card H4 = Heart & _4;
        public static Card H5 = Heart & _5;
        public static Card H6 = Heart & _6;
        public static Card H7 = Heart & _7;
        public static Card H8 = Heart & _8;
        public static Card H9 = Heart & _9;
        public static Card H0 = Heart & _10;
        public static Card HJ = Heart & J;
        public static Card HQ = Heart & Q;
        public static Card HK = Heart & K;
        public static Card CA = Club & A;
        public static Card C2 = Club & _2;
        public static Card C3 = Club & _3;
        public static Card C4 = Club & _4;
        public static Card C5 = Club & _5;
        public static Card C6 = Club & _6;
        public static Card C7 = Club & _7;
        public static Card C8 = Club & _8;
        public static Card C9 = Club & _9;
        public static Card C0 = Club & _10;
        public static Card CJ = Club & J;
        public static Card CQ = Club & Q;
        public static Card CK = Club & K;
        public static Card DA = Diamond & A;
        public static Card D2 = Diamond & _2;
        public static Card D3 = Diamond & _3;
        public static Card D4 = Diamond & _4;
        public static Card D5 = Diamond & _5;
        public static Card D6 = Diamond & _6;
        public static Card D7 = Diamond & _7;
        public static Card D8 = Diamond & _8;
        public static Card D9 = Diamond & _9;
        public static Card D0 = Diamond & _10;
        public static Card DJ = Diamond & J;
        public static Card DQ = Diamond & Q;
        public static Card DK = Diamond & K;
        public static Card jo = (CardSuit)null & joker;
        public static Card JO = (CardSuit)null & JOKER;

        public static IEnumerable<Card> AllCards { get; } = new List<Card>
        {
            S2, S3, S4, S5, S6, S7, S8, S9, S0, SJ, SQ, SK, SA,
            H2, H3, H4, H5, H6, H7, H8, H9, H0, HJ, HQ, HK, HA,
            C2, C3, C4, C5, C6, C7, C8, C9, C0, CJ, CQ, CK, CA,
            D2, D3, D4, D5, D6, D7, D8, D9, D0, DJ, DQ, DK, DA,
            jo, JO
        };

        private static Dictionary<string, Card> stringCardMatcher = new Dictionary<string, Card>
        {
            { "S2", S2 },{ "S3", S3 },{ "S4", S4 },{ "S5", S5 },{ "S6", S6 },{ "S7", S7 },{ "S8", S8 },
            { "S9", S9 },{ "S0", S0 },{ "SJ", SJ },{ "SQ", SQ },{ "SK", SK },{ "SA", SA },
            { "H2", H2 },{ "H3", H3 },{ "H4", H4 },{ "H5", H5 },{ "H6", H6 },{ "H7", H7 },{ "H8", H8 },
            { "H9", H9 },{ "H0", H0 },{ "HJ", HJ },{ "HQ", HQ },{ "HK", HK },{ "HA", HA },
            { "C2", C2 },{ "C3", C3 },{ "C4", C4 },{ "C5", C5 },{ "C6", C6 },{ "C7", C7 },{ "C8", C8 },
            { "C9", C9 },{ "C0", C0 },{ "CJ", CJ },{ "CQ", CQ },{ "CK", CK },{ "CA", CA },
            { "D2", D2 },{ "D3", D3 },{ "D4", D4 },{ "D5", D5 },{ "D6", D6 },{ "D7", D7 },{ "D8", D8 },
            { "D9", D9 },{ "D0", D0 },{ "DJ", DJ },{ "DQ", DQ },{ "DK", DK },{ "DA", DA },
            { "jo", jo },{ "JO", JO }
        };

        public static Card FromString(string str)
        {
            if (stringCardMatcher.ContainsKey(str)) return stringCardMatcher[str];
            return null;
        }
    }
}