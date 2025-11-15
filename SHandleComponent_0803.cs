// Decompiled with JetBrains decompiler
// Type: BlazeServer.SHandleComponent_0803
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BlazeServer;

internal class SHandleComponent_0803
{
  public SHandleComponent_0803(Blaze.Packet p, out byte[] buf)
  {
    try
    {
      byte[] numArray = (byte[]) null;
      Console.WriteLine((int) p.Command);
      if (p.Command == (ushort) 6)
      {
        MemoryStream memoryStream = new MemoryStream();
        getTemplate getTemplate = new getTemplate(p.ID);
        getTemplate.make();
        numArray = getTemplate.ByteArray();
      }
      buf = numArray;
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
    byte[] packet = Blaze.CreatePacket(p.Component, p.Command, 16386, (ushort) 12288 /*0x3000*/, p.ID, Content);
    buf = packet;
  }
}
