// Decompiled with JetBrains decompiler
// Type: BlazeServer.SHandleComponent_1C
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace BlazeServer;

internal class SHandleComponent_1C
{
  public SHandleComponent_1C(GameManager player, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      ushort command = p.Command;
      byte[] numArray;
      if (command != (ushort) 2)
      {
        switch (command)
        {
          case 100:
            SendEmpty sendEmpty1 = new SendEmpty(p);
            sendEmpty1.make();
            numArray = sendEmpty1.ByteArray();
            this.HandleComponent_1C_64(player, p);
            break;
          case 101:
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
      }
      else
      {
        SendEmpty sendEmpty3 = new SendEmpty(p);
        sendEmpty3.make();
        numArray = sendEmpty3.ByteArray();
      }
      buf = numArray;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_1C_64(GameManager player, Blaze.Packet p)
  {
    try
    {
      Blaze.TdfStruct tdfStruct1 = (Blaze.TdfStruct) ((Blaze.TdfStruct) Blaze.ReadPacketContent(p)[2]).Values[1];
      List<AllPlayersStatValue> allstatvalue = new List<AllPlayersStatValue>();
      for (int index1 = 0; index1 < tdfStruct1.Values.Count; ++index1)
      {
        Blaze.Tdf tdf = tdfStruct1.Values[index1];
        string label;
        if ((label = tdf.Label) != null && label == "PLYR")
        {
          Blaze.TdfDoubleList tdfDoubleList1 = (Blaze.TdfDoubleList) tdf;
          List<long> list1_1 = (List<long>) tdfDoubleList1.List1;
          long count = (long) ((List<Blaze.TdfStruct>) tdfDoubleList1.List2).Count;
          if (count > 1L)
          {
            for (int index2 = 0; (long) index2 < count; ++index2)
            {
              Blaze.TdfStruct tdfStruct2 = ((List<Blaze.TdfStruct>) tdfDoubleList1.List2)[index2];
              if (tdfStruct2.Values.Count != 0)
              {
                Blaze.TdfDoubleList tdfDoubleList2 = (Blaze.TdfDoubleList) tdfStruct2.Values[0];
                List<string> list1_2 = (List<string>) tdfDoubleList2.List1;
                List<float> list2 = (List<float>) tdfDoubleList2.List2;
                for (int index3 = 0; index3 < list1_2.Count; ++index3)
                {
                  AllPlayersStatValue playersStatValue = new AllPlayersStatValue();
                  if (((IEnumerable<string>) Helper.statname).Contains<string>(list1_2[index3]))
                  {
                    playersStatValue.pid = list1_1[index2];
                    playersStatValue.statname = list1_2[index3];
                    playersStatValue.value = list2[index3];
                    if (playersStatValue != null)
                      allstatvalue.Add(playersStatValue);
                  }
                }
              }
            }
            new SQL_RUN().StatOnRoundEnd(allstatvalue, count, player.GID);
          }
        }
      }
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
