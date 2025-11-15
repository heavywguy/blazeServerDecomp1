// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_9
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_9
{
  private static ManualResetEvent sendDone = new ManualResetEvent(false);
  private static ManualResetEvent closeDone = new ManualResetEvent(false);

  public CHandleComponent_9(
    GameClient player,
    Blaze.Packet p,
    out byte[] buf,
    out bool timeout_restart,
    out bool termination)
  {
    try
    {
      bool flag1 = false;
      bool flag2 = false;
      ushort command = p.Command;
      SendError sendError = new SendError(p, 16392);
      byte[] numArray;
      switch (command)
      {
        case 1:
          SendEmpty sendEmpty1 = new SendEmpty(p);
          sendEmpty1.make();
          numArray = sendEmpty1.ByteArray();
          break;
        case 2:
          ping ping = new ping(p.ID);
          ping.make();
          numArray = ping.ByteArray();
          flag1 = true;
          break;
        case 3:
        case 4:
        case 6:
        case 9:
          sendError.make();
          numArray = sendError.ByteArray();
          flag2 = true;
          break;
        case 5:
          getTelemetryServer getTelemetryServer = new getTelemetryServer(p.ID);
          getTelemetryServer.make();
          numArray = getTelemetryServer.ByteArray();
          break;
        case 7:
          List<Blaze.Tdf> tdfList1 = Blaze.ReadPacketContent(p);
          Blaze.TdfString tdfString1 = (Blaze.TdfString) ((Blaze.TdfStruct) tdfList1[0]).Values[2];
          Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) tdfList1[1];
          Blaze.TdfString tdfString2 = (Blaze.TdfString) tdfStruct.Values[1];
          Blaze.TdfString tdfString3 = (Blaze.TdfString) tdfStruct.Values[4];
          Console.WriteLine(tdfString3.Value);
          player.Version = tdfString3.Value;
          preAuth preAuth = new preAuth(tdfString1.Value, p.ID);
          preAuth.make();
          numArray = preAuth.ByteArray();
          break;
        case 8:
          Console.WriteLine("postAuth");
          postAuth postAuth = new postAuth(player.PlayerID, p.ID);
          postAuth.make();
          numArray = postAuth.ByteArray();
          break;
        case 10:
          SendEmpty sendEmpty2 = new SendEmpty(p);
          sendEmpty2.make();
          numArray = sendEmpty2.ByteArray();
          break;
        case 11:
          MemoryStream memoryStream = new MemoryStream();
          SendEmpty sendEmpty3 = new SendEmpty(p);
          sendEmpty3.make();
          byte[] buffer1 = sendEmpty3.ByteArray();
          memoryStream.Write(buffer1, 0, buffer1.Length);
          List<Blaze.Tdf> tdfList2 = Blaze.ReadPacketContent(p);
          for (int index = 0; index < tdfList2.Count; ++index)
          {
            Blaze.Tdf tdf = tdfList2[index];
            switch (tdf.Label)
            {
              case null:
                continue;
              case "DATA":
                Blaze.TdfString tdfString4 = (Blaze.TdfString) tdf;
                player.datavalue = tdfString4.Value;
                player.Update = true;
                break;
              case "KEY ":
                Blaze.TdfString tdfString5 = (Blaze.TdfString) tdf;
                player.keyvalue = tdfString5.Value;
                player.Update = true;
                break;
            }
          }
          if (player.keyvalue == "cust")
          {
            NotifyPlayerRemoved notifyPlayerRemoved = new NotifyPlayerRemoved(player.PlayerID, player.GameID, 6L);
            notifyPlayerRemoved.make();
            byte[] buffer2 = notifyPlayerRemoved.ByteArray();
            memoryStream.Write(buffer2, 0, buffer2.Length);
            new SQL_RUN().CustUserSettingsSave(player, player.datavalue);
          }
          if (player.keyvalue == "sdt")
            new SQL_RUN().SdtUserSettingsSave(player.PlayerID, player.datavalue);
          numArray = memoryStream.ToArray();
          break;
        case 12:
          List<string> List1 = new List<string>();
          List<string> List2 = new List<string>();
          List1.Add("cust");
          List2.Add(player.CustUserSettings);
          if (player.SdtUserSettings != null)
          {
            List1.Add("sdt");
            List2.Add(player.SdtUserSettings);
            UserSetingsLoad userSetingsLoad = new UserSetingsLoad((object) List1, (object) List2, 2, p.ID);
            userSetingsLoad.make();
            numArray = userSetingsLoad.ByteArray();
            break;
          }
          UserSetingsLoad userSetingsLoad1 = new UserSetingsLoad((object) List1, (object) List2, 1, p.ID);
          userSetingsLoad1.make();
          numArray = userSetingsLoad1.ByteArray();
          break;
        case 28:
          SendEmpty sendEmpty4 = new SendEmpty(p);
          sendEmpty4.make();
          numArray = sendEmpty4.ByteArray();
          break;
        default:
          if (command == (ushort) 22)
          {
            if (Config.UseClientMetrics == 1)
            {
              Blaze.TdfInteger tdfInteger = (Blaze.TdfInteger) Blaze.ReadPacketContent(p)[6];
              player.EXIP.IP = (uint) tdfInteger.Value;
            }
            SendEmpty sendEmpty5 = new SendEmpty(p);
            sendEmpty5.make();
            numArray = sendEmpty5.ByteArray();
            break;
          }
          goto case 3;
      }
      buf = numArray;
      timeout_restart = flag1;
      termination = flag2;
    }
    catch (Exception ex)
    {
      throw;
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

  private void SendCallback(IAsyncResult result)
  {
    try
    {
      ((Socket) result.AsyncState).EndSend(result);
      CHandleComponent_9.sendDone.Set();
    }
    catch (Exception ex)
    {
      Logger.Error("SendCallback error (component 9): ", ex);
    }
  }
}
