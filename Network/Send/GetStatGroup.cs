// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.GetStatGroup
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class GetStatGroup : SendPacket
{
  private const string player_awards = "player_awards";
  private const string player_awards2 = "player_awards2";
  private const string player_weapons1 = "player_weapons1";
  private const string player_statcategory = "player_statcategory";
  private const string player_core = "player_core";
  private const string coopplayer_coop = "coopplayer_coop";
  private const string player_reset = "player_reset";
  private ushort packetID;

  public GetStatGroup(ushort packetID) => this.packetID = packetID;

  protected internal override void make()
  {
    try
    {
      this.WriteStr("CNAM", "player_awards");
      this.WriteStr("DESC", "player_mpdefault2");
      this.WriteDoubleVal("ETYP", 30722L, 1L);
      this.WriteStr("META", "");
      this.WriteStr("NAME", "player_mpdefault2");
      List<Blaze.Tdf>[] tdfListArray = new List<Blaze.Tdf>[1432];
      List<Blaze.TdfStruct> list = new List<Blaze.TdfStruct>();
      string[] strArray = new string[1432];
      for (int index = 0; index < 993; ++index)
        strArray[index] = "player_awards";
      for (int index = 993; index < 1166; ++index)
        strArray[index] = "player_awards2";
      for (int index = 1166; index < 1296; ++index)
        strArray[index] = "player_weapons1";
      for (int index = 1296; index < 1405; ++index)
        strArray[index] = "player_statcategory";
      for (int index = 1405; index < 1426; ++index)
        strArray[index] = "player_core";
      strArray[1426] = "coopplayer_coop";
      strArray[1427] = "player_core";
      strArray[1428] = "player_core";
      strArray[1429] = "player_core";
      strArray[1430] = "player_statcategory";
      strArray[1431] = "player_reset";
      for (int index = 0; index < 1432; ++index)
      {
        tdfListArray[index] = new List<Blaze.Tdf>();
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("CATG", strArray[index]));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("DFLT", "0.00"));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfInteger.Create("DRVD", 0L));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("FRMT", "%.2f"));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("KIND", ""));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("LDSC", ""));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("META", ""));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("NAME", Helper.statname[index]));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfString.Create("SDSC", ""));
        tdfListArray[index].Add((Blaze.Tdf) Blaze.TdfInteger.Create("TYPE", 1L));
        list.Add(Blaze.CreateStructStub(tdfListArray[index]));
      }
      this.WriteList("STAT", (byte) 3, 1432, (object) list);
      this.CreatePacket((ushort) 7, (ushort) 4, 0, (ushort) 4112, this.packetID);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }
}
