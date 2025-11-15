// Decompiled with JetBrains decompiler
// Type: BlazeServer.UpdateOnline
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Timers;

#nullable disable
namespace BlazeServer;

internal class UpdateOnline
{
  private static UpdateOnline check;
  private Timer timer = new Timer(30000.0);

  public static UpdateOnline getInstance()
  {
    if (UpdateOnline.check == null)
      UpdateOnline.check = new UpdateOnline();
    return UpdateOnline.check;
  }

  public UpdateOnline()
  {
    try
    {
      this.timer.Elapsed += new ElapsedEventHandler(UpdateOnline.OnTimedEvent);
      this.timer.Enabled = true;
    }
    catch (Exception ex)
    {
      Logger.Error("UpdateOnline error :", ex);
    }
  }

  private static void OnTimedEvent(object source, ElapsedEventArgs e)
  {
  }
}
