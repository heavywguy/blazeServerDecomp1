// Decompiled with JetBrains decompiler
// Type: BlazeServer.Logger
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

internal class Logger
{
  private static object _sync = new object();
  private static string PacketLogFile = "PacketLog_" + $"{DateTime.Now:yyyy/MM/dd_HH/mm/ss/fff}";
  private static string loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
  public static string mainlogpath = $"{Logger.loc}logs\\BlazeServerLog_{$"{DateTime.Now:yyyy/MM/dd_HH/mm/ss/fff}"}.log";
  public static string bufferlogpath = $"{Logger.loc}logs\\BufferLog_{$"{DateTime.Now:yyyy/MM/dd_HH/mm/ss/fff}"}";
  public static string hexlogpath = $"{Logger.loc}logs\\HexLog_{$"{DateTime.Now:yyyy/MM/dd_HH/mm/ss/fff}"}";
  public static int LogLevel = 0;

  public static void Log(string msg, int Level = 0)
  {
    lock (Logger._sync)
    {
      try
      {
        if (Level > Logger.LogLevel)
          return;
        Console.WriteLine($"{string.Format("[{0:HH:mm:ss}]", (object) DateTime.Now)} {msg}");
        string str = $"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {msg}\n";
        if (!File.Exists(Logger.mainlogpath))
          File.WriteAllBytes(Logger.mainlogpath, new byte[0]);
        File.AppendAllText(Logger.mainlogpath, str.Replace("\n", "\r\n"));
      }
      catch (Exception ex)
      {
      }
    }
  }

  public static void Error(string message, Exception ex)
  {
    lock (Logger._sync)
    {
      try
      {
        Console.WriteLine($"{string.Format("[{0:HH:mm:ss}]", (object) DateTime.Now)} {message}{ex.Message}");
        string str = $"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {message}{ex.ToString()}\n";
        if (!File.Exists(Logger.mainlogpath))
          File.WriteAllBytes(Logger.mainlogpath, new byte[0]);
        File.AppendAllText(Logger.mainlogpath, str.Replace("\n", "\r\n"));
      }
      catch (Exception ex1)
      {
      }
    }
  }

  public static void LogOnly(string msg, int Level = 0)
  {
    lock (Logger._sync)
    {
      try
      {
        if (Level > Logger.LogLevel)
          return;
        string str = $"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {msg}\n";
        if (!File.Exists(Logger.mainlogpath))
          File.WriteAllBytes(Logger.mainlogpath, new byte[0]);
        File.AppendAllText(Logger.mainlogpath, str.Replace("\n", "\r\n"));
      }
      catch (Exception ex)
      {
      }
    }
  }

  public static void HexPacketLog(string msg, int ID, int Level = 0)
  {
    try
    {
      if (Level > Logger.LogLevel)
        return;
      string str = $"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {msg}\n";
      Encoding.Unicode.GetBytes(str.Replace("\n", "\r\n"));
      if (!File.Exists($"{Logger.hexlogpath}_{(object) ID}.log"))
        File.WriteAllBytes($"{Logger.hexlogpath}_{(object) ID}.log", new byte[0]);
      File.AppendAllText($"{Logger.hexlogpath}_{(object) ID}.log", str.Replace("\n", "\r\n"));
    }
    catch (Exception ex)
    {
    }
  }

  public static void BufferPacketLog(string msg, int ID, int Level = 0)
  {
    try
    {
      if (Level > Logger.LogLevel)
        return;
      string str = $"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {msg}\n";
      Encoding.Unicode.GetBytes(str.Replace("\n", "\r\n"));
      if (!File.Exists($"{Logger.bufferlogpath}_{(object) ID}.log"))
        File.WriteAllBytes($"{Logger.bufferlogpath}_{(object) ID}.log", new byte[0]);
      File.AppendAllText($"{Logger.bufferlogpath}_{(object) ID}.log", str.Replace("\n", "\r\n"));
    }
    catch (Exception ex)
    {
    }
  }

  public static void ConsoleOnly(string msg, int Level = 0)
  {
    try
    {
      if (Level > Logger.LogLevel)
        return;
      Console.WriteLine($"{string.Format("[{0:HH:mm:ss}]", (object) DateTime.Now)} {msg}");
    }
    catch (Exception ex)
    {
    }
  }

  public static void DumpPacket(byte[] buff, int ID, int Level = 0)
  {
    try
    {
      if (Level > Logger.LogLevel)
        return;
      new Thread(new ParameterizedThreadStart(Logger.ThreadPacketDump))
      {
        IsBackground = true
      }.Start((object) new Logger.DumpStruct()
      {
        buff = buff,
        ID = ID
      });
    }
    catch (Exception ex)
    {
    }
  }

  public static void ThreadPacketDump(object objs)
  {
    try
    {
      Logger.DumpStruct dumpStruct = (Logger.DumpStruct) objs;
      byte[] buff = dumpStruct.buff;
      lock (Logger._sync)
      {
        FileStream fileStream = new FileStream($"{Logger.loc}logs\\{Logger.PacketLogFile}_{(object) dumpStruct.ID}.bin", FileMode.Append, FileAccess.Write);
        fileStream.Write(buff, 0, buff.Length);
        fileStream.Close();
      }
    }
    catch (Exception ex)
    {
    }
  }

  public struct DumpStruct
  {
    public byte[] buff;
    public int ID;
  }
}
