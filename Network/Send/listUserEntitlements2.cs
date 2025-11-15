// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.listUserEntitlements2
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class listUserEntitlements2 : SendPacket
{
  private bool online_access;
  private ushort packetID;
  private string platform;

  public listUserEntitlements2(bool online_access, ushort packetID, string platform)
  {
    this.online_access = online_access;
    this.packetID = packetID;
    this.platform = platform;
  }

  protected internal override void make()
  {
    if (this.online_access)
    {
      this.CreatePacket((ushort) 1, (ushort) 29, 0, (ushort) 4096 /*0x1000*/, this.packetID);
    }
    else
    {
      string str = !(this.platform == "VeniceXpack51128071retail-1.0-0") ? "BF3PC" : "BF3PS3";
      this.WriteList("NLST", (byte) 3, 24, (object) new List<Blaze.TdfStruct>()
      {
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:11Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238359432L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF1942:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:10Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238360275L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF2:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:2Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238354538L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF2142:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:11Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238360961L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFVIETNAM:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:3Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238352095L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFBC:360:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:4Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238354893L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF1943:360:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2010-03-05T9:24Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 458119960L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "136844"),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFBC2:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:4Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238354815L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFP4F:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:11Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238362758L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFHEROES:PC:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-13T14:3Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "AddsVetRank"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1238354753L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BFMC:360:ADDSVETRANK"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2014-05-29T6:15Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1005150961807L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "305060"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:235665900"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "ONLINE_ACCESS"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 1L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2013-02-22T14:40Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1002134961807L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "305061"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:235663400"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "ONLINE_ACCESS"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 1L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2012-06-04T21:13Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1771457489L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "306678"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:50400"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PREMIUM_ACCESS"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2012-06-04T21:13Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1771457490L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "302777"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:234138400"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PC:B2K_PURCHASE"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2012-06-04T21:13Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1771457491L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "302776"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:48215"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PC:XPACK2_PURCHASE"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2014-02-07T20:15Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1004743136441L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "302778"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:51080"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PC:XPACK3_PURCHASE"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2012-11-26T9:4Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1000808118611L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "303129"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:55171"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PC:XPACK4_PURCHASE"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2013-03-07T2:21Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1002246118611L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "306409"),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:109546437"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PC:XPACK5_PURCHASE"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-21T17:0Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1253734436L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:235663400"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PROMO:PWP"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 1L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2013-02-22T14:40Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1002135561807L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "303984"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:234138500"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PROMO:SPECA"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2011-10-25T15:25Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1265210222L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", ""),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 1L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PROMO:DOGTAGS"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2013-02-22T14:40Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 5702949135L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "304697"),
          (Blaze.Tdf) this.SetInt("PRCA", 2L),
          (Blaze.Tdf) this.SetStr("PRID", "DR:234138200"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:PROMO:DOGTAGS"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2012-04-17T15:57Z"),
          (Blaze.Tdf) this.SetStr("GNAM", str),
          (Blaze.Tdf) this.SetInt("ID\0\0", 1684196754L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "306215"),
          (Blaze.Tdf) this.SetInt("PRCA", 1L),
          (Blaze.Tdf) this.SetStr("PRID", "OFB-EAST:48642"),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "BF3:SHORTCUT:COOP"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        }),
        this.SetStruct("0", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("DEVI", ""),
          (Blaze.Tdf) this.SetStr("GDAY", "2010-11-30T4:58Z"),
          (Blaze.Tdf) this.SetStr("GNAM", "GunClub"),
          (Blaze.Tdf) this.SetInt("ID\0\0", 785112884L),
          (Blaze.Tdf) this.SetInt("ISCO", 0L),
          (Blaze.Tdf) this.SetInt("PID\0", 0L),
          (Blaze.Tdf) this.SetStr("PJID", "302546"),
          (Blaze.Tdf) this.SetInt("PRCA", 0L),
          (Blaze.Tdf) this.SetStr("PRID", ""),
          (Blaze.Tdf) this.SetInt("STAT", 1L),
          (Blaze.Tdf) this.SetInt("STRC", 0L),
          (Blaze.Tdf) this.SetStr("TAG\0", "GUNCLUB:WEB:MEMBER"),
          (Blaze.Tdf) this.SetStr("TDAY", ""),
          (Blaze.Tdf) this.SetInt("TYPE", 5L)
        })
      });
      this.CreatePacket((ushort) 1, (ushort) 29, 0, (ushort) 4096 /*0x1000*/, this.packetID);
    }
  }
}
