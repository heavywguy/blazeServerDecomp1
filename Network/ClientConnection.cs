// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.ClientConnection
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

#nullable disable
namespace BlazeServer.Network;

public abstract class ClientConnection
{
  private static ManualResetEvent sendDone = new ManualResetEvent(false);
  private static ManualResetEvent receiveDone = new ManualResetEvent(false);
  private static ManualResetEvent closeDone = new ManualResetEvent(false);
  private System.Timers.Timer timer = new System.Timers.Timer(5000.0);
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
  protected GameClient Client;

  public ClientConnection(Socket socket) => this.wSocket = socket;

  public void SetClient(Socket socket, GameClient client)
  {
    this.wSocket = socket;
    this.Client = client;
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
      ClientConnection.closeDone.Set();
    }
    catch
    {
    }
  }

  public virtual void OnConnect()
  {
    Logger.Log($"ID'{(object) this.Client.ID}'connection to {this.GetLocalIP()} from {this.GetRemoteIP()} '{(object) (uint) Helper.GetIPfromString(this.GetRemoteIP())}'");
    this.BeginRead();
  }

  private void BeginRead()
  {
    if (this.wSocket == null)
      return;
    try
    {
      Logger.BufferPacketLog(nameof (BeginRead), this.Client.ID, 2);
      this.rcvdBytes = 0;
      this.sleepCount = 0;
      this.rBuffer = new byte[12];
      ClientConnection.receiveDone.Reset();
      this.wSocket.BeginReceive(this.rBuffer, 0, 12, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) null);
      ClientConnection.receiveDone.WaitOne();
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
      {
        Logger.Error("BeginRead error: ", ex);
        Logger.BufferPacketLog("BeginRead error:" + ex.Message, this.Client.ID, 2);
        this.RemoveClient(this.Client);
      }
      else
      {
        Logger.Error("BeginRead error: ", ex);
        Logger.BufferPacketLog("BeginRead error:" + ex.Message, this.Client.ID, 2);
        this.RemoveClient(this.Client);
      }
    }
  }

  private void ReceiveHeader(IAsyncResult result)
  {
    try
    {
      if (!ClientConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveClient(this.Client);
      }
      else
      {
        int num = this.wSocket.EndReceive(result);
        ClientConnection.receiveDone.Set();
        Logger.BufferPacketLog("ReceiveHeader, EndReceive, recievedBytes = " + num.ToString(), this.Client.ID, 2);
        this.rcvdBytes += num;
        if (this.rcvdBytes == 12)
        {
          MemoryStream memoryStream = new MemoryStream();
          memoryStream.Write(this.rBuffer, 0, 2);
          this.packet_length = (int) (ushort) (((uint) memoryStream.ToArray()[0] << 8) + (uint) memoryStream.ToArray()[1]) + 12;
          if (this.rcvdBytes == this.packet_length)
          {
            byte[] buf = new byte[0];
            bool termination = false;
            bool timeout_restart = false;
            PacketHandlerC packetHandlerC = new PacketHandlerC(this.Client, this.rBuffer, out buf, out termination, out timeout_restart);
            this.termination = termination;
            long elapsedMilliseconds = this.Client.Ptimer.ElapsedMilliseconds;
            this.Client.Ptimer.Restart();
            if (buf != null)
            {
              Array.Resize<byte>(ref this.sndBuffer, buf.Length);
              this.sndBuffer = buf;
              this.sndBytes = 0;
              ClientConnection.sendDone.Reset();
              this.wSocket.BeginSend(this.sndBuffer, 0, this.sndBuffer.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
              ClientConnection.sendDone.WaitOne();
            }
            else
            {
              Logger.BufferPacketLog("Response packet was null", this.Client.ID, 2);
              if (!this.termination)
                this.BeginRead();
            }
          }
          else if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {(this.packet_length - 12).ToString()} => EndRead", this.Client.ID, 2);
            Array.Resize<byte>(ref this.rBuffer, this.packet_length);
            ClientConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, 12, this.packet_length - 12, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
            ClientConnection.receiveDone.WaitOne();
          }
          else if (!ClientConnection.IsSocketConnected(this.wSocket))
          {
            ClientConnection.closeDone.Reset();
            this.Close();
            ClientConnection.closeDone.WaitOne();
            this.RemoveClient(this.Client);
          }
          else
          {
            this.sleepCount = 0;
            while (this.wSocket.Available == 0 && this.sleepCount < 200)
            {
              Thread.Sleep(20);
              ++this.sleepCount;
              Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Client.ID, 2);
            }
            if (!ClientConnection.IsSocketConnected(this.wSocket))
              this.RemoveClient(this.Client);
            else if (this.wSocket.Available > 0)
            {
              Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {(this.packet_length - 12).ToString()} => EndRead", this.Client.ID, 2);
              Array.Resize<byte>(ref this.rBuffer, this.packet_length);
              ClientConnection.receiveDone.Reset();
              this.wSocket.BeginReceive(this.rBuffer, 12, this.packet_length - 12, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
              ClientConnection.receiveDone.WaitOne();
            }
            else
              this.RemoveClient(this.Client);
          }
        }
        else if (this.wSocket.Available > 0)
        {
          Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead => ReceiveHeader", this.Client.ID, 2);
          ClientConnection.receiveDone.Reset();
          this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, 12 - this.rcvdBytes, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) null);
          ClientConnection.receiveDone.WaitOne();
        }
        else if (!ClientConnection.IsSocketConnected(this.wSocket))
        {
          this.RemoveClient(this.Client);
        }
        else
        {
          this.sleepCount = 0;
          while (this.wSocket.Available == 0 && this.sleepCount < 200)
          {
            Thread.Sleep(20);
            ++this.sleepCount;
            Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Client.ID, 2);
          }
          if (!ClientConnection.IsSocketConnected(this.wSocket))
            this.RemoveClient(this.Client);
          else if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead => ReceiveHeader", this.Client.ID, 2);
            ClientConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, 12 - this.rcvdBytes, SocketFlags.None, new AsyncCallback(this.ReceiveHeader), (object) null);
            ClientConnection.receiveDone.WaitOne();
          }
          else
            this.RemoveClient(this.Client);
        }
      }
    }
    catch (Exception ex)
    {
      Logger.Error("ReceiveHeader error: ", ex);
      this.RemoveClient(this.Client);
    }
  }

  private void EndRead(IAsyncResult result)
  {
    try
    {
      if (!ClientConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveClient(this.Client);
      }
      else
      {
        int num = this.wSocket.EndReceive(result);
        ClientConnection.receiveDone.Set();
        Logger.BufferPacketLog("EndRead, recievedBytes = " + num.ToString(), this.Client.ID, 2);
        this.rcvdBytes += num;
        if (this.rcvdBytes == this.packet_length)
        {
          byte[] buf = new byte[0];
          bool termination = false;
          bool timeout_restart = false;
          PacketHandlerC packetHandlerC = new PacketHandlerC(this.Client, this.rBuffer, out buf, out termination, out timeout_restart);
          this.termination = termination;
          if (buf != null)
          {
            Array.Resize<byte>(ref this.sndBuffer, buf.Length);
            this.sndBuffer = buf;
            this.sndBytes = 0;
            ClientConnection.sendDone.Reset();
            this.wSocket.BeginSend(this.sndBuffer, 0, this.sndBuffer.Length, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
            ClientConnection.sendDone.WaitOne();
          }
          else
          {
            Logger.BufferPacketLog("Response packet was null", this.Client.ID, 2);
            if (!this.termination)
              this.BeginRead();
          }
        }
        else
        {
          this.left_to_receive = this.packet_length - this.rcvdBytes;
          Logger.BufferPacketLog($"packet_length = {this.packet_length.ToString()}, rcvdBytes = {this.rcvdBytes.ToString()}, left_to_receive = {this.left_to_receive.ToString()}", this.Client.ID, 2);
          if (this.wSocket.Available > 0)
          {
            Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {this.left_to_receive.ToString()} => EndRead", this.Client.ID, 2);
            ClientConnection.receiveDone.Reset();
            this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, this.left_to_receive, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
            ClientConnection.receiveDone.WaitOne();
          }
          else if (!ClientConnection.IsSocketConnected(this.wSocket))
          {
            this.RemoveClient(this.Client);
          }
          else
          {
            this.sleepCount = 0;
            while (this.wSocket.Available == 0 && this.sleepCount < 200)
            {
              Thread.Sleep(20);
              ++this.sleepCount;
              Logger.BufferPacketLog("Sleep.Count = " + this.sleepCount.ToString(), this.Client.ID, 2);
            }
            if (!ClientConnection.IsSocketConnected(this.wSocket))
              this.RemoveClient(this.Client);
            else if (this.wSocket.Available > 0)
            {
              Logger.BufferPacketLog($"Socket.Available = {this.wSocket.Available.ToString()}, BeginRead {this.left_to_receive.ToString()} => EndRead", this.Client.ID, 2);
              ClientConnection.receiveDone.Reset();
              this.wSocket.BeginReceive(this.rBuffer, this.rcvdBytes, this.left_to_receive, SocketFlags.None, new AsyncCallback(this.EndRead), (object) null);
              ClientConnection.receiveDone.WaitOne();
            }
            else
              this.RemoveClient(this.Client);
          }
        }
      }
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
      {
        Logger.Error("EndReceive error: ", ex);
        this.RemoveClient(this.Client);
      }
      else
      {
        Logger.Error("EndReceive error: ", ex);
        this.RemoveClient(this.Client);
      }
    }
  }

  private void SendCallback(IAsyncResult result)
  {
    try
    {
      if (!ClientConnection.IsSocketConnected(this.wSocket))
      {
        this.RemoveClient(this.Client);
      }
      else
      {
        this.sndBytes += this.wSocket.EndSend(result);
        if (this.sndBytes == this.sndBuffer.Length)
        {
          Logger.BufferPacketLog($"SendDone, sndBytes = {(object) this.sndBytes}, sndBuffer.Length = {(object) this.sndBuffer.Length}", this.Client.ID, 2);
          ClientConnection.sendDone.Set();
        }
        else
        {
          this.leftSndBytes = this.sndBuffer.Length - this.sndBytes;
          Logger.BufferPacketLog("BeginSend, leftToSendBytes = " + this.leftSndBytes.ToString(), this.Client.ID, 2);
          ClientConnection.sendDone.Reset();
          this.wSocket.BeginSend(this.sndBuffer, this.sndBytes, this.leftSndBytes, SocketFlags.None, new AsyncCallback(this.SendCallback), (object) null);
          ClientConnection.sendDone.WaitOne();
        }
        if (this.termination && this.sndBytes == this.sndBuffer.Length)
        {
          Logger.BufferPacketLog("Termination", this.Client.ID, 2);
          this.RemoveClient(this.Client);
        }
        else if (!this.termination)
          this.BeginRead();
      }
    }
    catch (Exception ex)
    {
      if (ex is SocketException)
      {
        this.RemoveClient(this.Client);
      }
      else
      {
        Logger.Error("SendCallback error: ", ex);
        this.BeginRead();
      }
    }
  }

  private void RemoveClient(GameClient player)
  {
    try
    {
      this.timer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
      this.timer.Enabled = true;
    }
    catch (Exception ex)
    {
      Logger.Error("RemoveClient Error :", ex);
      this.deleteClient(player);
    }
  }

  private void OnTimedEvent(object source, ElapsedEventArgs e) => this.deleteClient(this.Client);

  private void deleteClient(GameClient player)
  {
    try
    {
      if (player != null && !player.isTerminated)
      {
        Logger.BufferPacketLog("!Dispose Client", player.ID, 2);
        ClientConnection.closeDone.Reset();
        this.Close();
        ClientConnection.closeDone.WaitOne();
        player.isActive = false;
        player.isTerminated = true;
        player.Update = true;
        Logger.Log($"ID'{(object) player.ID}'{player.NAME}'was slot '{(object) player.SID}' isActive '{(object) player.isActive}'");
      }
      this.timer.Enabled = false;
      if (player == null || player.isRemoved || !GameClient.AllPlayers.ContainsKey(player.PlayerID) || GameClient.AllPlayers[player.PlayerID].ID != player.ID)
        return;
      if (GameClient.AllPlayers.TryRemove(player.PlayerID, out GameClient _))
      {
        player.isRemoved = true;
        Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) slot '{(object) player.SID}' removed (c)");
        Logger.BufferPacketLog("!player deleted (c)", player.ID, 2);
      }
      else
        Logger.Log($"ID'{(object) player.ID}'{player.NAME} ({(object) player.GameID}) slot '{(object) player.SID}' remove false (c)");
      player.Update = true;
    }
    catch (Exception ex)
    {
      Logger.Error("[DeleteClient] error :", ex);
      this.deleteClient(player);
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
