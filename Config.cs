// Decompiled with JetBrains decompiler
// Type: BlazeServer.Config
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

public static class Config
{
  private static readonly object _sub = new object();
  private static string loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
  private static readonly object _sync = new object();
  public static List<string> Entries;
  public static List<string> S_entries;
  public static List<Config.subnet> subnets;
  public static List<Config.banned> bannedid;
  public static string Subnets;
  public static int NAT_Type;
  public static int ReadTimeout;
  public static int WriteTimeout;
  public static string RediHost;
  public static int ServerThreadPort;
  public static int ClientThreadPort;
  public static int UseClientMetrics;
  public static int UseLocalPlayerAlgorithm;
  public static string WanIpForLocalServer;
  public static string ServerDataBase;
  public static string DataBaseName;
  public static string DataBaseUID;
  public static string DataBasePASS;
  public static uint dbConnectionPort;
  public static bool dumplogs = false;
  public static bool norwifi = false;
  public static bool loadbanconfig = false;
  public static int serverlimit = 4;
  public static Thread CheckingConfig;
  public static Thread CheckingBanned;

  public static void threadCheckConfig(object objs) => CheckConfig.getInstance();

  public static void threadCheckBanned(object objs) => Anticheater.getInstance();

  public static void LoadInitialConfig()
  {
    try
    {
      if (File.Exists(Config.loc + "conf\\conf.txt"))
      {
        Config.Entries = new List<string>((IEnumerable<string>) File.ReadAllLines(Config.loc + "conf\\conf.txt"));
        Logger.LogLevel = Convert.ToInt32(Config.FindEntry("LogLevel"));
        Config.NAT_Type = Convert.ToInt32(Config.FindEntry("NATType"));
        Logger.Log("NAT Type = " + Config.NAT_Type.ToString(), 2);
        Config.ReadTimeout = Convert.ToInt32(Config.FindEntry("ReadTimeout"));
        Logger.Log($"Read time out limit = {Config.ReadTimeout.ToString()}ms", 2);
        Config.WriteTimeout = Convert.ToInt32(Config.FindEntry("WriteTimeout"));
        Logger.Log($"Write time out limit = {Config.WriteTimeout.ToString()}ms");
        Config.RediHost = Config.FindEntry("Redihost");
        Logger.Log("Redirect to host = " + Config.RediHost);
        Config.WanIpForLocalServer = Config.FindEntry("WanIpForLocalServer");
        Logger.Log("Wan Ip For Local Server = " + Config.WanIpForLocalServer);
        Config.ServerThreadPort = Convert.ToInt32(Config.FindEntry("ServerListeningPort"));
        Logger.Log("ServerListeningPort = " + Config.ServerThreadPort.ToString());
        Config.ClientThreadPort = Convert.ToInt32(Config.FindEntry("ClientListeningPort"));
        Logger.Log("ClientListeningPort = " + Config.ClientThreadPort.ToString());
        Config.UseClientMetrics = Convert.ToInt32(Config.FindEntry("UseClientMetrics"));
        Logger.Log("UseClientMetrics = " + Config.UseClientMetrics.ToString());
        Config.UseLocalPlayerAlgorithm = Convert.ToInt32(Config.FindEntry("UseLocalPlayerAlgorithm"));
        Logger.Log("UseLocalPlayerAlgorithm = " + Config.UseLocalPlayerAlgorithm.ToString());
        Config.ServerDataBase = Config.FindEntry("dbhost");
        Logger.Log("dbhost = " + Config.ServerDataBase);
        Config.DataBaseName = Config.FindEntry("dbname");
        Logger.Log("dbname = " + Config.DataBaseName);
        Config.DataBaseUID = Config.FindEntry("dbuser");
        Config.DataBasePASS = Config.FindEntry("dbpass");
        Config.dbConnectionPort = Convert.ToUInt32(Config.FindEntry("dbConnectionPort"));
        Logger.Log("dbConnectionPort = " + Config.dbConnectionPort.ToString());
      }
      else
        Logger.Log($"[BlazeServer]{Config.loc} conf\\conf.txt loading failed");
    }
    catch (Exception ex)
    {
      Logger.Error("LoadInitialConfig error: ", ex);
    }
  }

  public static void rundumplog() => Config.dumplogs = true;

  public static void runnorwifi()
  {
    Config.subnet_cfg_load();
    Config.CheckingConfig = new Thread(new ParameterizedThreadStart(Config.threadCheckConfig));
    Config.CheckingConfig.IsBackground = true;
    Config.CheckingConfig.Start();
    Application.DoEvents();
    Config.norwifi = true;
  }

  public static void run1g()
  {
    Config.banned_config_load();
    Config.CheckingBanned = new Thread(new ParameterizedThreadStart(Config.threadCheckBanned));
    Config.CheckingBanned.IsBackground = true;
    Config.CheckingBanned.Start();
    Application.DoEvents();
    Config.loadbanconfig = true;
  }

  public static void banned_config_load()
  {
    try
    {
      if (!File.Exists(Config.loc + "conf\\banned.txt"))
        return;
      Config.bannedid = new List<Config.banned>();
      Config.S_entries = new List<string>((IEnumerable<string>) File.ReadAllLines(Config.loc + "conf\\banned.txt"));
      for (int index = 0; index < Config.S_entries.Count; ++index)
      {
        string entry = Config.S_entries[index];
        if (entry.Trim() != "")
        {
          string[] strArray = entry.Trim().Split(':');
          Console.WriteLine($"[BlazeServer] banned: {strArray[0]}:{strArray[1]}");
          Config.bannedid.Add(new Config.banned()
          {
            processorid = strArray[0],
            hddserial = strArray[1]
          });
        }
      }
      Console.WriteLine("[BlazeServer] banned.txt was loaded");
    }
    catch (Exception ex)
    {
      Logger.Error("banned config loading error: ", ex);
    }
  }

  public static void bannned_cfg_reload()
  {
    try
    {
      if (!File.Exists(Config.loc + "conf\\banned.txt"))
        return;
      lock (Config.bannedid)
      {
        Config.bannedid.Clear();
        Config.S_entries = new List<string>((IEnumerable<string>) File.ReadAllLines(Config.loc + "conf\\banned.txt"));
        for (int index = 0; index < Config.S_entries.Count; ++index)
        {
          string entry = Config.S_entries[index];
          if (entry.Trim() != "")
          {
            string[] strArray = entry.Trim().Split(':');
            Config.bannedid.Add(new Config.banned()
            {
              processorid = strArray[0],
              hddserial = strArray[1]
            });
          }
        }
        Console.WriteLine("[BlazeServer] banned.txt was reloaded!");
      }
    }
    catch (Exception ex)
    {
      Logger.Error("banned config reloading error: ", ex);
    }
  }

  public static void subnet_cfg_load()
  {
    try
    {
      if (!File.Exists(Config.loc + "conf\\subnet.txt"))
        return;
      Config.subnets = new List<Config.subnet>();
      Config.S_entries = new List<string>((IEnumerable<string>) File.ReadAllLines(Config.loc + "conf\\subnet.txt"));
      for (int index = 0; index < Config.S_entries.Count; ++index)
      {
        string entry = Config.S_entries[index];
        if (entry.Trim() != "")
        {
          string[] strArray = entry.Trim().Split(':');
          Console.WriteLine($"[BlazeServer] Subnets: {strArray[0]}:{strArray[1]}:{strArray[2]}");
          Config.subnets.Add(new Config.subnet()
          {
            mask = (uint) Helper.GetIPfromString(strArray[0]),
            map_ip = (uint) Helper.GetIPfromString(strArray[1]),
            char_ip = (uint) Helper.GetIPfromString(strArray[2])
          });
        }
      }
      Console.WriteLine("[BlazeServer] subnet.txt was loaded");
    }
    catch (Exception ex)
    {
      Logger.Error("subnet config loading error: ", ex);
    }
  }

  public static void subnet_cfg_reload()
  {
    try
    {
      if (!File.Exists(Config.loc + "conf\\subnet.txt"))
        return;
      lock (Config.subnets)
      {
        Config.subnets.Clear();
        Config.S_entries = new List<string>((IEnumerable<string>) File.ReadAllLines(Config.loc + "conf\\subnet.txt"));
        for (int index = 0; index < Config.S_entries.Count; ++index)
        {
          string entry = Config.S_entries[index];
          if (entry.Trim() != "")
          {
            string[] strArray = entry.Trim().Split(':');
            Console.WriteLine($"[BlazeServer] Subnets: {strArray[0]}:{strArray[1]}:{strArray[2]}");
            Config.subnets.Add(new Config.subnet()
            {
              mask = (uint) Helper.GetIPfromString(strArray[0]),
              map_ip = (uint) Helper.GetIPfromString(strArray[1]),
              char_ip = (uint) Helper.GetIPfromString(strArray[2])
            });
          }
        }
      }
      Console.WriteLine("[BlazeServer] subnet.txt was reloaded");
    }
    catch (Exception ex)
    {
      Logger.Error("subnet config reloading error: ", ex);
    }
  }

  public static uint GetWanIP(uint ip, List<Config.subnet> subnets)
  {
    lock (Config._sub)
    {
      foreach (Config.subnet subnet in subnets)
      {
        if (((int) subnet.map_ip & (int) subnet.mask) == ((int) ip & (int) subnet.mask))
          return subnet.char_ip;
      }
    }
    return 0;
  }

  public static string FindEntry(string name)
  {
    string entry1 = "";
    lock (Config._sync)
    {
      for (int index = 0; index < Config.Entries.Count; ++index)
      {
        string entry2 = Config.Entries[index];
        if (!entry2.Trim().StartsWith("#"))
        {
          string[] strArray = entry2.Split('=');
          if (strArray.Length == 2 && strArray[0].Trim().ToLower() == name.ToLower())
            return strArray[1].Trim();
        }
      }
    }
    return entry1;
  }

  public static string RemoveControlCharacters(string inString)
  {
    if (inString == null)
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < inString.Length; ++index)
    {
      char c = inString[index];
      if (!char.IsControl(c))
        stringBuilder.Append(c);
    }
    return stringBuilder.ToString();
  }

  public class subnet
  {
    public uint mask;
    public uint map_ip;
    public uint char_ip;
  }

  public class banned
  {
    public string processorid;
    public string hddserial;
  }
}
