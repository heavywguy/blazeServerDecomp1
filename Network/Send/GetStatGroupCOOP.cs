// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.GetStatGroupCOOP
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class GetStatGroupCOOP : SendPacket
{
  private const string cnam = "coopplayer_coop";
  private ushort packetID;

  public GetStatGroupCOOP(ushort packetID) => this.packetID = packetID;

  protected internal override void make()
  {
    this.WriteStr("CNAM", "coopplayer_coop");
    this.WriteStr("DESC", "coopplayer_coopdefault");
    this.WriteDoubleVal("ETYP", 30722L, 1L);
    this.WriteStr("META", "");
    this.WriteStr("NAME", "coopplayer_coopdefault");
    List<Blaze.Tdf>[] tdfListArray = new List<Blaze.Tdf>[99];
    List<Blaze.TdfStruct> list = new List<Blaze.TdfStruct>();
    for (int index = 0; index < 99; ++index)
    {
      tdfListArray[index] = new List<Blaze.Tdf>();
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("CATG", "coopplayer_coop"));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("DFLT", "0.00"));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfInteger.Create("DRVD", 0L));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("FRMT", "%.2f"));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("KIND", ""));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("LDSC", ""));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("META", ""));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("NAME", Helper.coop_statname[index]));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("SDSC", ""));
      tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfInteger.Create("TYPE", 1L));
      list.Add(Blaze.CreateStructStub(tdfListArray[index]));
    }
    this.WriteList("STAT", (byte) 3, 99, (object) list);
    this.CreatePacket((ushort) 7, (ushort) 4, 0, (ushort) 4112, this.packetID);
  }
}
