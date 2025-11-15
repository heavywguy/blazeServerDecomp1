// Decompiled with JetBrains decompiler
// Type: BlazeServer.CHandleComponent_1C
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;

#nullable disable
namespace BlazeServer;

internal class CHandleComponent_1C
{
  public CHandleComponent_1C(GameClient player, Blaze.Packet p, out byte[] buf)
  {
    try
    {
      byte[] numArray;
      if (p.Command == (ushort) 2)
      {
        SendEmpty sendEmpty = new SendEmpty(p);
        sendEmpty.make();
        numArray = sendEmpty.ByteArray();
      }
      else
      {
        SendError sendError = new SendError(p, 16384 /*0x4000*/);
        sendError.make();
        numArray = sendError.ByteArray();
      }
      buf = numArray;
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}
