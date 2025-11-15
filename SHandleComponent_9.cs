// Decompiled with JetBrains decompiler
// Type: BlazeServer.SHandleComponent_9
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;

#nullable disable
namespace BlazeServer;

internal class SHandleComponent_9
{
  public SHandleComponent_9(
    GameManager player,
    Blaze.Packet p,
    out byte[] buf,
    out bool timeout_restart)
  {
    try
    {
      bool flag = false;
      byte[] numArray;
      switch (p.Command)
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
          flag = true;
          break;
        case 5:
          getTelemetryServer getTelemetryServer = new getTelemetryServer(p.ID);
          getTelemetryServer.make();
          numArray = getTelemetryServer.ByteArray();
          break;
        case 7:
          List<Blaze.Tdf> tdfList = Blaze.ReadPacketContent(p);
          Blaze.TdfString tdfString1 = (Blaze.TdfString) ((Blaze.TdfStruct) tdfList[0]).Values[2];
          Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) tdfList[1];
          Blaze.TdfString tdfString2 = (Blaze.TdfString) tdfStruct.Values[4];
          Blaze.TdfString tdfString3 = (Blaze.TdfString) tdfStruct.Values[2];
          player.ServerVersion = tdfString2.Value;
          player.ServerType = tdfString3.Value;
          Console.WriteLine(player.ServerVersion);
          preAuth preAuth = new preAuth(tdfString1.Value, p.ID);
          preAuth.make();
          numArray = preAuth.ByteArray();
          break;
        case 8:
          postAuth postAuth = new postAuth(player.GID, p.ID);
          postAuth.make();
          numArray = postAuth.ByteArray();
          break;
        case 22:
          if (Config.UseClientMetrics == 1)
          {
            Blaze.TdfInteger tdfInteger = (Blaze.TdfInteger) Blaze.ReadPacketContent(p)[6];
            player.EXIP.IP = (uint) tdfInteger.Value;
          }
          SendEmpty sendEmpty2 = new SendEmpty(p);
          sendEmpty2.make();
          numArray = sendEmpty2.ByteArray();
          break;
        default:
          SendError sendError = new SendError(p, 16386);
          sendError.make();
          numArray = sendError.ByteArray();
          break;
      }
      buf = numArray;
      timeout_restart = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
