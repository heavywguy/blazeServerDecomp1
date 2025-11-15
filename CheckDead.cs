// Decompiled with JetBrains decompiler
// Type: BlazeServer.CheckDead
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Timers;

#nullable disable
namespace BlazeServer;

internal class CheckDead
{
  private static CheckDead check;
  private Timer timer = new Timer(600000.0);

  public static CheckDead getInstance()
  {
    if (CheckDead.check == null)
      CheckDead.check = new CheckDead();
    return CheckDead.check;
  }

  public CheckDead()
  {
    try
    {
      this.timer.Elapsed += new ElapsedEventHandler(CheckDead.OnTimedEvent);
      this.timer.Enabled = true;
    }
    catch (Exception ex)
    {
      Logger.Error("CheckDead function error :", ex);
    }
  }

  private static void OnTimedEvent(object source, ElapsedEventArgs e) => Helper.RemoveDeadServers();
}
