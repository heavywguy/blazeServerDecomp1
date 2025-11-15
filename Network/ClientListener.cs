// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.ClientListener
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace BlazeServer.Network;

public class ClientListener
{
  private static cListener Listener;
  private static ManualResetEvent allDone = new ManualResetEvent(false);
  private static IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, Config.ClientThreadPort);
  private static ClientConnection Connection;

  public static void Start()
  {
    try
    {
      ClientListener.Listener = new cListener();
      ClientListener.Listener.Bind((EndPoint) ClientListener.localEndPoint);
      ClientListener.Listener.Listen(1000);
      Logger.Log("BlazeServer Started listening on " + Helper.EndpointToString((EndPoint) ClientListener.localEndPoint));
      ClientListener.BeginAccept();
    }
    catch (Exception ex)
    {
      Logger.Error("Start ClientListener error: ", ex);
    }
  }

  private static void BeginAccept()
  {
    try
    {
      ClientListener.Connection = (ClientConnection) new MasterClientConnection((Socket) null);
      ClientListener.allDone.Reset();
      ClientListener.Listener.BeginAccept(new AsyncCallback(ClientListener.EndAccept), (object) ClientListener.Listener);
      ClientListener.allDone.WaitOne();
    }
    catch (Exception ex)
    {
      Logger.Error("BeginAccept error: ", ex);
      ClientListener.BeginAccept();
    }
  }

  private static void EndAccept(IAsyncResult ar)
  {
    try
    {
      ClientListener.allDone.Set();
      Socket socket = ((Socket) ar.AsyncState).EndAccept(ar);
      socket.NoDelay = true;
      socket.ReceiveBufferSize = 8192 /*0x2000*/;
      socket.ReceiveTimeout = Config.ReadTimeout >= 1000 ? Config.ReadTimeout : 1000;
      socket.SendBufferSize = 8192 /*0x2000*/;
      socket.SendTimeout = Config.ReadTimeout >= 1000 ? Config.WriteTimeout : 1000;
      GameClient client = new GameClient(Program.counter, socket);
      ClientListener.Connection.SetClient(socket, client);
      ClientListener.Connection.OnConnect();
      ++Program.counter;
      ClientListener.BeginAccept();
    }
    catch (Exception ex)
    {
      Logger.Error("EndAccept error: ", ex);
      ClientListener.BeginAccept();
    }
  }
}
