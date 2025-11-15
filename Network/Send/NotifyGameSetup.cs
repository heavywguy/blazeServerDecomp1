// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.NotifyGameSetup
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class NotifyGameSetup : SendPacket
{
  private GameManager game;
  private long exip_ip;
  private long exip_port;
  private long inip_ip;
  private long inip_port;
  private List<string> ListNames;
  private List<string> ListValues;
  private long seed;

  public NotifyGameSetup(
    GameManager game,
    long exip_ip,
    long exip_port,
    long inip_ip,
    long inip_port)
  {
    long num = new Random().NextLong(1000L, 30000L);
    game.SEED = num;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (GameManager.Attribut attribute in game.Attributes)
    {
      stringList1.Add(attribute.Name);
      stringList2.Add(attribute.Value);
    }
    this.game = game;
    this.ListNames = stringList1;
    this.ListValues = stringList2;
    this.exip_ip = exip_ip;
    this.exip_port = exip_port;
    this.inip_ip = inip_ip;
    this.inip_port = inip_port;
    this.seed = num;
  }

  protected internal override void make()
  {
    this.WriteStruct("GAME", new List<Blaze.Tdf>()
    {
      (Blaze.Tdf) this.SetList("ADMN", (byte) 0, 2, (object) new List<long>()
      {
        1L,
        this.game.GID
      }),
      (Blaze.Tdf) this.SetDoubleList("ATTR", (byte) 1, (byte) 1, (object) this.ListNames, (object) this.ListValues, this.game.Attributes.Count),
      (Blaze.Tdf) this.SetList("CAP\0", (byte) 0, this.game.PCAP.Count, (object) this.game.PCAP),
      (Blaze.Tdf) this.SetInt("GID\0", this.game.GID),
      (Blaze.Tdf) this.SetStr("GNAM", this.game.GNAME),
      (Blaze.Tdf) this.SetInt("GPVH", 666L),
      (Blaze.Tdf) this.SetInt("GSET", this.game.GSET),
      (Blaze.Tdf) this.SetInt("GSID", 1L),
      (Blaze.Tdf) this.SetInt("GSTA", 1L),
      (Blaze.Tdf) this.SetStr("GTYP", "frostbite_multiplayer"),
      (Blaze.Tdf) this.SetList("HNET", (byte) 3, 1, (object) new List<Blaze.TdfStruct>()
      {
        Blaze.CreateStructStub(new List<Blaze.Tdf>()
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
        }, true)
      }),
      (Blaze.Tdf) this.SetInt("HSES", 13666L),
      (Blaze.Tdf) this.SetInt("IGNO", this.game.IGNO),
      (Blaze.Tdf) this.SetInt("MCAP", this.game.PMAX),
      (Blaze.Tdf) this.SetInt("NRES", this.game.NRES),
      (Blaze.Tdf) this.SetInt("NTOP", this.game.NTOP),
      (Blaze.Tdf) this.SetStr("PGID", this.game.PGID),
      (Blaze.Tdf) this.SetBlob("PGSR", this.game.PGSC),
      (Blaze.Tdf) this.SetStruct("PHST", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("HPID", this.game.GID),
        (Blaze.Tdf) this.SetInt("HSLT", 0L)
      }),
      (Blaze.Tdf) this.SetInt("PRES", this.game.PRES),
      (Blaze.Tdf) this.SetStr("PSAS", "ams"),
      (Blaze.Tdf) this.SetInt("QCAP", this.game.QCAP),
      (Blaze.Tdf) this.SetInt("REAS", 0L, (byte) 6),
      (Blaze.Tdf) this.SetStruct("VALU", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("DCTX", 0L)
      }),
      (Blaze.Tdf) this.SetInt("SEED", this.seed),
      (Blaze.Tdf) this.SetInt("TCAP", 0L),
      (Blaze.Tdf) this.SetStruct("THST", new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) this.SetInt("HPID", this.game.GID),
        (Blaze.Tdf) this.SetInt("HSLT", 0L)
      }),
      (Blaze.Tdf) this.SetStr("UUID", this.game.UUID),
      (Blaze.Tdf) this.SetInt("VOIP", 0L),
      (Blaze.Tdf) this.SetStr("VSTR", "67")
    });
    this.CreatePacket((ushort) 4, (ushort) 20, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
