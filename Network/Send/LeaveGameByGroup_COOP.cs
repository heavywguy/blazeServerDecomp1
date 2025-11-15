// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.LeaveGameByGroup_COOP
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class LeaveGameByGroup_COOP : SendPacket
{
  private long exip_ip;
  private long exip_port;
  private long inip_ip;
  private long inip_port;
  private long slot;
  private List<string> ListNames;
  private List<string> ListValues;
  private GameClient player;
  private GameClient game;
  private bool signaltoterminate;

  public LeaveGameByGroup_COOP(GameClient player, long playerslot, bool gamemode)
  {
    try
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      this.signaltoterminate = false;
      this.game = GameClient.AllServers[2L];
      this.slot = playerslot;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  protected internal override void make()
  {
    try
    {
      if (!this.signaltoterminate)
      {
        this.WriteStruct("GAME", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetList("ADMN", (byte) 0, 2, (object) new List<long>()
          {
            1L,
            1L
          }),
          (Blaze.Tdf) this.SetList("CAP\0", (byte) 0, 0, (object) 2),
          (Blaze.Tdf) this.SetInt("GID\0", 2L),
          (Blaze.Tdf) this.SetStr("GNAM", ""),
          (Blaze.Tdf) this.SetInt("GPVH", 666L),
          (Blaze.Tdf) this.SetInt("GSET", 1337L),
          (Blaze.Tdf) this.SetInt("GSID", 1L),
          (Blaze.Tdf) this.SetInt("GSTA", 131L),
          (Blaze.Tdf) this.SetStr("GTYP", "frostbite_multiplayer"),
          (Blaze.Tdf) this.SetList("HNET", (byte) 3, 1, (object) new List<Blaze.TdfStruct>()
          {
            Blaze.CreateStructStub(new List<Blaze.Tdf>()
            {
              (Blaze.Tdf) this.SetStruct("EXIP", new List<Blaze.Tdf>()
              {
                (Blaze.Tdf) this.SetInt("IP\0\0", 0L),
                (Blaze.Tdf) this.SetInt("PORT", 0L)
              }),
              (Blaze.Tdf) this.SetStruct("INIP", new List<Blaze.Tdf>()
              {
                (Blaze.Tdf) this.SetInt("IP\0\0", 3232237007L),
                (Blaze.Tdf) this.SetInt("PORT", 3659L)
              })
            }, true)
          }),
          (Blaze.Tdf) this.SetInt("HSES", 13666L),
          (Blaze.Tdf) this.SetInt("IGNO", 0L),
          (Blaze.Tdf) this.SetInt("MCAP", 2L),
          (Blaze.Tdf) this.SetStruct("NQQS", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetInt("DBPS", 0L),
            (Blaze.Tdf) this.SetInt("NATT", (long) Config.NAT_Type),
            (Blaze.Tdf) this.SetInt("UBPS", 0L)
          }),
          (Blaze.Tdf) this.SetInt("NRES", 0L),
          (Blaze.Tdf) this.SetInt("NTOP", 0L),
          (Blaze.Tdf) this.SetStr("PGID", ""),
          (Blaze.Tdf) this.SetStruct("PHST", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetInt("HPID", 1L),
            (Blaze.Tdf) this.SetInt("HSLT", 1L)
          }),
          (Blaze.Tdf) this.SetInt("PRES", 2L),
          (Blaze.Tdf) this.SetStr("PSAS", "ams"),
          (Blaze.Tdf) this.SetInt("QCAP", 11181L),
          (Blaze.Tdf) this.SetInt("SEED", 0L),
          (Blaze.Tdf) this.SetInt("TCAP", 0L),
          (Blaze.Tdf) this.SetStruct("THST", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetInt("HPID", 2L),
            (Blaze.Tdf) this.SetInt("HSLT", 0L)
          }),
          (Blaze.Tdf) this.SetStr("UUID", ""),
          (Blaze.Tdf) this.SetInt("VOIP", 0L),
          (Blaze.Tdf) this.SetStr("VSTR", "67"),
          (Blaze.Tdf) this.SetBlob("XNNC", new byte[0]),
          (Blaze.Tdf) this.SetBlob("XSES", new byte[0])
        });
        this.WriteList("PROS", (byte) 3, 1, (object) new List<Blaze.TdfStruct>()
        {
          this.SetStruct("0", new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) this.SetBlob("BLOB", new byte[0]),
            (Blaze.Tdf) this.SetInt("EXID", 0L),
            (Blaze.Tdf) this.SetInt("GID\0", 2L),
            (Blaze.Tdf) this.SetInt("LOC\0", 3232237007L),
            (Blaze.Tdf) this.SetStr("NAME", ""),
            (Blaze.Tdf) this.SetDoubleList("PATT", (byte) 1, (byte) 1, (object) new List<string>()
            {
              "premium"
            }, (object) new List<string>() { "false" }, 1),
            (Blaze.Tdf) this.SetInt("PID\0", 1L),
            (Blaze.Tdf) this.SetInt("PNET", 2L, (byte) 6),
            (Blaze.Tdf) this.SetStruct("VALU", new List<Blaze.Tdf>()
            {
              (Blaze.Tdf) this.SetStruct("EXIP", new List<Blaze.Tdf>()
              {
                (Blaze.Tdf) this.SetInt("IP\0\0", 0L),
                (Blaze.Tdf) this.SetInt("PORT", 0L)
              }),
              (Blaze.Tdf) this.SetStruct("INIP", new List<Blaze.Tdf>()
              {
                (Blaze.Tdf) this.SetInt("IP\0\0", 2130706433L /*0x7F000001*/),
                (Blaze.Tdf) this.SetInt("PORT", 3659L)
              })
            }),
            (Blaze.Tdf) this.SetInt("SID\0", 0L),
            (Blaze.Tdf) this.SetInt("SLOT", 0L),
            (Blaze.Tdf) this.SetInt("STAT", 2L),
            (Blaze.Tdf) this.SetInt("TIDX", (long) ushort.MaxValue),
            (Blaze.Tdf) this.SetInt("TIME", 0L),
            (Blaze.Tdf) this.SetTrippleVal("UGID", 0L, 0L, 0L),
            (Blaze.Tdf) this.SetInt("UID\0", 1L)
          })
        });
        this.WriteInt("REAS", 0L);
        this.WriteStruct("VALU", new List<Blaze.Tdf>()
        {
          (Blaze.Tdf) this.SetInt("DCTX", 3L)
        });
      }
      else
      {
        this.player.isActive = false;
        this.player.Update = true;
      }
      this.CreatePacket((ushort) 4, (ushort) 22, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  public void GetClientIPs(
    GameClient game,
    GameClient player,
    out long extip_ip,
    out long extip_port,
    out long intip_ip,
    out long intip_port)
  {
    try
    {
      if (Config.norwifi)
      {
        this.exip_ip = (long) Config.GetWanIP(player.EXIP.IP, Config.subnets);
        this.exip_port = (long) game.EXIP.PORT;
      }
      else
      {
        if (player.localPlayer && Config.UseLocalPlayerAlgorithm == 0)
          this.exip_ip = (long) game.EXIP.IP;
        if (player.localPlayer && Config.UseLocalPlayerAlgorithm == 1)
          this.exip_ip = (long) player.LOCALIP.IP;
        if (!player.localPlayer && Config.UseLocalPlayerAlgorithm == 0)
          this.exip_ip = !(Config.WanIpForLocalServer == "") ? Helper.GetIPfromString(Config.WanIpForLocalServer) : (long) game.EXIP.IP;
        if (!player.localPlayer && Config.UseLocalPlayerAlgorithm == 1)
          this.exip_ip = !(Config.WanIpForLocalServer == "") ? Helper.GetIPfromString(Config.WanIpForLocalServer) : (long) game.EXIP.IP;
        this.exip_port = (long) game.EXIP.PORT;
      }
      if (Config.norwifi)
      {
        this.inip_ip = (long) Config.GetWanIP(player.EXIP.IP, Config.subnets);
        this.inip_port = (long) game.INIP.PORT;
      }
      else
      {
        if (player.localPlayer && Config.UseLocalPlayerAlgorithm == 0)
          this.inip_ip = (long) game.INIP.IP;
        if (player.localPlayer && Config.UseLocalPlayerAlgorithm == 1)
          this.inip_ip = (long) player.LOCALIP.IP;
        if (!player.localPlayer && Config.UseLocalPlayerAlgorithm == 0)
          this.inip_ip = !(Config.WanIpForLocalServer == "") ? Helper.GetIPfromString(Config.WanIpForLocalServer) : (long) game.INIP.IP;
        if (!player.localPlayer && Config.UseLocalPlayerAlgorithm == 1)
          this.inip_ip = !(Config.WanIpForLocalServer == "") ? Helper.GetIPfromString(Config.WanIpForLocalServer) : (long) game.INIP.IP;
        this.inip_port = (long) game.INIP.PORT;
      }
      extip_ip = this.exip_ip;
      extip_port = this.exip_port;
      intip_ip = this.inip_ip;
      intip_port = this.inip_port;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public void GetSID(GameClient player, out bool signaltoterminate)
  {
    try
    {
      bool flag = false;
      GameManager allServer = GameManager.AllServers[player.GameID];
      lock (allServer.ServerSlots)
      {
        int freeSlot = Helper.FindFreeSlot(allServer);
        if ((long) freeSlot > allServer.PCAP[0])
        {
          flag = true;
          new SQL_RUN().UpdateOnlinePlayers(player);
          Logger.Log($"The server: {(object) player.GameID} is full for player '{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}')");
        }
        else
        {
          player.SID = (long) freeSlot;
          player.isActive = true;
          player.Update = true;
          allServer.ServerSlots[freeSlot - 1] = true;
          allServer.PlayerIDs[freeSlot - 1] = player.PlayerID;
          allServer.Update = true;
          Logger.Log($"ID'{(object) player.ID}'{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}') reserving slot '{(object) player.SID}' ({(object) player.GameID})");
        }
      }
      signaltoterminate = flag;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }
}
