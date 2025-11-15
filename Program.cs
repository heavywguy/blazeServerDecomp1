// Decompiled with JetBrains decompiler
// Type: BlazeServer.Program
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network;
using MSCoreeTypeLib;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

internal class Program
{
  public static int counter;
  public static Thread tRedirector1;
  public static Thread tMainServer1;
  public static Thread tMainServer2;
  public static Thread CheckingDead;

  public static void threadRedirectorListener(object objs) => Redirector.getInstance();

  public static void threadMainServerListener1(object objs) => ServerListener.Start();

  public static void threadMainServerListener2(object objs) => ClientListener.Start();

  public static void threadCheckDead(object objs) => CheckDead.getInstance();

  private static void Main(string[] args)
  {
    ((ICorThreadpool) new ThreadManager()).SetMaxThreads(2000U, 2000U);
    for (int index = 0; index < args.Length; ++index)
    {
      string str = args[index];
      if (str == "-dumplog")
        Config.rundumplog();
      if (str == "-norwifi")
        Config.runnorwifi();
      if (str == "-1g")
      {
        Config.rundumplog();
        Config.run1g();
      }
    }
    Console.Title = "BlazeServer";
    Config.LoadInitialConfig();
    SQL_RUN sqlRun = new SQL_RUN();
    sqlRun.DeleteAllServers();
    sqlRun.UpdateAllPlayers();
    Program.tMainServer1 = new Thread(new ParameterizedThreadStart(Program.threadMainServerListener1));
    Program.tMainServer1.Start();
    Application.DoEvents();
    Program.tMainServer2 = new Thread(new ParameterizedThreadStart(Program.threadMainServerListener2));
    Program.tMainServer2.Start();
    Application.DoEvents();
    Program.CheckingDead = new Thread(new ParameterizedThreadStart(Program.threadCheckDead));
    Program.CheckingDead.IsBackground = true;
    Program.CheckingDead.Start();
    Application.DoEvents();
    Process.GetCurrentProcess().WaitForExit();
  }
}
