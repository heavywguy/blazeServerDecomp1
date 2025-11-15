// Decompiled with JetBrains decompiler
// Type: BlazeServer.CheckConfig
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Timers;

#nullable disable
namespace BlazeServer;

internal class CheckConfig
{
  private static CheckConfig check;
  private Timer timer = new Timer(600000.0);

  public static CheckConfig getInstance()
  {
    if (CheckConfig.check == null)
      CheckConfig.check = new CheckConfig();
    return CheckConfig.check;
  }

  public CheckConfig()
  {
    try
    {
      this.timer.Elapsed += new ElapsedEventHandler(CheckConfig.OnTimedEvent);
      this.timer.Enabled = true;
    }
    catch (Exception ex)
    {
      Logger.Error("subnet config checking error :", ex);
    }
  }

  private static void OnTimedEvent(object source, ElapsedEventArgs e) => Config.subnet_cfg_reload();
}
