// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.postAuth
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class postAuth : SendPacket
{
  private ushort packetID;
  private long playerid;

  public postAuth(long playerid, ushort packetID)
  {
    this.packetID = packetID;
    this.playerid = playerid;
  }

  protected internal override void make()
  {
    this.WriteStruct("PSS\0", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetStr("ADRS", ""),
      (Blaze.Tdf) this.SetInt("AMAX", 0L),
      (Blaze.Tdf) this.SetInt("OMAX", 0L),
      (Blaze.Tdf) this.SetStr("PJID", ""),
      (Blaze.Tdf) this.SetInt("PORT", 0L),
      (Blaze.Tdf) this.SetInt("RPRT", 0L),
      (Blaze.Tdf) this.SetInt("TIID", 0L)
    });
    this.WriteStruct("TELE", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetStr("ADRS", "gostelemetry.blaze3.ea.com"),
      (Blaze.Tdf) this.SetInt("ANON", 0L),
      (Blaze.Tdf) this.SetStr("DISA", ""),
      (Blaze.Tdf) this.SetInt("EDCT", 0L),
      (Blaze.Tdf) this.SetStr("FILT", "-GAME/COMM/EXPD"),
      (Blaze.Tdf) this.SetInt("LOC\0", 1701729619L),
      (Blaze.Tdf) this.SetInt("MINR", 0L),
      (Blaze.Tdf) this.SetStr("NOOK", ""),
      (Blaze.Tdf) this.SetInt("PORT", 9988L),
      (Blaze.Tdf) this.SetInt("SDLY", 15000L),
      (Blaze.Tdf) this.SetStr("SESS", "tele_sess"),
      (Blaze.Tdf) this.SetStr("SKEY", "some_tele_key"),
      (Blaze.Tdf) this.SetInt("SPCT", 75L),
      (Blaze.Tdf) this.SetStr("STIM", "Default"),
      (Blaze.Tdf) this.SetStr("SVNM", "telemetry-3-common")
    });
    this.WriteStruct("TICK", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetStr("ADRS", "10.10.78.150"),
      (Blaze.Tdf) this.SetInt("PORT", 8999L),
      (Blaze.Tdf) this.SetStr("SKEY", this.playerid.ToString() + ",10.10.78.150:8999,battlefield-4-pc,10,50,50,50,50,0,0")
    });
    this.WriteStruct("UROP", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetInt("TMOP", 0L),
      (Blaze.Tdf) this.SetInt("UID\0", this.playerid)
    });
    this.CreatePacket((ushort) 9, (ushort) 8, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
