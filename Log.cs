// Decompiled with JetBrains decompiler
// Type: BlazeServer.Log
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

public class Log
{
  private static StreamWriter writer;
  private static string loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
  private static string mainlogpath = $"{Log.loc}logs\\BlazeServerLog_{$"{DateTime.Now:yyyy/MM/dd_HH/mm/ss/fff}"}.log";
  private static object console_mutex = new object();

  public static void Initialize()
  {
    try
    {
      Log.writer = new StreamWriter(Log.mainlogpath, true);
    }
    catch (Exception ex)
    {
      Console.WriteLine("Cannot initialize the Log!");
      Console.WriteLine(ex.Message);
      Console.WriteLine(ex.StackTrace);
    }
  }

  private static void AppendLog(string message)
  {
    if (Log.writer == null)
      return;
    lock (Log.writer)
    {
      Log.writer.WriteLine(message);
      Log.writer.Flush();
    }
  }

  public static void Info(string message)
  {
    Log.AppendLog($"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {message}\n");
    lock (Log.console_mutex)
      Console.WriteLine($"{string.Format("[{0:HH:mm:ss}]", (object) DateTime.Now)} {message}");
  }

  public static void Error(string message, Exception ex)
  {
    Log.AppendLog($"[{string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}", (object) DateTime.Now)}] {message}{ex.ToString()}\n");
    lock (Log.console_mutex)
      Console.WriteLine($"{string.Format("[{0:HH:mm:ss}]", (object) DateTime.Now)} {message}{ex.Message}");
  }
}
