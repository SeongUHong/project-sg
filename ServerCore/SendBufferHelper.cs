using System;
using System.Threading;

namespace ServerCore
{
    public class SendBufferHelper
    {
        // 각 스레드별로 고유한 상태를 설정할 수 있는 공간
        // 데이터 원자성이 보장되기에 lock없이 접근하여 사용이 가능
        public static ThreadLocal<SendBuffer> CurrentBuffer = new ThreadLocal<SendBuffer>(() => { return null; });

        public static int ChunkSize { get; set; } = 65535 * 100;

        // 버퍼 생성
        public static ArraySegment<byte> Open(int reserveSize)
        {
            // 버퍼가 없다면 생성
            if (CurrentBuffer.Value == null)
                CurrentBuffer.Value = new SendBuffer(ChunkSize);

            // 이미 버퍼가 있지만 공간이 충분치 않다면 생성
            if (CurrentBuffer.Value.FreeSize < reserveSize)
                CurrentBuffer.Value = new SendBuffer(ChunkSize);

            return CurrentBuffer.Value.Open(reserveSize);
        }

        public static ArraySegment<byte> Close(int usedSize)
        {
            return CurrentBuffer.Value.Close(usedSize);
        }
    }
}
