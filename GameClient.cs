// Decompiled with JetBrains decompiler
// Type: BlazeServer.GameClient
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace BlazeServer;

public class GameClient
{
  public static ConcurrentDictionary<long, GameClient> AllPlayers = new ConcurrentDictionary<long, GameClient>();
  public static ConcurrentDictionary<long, GameClient> AllServers = new ConcurrentDictionary<long, GameClient>();
  private static ManualResetEvent closeDone = new ManualResetEvent(false);
  public int ID;
  public long AID;
  public long SID;
  public long BUID;
  public long UserID;
  public long PlayerID;
  public long GameID;
  public string AUTH;
  public string NAME;
  public string GNAME;
  public string MAIL;
  public string PASSWORD;
  public string rank = "0";
  public long UATT = 11272997306368 /*0x0A40B3000000*/;
  public string IP;
  public string localAddr;
  public string CustUserSettings;
  public string SdtUserSettings;
  public string keyvalue;
  public string datavalue;
  public GameClient.NETDATA EXIP;
  public GameClient.NETDATA INIP;
  public GameClient.NETDATA LOCALIP;
  public Socket WorkSocket;
  public bool isActive = true;
  public bool isTerminated;
  public bool isRemoved;
  public bool localPlayer;
  public bool Update;
  public Stopwatch Ptimer;
  public List<string> serverattr;
  public List<string> serverattrval;
  public List<GameClient.Attribut> Attributes;
  public List<long> PCAP;
  public long QCAP;
  public long GSET;
  public long PMAX;
  public long IGNO;
  public long NRES;
  public long NTOP;
  public long PRES;
  public long TCAP;
  public string PGID;
  public byte[] PGSC;
  public string UUID;
  public long GID;
  public static long Slot;
  public string Version;

  public GameClient(int id, Socket handler)
  {
    this.ID = id;
    this.WorkSocket = handler;
    this.IP = (handler.RemoteEndPoint as IPEndPoint).Address.ToString();
    this.EXIP.IP = (uint) Helper.GetIPfromString(this.IP);
    UdpClient udpClient = new UdpClient(this.IP, Config.ClientThreadPort);
    this.localAddr = ((IPEndPoint) udpClient.Client.LocalEndPoint).Address.ToString();
    this.LOCALIP.IP = (uint) Helper.GetIPfromString(((IPEndPoint) udpClient.Client.LocalEndPoint).Address.ToString());
    this.Attributes = new List<GameClient.Attribut>();
    this.Ptimer = new Stopwatch();
    this.Ptimer.Start();
  }

  public void Close()
  {
    try
    {
      this.WorkSocket.Shutdown(SocketShutdown.Both);
      this.WorkSocket.Close();
      GameClient.closeDone.Set();
    }
    catch
    {
    }
  }

  public void UpdateAttributes(string[] names, string[] values)
  {
    for (int index = 0; index < names.Length; ++index)
    {
      int orCreate = this.FindOrCreate(names[index]);
      if (orCreate != -1)
      {
        GameClient.Attribut attribute = this.Attributes[orCreate] with
        {
          Value = values[index]
        };
        this.Attributes[orCreate] = attribute;
      }
      else
        this.Attributes.Add(new GameClient.Attribut()
        {
          Name = names[index],
          Value = values[index]
        });
    }
    this.Update = true;
  }

  public int FindOrCreate(string name)
  {
    for (int index = 0; index < this.Attributes.Count; ++index)
    {
      if (this.Attributes[index].Name == name)
        return index;
    }
    return -1;
  }

  public struct NETDATA
  {
    public uint IP;
    public uint PORT;
  }

  public struct Attribut
  {
    public string Name;
    public string Value;
  }
}
