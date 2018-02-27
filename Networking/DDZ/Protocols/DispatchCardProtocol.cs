using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class DispatchCardProtocol : IDDZGameFlowProtocols
    {
        public IList<IList<Card>> Dispatch { get; private set; }

        public DispatchCardProtocol(IList<IList<Card>> dispatch) => Dispatch = dispatch;

        public DispatchCardProtocol() { }

        public ProtocolType Type => ProtocolType.DDZ_DispatchCard;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            byte[] data = new byte[size];
            Array.Copy(bytes, startOffset, data, 0, size);

            Dispatch = new List<IList<Card>>();

            int nGroup = BitConverter.ToInt32(data, 0);
            List<int> groupCount = new List<int>();

            for (int i = 0; i < nGroup; i++)
            {
                Dispatch.Add(new List<Card>());
                groupCount.Add(BitConverter.ToInt32(data, (i + 1) * sizeof(int)));
            }

            int headerSize = (1 + nGroup) * sizeof(int);
            int counter = 0;
            byte[] strBytes = new byte[2];

            for (int i = 0; i < nGroup; i++)
            {
                for (int j = 0; j < groupCount[i]; j++)
                {
                    int pointer = headerSize + counter * 2;
                    Array.Copy(data, pointer, strBytes, 0, 2);
                    counter++;
                    string str = Encoding.UTF8.GetString(strBytes).Trim();
                    Dispatch[i].Add(Cards.FromString(str));
                }
            }
        }

        public byte[] ConvertDataToBytes()
        {
            int nCards = Dispatch.Sum(group => group.Count);
            int headerSize = sizeof(int) + sizeof(int) * Dispatch.Count;
            byte[] data = new byte[headerSize + 2 * nCards];
            Array.Clear(data, 0, data.Length);

            Array.Copy(BitConverter.GetBytes(Dispatch.Count), data, sizeof(int));
            for (int i = 0; i < Dispatch.Count; i++)
            {
                Array.Copy(BitConverter.GetBytes(Dispatch[i].Count), 0, data, (i + 1) * sizeof(int), sizeof(int));
            }

            int counter = 0;
            for (int i = 0; i < Dispatch.Count; i++)
            {
                var list = Dispatch[i];
                for (int j = 0; j < list.Count; j++)
                {
                    int pointer = headerSize + counter * 2;
                    byte[] card = Encoding.UTF8.GetBytes(Dispatch[i][j].ToString());
                    Array.Copy(card, 0, data, pointer, card.Length);
                    counter++;
                }
            }

            return data;
        }
    }
}
