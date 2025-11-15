// Decompiled with JetBrains decompiler
// Type: BlazeServer.SHandleComponent_1
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class SHandleComponent_1
{
  public SHandleComponent_1(
    GameManager player,
    Blaze.Packet p,
    out byte[] buf,
    out bool termination)
  {
    try
    {
      bool flag = false;
      byte[] numArray = (byte[]) null;
      switch (p.Command)
      {
        case 29:
          if (((Blaze.TdfString) Blaze.ReadPacketContent(p)[3]).Value == "ONLINE_ACCESS")
          {
            listUserEntitlements2 userEntitlements2 = new listUserEntitlements2(true, p.ID, "");
            userEntitlements2.make();
            numArray = userEntitlements2.ByteArray();
            break;
          }
          listUserEntitlements2 userEntitlements2_1 = new listUserEntitlements2(false, p.ID, "");
          userEntitlements2_1.make();
          numArray = userEntitlements2_1.ByteArray();
          break;
        case 36:
          getAuthToken getAuthToken = new getAuthToken(p.ID);
          getAuthToken.make();
          numArray = getAuthToken.ByteArray();
          break;
        case 40:
          List<Blaze.Tdf> tdfList1 = Blaze.ReadPacketContent(p);
          Blaze.TdfString mail1 = (Blaze.TdfString) tdfList1[1];
          Blaze.TdfString pass1 = (Blaze.TdfString) tdfList1[2];
          if (Helper.MaximumCountofServersReached())
          {
            Logger.Log("Error: Maximum count of servers is reached ");
            SendError sendError = new SendError(p, 16384 /*0x4000*/);
            sendError.make();
            numArray = sendError.ByteArray();
            player.isActive = false;
            player.Update = true;
            flag = true;
          }
          if (player.isActive)
          {
            new SQL_RUN().ServerAuthCheck(mail1, pass1, player);
            if (!player.isActive)
            {
              SendError sendError = new SendError(p, 16386);
              sendError.make();
              numArray = sendError.ByteArray();
              flag = true;
            }
          }
          if (player.isActive)
          {
            player.MAIL = mail1.Value;
            player.NAME = "bf3-server-pc";
            lock (GameManager.AllServers)
              player.GID = Helper.GetGID(player.ServerVersion);
            player.UserID = player.GID;
            player.Update = true;
            if (GameManager.AllServers.TryAdd(player.GID, player))
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GID}) Added");
            else
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GID}) Couldnt Add");
            login login = new login(player.NAME, player.GID, player.UserID, p.ID);
            login.make();
            numArray = login.ByteArray();
            break;
          }
          break;
        case 60:
          List<Blaze.Tdf> tdfList2 = Blaze.ReadPacketContent(p);
          Blaze.TdfString mail2 = (Blaze.TdfString) tdfList2[0];
          Blaze.TdfString pass2 = (Blaze.TdfString) tdfList2[1];
          if (Helper.MaximumCountofServersReached())
          {
            Logger.Log("Error: Maximum count of servers is reached ");
            SendError sendError = new SendError(p, 16384 /*0x4000*/);
            sendError.make();
            numArray = sendError.ByteArray();
            player.isActive = false;
            player.Update = true;
            flag = true;
          }
          if (player.isActive)
          {
            new SQL_RUN().ServerAuthCheck(mail2, pass2, player);
            if (!player.isActive)
            {
              SendError sendError = new SendError(p, 16384 /*0x4000*/);
              sendError.make();
              numArray = sendError.ByteArray();
              flag = true;
            }
          }
          if (player.isActive)
          {
            player.MAIL = mail2.Value;
            player.NAME = "bf4-server-pc";
            player.GID = Helper.GetGID(player.ServerVersion);
            player.UserID = player.GID;
            player.Update = true;
            if (GameManager.AllServers.TryAdd(player.GID, player))
              Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GID}) Added");
            MemoryStream memoryStream = new MemoryStream();
            ExpressLogin expressLogin = new ExpressLogin(player.GID, p.ID);
            expressLogin.make();
            byte[] buffer1 = expressLogin.ByteArray();
            memoryStream.Write(buffer1, 0, buffer1.Length);
            UserAuthenticated userAuthenticated = new UserAuthenticated(player.NAME, player.MAIL, player.GID, player.GID, 1701729619L);
            userAuthenticated.make();
            byte[] buffer2 = userAuthenticated.ByteArray();
            memoryStream.Write(buffer2, 0, buffer2.Length);
            UserAdded userAdded = new UserAdded(player.GID, player.NAME, 1701729619L, 0, 0L);
            userAdded.make();
            byte[] buffer3 = userAdded.ByteArray();
            memoryStream.Write(buffer3, 0, buffer3.Length);
            UpdateExtendedDataAttribute extendedDataAttribute = new UpdateExtendedDataAttribute(player.GID);
            extendedDataAttribute.make();
            byte[] buffer4 = extendedDataAttribute.ByteArray();
            memoryStream.Write(buffer4, 0, buffer4.Length);
            numArray = memoryStream.ToArray();
            break;
          }
          break;
        case 110:
          MemoryStream memoryStream1 = new MemoryStream();
          LoginPersona loginPersona = new LoginPersona(player.GID, player.MAIL, player.NAME, player.UserID, p.ID);
          loginPersona.make();
          byte[] buffer5 = loginPersona.ByteArray();
          memoryStream1.Write(buffer5, 0, buffer5.Length);
          UserAdded userAdded1 = new UserAdded(player.GID, player.NAME, 1701729619L, 0, 0L);
          userAdded1.make();
          byte[] buffer6 = userAdded1.ByteArray();
          memoryStream1.Write(buffer6, 0, buffer6.Length);
          UpdateExtendedDataAttribute extendedDataAttribute1 = new UpdateExtendedDataAttribute(player.GID);
          extendedDataAttribute1.make();
          byte[] buffer7 = extendedDataAttribute1.ByteArray();
          memoryStream1.Write(buffer7, 0, buffer7.Length);
          numArray = memoryStream1.ToArray();
          break;
        default:
          SendError sendError1 = new SendError(p, 16386);
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
