using System;
using System.Text;
using GameFlow.Core;

namespace Networking.DDZ
{
    public class PlayerInfo : IPlayerForInfo
    {
        public PlayerID ID { get; set; }

        public string Name { get; set; }

        public bool Exist { get; set; }

        public bool Ready { get; set; }

        public PlayerInfo(IPlayerForInfo info)
        {
            ID = info.ID;
            Name = info.Name;
            Exist = info.Exist;
            Ready = info.Ready;
        }

        public PlayerInfo() { }

        public byte[] ToBytes()
        {
            byte[] name = Encoding.UTF8.GetBytes(Name);
            byte[] result = new byte[name.Length + 3 * sizeof(int)];
            Array.Copy(BitConverter.GetBytes((int)ID), 0, result, 0, sizeof(int));
            Array.Copy(BitConverter.GetBytes(Exist ? 1 : 0), 0, result, sizeof(int), sizeof(int));
            Array.Copy(BitConverter.GetBytes(Ready ? 1 : 0), 0, result, sizeof(int) * 2, sizeof(int));
            Array.Copy(name, 0, result, sizeof(int) * 3, name.Length);
            return result;
        }

        public void FromBytes(byte[] bytes, int startOffset, int size)
        {
            byte[] data = new byte[size];
            Array.Copy(bytes, startOffset, data, 0, size);

            ID = (PlayerID)BitConverter.ToInt32(data, 0);
            Exist = BitConverter.ToInt32(data, sizeof(int)) == 1;
            Ready = BitConverter.ToInt32(data, 2 * sizeof(int)) == 1;
            Name = Encoding.UTF8.GetString(data, 3 * sizeof(int), size - 3 * sizeof(int));
        }
    }
}
