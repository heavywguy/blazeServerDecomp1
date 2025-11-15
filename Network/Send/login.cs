// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.login
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class login : SendPacket
{
  private string name;
  private long gameid;
  private long userid;
  private ushort packetID;

  public login(string name, long gameid, long userid, ushort packetID)
  {
    this.name = name;
    this.gameid = gameid;
    this.userid = userid;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteStr("LDHT", "");
    this.WriteInt("LDHT", 0L);
    this.WriteStr("PCTK", "some_token_server");
    this.WriteList("PLST", (byte) 3, 1, (object) new List<Blaze.TdfStruct>()
    {
      this.SetStruct("0", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStr("DSNM", "bf3-server-pc"),
        (Blaze.Tdf) this.SetInt("LAST", (long) Helper.GetUnixTimeStamp()),
        (Blaze.Tdf) this.SetInt("PID\0", this.gameid),
        (Blaze.Tdf) this.SetInt("STAS", 2L),
        (Blaze.Tdf) this.SetInt("XREF", 0L),
        (Blaze.Tdf) this.SetInt("XTYP", 0L)
      })
    });
    this.WriteStr("PRIV", "");
    this.WriteStr("SKEY", "some_key_login");
    this.WriteInt("SPAM", 1L);
    this.WriteStr("THST", "");
    this.WriteStr("TSUI", "");
    this.WriteStr("TURI", "");
    this.WriteInt("UID\0", this.userid);
    this.CreatePacket((ushort) 1, (ushort) 40, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
