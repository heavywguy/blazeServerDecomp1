// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.JoinGame
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class JoinGame : SendPacket
{
  private long gameid;
  private ushort packetID;

  public JoinGame(long gameid, ushort packetID)
  {
    this.gameid = gameid;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteInt("GID\0", this.gameid);
    this.WriteInt("JGS\0", 0L);
    this.CreatePacket((ushort) 4, (ushort) 9, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
