// Decompiled with JetBrains decompiler
// Type: BlazeServer.PacketHandlerC
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
internal class PacketHandlerC
{
  public PacketHandlerC(
    GameClient player,
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
      Console.WriteLine("当前客户端请求响应为({0})0x{1}::({2})0x{3}", new object[4]
      {
        (object) component,
        (object) str1,
        (object) command,
        (object) str2
      });
      if (component <= (ushort) 19)
      {
        if (component == (ushort) 1)
        {
          CHandleComponent_1 chandleComponent1 = new CHandleComponent_1(player, p, out buf1, out termination1);
          goto label_28;
        }
        if (component == (ushort) 4)
        {
          CHandleComponent_4 chandleComponent4 = new CHandleComponent_4(player, p, out buf1, out termination1);
          goto label_28;
        }
        switch (component)
        {
          case 7:
            CHandleComponent_7 chandleComponent7 = new CHandleComponent_7(player, p, out buf1);
            goto label_28;
          case 9:
            CHandleComponent_9 chandleComponent9 = new CHandleComponent_9(player, p, out buf1, out timeout_restart1, out termination1);
            goto label_28;
          case 11:
            if (p.Command == (ushort) 2700)
            {
              SendEmpty sendEmpty = new SendEmpty(p);
              sendEmpty.make();
              buf1 = sendEmpty.ByteArray();
              goto label_28;
            }
            goto label_28;
          case 19:
            SendEmpty sendEmpty1 = new SendEmpty(p);
            sendEmpty1.make();
            buf1 = sendEmpty1.ByteArray();
            goto label_28;
        }
      }
      else if (component <= (ushort) 35)
      {
        if (component == (ushort) 25)
        {
          SendEmpty sendEmpty = new SendEmpty(p);
          sendEmpty.make();
          buf1 = sendEmpty.ByteArray();
          goto label_28;
        }
        if (component == (ushort) 28)
        {
          CHandleComponent_1C chandleComponent1C = new CHandleComponent_1C(player, p, out buf1);
          goto label_28;
        }
        if (component == (ushort) 35)
        {
          CHandleComponent_0023 chandleComponent0023 = new CHandleComponent_0023(player, p, out buf1, out termination1);
          goto label_28;
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
              goto label_28;
            }
            goto label_28;
          case 1:
            break;
          case 2:
            if (p.Command == (ushort) 6)
            {
              SendEmpty sendEmpty = new SendEmpty(p);
              sendEmpty.make();
              buf1 = sendEmpty.ByteArray();
              goto label_28;
            }
            goto label_28;
          default:
            if (component == (ushort) 30722)
            {
              CHandleComponent_7802 chandleComponent7802 = new CHandleComponent_7802(player, p, out buf1, out termination1);
              goto label_28;
            }
            goto case 1;
        }
      }
      SendError sendError1 = new SendError(p, 16384 /*0x4000*/);
      sendError1.make();
      buf1 = sendError1.ByteArray();
      termination1 = true;
label_28:
      Logger.HexPacketLog($"[->] [{(object) p.ID}]{Blaze.PacketToDescriber(p)}, packet response len = {(object) buf1.Length}", player.ID, 2);
      buf = buf1;
      termination = termination1;
      timeout_restart = timeout_restart1;
    }
    catch (Exception ex)
    {
      Logger.Error("PacketHandlerC error :", ex);
      Console.WriteLine($"空间名：{ex.Source}；\n方法名：{ex.TargetSite?.ToString()}\n故障点：{ex.StackTrace.Substring(ex.StackTrace.LastIndexOf("\\") + 1, ex.StackTrace.Length - ex.StackTrace.LastIndexOf("\\") - 1)}\n错误提示：{ex.Message}");
      buf = buf1;
      termination = termination1;
      timeout_restart = timeout_restart1;
    }
  }
}
