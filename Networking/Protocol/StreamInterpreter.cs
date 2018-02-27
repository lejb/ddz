using System;

namespace Networking.Protocol
{
    public delegate void BufferOverflowEventHandler();
    public delegate void NewMessageEventHandler(IProtocol protocol);

    public abstract class StreamInterpreter
    {
        public event BufferOverflowEventHandler BufferOverflow;
        public abstract event NewMessageEventHandler NewMessage;

        protected const int bufferSize = 65536;
        protected byte[] buffer;
        protected int bufferPointer;

        public StreamInterpreter() => Reset();

        public void Reset()
        {
            bufferPointer = 0;
            buffer = new byte[bufferSize];
            Array.Clear(buffer, 0, buffer.Length);
        }

        public void AddToBuffer(byte[] bytes, int offset, int size)
        {
        //    Console.WriteLine($"receive {size} bytes");

            int dataSize = bufferPointer;
            int newSize = dataSize + size;

            if (newSize > bufferSize)
            {
                BufferOverflow?.Invoke();
                return;
            }

            Array.Copy(bytes, offset, buffer, bufferPointer, size);
            bufferPointer += size;
            OnData();
        }

        protected void Eat(int size)
        {
            int shiftSize = Math.Min(size, bufferPointer);
            Array.Copy(buffer, shiftSize, buffer, 0, bufferPointer - shiftSize);
            bufferPointer -= shiftSize;
        }

        protected abstract void OnData();
    }
}
