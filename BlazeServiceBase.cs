// Decompiled with JetBrains decompiler
// Type: BlazeServer.BlazeServiceBase
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.ServiceProcess;
using System.Threading;

#nullable disable
namespace BlazeServer;

public class BlazeServiceBase : ServiceBase
{
  protected Thread m_thread;
  protected ManualResetEvent m_shutdownEvent;
  protected TimeSpan m_delay;

  public BlazeServiceBase() => this.m_delay = new TimeSpan(0, 0, 0, 10, 0);

  protected override void OnStart(string[] args)
  {
    ThreadStart start = new ThreadStart(this.ServiceMain);
    this.m_shutdownEvent = new ManualResetEvent(false);
    this.m_thread = new Thread(start);
    this.m_thread.Start();
    base.OnStart(args);
  }

  protected override void OnStop()
  {
    this.m_shutdownEvent.Set();
    this.m_thread.Join(10000);
    base.OnStop();
  }

  protected void ServiceMain()
  {
    while (true)
    {
      if (!this.m_shutdownEvent.WaitOne(this.m_delay, true))
        this.Execute();
      else
        break;
    }
  }

  protected virtual int Execute() => -1;
}
