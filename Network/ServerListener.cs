// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.ServerListener
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace BlazeServer.Network;

public class ServerListener
{
  private static sListener Listener;
  private static ManualResetEvent allDone = new ManualResetEvent(false);
  private static IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, Config.ServerThreadPort);
  private static ServerConnection Connection;

  public static void Start()
  {
    try
    {
      ServerListener.Listener = new sListener();
      ServerListener.Listener.Bind((EndPoint) ServerListener.localEndPoint);
      ServerListener.Listener.Listen(1000);
      Logger.Log("BlazeServer Started listening on " + Helper.EndpointToString((EndPoint) ServerListener.localEndPoint));
      ServerListener.BeginAccept();
    }
    catch (Exception ex)
    {
      Logger.Error("Start ServerListener error: ", ex);
    }
  }

  private static void BeginAccept()
  {
    try
    {
      ServerListener.Connection = (ServerConnection) new MasterServerConnection((Socket) null);
      ServerListener.allDone.Reset();
      ServerListener.Listener.BeginAccept(new AsyncCallback(ServerListener.EndAccept), (object) ServerListener.Listener);
      ServerListener.allDone.WaitOne();
    }
    catch (Exception ex)
    {
      Logger.Error("BeginAccept error: ", ex);
      ServerListener.BeginAccept();
    }
  }

  private static void EndAccept(IAsyncResult ar)
  {
    try
    {
      ServerListener.allDone.Set();
      Socket socket = ((Socket) ar.AsyncState).EndAccept(ar);
      socket.NoDelay = true;
      socket.ReceiveBufferSize = 32768 /*0x8000*/;
      socket.ReceiveTimeout = Config.ReadTimeout >= 1000 ? Config.ReadTimeout : 1000;
      socket.SendBufferSize = 32768 /*0x8000*/;
      socket.SendTimeout = Config.ReadTimeout >= 1000 ? Config.WriteTimeout : 1000;
      GameManager server = new GameManager(Program.counter, socket);
      ServerListener.Connection.SetClient(socket, server);
      ServerListener.Connection.OnConnect();
      ++Program.counter;
      ServerListener.BeginAccept();
    }
    catch (Exception ex)
    {
      Logger.Error("EndAccept error: ", ex);
      ServerListener.BeginAccept();
    }
  }
}
