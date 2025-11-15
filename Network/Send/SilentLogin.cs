// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.SilentLogin
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class SilentLogin : SendPacket
{
  private string name;
  private string mail;
  private long playerid;
  private long gameid;
  private long buid;
  private long userid;
  private ushort packetID;

  public SilentLogin(
    string name,
    string mail,
    long playerid,
    long gameid,
    long buid,
    long userid,
    ushort packetID)
  {
    this.name = name;
    this.mail = mail;
    this.playerid = playerid;
    this.gameid = gameid;
    this.buid = buid;
    this.userid = userid;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteInt("AGUP", 0L);
    this.WriteStr("LDHT", "");
    this.WriteInt("NTOS", 0L);
    this.WriteStr("PCTK", "fa1a26c1-d934-422a-a6ba-ed92614f7d87");
    this.WriteStr("PRIV", "");
    this.WriteStruct("SESS", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetInt("BUID", this.buid),
      (Blaze.Tdf) this.SetInt("FRST", 0L),
      (Blaze.Tdf) this.SetStr("KEY\0", "some_key_client"),
      (Blaze.Tdf) this.SetInt("LLOG", (long) Helper.GetUnixTimeStamp()),
      (Blaze.Tdf) this.SetStr("MAIL", this.mail),
      (Blaze.Tdf) this.SetStruct("PDTL", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStr("DSNM", this.name),
        (Blaze.Tdf) this.SetInt("LAST", (long) Helper.GetUnixTimeStamp()),
        (Blaze.Tdf) this.SetInt("PID\0", this.playerid),
        (Blaze.Tdf) this.SetInt("STAS", 0L),
        (Blaze.Tdf) this.SetInt("XREF", 0L),
        (Blaze.Tdf) this.SetInt("XTYP", 0L)
      }),
      (Blaze.Tdf) this.SetInt("UID\0", this.userid)
    });
    this.WriteInt("SPAM", 0L);
    this.WriteStr("THST", "");
    this.WriteStr("TSUI", "");
    this.WriteStr("TURI", "");
    this.CreatePacket((ushort) 1, (ushort) 50, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
