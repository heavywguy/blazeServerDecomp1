// Decompiled with JetBrains decompiler
// Type: BlazeServer.PacketHandlerS
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.IO;
using System.Runtime.Remoting.Contexts;

#nullable disable
namespace BlazeServer;

[Synchronization]
internal class PacketHandlerS
{
  public PacketHandlerS(
    GameManager player,
    byte[] buff,
    out byte[] buf,
    out bool termination,
    out bool timeout_restart)
  {
    bool timeout_restart1 = false;
    byte[] buf1 = (byte[]) null;
    bool termination1 = false;
    try
    {
      Blaze.Packet p = Blaze.ReadBlazePacket(new BinaryReader((Stream) new MemoryStream(buff)));
      ushort component = p.Component;
      ushort command = p.Command;
      string str1 = component.ToString("x8");
      string str2 = command.ToString("x8");
      Console.WriteLine("当前服务端请求响应为({0})0x{1}::({2})0x{3}", new object[4]
      {
        (object) component,
        (object) str1,
        (object) command,
        (object) str2
      });
      if (component <= (ushort) 20)
      {
        if (component == (ushort) 1)
        {
          SHandleComponent_1 shandleComponent1 = new SHandleComponent_1(player, p, out buf1, out termination1);
          goto label_25;
        }
        if (component == (ushort) 4)
        {
          SHandleComponent_4 shandleComponent4 = new SHandleComponent_4(player, p, out buf1, out termination1);
          goto label_25;
        }
        switch (component)
        {
          case 7:
            SHandleComponent_7 shandleComponent7 = new SHandleComponent_7(player, p, out buf1);
            goto label_25;
          case 9:
            SHandleComponent_9 shandleComponent9 = new SHandleComponent_9(player, p, out buf1, out timeout_restart1);
            goto label_25;
          case 11:
            if (p.Command == (ushort) 2700)
            {
              SendEmpty sendEmpty = new SendEmpty(p);
              sendEmpty.make();
              buf1 = sendEmpty.ByteArray();
              goto label_25;
            }
            goto label_25;
          case 20:
            SendEmpty sendEmpty1 = new SendEmpty(p);
            sendEmpty1.make();
            buf1 = sendEmpty1.ByteArray();
            goto label_25;
        }
      }
      else if (component <= (ushort) 28)
      {
        if (component == (ushort) 25)
        {
          SHandleComponent_19 shandleComponent19 = new SHandleComponent_19(p, out buf1);
          goto label_25;
        }
        if (component == (ushort) 28)
        {
          SHandleComponent_1C shandleComponent1C = new SHandleComponent_1C(player, p, out buf1);
          goto label_25;
        }
      }
      else
      {
        switch ((int) component - 2049)
        {
          case 0:
            if (p.Command == (ushort) 50)
            {
              SendError sendError = new SendError(p, 16384 /*0x4000*/);
              sendError.make();
              buf1 = sendError.ByteArray();
              goto label_25;
            }
            goto label_25;
          case 1:
            break;
          case 2:
            SHandleComponent_0803 shandleComponent0803 = new SHandleComponent_0803(p, out buf1);
            goto label_25;
          default:
            if (component == (ushort) 30722)
            {
              SHandleComponent_7802 shandleComponent7802 = new SHandleComponent_7802(player, p, out buf1, out termination1);
              goto label_25;
            }
            goto case 1;
        }
      }
      SendError sendError1 = new SendError(p, 16386);
      sendError1.make();
      buf1 = sendError1.ByteArray();
      termination1 = true;
label_25:
      buf = buf1;
      termination = termination1;
      timeout_restart = timeout_restart1;
    }
    catch (Exception ex)
    {
      Logger.Error("PacketHandlerS error: ", ex);
      buf = buf1;
      termination = termination1;
      timeout_restart = timeout_restart1;
    }
  }
}
