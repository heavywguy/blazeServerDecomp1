// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.getGameListSnapshot
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class getGameListSnapshot : SendPacket
{
  private long gameid;
  private long gsta;

  public getGameListSnapshot(long gameid, long gsta)
  {
    this.gameid = gameid;
    this.gsta = gsta;
  }

  protected internal override void make()
  {
    this.WriteInt("GID\0", this.gameid);
    this.WriteInt("GSTA", this.gsta);
    this.CreatePacket((ushort) 4, (ushort) 100, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
