// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.preAuth
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class preAuth : SendPacket
{
  private ushort packetID;
  private string name;

  public preAuth(string name, ushort packetID)
  {
    this.packetID = packetID;
    this.name = name;
  }

  protected internal override void make()
  {
    if (this.name == "battlefield-3-pc" || this.name == "battlefield-3-pc-trial" || this.name == "battlefield-3-ps3-beta" || this.name == "battlefield-3-xbl2" || this.name == "battlefield-3-ps3")
    {
      this.WriteInt("ANON", 0L);
      this.WriteStr("ASRC", "300294");
      this.WriteList("CIDS", (byte) 0, 18, (object) new List<long>()
      {
        1L,
        25L,
        4L,
        27L,
        28L,
        6L,
        7L,
        9L,
        10L,
        11L,
        30720L,
        30721L,
        30722L,
        30723L,
        20L,
        30725L,
        30726L,
        2000L
      });
      this.WriteStr("CNGN", "");
      this.WriteStruct("CONF", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetDoubleList("CONF", (byte) 1, (byte) 1, (object) new List<string>()
        {
          "connIdleTimeout",
          "defaultRequestTimeout",
          "pingPeriod",
          "voipHeadsetUpdateRate",
          "xlspConnectionIdleTimeout"
        }, (object) new List<string>()
        {
          "90s",
          "80s",
          "20s",
          "1000",
          "300"
        }, 5)
      });
      this.WriteStr("INST", "battlefield-3-pc");
      this.WriteInt("MINR", 0L);
      this.WriteStr("NASP", "cem_ea_id");
      this.WriteStr("PILD", "");
      this.WriteStr("PLAT", "pc");
      this.WriteStr("PTAG", "");
      this.WriteStruct("QOSS", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStruct("BWPS", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("PSA\0", "10.0.0.1"),
          (Blaze.Tdf) this.SetInt("PSP\0", 17502L),
          (Blaze.Tdf) this.SetStr("SNA\0", "ams")
        }),
        (Blaze.Tdf) this.SetInt("LNP\0", 10L),
        (Blaze.Tdf) this.SetDoubleList("LTPS", (byte) 1, (byte) 3, (object) new List<string>()
        {
          "ams"
        }, (object) new List<Blaze.TdfStruct>()
        {
          this.SetStruct("0", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetStr("PSA\0", "10.0.0.1"),
            (Blaze.Tdf) this.SetInt("PSP\0", 17502L),
            (Blaze.Tdf) this.SetStr("SNA\0", "ams")
          })
        }, 1),
        (Blaze.Tdf) this.SetInt("SVID", 1161889797L)
      });
      this.WriteStr("RSRC", "300294");
      this.WriteStr("SVER", "Blaze 3.15.08.0 (CL# 984104)");
    }
    if (this.name == "battlefield-4-pc" || this.name == "battlefield-4-pc-alpha" || this.name == "battlefield-4-pc-beta")
    {
      this.WriteStr("ASRC", "302123");
      this.WriteList("CIDS", (byte) 0, 22, (object) new List<long>()
      {
        30728L,
        1L,
        30729L,
        25L,
        30730L,
        27L,
        4L,
        28L,
        6L,
        7L,
        9L,
        10L,
        63490L,
        35L,
        15L,
        30720L,
        30722L,
        30723L,
        30724L,
        30726L,
        2000L,
        30727L
      });
      this.WriteStruct("CONF", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetDoubleList("CONF", (byte) 1, (byte) 1, (object) new List<string>()
        {
          "associationListSkipInitialSet",
          "blazeServerClientId",
          "bytevaultHostname",
          "bytevaultPort",
          "bytevaultSecure",
          "capsStringValidationUri",
          "connIdleTimeout",
          "defaultRequestTimeout",
          "identityDisplayUri",
          "identityRedirectUri",
          "nucleusConnect",
          "nucleusProxy",
          "pingPeriod",
          "userManagerMaxCachedUsers",
          "voipHeadsetUpdateRate",
          "xblTokenUrn",
          "xlspConnectionIdleTimeout"
        }, (object) new List<string>()
        {
          "1",
          "GOS-BlazeServer-BF4-PC",
          "bytevault.gameservices.ea.com",
          "42210",
          "true",
          "client-strings.xboxlive.com",
          "90s",
          "60s",
          "console2/welcome",
          "http://127.0.0.1/success",
          "https://accounts.ea.com",
          "https://gateway.ea.com",
          "30s",
          "0",
          "1000",
          "accounts.ea.com",
          "300"
        }, 17)
      });
      this.WriteStr("ESRC", "302123");
      this.WriteStr("INST", "battlefield-4-pc");
      this.WriteInt("MINR", 0L);
      this.WriteStr("NASP", "cem_ea_id");
      this.WriteStr("PILD", "");
      this.WriteStr("PLAT", "pc");
      this.WriteStr("PTAG", "");
      this.WriteStruct("QOSS", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetStruct("BWPS", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetStr("PSA\0", "10.0.0.1"),
          (Blaze.Tdf) this.SetInt("PSP\0", 17502L),
          (Blaze.Tdf) this.SetStr("SNA\0", "bio-prod-ams-bf4")
        }),
        (Blaze.Tdf) this.SetInt("LNP\0", 10L),
        (Blaze.Tdf) this.SetDoubleList("LTPS", (byte) 1, (byte) 3, (object) new List<string>()
        {
          "ams"
        }, (object) new List<Blaze.TdfStruct>()
        {
          this.SetStruct("0", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetStr("PSA\0", "10.0.0.1"),
            (Blaze.Tdf) this.SetInt("PSP\0", 17502L),
            (Blaze.Tdf) this.SetStr("SNA\0", "bio-prod-ams-bf4")
          })
        }, 1),
        (Blaze.Tdf) this.SetInt("SVID", 1161889797L)
      });
      this.WriteStr("RSRC", "302123");
      this.WriteStr("SVER", "Blaze 13.3.1.8.0 (CL# 1034743)");
    }
    this.CreatePacket((ushort) 9, (ushort) 7, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
