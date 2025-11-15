// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.getAuthToken
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class getAuthToken : SendPacket
{
  private ushort packetID;

  public getAuthToken(ushort packetID) => this.packetID = packetID;

  protected internal override void make()
  {
    this.WriteStr("AUTH", "some_key_login");
    this.CreatePacket((ushort) 1, (ushort) 36, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
