using System.Collections.Generic;

public class PacketQueue
{
    public static PacketQueue _instance;
    public static PacketQueue Instance
    {
        get
        { 
            if (_instance == null)
            {
                _instance = new PacketQueue();
            }
            return _instance; 
        }
    }

    Queue<IPacket> _packetQueue = new Queue<IPacket>();
    object _lock = new object();

    public void Push(IPacket packet)
    {
        lock (_lock)
        {
            _packetQueue.Enqueue(packet);
        }
    }

    public IPacket Pop()
    {
        lock (_lock)
        {
            if (_packetQueue.Count == 0)
            {
                return null;
            }
        }

        return _packetQueue.Dequeue();
    }

    public List<IPacket> PapAll()
    {
        List<IPacket> list = new List<IPacket>();

        lock (_lock)
        {
            while (_packetQueue.Count > 0)
            {
                list.Add(_packetQueue.Dequeue());
            }
        }

        return list;
    }
}
