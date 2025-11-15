// Decompiled with JetBrains decompiler
// Type: BlazeServer.BlazeServerService
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

public class BlazeServerService : BlazeServiceBase
{
  public static Thread tRedirector;
  public static Thread tMainServer1;
  public static Thread tMainServer2;

  public static void threadRedirector(object objs) => Redirector.getInstance();

  public static void threadMainServerListener1(object objs)
  {
  }

  public static void threadMainServerListener2(object objs)
  {
  }

  public BlazeServerService() => this.ServiceName = "BlazeServer";

  protected override void OnStart(string[] args)
  {
    base.OnStart(args);
    Config.LoadInitialConfig();
    Log.Initialize();
    SQL_RUN sqlRun = new SQL_RUN();
    sqlRun.DeleteAllServers();
    sqlRun.UpdateAllPlayers();
    BlazeServerService.tRedirector = new Thread(new ParameterizedThreadStart(BlazeServerService.threadRedirector));
    BlazeServerService.tRedirector.Start();
    Application.DoEvents();
    BlazeServerService.tMainServer1 = new Thread(new ParameterizedThreadStart(BlazeServerService.threadMainServerListener1));
    BlazeServerService.tMainServer1.Start();
    Application.DoEvents();
    BlazeServerService.tMainServer2 = new Thread(new ParameterizedThreadStart(BlazeServerService.threadMainServerListener2));
    BlazeServerService.tMainServer2.Start();
    Application.DoEvents();
  }

  protected override void OnStop() => base.OnStop();

  protected override int Execute()
  {
    EventLog.WriteEntry("BlazeServer", this.ServiceName + "::Execute()");
    return 0;
  }
}
