// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.UserSessionExtendedDataUpdate
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class UserSessionExtendedDataUpdate : SendPacket
{
  private long exip_ip;
  private long exip_port;
  private long inip_ip;
  private long inip_port;
  private long uatt;
  private long longid;
  private long gameid;
  private bool ulst;

  public UserSessionExtendedDataUpdate(
    long exip_ip,
    long exip_port,
    long inip_ip,
    long inip_port,
    long uatt,
    long longid,
    long gameid,
    bool ulst)
  {
    this.exip_ip = exip_ip;
    this.exip_port = exip_port;
    this.inip_ip = inip_ip;
    this.inip_port = inip_port;
    this.uatt = uatt;
    this.longid = longid;
    this.gameid = gameid;
    this.ulst = ulst;
  }

  protected internal override void make()
  {
    if (this.ulst)
    {
      this.WriteStruct("DATA", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("ADDR", 2L, (byte) 6),
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
        (Blaze.Tdf) this.SetStr("BPS\0", "ams"),
        (Blaze.Tdf) this.SetStr("CTY\0", ""),
        (Blaze.Tdf) this.SetIntegerList("CVAR", 0, (List<long>) null),
        (Blaze.Tdf) this.SetDoubleList("DMAP", (byte) 0, (byte) 0, (object) new List<long>()
        {
          458753L /*0x070001*/,
          458754L /*0x070002*/
        }, (object) new List<long>() { 82L, 933L }, 2),
        (Blaze.Tdf) this.SetInt("HWFG", 0L),
        (Blaze.Tdf) this.SetList("PSLM", (byte) 0, 7, (object) new List<long>()
        {
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L
        }),
        (Blaze.Tdf) this.SetStruct("QDAT", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("DBPS", 0L),
          (Blaze.Tdf) this.SetInt("NATT", (long) Config.NAT_Type),
          (Blaze.Tdf) this.SetInt("UBPS", 0L)
        }),
        (Blaze.Tdf) this.SetInt("UATT", this.uatt),
        (Blaze.Tdf) this.SetList("ULST", (byte) 9, 1, (object) new List<Blaze.TrippleVal>()
        {
          new Blaze.TrippleVal(4L, 1L, this.gameid)
        })
      });
      this.WriteInt("USID", this.longid);
      this.CreatePacket((ushort) 30722, (ushort) 1, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
    else
    {
      this.WriteStruct("DATA", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("ADDR", 2L, (byte) 6),
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
        (Blaze.Tdf) this.SetStr("BPS\0", "ams"),
        (Blaze.Tdf) this.SetStr("CTY\0", ""),
        (Blaze.Tdf) this.SetIntegerList("CVAR", 0, (List<long>) null),
        (Blaze.Tdf) this.SetDoubleList("DMAP", (byte) 0, (byte) 0, (object) new List<long>()
        {
          458753L /*0x070001*/,
          458754L /*0x070002*/
        }, (object) new List<long>() { 82L, 933L }, 2),
        (Blaze.Tdf) this.SetInt("HWFG", 0L),
        (Blaze.Tdf) this.SetList("PSLM", (byte) 0, 7, (object) new List<long>()
        {
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L,
          268374015L
        }),
        (Blaze.Tdf) this.SetStruct("QDAT", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("DBPS", 0L),
          (Blaze.Tdf) this.SetInt("NATT", (long) Config.NAT_Type),
          (Blaze.Tdf) this.SetInt("UBPS", 0L)
        }),
        (Blaze.Tdf) this.SetInt("UATT", this.uatt)
      });
      this.WriteInt("USID", this.longid);
      this.CreatePacket((ushort) 30722, (ushort) 1, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
  }
}
