// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.UpdateExtendedDataAttribute
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class UpdateExtendedDataAttribute : SendPacket
{
  private long GameID;

  public UpdateExtendedDataAttribute(long GameID) => this.GameID = GameID;

  protected internal override void make()
  {
    this.WriteInt("FLGS", 3L);
    this.WriteInt("ID\0\0", this.GameID);
    this.CreatePacket((ushort) 30722, (ushort) 5, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
