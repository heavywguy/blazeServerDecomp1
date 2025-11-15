// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.NotifyPlayerRemoved
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class NotifyPlayerRemoved : SendPacket
{
  private long playerid;
  private long gameid;
  private long reas;

  public NotifyPlayerRemoved(long playerid, long gameid, long reas)
  {
    this.playerid = playerid;
    this.gameid = gameid;
    this.reas = reas;
  }

  protected internal override void make()
  {
    this.WriteInt("CNTX", 0L);
    this.WriteInt("GID\0", this.gameid);
    this.WriteInt("PID\0", this.playerid);
    this.WriteInt("REAS", this.reas);
    this.CreatePacket((ushort) 4, (ushort) 40, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
