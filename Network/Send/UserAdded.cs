// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.UserAdded
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class UserAdded : SendPacket
{
  private long longid;
  private long aloc;
  private string name;
  private int nat_type;
  private long uatt;

  public UserAdded(long longid, string name, long aloc, int nat_type, long uatt)
  {
    this.longid = longid;
    this.name = name;
    this.aloc = aloc;
    this.nat_type = nat_type;
    this.uatt = uatt;
  }

  protected internal override void make()
  {
    if (this.name == "bf3-server-pc")
    {
      this.WriteStruct("DATA", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("ADDR", 63L /*0x3F*/, (byte) 6),
        (Blaze.Tdf) this.SetStr("BPS\0", ""),
        (Blaze.Tdf) this.SetStr("CTY\0", ""),
        (Blaze.Tdf) this.SetIntegerList("CVAR", 0, (List<long>) null),
        (Blaze.Tdf) this.SetInt("HWFG", 0L),
        (Blaze.Tdf) this.SetStruct("QDAT", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("DBPS", 0L),
          (Blaze.Tdf) this.SetInt("NATT", (long) this.nat_type),
          (Blaze.Tdf) this.SetInt("UBPS", 0L)
        }),
        (Blaze.Tdf) this.SetInt("UATT", this.uatt)
      });
      this.WriteStruct("USER", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("AID\0", this.longid),
        (Blaze.Tdf) this.SetInt("ALOC", this.aloc),
        (Blaze.Tdf) this.SetBlob("EXBB", new byte[0]),
        (Blaze.Tdf) this.SetInt("EXID", 0L),
        (Blaze.Tdf) this.SetInt("ID\0\0", this.longid),
        (Blaze.Tdf) this.SetStr("NAME", this.name)
      });
      this.CreatePacket((ushort) 30722, (ushort) 2, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
    else
    {
      this.WriteStruct("DATA", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("ADDR", 63L /*0x3F*/, (byte) 6),
        (Blaze.Tdf) this.SetStr("BPS\0", ""),
        (Blaze.Tdf) this.SetStr("CTY\0", ""),
        (Blaze.Tdf) this.SetIntegerList("CVAR", 0, (List<long>) null),
        (Blaze.Tdf) this.SetDoubleList("DMAP", (byte) 0, (byte) 0, (object) new List<long>()
        {
          458753L /*0x070001*/,
          458754L /*0x070002*/
        }, (object) new List<long>() { 82L, 933L }, 2),
        (Blaze.Tdf) this.SetInt("HWFG", 0L),
        (Blaze.Tdf) this.SetStruct("QDAT", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("DBPS", 0L),
          (Blaze.Tdf) this.SetInt("NATT", (long) this.nat_type),
          (Blaze.Tdf) this.SetInt("UBPS", 0L)
        }),
        (Blaze.Tdf) this.SetInt("UATT", this.uatt)
      });
      this.WriteStruct("USER", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("AID\0", this.longid),
        (Blaze.Tdf) this.SetInt("ALOC", this.aloc),
        (Blaze.Tdf) this.SetBlob("EXBB", new byte[0]),
        (Blaze.Tdf) this.SetInt("EXID", 0L),
        (Blaze.Tdf) this.SetInt("ID\0\0", this.longid),
        (Blaze.Tdf) this.SetStr("NAME", this.name)
      });
      this.CreatePacket((ushort) 30722, (ushort) 2, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
  }
}
