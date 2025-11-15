// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_0023
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_0023
{
  public CHandleComponent_0023(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      byte[] numArray;
      if (p.Command == (ushort) 10)
      {
        MemoryStream memoryStream = new MemoryStream();
        long key = (long) int.Parse(((Blaze.TdfString) Blaze.ReadPacketContent(p)[0]).Value);
        if (GameClient.AllPlayers.ContainsKey(key))
        {
          GameClient allPlayer = GameClient.AllPlayers[key];
          if (allPlayer.Ptimer.ElapsedMilliseconds > 60000L)
          {
            allPlayer.Ptimer.Stop();
            allPlayer.Close();
            if (GameClient.AllPlayers.TryRemove(key, out GameClient _))
              Logger.Log($"ID'{(object) allPlayer.ID}' ({key.ToString()}'{key.ToString("X")}')  inactive player removed");
            else
              Logger.Log($"ID'{(object) allPlayer.ID}' ({key.ToString()}'{key.ToString("X")}')  inactive player remove false");
          }
          else
          {
            Logger.Log($"Error: ID'{(object) player.ID}' ({key.ToString()}'{key.ToString("X")}')  already ingame");
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
        player.PlayerID = key;
        player.Update = true;
        if (player.isActive)
          new SQL_RUN().SilentLoginAuthCheck(player);
        if (player.isActive)
        {
          if (GameClient.AllPlayers.TryAdd(player.PlayerID, player))
            Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.PlayerID}) Added {player.AUTH}");
          else
            Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) Couldnt Add");
          LoginBF4 loginBf4 = new LoginBF4(player.NAME, player.MAIL, player.PlayerID, player.GameID, player.BUID, player.UserID, p.ID);
          loginBf4.make();
          byte[] buffer1 = loginBf4.ByteArray();
          memoryStream.Write(buffer1, 0, buffer1.Length);
          UserAdded userAdded = new UserAdded(player.PlayerID, player.NAME, 1920292179L, 0, player.UATT);
          userAdded.make();
          byte[] buffer2 = userAdded.ByteArray();
          memoryStream.Write(buffer2, 0, buffer2.Length);
          UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.PlayerID);
          extendedDataAttribute.make();
          byte[] buffer3 = extendedDataAttribute.ByteArray();
          memoryStream.Write(buffer3, 0, buffer3.Length);
          UserAuthenticated userAuthenticated = new UserAuthenticated(player.NAME, player.MAIL, player.GID, player.GID, 1701729619L);
          userAuthenticated.make();
          byte[] buffer4 = userAuthenticated.ByteArray();
          memoryStream.Write(buffer4, 0, buffer4.Length);
        }
        else
        {
          SendError sendError = new SendError(p, 16384 /*0x4000*/);
          sendError.make();
          byte[] buffer = sendError.ByteArray();
          memoryStream.Write(buffer, 0, buffer.Length);
          flag = true;
        }
        numArray = memoryStream.ToArray();
      }
      else
      {
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        numArray = sendError.ByteArray();
        flag = true;
      }
      buf = numArray;
      termination = flag;
    }
    catch (Exception ex)
    {
      Logger.Error("PacketHandlerC error :", ex);
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      throw;
    }
  }
}
