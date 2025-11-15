// Decompiled with JetBrains decompiler
// Type: BlazeServer.GameManager
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

#nullable disable
namespace BlazeServer;

public class GameManager
{
  public static ConcurrentDictionary<long, GameManager> AllServers = new ConcurrentDictionary<long, GameManager>();
  public List<bool> ServerSlots;
  public List<long> PlayerIDs;
  public List<GameManager.Attribut> Attributes;
  public List<string> serverattr;
  public List<string> serverattrval;
  public List<long> PCAP;
  public int ID;
  public string IP;
  public string localAddr;
  public string GNAME;
  public string MAIL;
  public string NAME;
  public long SEED;
  public long GSET;
  public long PMAX;
  public long QCAP;
  public long IGNO;
  public long NRES;
  public long NTOP;
  public long PRES;
  public long TCAP;
  public string PGID;
  public byte[] PGSC;
  public string UUID;
  public long GID;
  public long UserID;
  public GameManager.NETDATA EXIP;
  public GameManager.NETDATA INIP;
  public GameManager.NETDATA LOCALIP;
  public Socket WorkSocket;
  public bool isActive = true;
  public bool isTerminated;
  public bool localPlayer;
  public bool Update;
  public Stopwatch Ptimer;
  public string ServerVersion;
  public string ServerType;

  public GameManager(int id, Socket handler)
  {
    this.ID = id;
    this.WorkSocket = handler;
    this.IP = (handler.RemoteEndPoint as IPEndPoint).Address.ToString();
    this.EXIP.IP = (uint) Helper.GetIPfromString(this.IP);
    UdpClient udpClient = new UdpClient(this.IP, Config.ClientThreadPort);
    this.localAddr = ((IPEndPoint) udpClient.Client.LocalEndPoint).Address.ToString();
    this.LOCALIP.IP = (uint) Helper.GetIPfromString(((IPEndPoint) udpClient.Client.LocalEndPoint).Address.ToString());
    this.ServerSlots = new List<bool>()
    {
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false,
      false
    };
    this.PlayerIDs = new List<long>()
    {
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L,
      -1L
    };
    this.Attributes = new List<GameManager.Attribut>();
    this.Ptimer = new Stopwatch();
    this.Ptimer.Start();
  }

  public void UpdateAttributes(string[] names, string[] values)
  {
    for (int index = 0; index < names.Length; ++index)
    {
      int orCreate = this.FindOrCreate(names[index]);
      if (orCreate != -1)
      {
        GameManager.Attribut attribute = this.Attributes[orCreate] with
        {
          Value = values[index]
        };
        this.Attributes[orCreate] = attribute;
      }
      else
        this.Attributes.Add(new GameManager.Attribut()
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

  public struct Attribut
  {
    public string Name;
    public string Value;
  }

  public struct NETDATA
  {
    public uint IP;
    public uint PORT;
  }
}
