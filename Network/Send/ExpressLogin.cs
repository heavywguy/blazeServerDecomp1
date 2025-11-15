// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.ExpressLogin
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class ExpressLogin : SendPacket
{
  private long gameid;
  private ushort packetID;

  public ExpressLogin(long gameid, ushort packetID)
  {
    this.gameid = gameid;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteInt("AGUP", 0L);
    this.WriteInt("ANON", 0L);
    this.WriteInt("NTOS", 0L);
    this.WriteStr("PCTK", "some_token_server");
    this.WriteStruct("SESS", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetInt("BUID", this.gameid),
      (Blaze.Tdf) this.SetInt("FRST", 0L),
      (Blaze.Tdf) this.SetStr("KEY\0", "some_key_client"),
      (Blaze.Tdf) this.SetInt("LLOG", (long) Helper.GetUnixTimeStamp()),
      (Blaze.Tdf) this.SetStr("MAIL", "bf4.server.pa@ea.com"),
      (Blaze.Tdf) this.SetStruct("PDTL", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStr("DSNM", "bf4-server-pc"),
        (Blaze.Tdf) this.SetInt("LAST", (long) Helper.GetUnixTimeStamp()),
        (Blaze.Tdf) this.SetInt("PID\0", this.gameid),
        (Blaze.Tdf) this.SetInt("PLAT", 4L),
        (Blaze.Tdf) this.SetInt("STAS", 0L),
        (Blaze.Tdf) this.SetInt("XREF", 0L),
        (Blaze.Tdf) this.SetInt("XTYP", 0L)
      }),
      (Blaze.Tdf) this.SetInt("UID\0", this.gameid)
    });
    this.WriteInt("SPAM", 1L);
    this.WriteStr("SKEY", "some_key_login");
    this.WriteInt("UNDR", 1L);
    this.CreatePacket((ushort) 1, (ushort) 60, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
