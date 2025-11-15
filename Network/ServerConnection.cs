// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.ServerConnection
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

#nullable disable
namespace BlazeServer.Network;

public abstract class ServerConnection
{
  protected int packet_length;
  protected int left_to_receive;
  protected int left_to_receive_h;
  protected byte[] rBuffer;
  protected int rcvdBytes;
  protected byte[] sndBuffer;
  protected int sndBytes;
  protected int leftSndBytes;
  protected int sleepCount;
  protected bool termination;
  protected Socket wSocket;
  protected GameManager Server;
  private static ManualResetEvent sendDone = new ManualResetEvent(false);
  private static ManualResetEvent receiveDone = new ManualResetEvent(false);
  private static ManualResetEvent closeDone = new ManualResetEvent(false);

  public ServerConnection(Socket s) => this.wSocket = s;

  public void SetClient(Socket socket, GameManager server)
  {
    this.wSocket = socket;
    this.Server = server;
  }

  public string GetRemoteIP() => (this.wSocket.RemoteEndPoint as IPEndPoint).Address.ToString();

  public string GetLocalIP()
  {
    return ((IPEndPoint) new UdpClient((this.wSocket.RemoteEndPoint as IPEndPoint).Address.ToString(), Config.ClientThreadPort).Client.LocalEndPoint).Address.ToString();
  }

  public void Close()
  {
    try
    {
      this.OnDisconnect();
      this.wSocket.Shutdown(SocketShutdown.Both);
      this.wSocket.Close();
      this.wSocket.Dispose();
      ServerConnection.closeDone.Set();
    }
    catch
    {
    }
  }

  public virtual void OnConnect()
  {
    Logger.Log($"ID'{(object) this.Server.ID}'connection to {this.GetLocalIP()} from {this.GetRemoteIP()} '{(object) (uint) Helper.GetIPfromString(this.GetRemoteIP())}'");
    this.BeginRead();
  }

  private void BeginRead()
  {
    if (this.wSocket == null)
      return;
    try
    {
      Logger.BufferPacketLog(nameof (BeginRead), this.Server.ID, 2);
      this.rcvdBytes = 0;
      this.sleepCount = 0;
      this.rBuffer = new byte[14];
      ServerConnection.receiveDone.Reset();
      this.wSocket.BeginReceive(this.rBuffer, 0, 14, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) this.rBuffer);
      ServerConnection.receiveDone.WaitOne();
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
      {
        Logger.Error("BeginRead error: ", ex);
        Logger.BufferPacketLog("BeginRead error:" + ex.Message, this.Server.ID, 2);
        this.RemoveServer(this.Server);
      }
      else
      {
        Logger.Error("BeginRead error: ", ex);
        Logger.BufferPacketLog("BeginRead error:" + ex.Message, this.Server.ID, 2);
        this.RemoveServer(this.Server);
      }
    }
  }

  private void ReceiveHeader(IAsyncResult result)
  {
    try
    {
      if (!ServerConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveServer(this.Server);
      }
      else
      {
        int num1 = this.wSocket.EndReceive(result);
        ServerConnection.receiveDone.Set();
        Logger.BufferPacketLog("ReceiveHeader, EndReceive, recievedBytes = " + num1.ToString(), this.Server.ID, 2);
        this.rcvdBytes += num1;
        if (this.rcvdBytes == 14)
        {
          MemoryStream memoryStream1 = new MemoryStream();
          memoryStream1.Write(this.rBuffer, 0, 2);
          int num2 = (int) (ushort) (((uint) memoryStream1.ToArray()[0] << 8) + (uint) memoryStream1.ToArray()[1]);
          int num3 = 14;
          MemoryStream memoryStream2 = new MemoryStream();
          memoryStream2.Write(this.rBuffer, 12, 2);
          int num4 = (int) (ushort) (((uint) memoryStream2.ToArray()[0] << 8) + (uint) memoryStream2.ToArray()[1]) << 16 /*0x10*/;
          this.packet_length = num3 + num2 + num4;
          if (this.rcvdBytes == this.packet_length)
          {
            byte[] buf = new byte[0];
            bool termination = false;
            bool timeout_restart = false;
            PacketHandlerS packetHandlerS = new PacketHandlerS(this.Server, this.rBuffer, out buf, out termination, out timeout_restart);
            this.termination = termination;
            long elapsedMilliseconds = this.Server.Ptimer.ElapsedMilliseconds;
            this.Server.Ptimer.Restart();
            if (buf != null)
            {
              Array.Resize<byte>(ref this.sndBuffer, buf.Length);
              this.sndBuffer = buf;
              this.sndBytes = 0;
              ServerConnection.sendDone.Reset();
              this.wSocket.BeginSend(this.sndBuffer, 0, this.sndBuffer.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
              ServerConnection.sendDone.WaitOne();
            }
            else
            {
              Logger.BufferPacketLog("Response packet was null", this.Server.ID, 2);
              if (!this.termination)
                this.BeginRead();
            }
          }
          else if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {(this.packet_length - 14).ToString()} => EndRead", this.Server.ID, 2);
            Array.Resize<byte>(ref this.rBuffer, this.packet_length);
            ServerConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, 14, this.packet_length - 14, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
            ServerConnection.receiveDone.WaitOne();
          }
          else if (!ServerConnection.IsSocketConnected(this.wSocket))
          {
            this.RemoveServer(this.Server);
          }
          else
          {
            this.sleepCount = 0;
            while (this.wSocket.Available == 0 && this.sleepCount < 200)
            {
              Thread.Sleep(20);
              ++this.sleepCount;
              Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Server.ID, 2);
            }
            if (!ServerConnection.IsSocketConnected(this.wSocket))
              this.RemoveServer(this.Server);
            else if (this.wSocket.Available > 0)
            {
              Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {(this.packet_length - 14).ToString()} => EndRead", this.Server.ID, 2);
              Array.Resize<byte>(ref this.rBuffer, this.packet_length);
              ServerConnection.receiveDone.Reset();
              this.wSocket.BeginReceive(this.rBuffer, 14, this.packet_length - 14, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
              ServerConnection.receiveDone.WaitOne();
            }
            else
              this.RemoveServer(this.Server);
          }
        }
        else if (this.wSocket.Available > 0)
        {
          Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead => ReceiveHeader", this.Server.ID, 2);
          ServerConnection.receiveDone.Reset();
          this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, 10 - this.rcvdBytes, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) null);
          ServerConnection.receiveDone.WaitOne();
        }
        else if (!ServerConnection.IsSocketConnected(this.wSocket))
        {
          this.RemoveServer(this.Server);
        }
        else
        {
          this.sleepCount = 0;
          while (this.wSocket.Available == 0 && this.sleepCount < 200)
          {
            Thread.Sleep(20);
            ++this.sleepCount;
            Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Server.ID, 2);
          }
          if (!ServerConnection.IsSocketConnected(this.wSocket))
            this.RemoveServer(this.Server);
          else if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead => ReceiveHeader", this.Server.ID, 2);
            ServerConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, 10 - this.rcvdBytes, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) null);
            ServerConnection.receiveDone.WaitOne();
          }
          else
            this.RemoveServer(this.Server);
        }
      }
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
        this.RemoveServer(this.Server);
      else
        Logger.Error("ReceiveHeader error: ", ex);
    }
  }

  private void EndRead(IAsyncResult result)
  {
    try
    {
      if (!ServerConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveServer(this.Server);
      }
      else
      {
        int num = this.wSocket.EndReceive(result);
        ServerConnection.receiveDone.Set();
        Logger.BufferPacketLog("EndRead, recievedBytes = " + num.ToString(), this.Server.ID, 2);
        this.rcvdBytes += num;
        if (this.rcvdBytes == this.packet_length)
        {
          byte[] buf = new byte[0];
          bool termination = false;
          bool timeout_restart = false;
          PacketHandlerS packetHandlerS = new PacketHandlerS(this.Server, this.rBuffer, out buf, out termination, out timeout_restart);
          this.termination = termination;
          if (buf != null)
          {
            Array.Resize<byte>(ref this.sndBuffer, buf.Length);
            this.sndBuffer = buf;
            this.sndBytes = 0;
            ServerConnection.sendDone.Reset();
            this.wSocket.BeginSend(this.sndBuffer, 0, this.sndBuffer.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
            ServerConnection.sendDone.WaitOne();
          }
          else
          {
            Logger.BufferPacketLog("Response packet was null", this.Server.ID, 2);
            if (!this.termination)
              this.BeginRead();
          }
        }
        else
        {
          this.left_to_receive = this.packet_length - this.rcvdBytes;
          Logger.BufferPacketLog($"packet_length = {this.packet_length.ToString()}, rcvdBytes = {this.rcvdBytes.ToString()}, left_to_receive = {this.left_to_receive.ToString()}", this.Server.ID, 2);
          if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {this.left_to_receive.ToString()} => EndRead", this.Server.ID, 2);
            ServerConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, this.left_to_receive, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
            ServerConnection.receiveDone.WaitOne();
          }
          else if (!ServerConnection.IsSocketConnected(this.wSocket))
          {
            this.RemoveServer(this.Server);
          }
          else
          {
            this.sleepCount = 0;
            while (this.wSocket.Available == 0 && this.sleepCount < 200)
            {
              Thread.Sleep(20);
              ++this.sleepCount;
              Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Server.ID, 2);
            }
            if (!ServerConnection.IsSocketConnected(this.wSocket))
              this.RemoveServer(this.Server);
            else if (this.wSocket.Available > 0)
            {
              Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {this.left_to_receive.ToString()} => EndRead", this.Server.ID, 2);
              ServerConnection.receiveDone.Reset();
              this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, this.left_to_receive, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
              ServerConnection.receiveDone.WaitOne();
            }
            else
              this.RemoveServer(this.Server);
          }
        }
      }
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
      {
        Logger.Error("EndRead error: ", ex);
        this.RemoveServer(this.Server);
      }
      else
      {
        Logger.Error("EndRead error: ", ex);
        this.RemoveServer(this.Server);
      }
    }
  }

  private void SendCallback(IAsyncResult result)
  {
    try
    {
      if (!ServerConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveServer(this.Server);
      }
      else
      {
        this.sndBytes += this.wSocket.EndSend(result);
        if (this.sndBytes == this.sndBuffer.Length)
        {
          Logger.BufferPacketLog($"SendDone, sndBytes = {(object) this.sndBytes}, sndBuffer.Length = {(object) this.sndBuffer.Length}", this.Server.ID, 2);
          ServerConnection.sendDone.Set();
        }
        else
        {
          this.leftSndBytes = this.sndBuffer.Length - this.sndBytes;
          Logger.BufferPacketLog("BeginSend leftToSendBytes = " + this.leftSndBytes.ToString(), this.Server.ID, 2);
          ServerConnection.sendDone.Reset();
          this.wSocket.BeginSend(this.sndBuffer, this.sndBytes, this.leftSndBytes, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
          ServerConnection.sendDone.WaitOne();
        }
        if (this.termination && this.sndBytes == this.sndBuffer.Length)
        {
          Logger.BufferPacketLog("Termination", this.Server.ID, 2);
          this.RemoveServer(this.Server);
        }
        else if (!this.termination)
          this.BeginRead();
      }
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
        this.RemoveServer(this.Server);
      else
        Logger.Error("SendCallback error: ", ex);
    }
  }

  private void RemoveServer(GameManager game)
  {
    try
    {
      Thread.Sleep(200);
      if (game == null || game.isTerminated)
        return;
      ServerConnection.closeDone.Reset();
      this.Close();
      ServerConnection.closeDone.WaitOne();
      game.isActive = false;
      game.isTerminated = true;
      game.Update = true;
      Logger.Log($"ID'{(object) game.ID}'{game.NAME} isActive '{(object) game.isActive}'");
      this.deleteServer(game);
    }
    catch (Exception ex)
    {
      Logger.Error("RemoveServer error: ", ex);
      this.deleteServer(game);
    }
  }

  private void deleteServer(GameManager game)
  {
    try
    {
      Logger.BufferPacketLog("!Delete Server", game.ID, 2);
      if (game == null)
        return;
      Logger.Log($"ID'{(object) game.ID}' GameID '{(object) game.GID} '{game.GNAME}' removing");
      if (GameManager.AllServers.ContainsKey(game.GID))
      {
        if (GameManager.AllServers.TryRemove(game.GID, out GameManager _))
        {
          Logger.Log($"ID'{(object) game.ID}' GameID '{(object) game.GID} '{game.GNAME}' removing done");
          Logger.BufferPacketLog("!server deleted (c)", game.ID, 2);
        }
        else
          Logger.Log($"ID'{(object) game.ID}' GameID '{(object) game.GID} '{game.GNAME}' remove false");
      }
      new SQL_RUN().DeleteServer(game.GID);
    }
    catch (Exception ex)
    {
      Logger.Error("DeleteServer Error: ", ex);
      this.deleteServer(game);
    }
  }

  private static bool IsSocketConnected(Socket s)
  {
    bool flag;
    try
    {
      flag = (!s.Poll(1000, SelectMode.SelectRead) || s.Available != 0) && s.Connected;
    }
    catch
    {
      flag = false;
    }
    return flag;
  }

  public virtual void OnDisconnect()
  {
  }
}
