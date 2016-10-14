using System;
using System.Collections.Generic;

public class TheatricsMessage : Message
{
    public byte fanIntensity { get; private set; }

    public TheatricsMessage(byte fanIntensity, DateTime timestamp) :
        base(0x3, timestamp)
    {
        this.fanIntensity = fanIntensity;
    }
    public TheatricsMessage(byte[] bytes) :
        base(0, DateTime.Now)
    {
        TryParseBytes(bytes);
    }
    protected override bool TryParseBytes(byte[] bytes)
    {
        
        //This is not needed, we will never get an inbound Theatrics Message.
        return true;
    }
   public override byte[] GetBytes()
    {
        var bytes = new List<byte>();
        bytes.AddRange(GetBaseBytes());
        bytes.Add(fanIntensity);
        return bytes.ToArray();

    }

	
}
