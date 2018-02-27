using System;
using System.Collections.Generic;
using System.Linq;
using GameFlow.Core;
using Networking.Protocol;

namespace Networking.DDZ.Protocols
{
    public class UpdateInfoProtocol_S : IDDZGameFlowProtocols
    {
        public IList<IPlayerForInfo> Infos { get; private set; }

        public UpdateInfoProtocol_S(IList<IPlayerForInfo> infos) => Infos = infos;

        public UpdateInfoProtocol_S() { }

        public ProtocolType Type => ProtocolType.DDZ_UpdateInfo_S;

        public void ConstructFromBytesData(byte[] bytes, int startOffset, int size)
        {
            byte[] data = new byte[size];
            Array.Copy(bytes, startOffset, data, 0, size);

            int listCount = BitConverter.ToInt32(data, 0);
            Infos = new List<IPlayerForInfo>();

            int pointer = sizeof(int);
            for (int i = 0; i < listCount; i++)
            {
                int elementSize = BitConverter.ToInt32(data, pointer);
                pointer += sizeof(int);
                var info = new PlayerInfo();
                info.FromBytes(data, pointer, elementSize);
                pointer += elementSize;
                Infos.Add(info);
            }
        }

        public byte[] ConvertDataToBytes()
        {
            var infoList = (from info in Infos select (new PlayerInfo(info)).ToBytes()).ToList();
            int listCount = infoList.Count;
            var sizeList = (from bytes in infoList select bytes.Length).ToList();
            int headerSize = sizeof(int) * (1 + listCount);
            int totalSize = headerSize + sizeList.Sum();

            byte[] result = new byte[totalSize];
            Array.Copy(BitConverter.GetBytes(listCount), 0, result, 0, sizeof(int));
            int pointer = sizeof(int);
            for (int i = 0; i < listCount; i++)
            {
                Array.Copy(BitConverter.GetBytes(sizeList[i]), 0, result, pointer, sizeof(int));
                pointer += sizeof(int);
                Array.Copy(infoList[i], 0, result, pointer, sizeList[i]);
                pointer += sizeList[i];
            }

            return result;
        }
    }
}
