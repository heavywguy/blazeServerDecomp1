// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.UserSetingsLoad
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class UserSetingsLoad : SendPacket
{
  private ushort packetID;
  private object List1;
  private object List2;
  private int count;

  public UserSetingsLoad(object List1, object List2, int count, ushort packetID)
  {
    this.packetID = packetID;
    this.List1 = List1;
    this.List2 = List2;
    this.count = count;
  }

  protected internal override void make()
  {
    this.WriteDoubleList("SMAP", (byte) 1, (byte) 1, this.List1, this.List2, this.count);
    this.CreatePacket((ushort) 9, (ushort) 12, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
