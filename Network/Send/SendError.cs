// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.SendError
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class SendError : SendPacket
{
  private ushort Component;
  private ushort Command;
  private ushort packetID;
  private int reasonID;

  public SendError(Blaze.Packet p, int reasonID)
  {
    this.Component = p.Component;
    this.Command = p.Command;
    this.packetID = p.ID;
    this.reasonID = reasonID;
  }

  protected internal override void make()
  {
    this.CreatePacket(this.Component, this.Command, this.reasonID, (ushort) 12288 /*0x3000*/, this.packetID);
  }
}
