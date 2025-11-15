// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_1
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_1
{
  public CHandleComponent_1(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      byte[] numArray;
      switch (p.Command)
      {
        case 29:
          if (((Blaze.TdfString) Blaze.ReadPacketContent(p)[3]).Value == "ONLINE_ACCESS")
          {
            listUserEntitlements2 userEntitlements2 = new listUserEntitlements2(true, p.ID, player.Version);
            userEntitlements2.make();
            numArray = userEntitlements2.ByteArray();
            break;
          }
          listUserEntitlements2 userEntitlements2_1 = new listUserEntitlements2(false, p.ID, player.Version);
          userEntitlements2_1.make();
          numArray = userEntitlements2_1.ByteArray();
          break;
        case 36:
          getAuthToken getAuthToken = new getAuthToken(p.ID);
          getAuthToken.make();
          numArray = getAuthToken.ByteArray();
          break;
        case 39:
          SendEmpty sendEmpty = new SendEmpty(p);
          sendEmpty.make();
          numArray = sendEmpty.ByteArray();
          break;
        case 50:
          MemoryStream memoryStream1 = new MemoryStream();
          List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
          Blaze.TdfString tdfString = (Blaze.TdfString) tdfList[0];
          Blaze.TdfInteger tdfInteger = (Blaze.TdfInteger) tdfList[1];
          if (GameClient.AllPlayers.ContainsKey(tdfInteger.Value))
          {
            GameClient allPlayer = GameClient.AllPlayers[tdfInteger.Value];
            if (allPlayer.Ptimer.ElapsedMilliseconds > 60000L)
            {
              allPlayer.Ptimer.Stop();
              allPlayer.Close();
              if (GameClient.AllPlayers.TryRemove(tdfInteger.Value, out GameClient _))
                Logger.Log($"ID'{(object) allPlayer.ID}' ({tdfInteger.Value.ToString()}'{tdfInteger.Value.ToString("X")}')  inactive player removed");
              else
                Logger.Log($"ID'{(object) allPlayer.ID}' ({tdfInteger.Value.ToString()}'{tdfInteger.Value.ToString("X")}')  inactive player remove false");
            }
            else
            {
              Logger.Log($"Error: ID'{(object) player.ID}' ({tdfInteger.Value.ToString()}'{tdfInteger.Value.ToString("X")}')  already ingame");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig)
          {
            player.AUTH = tdfString.Value;
            if (player.AUTH.Contains("0-0-0-0-0-1"))
            {
              Logger.Log($"ID'{player.ID.ToString()}' Outdated Redirector is not allowed!");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig && Helper.CheckBanned(player.AUTH))
          {
            Logger.Log($"ID'{(object) player.ID}' banned players({player.AUTH}) are not allowed!");
            player.isActive = false;
            player.Update = true;
          }
          player.PlayerID = tdfInteger.Value;
          player.Update = true;
          if (player.isActive)
            new SQL_RUN().SilentLoginAuthCheck(player);
          if (player.isActive)
          {
            if (GameClient.AllPlayers.TryAdd(player.PlayerID, player))
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.PlayerID}) Added {player.AUTH}");
            else
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) Couldnt Add");
            SilentLogin silentLogin = new SilentLogin(player.NAME, player.MAIL, player.PlayerID, player.GameID, player.BUID, player.UserID, p.ID);
            silentLogin.make();
            byte[] buffer1 = silentLogin.ByteArray();
            memoryStream1.Write(buffer1, 0, buffer1.Length);
            UserAdded userAdded = new UserAdded(player.PlayerID, player.NAME, 1920292179L, 0, player.UATT);
            userAdded.make();
            byte[] buffer2 = userAdded.ByteArray();
            memoryStream1.Write(buffer2, 0, buffer2.Length);
            UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.PlayerID);
            extendedDataAttribute.make();
            byte[] buffer3 = extendedDataAttribute.ByteArray();
            memoryStream1.Write(buffer3, 0, buffer3.Length);
          }
          else
          {
            SendError sendError = new SendError(p, 16384 /*0x4000*/);
            sendError.make();
            byte[] buffer = sendError.ByteArray();
            memoryStream1.Write(buffer, 0, buffer.Length);
            flag = true;
          }
          numArray = memoryStream1.ToArray();
          break;
        case 60:
          MemoryStream memoryStream2 = new MemoryStream();
          Blaze.ReadPacketContent(p);
          long key1 = 2;
          if (GameClient.AllPlayers.ContainsKey(key1))
          {
            GameClient allPlayer = GameClient.AllPlayers[key1];
            if (allPlayer.Ptimer.ElapsedMilliseconds > 60000L)
            {
              allPlayer.Ptimer.Stop();
              allPlayer.Close();
              if (GameClient.AllPlayers.TryRemove(key1, out GameClient _))
                Logger.Log($"ID'{(object) allPlayer.ID}' ({key1.ToString()}'{key1.ToString("X")}')  inactive player removed");
              else
                Logger.Log($"ID'{(object) allPlayer.ID}' ({key1.ToString()}'{key1.ToString("X")}')  inactive player remove false");
            }
            else
            {
              Logger.Log($"Error: ID'{(object) player.ID}' ({key1.ToString()}'{key1.ToString("X")}')  already ingame");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig)
          {
            player.AUTH = "0-0-0-0-0-0";
            if (player.AUTH.Contains("0-0-0-0-0-1"))
            {
              Logger.Log($"ID'{player.ID.ToString()}' Outdated Redirector is not allowed!");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig && Helper.CheckBanned(player.AUTH))
          {
            Logger.Log($"ID'{(object) player.ID}' banned players({player.AUTH}) are not allowed!");
            player.isActive = false;
            player.Update = true;
          }
          player.PlayerID = key1;
          player.Update = true;
          if (player.isActive)
            new SQL_RUN().SilentLoginAuthCheck(player);
          if (player.isActive)
          {
            if (GameClient.AllPlayers.TryAdd(player.PlayerID, player))
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.PlayerID}) Added {player.AUTH}");
            else
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) Couldnt Add");
            ExpressLogin expressLogin = new ExpressLogin(player.GID, p.ID);
            expressLogin.make();
            byte[] buffer4 = expressLogin.ByteArray();
            memoryStream2.Write(buffer4, 0, buffer4.Length);
            UserAdded userAdded = new UserAdded(player.PlayerID, player.NAME, 1920292179L, 0, player.UATT);
            userAdded.make();
            byte[] buffer5 = userAdded.ByteArray();
            memoryStream2.Write(buffer5, 0, buffer5.Length);
            UserAuthenticated userAuthenticated = new UserAuthenticated(player.NAME, player.MAIL, player.GID, player.GID, 1701729619L);
            userAuthenticated.make();
            byte[] buffer6 = userAuthenticated.ByteArray();
            memoryStream2.Write(buffer6, 0, buffer6.Length);
            UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.PlayerID);
            extendedDataAttribute.make();
            byte[] buffer7 = extendedDataAttribute.ByteArray();
            memoryStream2.Write(buffer7, 0, buffer7.Length);
          }
          else
          {
            SendError sendError = new SendError(p, 16384 /*0x4000*/);
            sendError.make();
            byte[] buffer = sendError.ByteArray();
            memoryStream2.Write(buffer, 0, buffer.Length);
            flag = true;
          }
          numArray = memoryStream2.ToArray();
          break;
        case 110:
          MemoryStream memoryStream3 = new MemoryStream();
          LoginPersona loginPersona = new LoginPersona(player.GameID, player.MAIL, player.NAME, player.UserID, p.ID);
          loginPersona.make();
          byte[] buffer8 = loginPersona.ByteArray();
          memoryStream3.Write(buffer8, 0, buffer8.Length);
          UserAdded userAdded1 = new UserAdded(player.GameID, player.NAME, 1701729619L, 0, 0L);
          userAdded1.make();
          byte[] buffer9 = userAdded1.ByteArray();
          memoryStream3.Write(buffer9, 0, buffer9.Length);
          UpdateExtendedDataAttribute extendedDataAttribute1 = new UpdateExtendedDataAttribute(player.GameID);
          extendedDataAttribute1.make();
          byte[] buffer10 = extendedDataAttribute1.ByteArray();
          memoryStream3.Write(buffer10, 0, buffer10.Length);
          numArray = memoryStream3.ToArray();
          break;
        case 200:
          MemoryStream memoryStream4 = new MemoryStream();
          Blaze.ReadPacketContent(p);
          long key2 = new Random().NextLong(10000L, 14999L);
          if (GameClient.AllPlayers.ContainsKey(key2))
          {
            GameClient allPlayer = GameClient.AllPlayers[key2];
            if (allPlayer.Ptimer.ElapsedMilliseconds > 60000L)
            {
              allPlayer.Ptimer.Stop();
              allPlayer.Close();
              if (GameClient.AllPlayers.TryRemove(key2, out GameClient _))
                Logger.Log($"ID'{(object) allPlayer.ID}' ({key2.ToString()}'{key2.ToString("X")}')  inactive player removed");
              else
                Logger.Log($"ID'{(object) allPlayer.ID}' ({key2.ToString()}'{key2.ToString("X")}')  inactive player remove false");
            }
            else
            {
              Logger.Log($"Error: ID'{(object) player.ID}' ({key2.ToString()}'{key2.ToString("X")}')  already ingame");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig)
          {
            player.AUTH = "0-0-0-0-0-0";
            if (player.AUTH.Contains("0-0-0-0-0-1"))
            {
              Logger.Log($"ID'{player.ID.ToString()}' Outdated Redirector is not allowed!");
              player.isActive = false;
              player.Update = true;
            }
          }
          if (player.isActive && Config.loadbanconfig && Helper.CheckBanned(player.AUTH))
          {
            Logger.Log($"ID'{(object) player.ID}' banned players({player.AUTH}) are not allowed!");
            player.isActive = false;
            player.Update = true;
          }
          player.PlayerID = key2;
          player.Update = true;
          if (player.isActive)
            new SQL_RUN().SilentLoginAuthCheck(player);
          if (player.isActive)
          {
            if (GameClient.AllPlayers.TryAdd(player.PlayerID, player))
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.PlayerID}) Added {player.AUTH}");
            else
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) Couldnt Add");
            PS3Login ps3Login = new PS3Login(player.NAME, player.MAIL, player.PlayerID, player.GameID, player.BUID, player.UserID, p.ID);
            ps3Login.make();
            byte[] buffer11 = ps3Login.ByteArray();
            memoryStream4.Write(buffer11, 0, buffer11.Length);
            UserAdded userAdded2 = new UserAdded(player.PlayerID, player.NAME, 1920292179L, 0, player.UATT);
            userAdded2.make();
            byte[] buffer12 = userAdded2.ByteArray();
            memoryStream4.Write(buffer12, 0, buffer12.Length);
            UpdateExtendedDataAttribute extendedDataAttribute2 = new UpdateExtendedDataAttribute(player.PlayerID);
            extendedDataAttribute2.make();
            byte[] buffer13 = extendedDataAttribute2.ByteArray();
            memoryStream4.Write(buffer13, 0, buffer13.Length);
          }
          else
          {
            SendError sendError = new SendError(p, 16384 /*0x4000*/);
            sendError.make();
            byte[] buffer14 = sendError.ByteArray();
            memoryStream4.Write(buffer14, 0, buffer14.Length);
            flag = true;
          }
          numArray = memoryStream4.ToArray();
          break;
        default:
          SendError sendError1 = new SendError(p, 16384 /*0x4000*/);
          sendError1.make();
          numArray = sendError1.ByteArray();
          flag = true;
          break;
      }
      buf = numArray;
      termination = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
