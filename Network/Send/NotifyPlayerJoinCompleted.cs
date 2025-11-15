// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.NotifyPlayerJoinCompleted
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class NotifyPlayerJoinCompleted : SendPacket
{
  private long PlayerID;
  private long GameID;

  public NotifyPlayerJoinCompleted(long PlayerID, long GameID)
  {
    this.PlayerID = PlayerID;
    this.GameID = GameID;
  }

  protected internal override void make()
  {
    this.WriteInt("GID\0", this.GameID);
    this.WriteInt("PID\0", this.PlayerID);
    this.CreatePacket((ushort) 4, (ushort) 30, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
