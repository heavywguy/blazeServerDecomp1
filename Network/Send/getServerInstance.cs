// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.getServerInstance
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class getServerInstance : SendPacket
{
  private ushort packetID;
  private int port;

  public getServerInstance(ushort packetID, int port)
  {
    this.packetID = packetID;
    this.port = port;
  }

  protected internal override void make()
  {
    this.WriteInt("ADDR", 0L, (byte) 6);
    this.WriteStruct("VALU", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetStr("HOST", Config.RediHost),
      (Blaze.Tdf) this.SetInt("IP\0\0", Helper.GetIPfromString(Config.RediHost)),
      (Blaze.Tdf) this.SetInt("PORT", (long) this.port)
    });
    this.WriteInt("SECU", 0L);
    this.WriteInt("XDNS", 0L);
    this.CreatePacket((ushort) 5, (ushort) 1, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
