// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.joinGameByGroup
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class joinGameByGroup : SendPacket
{
  private string name;
  private long gameid;
  private long playerid;
  private long sid;
  private long exip_ip;
  private long exip_port;
  private long inip_ip;
  private long inip_port;

  public joinGameByGroup(
    string name,
    long gameid,
    long playerid,
    long sid,
    long exip_ip,
    long inip_ip,
    long exip_port,
    long inip_port)
  {
    this.name = name;
    this.gameid = gameid;
    this.playerid = playerid;
    this.sid = sid;
    this.exip_ip = exip_ip;
    this.exip_port = exip_port;
    this.inip_ip = inip_ip;
    this.inip_port = inip_port;
  }

  protected internal override void make()
  {
    this.WriteInt("GID\0", this.gameid);
    this.WriteStruct("PDAT", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetInt("CONG", this.playerid),
      (Blaze.Tdf) this.SetInt("CSID", 2L),
      (Blaze.Tdf) this.SetInt("EXID", 0L),
      (Blaze.Tdf) this.SetInt("GID\0", this.gameid),
      (Blaze.Tdf) this.SetInt("LOC", 1701729619L),
      (Blaze.Tdf) this.SetStr("NAME", this.name),
      (Blaze.Tdf) this.SetDoubleList("PATT", (byte) 1, (byte) 1, (object) new List<string>()
      {
        "premium"
      }, (object) new List<string>() { "false" }, 1),
      (Blaze.Tdf) this.SetInt("PID\0", this.playerid),
      (Blaze.Tdf) this.SetInt("PNET", 2L, (byte) 6),
      (Blaze.Tdf) this.SetStruct("VALU", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStruct("EXIP", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("IP\0\0", this.exip_ip),
          (Blaze.Tdf) this.SetInt("PORT", this.exip_port)
        }),
        (Blaze.Tdf) this.SetStruct("INIP", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("IP\0\0", this.inip_ip),
          (Blaze.Tdf) this.SetInt("PORT", this.inip_port)
        })
      }),
      (Blaze.Tdf) this.SetInt("SID\0", this.sid),
      (Blaze.Tdf) this.SetInt("SLOT", 0L),
      (Blaze.Tdf) this.SetInt("STAT", 2L),
      (Blaze.Tdf) this.SetInt("TIDX", (long) ushort.MaxValue),
      (Blaze.Tdf) this.SetInt("TIME", 0L),
      (Blaze.Tdf) this.SetTrippleVal("UGID", 0L, 0L, 0L),
      (Blaze.Tdf) this.SetInt("UID\0", this.playerid)
    });
    this.CreatePacket((ushort) 4, (ushort) 21, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
