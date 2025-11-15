// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.GetStatsAsyncNotificationCOOP
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class GetStatsAsyncNotificationCOOP : SendPacket
{
  private List<long> eidlist;
  private List<string> statvalues;
  private long VidValue;
  private bool server;

  public GetStatsAsyncNotificationCOOP(
    List<long> eidlist,
    List<string> statvalues,
    long VidValue,
    bool server)
  {
    this.eidlist = eidlist;
    this.statvalues = statvalues;
    this.VidValue = VidValue;
    this.server = server;
  }

  protected internal override void make()
  {
    if (this.server)
    {
      this.WriteStr("GRNM", "coopplayer_coopdefault");
      this.WriteStr("KEY\0", "No_Scope_Defined");
      this.WriteInt("LAST", 1L);
      this.WriteStruct("STS\0", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetList("STAT", (byte) 3, 1, (object) new List<Blaze.TdfStruct>()
        {
          this.SetStruct("0", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetInt("EID\0", this.eidlist[0]),
            (Blaze.Tdf) this.SetDoubleVal("ETYP", new Blaze.DoubleVal(30722L, 1L)),
            (Blaze.Tdf) this.SetInt("POFF", 0L),
            (Blaze.Tdf) this.SetList("STAT", (byte) 1, 1433, (object) this.statvalues)
          })
        })
      });
      this.WriteInt("VID\0", this.VidValue);
      this.CreatePacket((ushort) 7, (ushort) 50, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
    else
    {
      this.WriteStr("GRNM", "coopplayer_coopdefault");
      this.WriteStr("KEY\0", "No_Scope_Defined");
      this.WriteInt("LAST", 1L);
      this.WriteStruct("STS\0", new List<Blaze.Tdf>());
      this.WriteInt("VID\0", this.VidValue);
      this.CreatePacket((ushort) 7, (ushort) 50, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
  }
}
