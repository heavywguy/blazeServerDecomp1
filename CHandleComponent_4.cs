// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_4
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_4
{
  public CHandleComponent_4(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      byte[] buf1 = (byte[]) null;
      bool termination1 = false;
      switch (p.Command)
      {
        case 1:
          this.HandleComponent_4_1(player, p, out buf1);
          break;
        case 3:
          MemoryStream memoryStream1 = new MemoryStream();
          SendEmpty sendEmpty1 = new SendEmpty(p);
          sendEmpty1.make();
          memoryStream1.Write(sendEmpty1.ByteArray(), 0, sendEmpty1.ByteArray().Length);
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
              GameManager allServer = GameManager.AllServers[player.GameID];
              new SQL_RUN().UpdateServer(player.GID, player.MAIL, allServer.serverattrval, allServer.GNAME, allServer.PCAP[0], "");
            }
          }
          buf1 = memoryStream1.ToArray();
          break;
        case 7:
          MemoryStream memoryStream2 = new MemoryStream();
          Blaze.TdfDoubleList tdfDoubleList = (Blaze.TdfDoubleList) Blaze.ReadPacketContent(p)[0];
          player?.UpdateAttributes(((List<string>) tdfDoubleList.List1).ToArray(), ((List<string>) tdfDoubleList.List2).ToArray());
          player.Update = true;
          List<string> attribval = new List<string>();
          foreach (GameClient.Attribut attribute in player.Attributes)
            attribval.Add(attribute.Value);
          new SQL_RUN().UpdateServerATTR(player.GID, player.MAIL, attribval, "1");
          SendEmpty sendEmpty2 = new SendEmpty(p);
          sendEmpty2.make();
          memoryStream2.Write(sendEmpty2.ByteArray(), 0, sendEmpty2.ByteArray().Length);
          GameManagerComponent50 managerComponent50 = new GameManagerComponent50(player.GID);
          managerComponent50.make();
          memoryStream2.Write(managerComponent50.ByteArray(), 0, managerComponent50.ByteArray().Length);
          buf1 = memoryStream2.ToArray();
          break;
        case 9:
          if (player.Version == "VeniceOpenBeta0final-1.0" || player.Version == "VeniceOpenBeta862700retail-1.0" || player.Version == "VeniceAlphaTrial749691retail-1.0")
          {
            this.HandleComponent_4_9_Beta(player, p, out buf1, out termination1);
            break;
          }
          if (player.Version == "pc")
          {
            this.HandleComponent_4_9_bf4at(player, p, out buf1, out termination1);
            break;
          }
          this.HandleComponent_4_9(player, p, out buf1, out termination1);
          break;
        case 11:
          MemoryStream memoryStream3 = new MemoryStream();
          List<Blaze.Tdf> tdfList2 = Blaze.ReadPacketContent(p);
          Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList2[2];
          Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfList2[3];
          Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfList2[4];
          SendEmpty sendEmpty3 = new SendEmpty(p);
          sendEmpty3.make();
          memoryStream3.Write(sendEmpty3.ByteArray(), 0, sendEmpty3.ByteArray().Length);
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(tdfInteger2.Value, tdfInteger1.Value, 6L);
          notifyPlayerRemoved.make();
          memoryStream3.Write(notifyPlayerRemoved.ByteArray(), 0, notifyPlayerRemoved.ByteArray().Length);
          buf1 = memoryStream3.ToArray();
          break;
        case 13:
          this.HandleComponent_4_0D(player, p, out buf1, out termination1);
          break;
        case 15:
          MemoryStream memoryStream4 = new MemoryStream();
          SendEmpty sendEmpty4 = new SendEmpty(p);
          sendEmpty4.make();
          memoryStream4.Write(sendEmpty4.ByteArray(), 0, sendEmpty4.ByteArray().Length);
          UserSessionExtendedDataUpdateCOOP extendedDataUpdateCoop = new UserSessionExtendedDataUpdateCOOP((long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT, player.UATT, player.PlayerID, player.GID, true);
          extendedDataUpdateCoop.make();
          memoryStream4.Write(extendedDataUpdateCoop.ByteArray(), 0, extendedDataUpdateCoop.ByteArray().Length);
          buf1 = memoryStream4.ToArray();
          break;
        case 19:
          MemoryStream memoryStream5 = new MemoryStream();
          SendEmpty sendEmpty5 = new SendEmpty(p);
          sendEmpty5.make();
          memoryStream5.Write(sendEmpty5.ByteArray(), 0, sendEmpty5.ByteArray().Length);
          getGameListSnapshot gameListSnapshot1 = new getGameListSnapshot(player.GameID, 130L);
          gameListSnapshot1.make();
          memoryStream5.Write(gameListSnapshot1.ByteArray(), 0, gameListSnapshot1.ByteArray().Length);
          buf1 = memoryStream5.ToArray();
          break;
        case 29:
          if (player.Version == "pc")
          {
            this.HandleComponent_4_1D_client_bf4at(player, p, out buf1, out termination1);
            break;
          }
          this.HandleComponent_4_1D_client(player, p, out buf1, out termination1);
          break;
        default:
          SendError sendError = new SendError(p, 16384 /*0x4000*/);
          sendError.make();
          buf1 = sendError.ByteArray();
          break;
      }
      buf = buf1;
      termination = termination1;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_1(GameClient player, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      player.GID = player.PlayerID;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfDoubleList tdfDoubleList = (Blaze.TdfDoubleList) tdfList[0];
      List<string> list1 = (List<string>) tdfDoubleList.List1;
      List<string> list2 = (List<string>) tdfDoubleList.List2;
      player.serverattr = list1;
      player.serverattrval = list2;
      player.Update = true;
      player?.UpdateAttributes(((List<string>) tdfDoubleList.List1).ToArray(), ((List<string>) tdfDoubleList.List2).ToArray());
      player.Update = true;
      Blaze.TdfString tdfString1 = (Blaze.TdfString) tdfList[4];
      player.GNAME = tdfString1.Value;
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[5];
      player.GSET = tdfInteger1.Value;
      List<Blaze.Tdf> values = ((List<Blaze.TdfStruct>) ((Blaze.TdfList) tdfList[8]).List)[0].Values;
      Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) values[1];
      if (values.Count == 2)
      {
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger2.Value;
        player.INIP.PORT = (uint) tdfInteger3.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
      }
      if (values.Count == 3)
      {
        Blaze.TdfInteger tdfInteger4 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger5 = (Blaze.TdfInteger) tdfStruct.Values[2];
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger4.Value;
        player.INIP.PORT = (uint) tdfInteger5.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
      }
      Blaze.TdfInteger tdfInteger6 = (Blaze.TdfInteger) tdfList[9];
      player.IGNO = tdfInteger6.Value;
      Blaze.TdfInteger tdfInteger7 = (Blaze.TdfInteger) tdfList[10];
      player.NRES = tdfInteger7.Value;
      Blaze.TdfInteger tdfInteger8 = (Blaze.TdfInteger) tdfList[11];
      player.NTOP = tdfInteger8.Value;
      List<long> list = (List<long>) ((Blaze.TdfList) tdfList[12]).List;
      player.PCAP = list;
      Blaze.TdfString tdfString2 = (Blaze.TdfString) tdfList[13];
      player.PGID = tdfString2.Value;
      Blaze.TdfBlob tdfBlob = (Blaze.TdfBlob) tdfList[14];
      player.PGSC = tdfBlob.data;
      Blaze.TdfInteger tdfInteger9 = (Blaze.TdfInteger) tdfList[15];
      player.PMAX = tdfInteger9.Value;
      Blaze.TdfInteger tdfInteger10 = (Blaze.TdfInteger) tdfList[16 /*0x10*/];
      player.PRES = tdfInteger10.Value;
      Blaze.TdfInteger tdfInteger11 = (Blaze.TdfInteger) tdfList[17];
      player.QCAP = tdfInteger11.Value;
      Blaze.TdfInteger tdfInteger12 = (Blaze.TdfInteger) tdfList[20];
      player.TCAP = tdfInteger12.Value;
      string str = "714b05dc-93bc-49ac-961c-cb38b574f30a";
      player.UUID = str;
      player.Update = true;
      CreateGame createGame = new CreateGame(player.GID, p.ID);
      createGame.make();
      memoryStream.Write(createGame.ByteArray(), 0, createGame.ByteArray().Length);
      NotifyGameSetupCOOP notifyGameSetupCoop = new NotifyGameSetupCOOP(player, (long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT);
      notifyGameSetupCoop.make();
      memoryStream.Write(notifyGameSetupCoop.ByteArray(), 0, notifyGameSetupCoop.ByteArray().Length);
      GameClient.AllServers.TryAdd(player.GID, player);
      Logger.Log($"ID'{(object) player.ID}'COOP Server '{player.GNAME}' ({(object) player.GID}) registered");
      buf = memoryStream.ToArray();
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_4_1D_client_bf4at(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[1];
      Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfList[1];
      Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfList[3];
      if (tdfInteger2.Value == 0L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer = sendEmpty.ByteArray();
        memoryStream.Write(buffer, 0, buffer.Length);
        NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
        notifyPlayerRemoved.make();
        try
        {
          GameManager allServer = GameManager.AllServers[player.GameID];
          if (allServer != null && CHandleComponent_4.IsSocketConnected(allServer.WorkSocket))
            allServer.WorkSocket.Send(notifyPlayerRemoved.ByteArray(), 0, notifyPlayerRemoved.ByteArray().Length, SocketFlags.None);
        }
        catch (Exception ex)
        {
          throw;
        }
      }
      if (tdfInteger2.Value == 2L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer1 = sendEmpty.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        UserSessionExtendedDataUpdate extendedDataUpdate = new UserSessionExtendedDataUpdate((long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT, player.UATT, player.PlayerID, player.GameID, true);
        extendedDataUpdate.make();
        byte[] buffer2 = extendedDataUpdate.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        NotifyGamePlayerStateChange playerStateChange = new NotifyGamePlayerStateChange(player.PlayerID, tdfInteger1.Value);
        playerStateChange.make();
        byte[] buffer3 = playerStateChange.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        NotifyPlayerJoinCompleted playerJoinCompleted = new NotifyPlayerJoinCompleted(player.PlayerID, tdfInteger1.Value);
        playerJoinCompleted.make();
        byte[] buffer4 = playerJoinCompleted.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_4_1D_client(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[0];
      List<Blaze.Tdf> values = ((List<Blaze.TdfStruct>) ((Blaze.TdfList) tdfList[1]).List)[0].Values;
      Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) values[2];
      Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) values[1];
      if (tdfInteger2.Value == 0L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer = sendEmpty.ByteArray();
        memoryStream.Write(buffer, 0, buffer.Length);
        NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
        notifyPlayerRemoved.make();
        try
        {
          GameManager allServer = GameManager.AllServers[player.GameID];
          if (allServer != null && CHandleComponent_4.IsSocketConnected(allServer.WorkSocket))
            allServer.WorkSocket.Send(notifyPlayerRemoved.ByteArray(), 0, notifyPlayerRemoved.ByteArray().Length, SocketFlags.None);
        }
        catch (Exception ex)
        {
          throw;
        }
      }
      if (tdfInteger2.Value == 2L)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        byte[] buffer1 = sendEmpty.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        UserSessionExtendedDataUpdate extendedDataUpdate = new UserSessionExtendedDataUpdate((long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT, player.UATT, player.PlayerID, player.GameID, true);
        extendedDataUpdate.make();
        byte[] buffer2 = extendedDataUpdate.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        NotifyGamePlayerStateChange playerStateChange = new NotifyGamePlayerStateChange(player.PlayerID, tdfInteger1.Value);
        playerStateChange.make();
        byte[] buffer3 = playerStateChange.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        NotifyPlayerJoinCompleted playerJoinCompleted = new NotifyPlayerJoinCompleted(player.PlayerID, tdfInteger1.Value);
        playerJoinCompleted.make();
        byte[] buffer4 = playerJoinCompleted.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_4_0D(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      long key = 1;
      if (!GameManager.AllServers.ContainsKey(key))
      {
        Logger.Log($"Server: {(object) key} not found for player '{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}')");
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        memoryStream.Write(sendError.ByteArray(), 0, sendError.ByteArray().Length);
        flag = true;
        new SQL_RUN().UpdateOnlinePlayers(player);
      }
      player.GameID = key;
      player.Update = true;
      if (!flag)
      {
        long num = new Random().NextLong(1008611L, 9999999L);
        Console.WriteLine(tdfList.Count);
        Blaze.TdfInteger tdfInteger1;
        Blaze.TdfInteger tdfInteger2;
        if (player.Version == "VeniceXpack51128071retail-1.0-0")
        {
          Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) tdfList[14]).Values[1];
          tdfInteger1 = (Blaze.TdfInteger) tdfStruct.Values[0];
          tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[1];
        }
        else
        {
          Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) tdfList[13]).Values[1];
          tdfInteger1 = (Blaze.TdfInteger) tdfStruct.Values[0];
          tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[1];
        }
        long playerslot = 0;
        GameClient.Slot = 0L;
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger1.Value;
        player.INIP.PORT = (uint) tdfInteger2.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
        StartMatchMaking startMatchMaking = new StartMatchMaking(num, p.ID);
        startMatchMaking.make();
        byte[] buffer1 = startMatchMaking.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        memoryStream.Write(Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID), 0, Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID).Length);
        joinGameByGroup joinGameByGroup = new joinGameByGroup(player.NAME, player.GameID, player.PlayerID, player.SID, (long) player.EXIP.IP, (long) player.INIP.IP, (long) player.EXIP.PORT, (long) player.INIP.PORT);
        joinGameByGroup.make();
        byte[] buffer2 = joinGameByGroup.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        UserAdded userAdded = new UserAdded(player.GameID, "bf3-server-pc", 1701729619L, 0, 0L);
        userAdded.make();
        byte[] buffer3 = userAdded.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.GameID);
        extendedDataAttribute.make();
        byte[] buffer4 = extendedDataAttribute.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        NotifyGameSetupPS3 notifyGameSetupPs3 = new NotifyGameSetupPS3(player, playerslot, num);
        notifyGameSetupPs3.make();
        byte[] buffer5 = notifyGameSetupPs3.ByteArray();
        memoryStream.Write(buffer5, 0, buffer5.Length);
        NotifyMatchmakingAsyncStatus matchmakingAsyncStatus = new NotifyMatchmakingAsyncStatus(num, player.PlayerID);
        matchmakingAsyncStatus.make();
        matchmakingAsyncStatus.ByteArray();
        if (!player.isActive)
        {
          NotifyMatchmakingFailed matchmakingFailed = new NotifyMatchmakingFailed(num, player.PlayerID);
          matchmakingFailed.make();
          byte[] buffer6 = matchmakingFailed.ByteArray();
          memoryStream.Write(buffer6, 0, buffer6.Length);
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
          notifyPlayerRemoved.make();
          byte[] buffer7 = notifyPlayerRemoved.ByteArray();
          memoryStream.Write(buffer7, 0, buffer7.Length);
          flag = true;
        }
        else
        {
          this.SendNotifyPlayerJoin(player, p);
          new SQL_RUN().UpdateOnlinePlayers(player);
        }
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_9_bf4at(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[2];
      long playerslot = 0;
      if (!GameManager.AllServers.ContainsKey(tdfInteger1.Value))
      {
        Logger.Log($"Server: {(object) tdfInteger1.Value} not found for player '{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}')");
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        memoryStream.Write(sendError.ByteArray(), 0, sendError.ByteArray().Length);
        flag = true;
        new SQL_RUN().UpdateOnlinePlayers(player);
      }
      player.GameID = tdfInteger1.Value;
      player.Update = true;
      if (!flag)
      {
        Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) tdfList[6]).Values[1];
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger2.Value;
        player.INIP.PORT = (uint) tdfInteger3.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
        JoinGame joinGame = new JoinGame(player.GameID, p.ID);
        joinGame.make();
        byte[] buffer1 = joinGame.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        memoryStream.Write(Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID), 0, Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID).Length);
        UserAdded userAdded = new UserAdded(player.GameID, "bf3-server-pc", 1701729619L, 0, 0L);
        userAdded.make();
        byte[] buffer2 = userAdded.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.GameID);
        extendedDataAttribute.make();
        byte[] buffer3 = extendedDataAttribute.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        LeaveGameByGroup leaveGameByGroup = new LeaveGameByGroup(player, playerslot);
        leaveGameByGroup.make();
        byte[] buffer4 = leaveGameByGroup.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        if (!player.isActive)
        {
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
          notifyPlayerRemoved.make();
          byte[] buffer5 = notifyPlayerRemoved.ByteArray();
          memoryStream.Write(buffer5, 0, buffer5.Length);
          flag = true;
        }
        else
        {
          this.SendNotifyPlayerJoin(player, p);
          new SQL_RUN().UpdateOnlinePlayers(player);
        }
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_9(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[3];
      long playerslot = ((Blaze.TdfInteger) tdfList[9]).Value;
      if (!GameManager.AllServers.ContainsKey(tdfInteger1.Value))
      {
        Logger.Log($"Server: {(object) tdfInteger1.Value} not found for player '{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}')");
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        memoryStream.Write(sendError.ByteArray(), 0, sendError.ByteArray().Length);
        flag = true;
        new SQL_RUN().UpdateOnlinePlayers(player);
      }
      player.GameID = tdfInteger1.Value;
      player.Update = true;
      if (!flag)
      {
        Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) tdfList[7]).Values[1];
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger2.Value;
        player.INIP.PORT = (uint) tdfInteger3.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
        JoinGame joinGame = new JoinGame(player.GameID, p.ID);
        joinGame.make();
        byte[] buffer1 = joinGame.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        memoryStream.Write(Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID), 0, Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID).Length);
        UserAdded userAdded = new UserAdded(player.GameID, "bf3-server-pc", 1701729619L, 0, 0L);
        userAdded.make();
        byte[] buffer2 = userAdded.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.GameID);
        extendedDataAttribute.make();
        byte[] buffer3 = extendedDataAttribute.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        LeaveGameByGroup leaveGameByGroup = new LeaveGameByGroup(player, playerslot);
        leaveGameByGroup.make();
        byte[] buffer4 = leaveGameByGroup.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        if (!player.isActive)
        {
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
          notifyPlayerRemoved.make();
          byte[] buffer5 = notifyPlayerRemoved.ByteArray();
          memoryStream.Write(buffer5, 0, buffer5.Length);
          flag = true;
        }
        else
        {
          this.SendNotifyPlayerJoin(player, p);
          new SQL_RUN().UpdateOnlinePlayers(player);
        }
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }

  private void HandleComponent_4_9_Beta(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      MemoryStream memoryStream = new MemoryStream();
      List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
      Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfList[2];
      if (!GameManager.AllServers.ContainsKey(tdfInteger1.Value))
      {
        Logger.Log($"Server: {(object) tdfInteger1.Value} not found for player '{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}')");
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        memoryStream.Write(sendError.ByteArray(), 0, sendError.ByteArray().Length);
        flag = true;
        new SQL_RUN().UpdateOnlinePlayers(player);
      }
      player.GameID = tdfInteger1.Value;
      player.Update = true;
      if (!flag)
      {
        Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) tdfList[6]).Values[1];
        Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[0];
        Blaze.TdfInteger tdfInteger3 = (Blaze.TdfInteger) tdfStruct.Values[1];
        long playerslot = 0;
        GameClient.Slot = 0L;
        player.INIP = new GameClient.NETDATA();
        player.INIP.IP = (uint) tdfInteger2.Value;
        player.INIP.PORT = (uint) tdfInteger3.Value;
        player.EXIP.PORT = player.INIP.PORT;
        player.Update = true;
        JoinGame joinGame = new JoinGame(player.GameID, p.ID);
        joinGame.make();
        byte[] buffer1 = joinGame.ByteArray();
        memoryStream.Write(buffer1, 0, buffer1.Length);
        memoryStream.Write(Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID), 0, Helper.MakeCompleateAsyncPacket(player.GameID, player.PlayerID).Length);
        UserAdded userAdded = new UserAdded(player.GameID, "bf3-server-pc", 1701729619L, 0, 0L);
        userAdded.make();
        byte[] buffer2 = userAdded.ByteArray();
        memoryStream.Write(buffer2, 0, buffer2.Length);
        UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.GameID);
        extendedDataAttribute.make();
        byte[] buffer3 = extendedDataAttribute.ByteArray();
        memoryStream.Write(buffer3, 0, buffer3.Length);
        LeaveGameByGroup_Beta leaveGameByGroupBeta = new LeaveGameByGroup_Beta(player, playerslot);
        leaveGameByGroupBeta.make();
        byte[] buffer4 = leaveGameByGroupBeta.ByteArray();
        memoryStream.Write(buffer4, 0, buffer4.Length);
        if (!player.isActive)
        {
          NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
          notifyPlayerRemoved.make();
          byte[] buffer5 = notifyPlayerRemoved.ByteArray();
          memoryStream.Write(buffer5, 0, buffer5.Length);
          flag = true;
        }
        else
        {
          this.SendNotifyPlayerJoin(player, p);
          new SQL_RUN().UpdateOnlinePlayers(player);
        }
      }
      buf = memoryStream.ToArray();
      termination = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void SendNotifyPlayerJoin(GameClient player, Blaze.Packet p)
  {
    try
    {
      MemoryStream memoryStream = new MemoryStream();
      GameManager allServer = GameManager.AllServers[player.GameID];
      BlazeServer.Network.Send.SendNotifyPlayerJoin notifyPlayerJoin = new BlazeServer.Network.Send.SendNotifyPlayerJoin(player.NAME, player.GameID, player.PlayerID, player.SID, (long) player.EXIP.IP, (long) player.INIP.IP, (long) player.EXIP.PORT, (long) player.INIP.PORT);
      notifyPlayerJoin.make();
      byte[] buffer1 = notifyPlayerJoin.ByteArray();
      memoryStream.Write(buffer1, 0, buffer1.Length);
      NotifyPlayerClaimingReservation claimingReservation = new NotifyPlayerClaimingReservation(player.NAME, player.GameID, player.PlayerID, player.SID, (long) player.EXIP.IP, (long) player.INIP.IP, (long) player.EXIP.PORT, (long) player.INIP.PORT);
      claimingReservation.make();
      byte[] buffer2 = claimingReservation.ByteArray();
      memoryStream.Write(buffer2, 0, buffer2.Length);
      UserSessionExtendedDataUpdate extendedDataUpdate = new UserSessionExtendedDataUpdate((long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT, player.UATT, player.PlayerID, player.GameID, true);
      extendedDataUpdate.make();
      byte[] buffer3 = extendedDataUpdate.ByteArray();
      memoryStream.Write(buffer3, 0, buffer3.Length);
      if (allServer == null || !CHandleComponent_4.IsSocketConnected(allServer.WorkSocket))
        return;
      allServer.WorkSocket.Send(memoryStream.ToArray(), 0, memoryStream.ToArray().Length, SocketFlags.None);
    }
    catch (Exception ex)
    {
      Logger.Error("SendNotifyPlayerJoin error (component 4): ", ex);
    }
  }

  private static bool IsSocketConnected(Socket s)
  {
    bool flag;
    try
    {
      flag = (!s.Poll(1000, SelectMode.SelectRead) || s.Available != 0) && s.Connected;
    }
    catch (Exception ex)
    {
      flag = false;
    }
    return flag;
  }
}
