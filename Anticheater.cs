// Decompiled with JetBrains decompiler
// Type: BlazeServer.Anticheater
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Timers;

#nullable disable
namespace BlazeServer;

internal class Anticheater
{
  private static Anticheater check;
  private Timer timer = new Timer(300000.0);

  public static Anticheater getInstance()
  {
    if (Anticheater.check == null)
      Anticheater.check = new Anticheater();
    return Anticheater.check;
  }

  public Anticheater()
  {
    try
    {
      this.timer.Elapsed += new ElapsedEventHandler(Anticheater.OnTimedEvent);
      this.timer.Enabled = true;
    }
    catch (Exception ex)
    {
      Logger.Error("Anticheater config checking error :", ex);
    }
  }

  private static void OnTimedEvent(object source, ElapsedEventArgs e)
  {
    Config.bannned_cfg_reload();
  }
}
