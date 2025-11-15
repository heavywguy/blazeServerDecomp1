// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_19
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_19
{
  public CHandleComponent_19(Blaze.Packet p, out byte[] buf, out bool termination)
  {
    try
    {
      byte[] buf1 = (byte[]) null;
      bool flag = false;
      switch (p.Command)
      {
        case 4:
          this.SendEmpty(p, (ushort) 4096 /*0x1000*/, out buf1);
          break;
        case 6:
          this.HandleComponent_19_6(p, out buf1);
          break;
        default:
          this.SendError(p, out buf1);
          flag = true;
          break;
      }
      buf = buf1;
      termination = flag;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  private void HandleComponent_19_6(Blaze.Packet p, out byte[] buf)
  {
    try
    {
      byte[] packet = Blaze.CreatePacket((ushort) 25, (ushort) 6, 0, (ushort) 4096 /*0x1000*/, p.ID, new List<Blaze.Tdf>()
      {
        (Blaze.Tdf) Blaze.TdfList.Create("LMAP", (byte) 3, 1, (object) new List<List<Blaze.Tdf>>()
        {
          new List<Blaze.Tdf>()
          {
            (Blaze.Tdf) Blaze.TdfStruct.Create("INFO", new List<Blaze.Tdf>()
            {
              (Blaze.Tdf) Blaze.TdfTrippleVal.Create("BOID", new Blaze.TrippleVal(25L, 1L, 42293235L)),
              (Blaze.Tdf) Blaze.TdfInteger.Create("FLGS", 4L),
              (Blaze.Tdf) Blaze.TdfStruct.Create("LID\0", new List<Blaze.Tdf>()
              {
                (Blaze.Tdf) Blaze.TdfString.Create("LNM\0", "friendList"),
                (Blaze.Tdf) Blaze.TdfInteger.Create("TYPE", 1L)
              }),
              (Blaze.Tdf) Blaze.TdfInteger.Create("LMS\0", 200L),
              (Blaze.Tdf) Blaze.TdfInteger.Create("PRID", 0L)
            }),
            (Blaze.Tdf) Blaze.TdfInteger.Create("OFRC", 0L),
            (Blaze.Tdf) Blaze.TdfInteger.Create("TOCT", 0L)
          }
        })
      });
      buf = packet;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public void SendEmpty(Blaze.Packet p, ushort Qtype, out byte[] buf)
  {
    List<Blaze.Tdf> Content = new List<Blaze.Tdf>();
    byte[] packet = Blaze.CreatePacket(p.Component, p.Command, 0, Qtype, p.ID, Content);
    buf = packet;
  }

  public void SendError(Blaze.Packet p, out byte[] buf)
  {
    List<Blaze.Tdf> Content = new List<Blaze.Tdf>();
    byte[] packet = Blaze.CreatePacket(p.Component, p.Command, 16384 /*0x4000*/, (ushort) 12288 /*0x3000*/, p.ID, Content);
    buf = packet;
  }
}
