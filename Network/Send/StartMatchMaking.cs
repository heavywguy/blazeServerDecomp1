// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.StartMatchMaking
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class StartMatchMaking : SendPacket
{
  private long Session;
  private ushort packetID;

  public StartMatchMaking(long Session, ushort packetID)
  {
    this.Session = Session;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteStr("ESNM", "Yuzusoft");
    this.WriteInt("MSID", this.Session);
    this.WriteStr("SCID", "f9f7d98a-4c44-41fc-b451-757d8b0538df");
    this.WriteStr("STMN", "BlazeSample");
    this.CreatePacket((ushort) 4, (ushort) 13, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
