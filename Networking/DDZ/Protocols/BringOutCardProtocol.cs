using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Core;
using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class BringOutCardProtocol : IDDZGameFlowProtocols
    {
        public PlayerID PlayerID { get; private set; }

        public IList<Card> Cards { get; private set; }

        public BringOutCardProtocol(PlayerID playerID, IEnumerable<Card> cards)
        {
            PlayerID = playerID;
            Cards = cards.ToList();
        }

        public BringOutCardProtocol() { }

        public ProtocolType Type => ProtocolType.DDZ_BringOutCard;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            byte[] data = new byte[size];
            Array.Copy(bytes, startOffset, data, 0, size);

            PlayerID = (PlayerID)BitConverter.ToInt32(data, 0);
            int nCards = BitConverter.ToInt32(data, sizeof(int));

            Cards = new List<Card>();
            for (int i = 0; i < nCards; i++)
            {
                byte[] card = new byte[2];
                Array.Copy(data, 2 * sizeof(int) + 2 * i, card, 0, 2);
                Cards.Add(Logic.Core.Cards.FromString(Encoding.UTF8.GetString(card)));
            }
        }

        public byte[] ConvertDataToBytes()
        {
            int nCards = Cards.Count();
            int headerSize = 2 * sizeof(int);
            byte[] data = new byte[headerSize + 2 * nCards];
            Array.Clear(data, 0, data.Length);

            Array.Copy(BitConverter.GetBytes((int)PlayerID), 0, data, 0, sizeof(int));
            Array.Copy(BitConverter.GetBytes(nCards), 0, data, sizeof(int), sizeof(int));
            for (int i = 0; i < nCards; i++)
            {
                byte[] card = Encoding.UTF8.GetBytes(Cards[i].ToString());
                Array.Copy(card, 0, data, 2 * i + 2 * sizeof(int), card.Length);
            }

            return data;
        }
    }
}
