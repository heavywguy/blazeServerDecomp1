// Decompiled with JetBrains decompiler
// Type: BlazeServer.SHandleComponent_4
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

#nullable disable
namespace BlazeServer;

internal class SHandleComponent_4
{
  private static ManualResetEvent sendDone = new ManualResetEvent(false);
  private static ManualResetEvent closeDone = new ManualResetEvent(false);

  public SHandleComponent_4(
    GameManager player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      byte[] buf1 = (byte[]) null;
      bool flag = false;
      switch (p.Command)
      {
        case 1:
          if (player.ServerType == "warsaw server")
          {
            this.HandleComponent_4_1_bf4at(player, p, out buf1);
            break;
          }
          this.HandleComponent_4_1(player, p, out buf1);
          break;
        case 2:
          if (player != null)
          {
            Logger.Log($"ID'{(object) player.ID}' GameID '{(object) player.GID} '{player.GNAME}' removing");
            if (GameManager.AllServers.ContainsKey(player.GID))
            {
              if (GameManager.AllServers.TryRemove(player.GID, out GameManager _))
                Logger.Log($"ID'{(object) player.ID}' GameID '{(object) player.GID} '{player.GNAME}' removing done");
              else
                Logger.Log($"ID'{(object) player.ID}' GameID '{(object) player.GID} '{player.GNAME}' remove false");
            }
            new SQL_RUN().DeleteServer(player.GID);
          }
          SendEmpty sendEmpty1 = new SendEmpty(p);
          sendEmpty1.make();
          buf1 = sendEmpty1.ByteArray();
          break;
        case 3:
          MemoryStream memoryStream1 = new MemoryStream();
          SendEmpty sendEmpty2 = new SendEmpty(p);
          sendEmpty2.make();
          memoryStream1.Write(sendEmpty2.ByteArray(), 0, sendEmpty2.ByteArray().Length);
          List<Blaze.Tdf> tdfList1 = Blaze.ReadPacketContent(p);
          if (tdfList1.Count == 2 && tdfList1[1].Type == (byte) 0 && tdfList1[1].Label == "GSTA")
          {
            Blaze.TdfInteger tdfInteger = (Blaze.TdfInteger) tdfList1[1];
            if (tdfInteger.Value == 4L)
            {
              getGameListSnapshot gameListSnapshot = new getGameListSnapshot(player.GID, 4L);
              gameListSnapshot.make();
              memoryStream1.Write(gameListSnapshot.ByteArray(), 0, gameListSnapshot.ByteArray().Length);
            }
            if (tdfInteger.Value == 130L)
            {
              getGameListSnapshot gameListSnapshot = new getGameListSnapshot(player.GID, 130L);
              gameListSnapshot.make();
              memoryStream1.Write(gameListSnapshot.ByteArray(), 0, gameListSnapshot.ByteArray().Length);
            }
            if (tdfInteger.Value == 131L)
            {
              getGameListSnapshot gameListSnapshot = new getGameListSnapshot(player.GID, 131L);
              gameListSnapshot.make();
              memoryStream1.Write(gameListSnapshot.ByteArray(), 0, gameListSnapshot.ByteArray().Length);
              new SQL_RUN().UpdateServer(player.GID, player.MAIL, player.serverattrval, player.GNAME, player.PCAP[0], player.ServerVersion);
            }
          }
          buf1 = memoryStream1.ToArray();
          break;
        case 4:
          MemoryStream memoryStream2 = new MemoryStream();
          List<Blaze.Tdf> tdfList2 = Blaze.ReadPacketContent(p);
          Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList2[0];
          Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfList2[1];
          player.GSET = tdfInteger2.Value;
          player.Update = true;
          SendEmpty sendEmpty3 = new SendEmpty(p);
          sendEmpty3.make();
          memoryStream2.Write(sendEmpty3.ByteArray(), 0, sendEmpty3.ByteArray().Length);
          migrateAdminPlayer migrateAdminPlayer = new migrateAdminPlayer(tdfInteger1.Value, tdfInteger2.Value);
          migrateAdminPlayer.make();
          memoryStream2.Write(migrateAdminPlayer.ByteArray(), 0, migrateAdminPlayer.ByteArray().Length);
          buf1 = memoryStream2.ToArray();
          break;
        case 5:
          MemoryStream memoryStream3 = new MemoryStream();
          List<Blaze.Tdf> tdfList3 = Blaze.ReadPacketContent(p);
          Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfList3[0];
          Blaze.TdfList tdfList4 = (Blaze.TdfList) tdfList3[1];
          Blaze.TdfInteger tdfInteger4 = (Blaze.TdfInteger) tdfList3[2];
          List<long> list = (List<long>) tdfList4.List;
          player.PCAP = list;
          SendEmpty sendEmpty4 = new SendEmpty(p);
          sendEmpty4.make();
          memoryStream3.Write(((IEnumerable<byte>) sendEmpty4.ByteArray()).ToArray<byte>(), 0, sendEmpty4.ByteArray().Length);
          getUserSetGameListSubscription listSubscription = new getUserSetGameListSubscription(list, tdfInteger3.Value, tdfInteger4.Value);
          listSubscription.make();
          memoryStream3.Write(((IEnumerable<byte>) listSubscription.ByteArray()).ToArray<byte>(), 0, listSubscription.ByteArray().Length);
          buf1 = memoryStream3.ToArray();
          player.Update = true;
          new SQL_RUN().UpdatePCAPofServer(player, list[0]);
          break;
        case 7:
          Blaze.TdfDoubleList tdfDoubleList = (Blaze.TdfDoubleList) Blaze.ReadPacketContent(p)[0];
          player?.UpdateAttributes(((List<string>) tdfDoubleList.List1).ToArray(), ((List<string>) tdfDoubleList.List2).ToArray());
          player.Update = true;
          List<string> attribval = new List<string>();
          foreach (GameManager.Attribut attribute in player.Attributes)
            attribval.Add(attribute.Value);
          new SQL_RUN().UpdateServerATTR(player.GID, player.MAIL, attribval, player.ServerVersion);
          SendEmpty sendEmpty5 = new SendEmpty(p);
          sendEmpty5.make();
          buf1 = sendEmpty5.ByteArray();
          break;
        case 11:
          MemoryStream memoryStream4 = new MemoryStream();
          List<Blaze.Tdf> tdfList5 = Blaze.ReadPacketContent(p);
          Blaze.TdfInteger tdfInteger5 = (Blaze.TdfInteger) tdfList5[2];
          Blaze.TdfInteger tdfInteger6 = (Blaze.TdfInteger) tdfList5[3];
          Blaze.TdfInteger tdfInteger7 = (Blaze.TdfInteger) tdfList5[4];
          SendEmpty sendEmpty6 = new SendEmpty(p);
          sendEmpty6.make();
          memoryStream4.Write(sendEmpty6.ByteArray(), 0, sendEmpty6.ByteArray().Length);
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(tdfInteger6.Value, tdfInteger5.Value, 6L);
          notifyPlayerRemoved.make();
          memoryStream4.Write(notifyPlayerRemoved.ByteArray(), 0, notifyPlayerRemoved.ByteArray().Length);
          buf1 = memoryStream4.ToArray();
          break;
        case 15:
          SendEmpty sendEmpty7 = new SendEmpty(p);
          sendEmpty7.make();
          buf1 = sendEmpty7.ByteArray();
          break;
        case 19:
          MemoryStream memoryStream5 = new MemoryStream();
          SendEmpty sendEmpty8 = new SendEmpty(p);
          sendEmpty8.make();
          memoryStream5.Write(sendEmpty8.ByteArray(), 0, sendEmpty8.ByteArray().Length);
          getGameListSnapshot gameListSnapshot1 = new getGameListSnapshot(player.GID, 130L);
          gameListSnapshot1.make();
          memoryStream5.Write(gameListSnapshot1.ByteArray(), 0, gameListSnapshot1.ByteArray().Length);
          buf1 = memoryStream5.ToArray();
          break;
        case 29:
          if (player.ServerVersion == "pc")
          {
            this.HandleComponent_4_1D_server_bf4at(player, p, out buf1);
            break;
          }
          this.HandleComponent_4_1D_server(player, p, out buf1);
          break;
        case 39:
          List<Blaze.Tdf> tdfList6 = Blaze.ReadPacketContent(p);
          Blaze.TdfInteger tdfInteger8 = (Blaze.TdfInteger) tdfList6[0];
          Blaze.TdfString tdfString = (Blaze.TdfString) tdfList6[1];
          player.GNAME = tdfString.Value;
          player.Update = true;
          new SQL_RUN().UpdateNameofServer(tdfString.Value, tdfInteger8.Value);
          SendEmpty sendEmpty9 = new SendEmpty(p);
          sendEmpty9.make();
          buf1 = sendEmpty9.ByteArray();
          break;
        default:
          SendError sendError = new SendError(p, 16386);
          sendError.make();
          buf1 = sendError.ByteArray();
          break;
      }
      buf = buf1;
      termination = flag;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_1D_server_bf4at(GameManager game, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[1];
      Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfList[1];
      Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfList[3];
      if (tdfInteger3.Value == 0L)
      {
        lock (game.ServerSlots)
        {
          if (game != null && game.ServerSlots != null && game.PlayerIDs != null)
          {
            int usedSlot = Helper.FindUsedSlot(tdfInteger2.Value, game);
            if (usedSlot != 0)
            {
              game.ServerSlots[usedSlot - 1] = false;
              game.PlayerIDs[usedSlot - 1] = -1L;
              game.Update = true;
              Logger.Log($"ID'{(object) game.ID}'bf3-server-pc ({(object) game.GID}) slot '{(object) usedSlot}' is free now");
            }
          }
        }
        Helper.RemovePlayer(tdfInteger2.Value, game.GID);
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer1 = sendEmpty.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(tdfInteger2.Value, game.GID, 1L);
        notifyPlayerRemoved.make();
        byte[] buffer2 = notifyPlayerRemoved.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
      }
      if (tdfInteger3.Value == 2L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer3 = sendEmpty.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        NotifyGamePlayerStateChange playerStateChange = new NotifyGamePlayerStateChange(tdfInteger2.Value, tdfInteger1.Value);
        playerStateChange.make();
        byte[] buffer4 = playerStateChange.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        NotifyPlayerJoinCompleted playerJoinCompleted = new NotifyPlayerJoinCompleted(tdfInteger2.Value, tdfInteger1.Value);
        playerJoinCompleted.make();
        byte[] buffer5 = playerJoinCompleted.ByteArray();
        memoryStream.Write(buffer5, 0, buffer5.Length);
      }
      buf = memoryStream.ToArray();
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_4_1D_server(GameManager game, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[0];
      List<Blaze.Tdf> values = ((List<Blaze.TdfStruct>) ((Blaze.TdfList) tdfList[1]).List)[0].Values;
      Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) values[1];
      Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) values[2];
      if (tdfInteger3.Value == 0L)
      {
        lock (game.ServerSlots)
        {
          if (game != null && game.ServerSlots != null && game.PlayerIDs != null)
          {
            int usedSlot = Helper.FindUsedSlot(tdfInteger2.Value, game);
            if (usedSlot != 0)
            {
              game.ServerSlots[usedSlot - 1] = false;
              game.PlayerIDs[usedSlot - 1] = -1L;
              game.Update = true;
              Logger.Log($"ID'{(object) game.ID}'bf3-server-pc ({(object) game.GID}) slot '{(object) usedSlot}' is free now");
            }
          }
        }
        Helper.RemovePlayer(tdfInteger2.Value, game.GID);
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer1 = sendEmpty.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(tdfInteger2.Value, game.GID, 1L);
        notifyPlayerRemoved.make();
        byte[] buffer2 = notifyPlayerRemoved.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
      }
      if (tdfInteger3.Value == 2L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer3 = sendEmpty.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        NotifyGamePlayerStateChange playerStateChange = new NotifyGamePlayerStateChange(tdfInteger2.Value, tdfInteger1.Value);
        playerStateChange.make();
        byte[] buffer4 = playerStateChange.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        NotifyPlayerJoinCompleted playerJoinCompleted = new NotifyPlayerJoinCompleted(tdfInteger2.Value, tdfInteger1.Value);
        playerJoinCompleted.make();
        byte[] buffer5 = playerJoinCompleted.ByteArray();
        memoryStream.Write(buffer5, 0, buffer5.Length);
      }
      buf = memoryStream.ToArray();
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_4_1(GameManager game, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfDoubleList tdfDoubleList = (Blaze.TdfDoubleList) tdfList[0];
      List<string> list1 = (List<string>) tdfDoubleList.List1;
      List<string> list2 = (List<string>) tdfDoubleList.List2;
      game.serverattr = list1;
      game.serverattrval = list2;
      game.Update = true;
      game?.UpdateAttributes(((List<string>) tdfDoubleList.List1).ToArray(), ((List<string>) tdfDoubleList.List2).ToArray());
      game.Update = true;
      Blaze.TdfString tdfString1 = (Blaze.TdfString) tdfList[4];
      game.GNAME = tdfString1.Value;
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[5];
      game.GSET = tdfInteger1.Value;
      List<Blaze.Tdf> values = ((List<Blaze.TdfStruct>) ((Blaze.TdfList) tdfList[8]).List)[0].Values;
      Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) values[1];
      if (values.Count == 2)
      {
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        game.INIP = new GameManager.NETDATA();
        game.INIP.IP = (uint) tdfInteger2.Value;
        game.INIP.PORT = (uint) tdfInteger3.Value;
        game.EXIP.PORT = game.INIP.PORT;
        game.Update = true;
      }
      if (values.Count == 3)
      {
        Blaze.TdfInteger tdfInteger4 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger5 = (Blaze.TdfInteger) tdfStruct.Values[2];
        game.INIP = new GameManager.NETDATA();
        game.INIP.IP = (uint) tdfInteger4.Value;
        game.INIP.PORT = (uint) tdfInteger5.Value;
        game.EXIP.PORT = game.INIP.PORT;
        game.Update = true;
      }
      Blaze.TdfInteger tdfInteger6 = (Blaze.TdfInteger) tdfList[9];
      game.IGNO = tdfInteger6.Value;
      Blaze.TdfInteger tdfInteger7 = (Blaze.TdfInteger) tdfList[10];
      game.NRES = tdfInteger7.Value;
      Blaze.TdfInteger tdfInteger8 = (Blaze.TdfInteger) tdfList[11];
      game.NTOP = tdfInteger8.Value;
      List<long> list = (List<long>) ((Blaze.TdfList) tdfList[12]).List;
      game.PCAP = list;
      Blaze.TdfString tdfString2 = (Blaze.TdfString) tdfList[13];
      game.PGID = tdfString2.Value;
      Blaze.TdfBlob tdfBlob = (Blaze.TdfBlob) tdfList[14];
      game.PGSC = tdfBlob.data;
      Blaze.TdfInteger tdfInteger9 = (Blaze.TdfInteger) tdfList[15];
      game.PMAX = tdfInteger9.Value;
      Blaze.TdfInteger tdfInteger10 = (Blaze.TdfInteger) tdfList[16 /*0x10*/];
      game.PRES = tdfInteger10.Value;
      Blaze.TdfInteger tdfInteger11 = (Blaze.TdfInteger) tdfList[17];
      game.QCAP = tdfInteger11.Value;
      Blaze.TdfInteger tdfInteger12 = (Blaze.TdfInteger) tdfList[20];
      game.TCAP = tdfInteger12.Value;
      string str = "714b05dc-93bc-49ac-961c-cb38b574f30a";
      game.UUID = str;
      game.Update = true;
      CreateGame createGame = new CreateGame(game.GID, p.ID);
      createGame.make();
      memoryStream.Write(createGame.ByteArray(), 0, createGame.ByteArray().Length);
      NotifyGameSetup notifyGameSetup = new NotifyGameSetup(game, (long) game.EXIP.IP, (long) game.EXIP.PORT, (long) game.INIP.IP, (long) game.INIP.PORT);
      notifyGameSetup.make();
      memoryStream.Write(notifyGameSetup.ByteArray(), 0, notifyGameSetup.ByteArray().Length);
      getGameListSnapshot gameListSnapshot = new getGameListSnapshot(game.GID, 1L);
      gameListSnapshot.make();
      memoryStream.Write(gameListSnapshot.ByteArray(), 0, gameListSnapshot.ByteArray().Length);
      Logger.Log($"ID'{(object) game.ID}'Server '{game.GNAME}' ({(object) game.GID}) registered");
      buf = memoryStream.ToArray();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_1_bf4at(GameManager game, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfDoubleList tdfDoubleList = (Blaze.TdfDoubleList) tdfList[0];
      List<string> list1 = (List<string>) tdfDoubleList.List1;
      List<string> list2 = (List<string>) tdfDoubleList.List2;
      game.serverattr = list1;
      game.serverattrval = list2;
      game.Update = true;
      game?.UpdateAttributes(((List<string>) tdfDoubleList.List1).ToArray(), ((List<string>) tdfDoubleList.List2).ToArray());
      game.Update = true;
      Blaze.TdfString tdfString1 = (Blaze.TdfString) tdfList[5];
      game.GNAME = tdfString1.Value;
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[6];
      game.GSET = tdfInteger1.Value;
      List<Blaze.Tdf> values = ((List<Blaze.TdfStruct>) ((Blaze.TdfList) tdfList[9]).List)[0].Values;
      Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) values[1];
      if (values.Count == 2)
      {
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        game.INIP = new GameManager.NETDATA();
        game.INIP.IP = (uint) tdfInteger2.Value;
        game.INIP.PORT = (uint) tdfInteger3.Value;
        game.EXIP.PORT = game.INIP.PORT;
        game.Update = true;
      }
      if (values.Count == 3)
      {
        Blaze.TdfInteger tdfInteger4 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger5 = (Blaze.TdfInteger) tdfStruct.Values[2];
        game.INIP = new GameManager.NETDATA();
        game.INIP.IP = (uint) tdfInteger4.Value;
        game.INIP.PORT = (uint) tdfInteger5.Value;
        game.EXIP.PORT = game.INIP.PORT;
        game.Update = true;
      }
      Blaze.TdfInteger tdfInteger6 = (Blaze.TdfInteger) tdfList[10];
      game.IGNO = tdfInteger6.Value;
      Blaze.TdfInteger tdfInteger7 = (Blaze.TdfInteger) tdfList[11];
      game.NRES = tdfInteger7.Value;
      Blaze.TdfInteger tdfInteger8 = (Blaze.TdfInteger) tdfList[12];
      game.NTOP = tdfInteger8.Value;
      List<long> list = (List<long>) ((Blaze.TdfList) tdfList[13]).List;
      game.PCAP = list;
      Blaze.TdfString tdfString2 = (Blaze.TdfString) tdfList[14];
      game.PGID = tdfString2.Value;
      Blaze.TdfBlob tdfBlob = (Blaze.TdfBlob) tdfList[15];
      game.PGSC = tdfBlob.data;
      Blaze.TdfInteger tdfInteger9 = (Blaze.TdfInteger) tdfList[16 /*0x10*/];
      game.PMAX = tdfInteger9.Value;
      Blaze.TdfInteger tdfInteger10 = (Blaze.TdfInteger) tdfList[17];
      game.PRES = tdfInteger10.Value;
      Blaze.TdfInteger tdfInteger11 = (Blaze.TdfInteger) tdfList[18];
      game.QCAP = tdfInteger11.Value;
      game.TCAP = 0L;
      string str = "714b05dc-93bc-49ac-961c-cb38b574f30a";
      game.UUID = str;
      game.Update = true;
      CreateGame createGame = new CreateGame(game.GID, p.ID);
      createGame.make();
      memoryStream.Write(createGame.ByteArray(), 0, createGame.ByteArray().Length);
      NotifyGameSetup notifyGameSetup = new NotifyGameSetup(game, (long) game.EXIP.IP, (long) game.EXIP.PORT, (long) game.INIP.IP, (long) game.INIP.PORT);
      notifyGameSetup.make();
      memoryStream.Write(notifyGameSetup.ByteArray(), 0, notifyGameSetup.ByteArray().Length);
      getGameListSnapshot gameListSnapshot = new getGameListSnapshot(game.GID, 1L);
      gameListSnapshot.make();
      memoryStream.Write(gameListSnapshot.ByteArray(), 0, gameListSnapshot.ByteArray().Length);
      Logger.Log($"ID'{(object) game.ID}'Server '{game.GNAME}' ({(object) game.GID}) registered");
      buf = memoryStream.ToArray();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }
}
