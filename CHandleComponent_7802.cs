// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_7802
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_7802
{
  public CHandleComponent_7802(
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
        case 20:
          Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) ((Blaze.TdfStruct) Blaze.ReadPacketContent(p)[1]).Values[1];
          Blaze.TdfInteger tdfInteger1 = (Blaze.TdfInteger) tdfStruct.Values[0];
          Blaze.TdfInteger tdfInteger2 = (Blaze.TdfInteger) tdfStruct.Values[1];
          player.INIP = new GameClient.NETDATA();
          player.INIP.IP = (uint) tdfInteger1.Value;
          player.INIP.PORT = (uint) tdfInteger2.Value;
          player.EXIP.PORT = player.INIP.PORT;
          player.Update = true;
          MemoryStream memoryStream = new MemoryStream();
          SendEmpty sendEmpty = new SendEmpty(p);
          sendEmpty.make();
          byte[] buffer1 = sendEmpty.ByteArray();
          memoryStream.Write(buffer1, 0, buffer1.Length);
          UserSessionExtendedDataUpdate extendedDataUpdate = new UserSessionExtendedDataUpdate((long) player.EXIP.IP, (long) player.EXIP.PORT, (long) player.INIP.IP, (long) player.INIP.PORT, player.UATT, player.PlayerID, player.GameID, false);
          extendedDataUpdate.make();
          byte[] buffer2 = extendedDataUpdate.ByteArray();
          memoryStream.Write(buffer2, 0, buffer2.Length);
          numArray = memoryStream.ToArray();
          break;
        case 35:
          SendError sendError1 = new SendError(p, 16384 /*0x4000*/);
          sendError1.make();
          numArray = sendError1.ByteArray();
          flag = true;
          break;
        default:
          SendError sendError2 = new SendError(p, 16384 /*0x4000*/);
          sendError2.make();
          numArray = sendError2.ByteArray();
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
