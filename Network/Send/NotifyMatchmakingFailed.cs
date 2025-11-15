// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.NotifyMatchmakingFailed
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class NotifyMatchmakingFailed : SendPacket
{
  private long Session;
  private long PersonaID;

  public NotifyMatchmakingFailed(long Session, long PersonaID)
  {
    this.Session = Session;
    this.PersonaID = PersonaID;
  }

  protected internal override void make()
  {
    this.WriteInt("MAXF", 0L);
    this.WriteInt("RSLT", 3L);
    this.WriteInt("MSID", this.Session);
    this.WriteInt("USID", this.PersonaID);
    this.CreatePacket((ushort) 4, (ushort) 10, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
