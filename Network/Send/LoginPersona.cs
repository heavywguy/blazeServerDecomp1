// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.LoginPersona
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class LoginPersona : SendPacket
{
  private ushort packetID;
  private long GameID;
  private string MAIL;
  private string NAME;
  private long UserID;

  public LoginPersona(long GameID, string MAIL, string NAME, long UserID, ushort packetID)
  {
    this.packetID = packetID;
    this.GameID = GameID;
    this.MAIL = MAIL;
    this.NAME = NAME;
    this.UserID = UserID;
  }

  protected internal override void make()
  {
    this.WriteInt("BUID", this.GameID);
    this.WriteInt("FRST", 0L);
    this.WriteStr("KEY\0", "85e940b5-59a3-4cdf-a322-701d6477f1ec");
    this.WriteInt("LLOG", (long) Helper.GetUnixTimeStamp());
    this.WriteStr("MAIL", this.MAIL);
    this.WriteStruct("PDTL", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetStr("DSNM", this.NAME),
      (Blaze.Tdf) this.SetInt("LAST", (long) Helper.GetUnixTimeStamp()),
      (Blaze.Tdf) this.SetInt("PID\0", this.GameID),
      (Blaze.Tdf) this.SetInt("STAS", 0L),
      (Blaze.Tdf) this.SetInt("XREF", 0L),
      (Blaze.Tdf) this.SetInt("XTYP", 0L)
    });
    this.WriteInt("UID\0", this.UserID);
    this.CreatePacket((ushort) 1, (ushort) 110, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
