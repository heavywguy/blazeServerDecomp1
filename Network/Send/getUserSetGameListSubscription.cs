// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.getUserSetGameListSubscription
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class getUserSetGameListSubscription : SendPacket
{
  private List<long> pcap;
  private long gid;
  private long tcap;

  public getUserSetGameListSubscription(List<long> pcap, long gid, long tcap)
  {
    this.pcap = pcap;
    this.gid = gid;
    this.tcap = tcap;
  }

  protected internal override void make()
  {
    this.WriteList("CAP\0", (byte) 0, this.pcap.Count, (object) this.pcap);
    this.WriteInt("GID\0", this.gid);
    this.WriteInt("TCAP", this.tcap);
    this.CreatePacket((ushort) 4, (ushort) 111, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
