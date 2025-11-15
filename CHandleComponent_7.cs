// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_7
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_7
{
  public CHandleComponent_7(GameClient player, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      ushort command = p.Command;
      byte[] numArray;
      switch (command)
      {
        case 4:
          if (((Blaze.TdfString) Blaze.ReadPacketContent(p)[0]).Value == "coopplayer_coopdefault")
          {
            GetStatGroupCOOP getStatGroupCoop = new GetStatGroupCOOP(p.ID);
            getStatGroupCoop.make();
            numArray = getStatGroupCoop.ByteArray();
          }
          else
          {
            GetStatGroup getStatGroup = new GetStatGroup(p.ID);
            getStatGroup.make();
            numArray = getStatGroup.ByteArray();
          }
          break;
        case 10:
          SendEmpty sendEmpty1 = new SendEmpty(p);
          sendEmpty1.make();
          numArray = sendEmpty1.ByteArray();
          break;
        default:
          switch (command)
          {
            case 14:
              SendEmpty sendEmpty2 = new SendEmpty(p);
              sendEmpty2.make();
              numArray = sendEmpty2.ByteArray();
              break;
            case 16 /*0x10*/:
              List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
              Blaze.TdfString tdfString = (Blaze.TdfString) tdfList[2];
              Blaze.TdfInteger tdfInteger = (Blaze.TdfInteger) tdfList[7];
              List<long> list = (List<long>) ((Blaze.TdfList) tdfList[1]).List;
              GameClient allPlayer = GameClient.AllPlayers[list[0]];
              List<string> stringList = new List<string>();
              new SQL_RUN().UserStatLoad(allPlayer, stringList);
              MemoryStream memoryStream = new MemoryStream();
              getStatsByGroupAsync statsByGroupAsync = new getStatsByGroupAsync(p.ID);
              statsByGroupAsync.make();
              byte[] buffer1 = statsByGroupAsync.ByteArray();
              memoryStream.Write(buffer1, 0, buffer1.Length);
              if (tdfString.Value == "coopplayer_coopdefault")
              {
                GetStatsAsyncNotificationCOOP notificationCoop = new GetStatsAsyncNotificationCOOP(list, stringList, tdfInteger.Value, false);
                notificationCoop.make();
                byte[] buffer2 = notificationCoop.ByteArray();
                memoryStream.Write(buffer2, 0, buffer2.Length);
              }
              else
              {
                GetStatsAsyncNotification asyncNotification = new GetStatsAsyncNotification(list, stringList, tdfInteger.Value);
                asyncNotification.make();
                byte[] buffer3 = asyncNotification.ByteArray();
                memoryStream.Write(buffer3, 0, buffer3.Length);
              }
              numArray = memoryStream.ToArray();
              Logger.Log($"ID'{(object) allPlayer.ID}'{allPlayer.NAME}' ({(object) allPlayer.PlayerID}'{allPlayer.PlayerID.ToString("X")}') rank '{allPlayer.rank}'  entered the game");
              break;
            default:
              SendError sendError = new SendError(p, 16384 /*0x4000*/);
              sendError.make();
              numArray = sendError.ByteArray();
              break;
          }
          break;
      }
      buf = numArray;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
