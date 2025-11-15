// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.LoginBF4
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class LoginBF4 : SendPacket
{
  private string name;
  private string mail;
  private long playerid;
  private long buid;
  private long userid;
  private ushort packetID;

  public LoginBF4(
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
    this.buid = buid;
    this.userid = userid;
    this.packetID = packetID;
  }

  protected internal override void make()
  {
    this.WriteInt("ANON", 0L);
    this.WriteInt("NTOS", 0L);
    this.WriteStruct("SESS", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetInt("BUID", this.buid),
      (Blaze.Tdf) this.SetInt("FRSC", 0L),
      (Blaze.Tdf) this.SetInt("FRST", 0L),
      (Blaze.Tdf) this.SetStr("KEY\0", "Yuzusoft"),
      (Blaze.Tdf) this.SetInt("LLOG", (long) Helper.GetUnixTimeStamp()),
      (Blaze.Tdf) this.SetStr("MAIL", this.mail),
      (Blaze.Tdf) this.SetStruct("PDTL", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStr("DSNM", this.name),
        (Blaze.Tdf) this.SetInt("PID\0", this.playerid),
        (Blaze.Tdf) this.SetInt("PLAT", 4L)
      }),
      (Blaze.Tdf) this.SetInt("UID\0", this.userid)
    });
    this.WriteInt("SPAM", 1L);
    this.WriteInt("UNDR", 0L);
    this.CreatePacket((ushort) 35, (ushort) 10, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
