// Decompiled with JetBrains decompiler
// Type: BlazeServer.SecureTcpServer
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

#nullable disable
namespace BlazeServer;

public class SecureTcpServer : IDisposable
{
  private X509Certificate serverCert;
  private RemoteCertificateValidationCallback certValidationCallback;
  private SecureConnectionResultsCallback connectionCallback;
  private AsyncCallback onAcceptConnection;
  private AsyncCallback onAuthenticateAsServer;
  private bool started;
  private int listenPort;
  private TcpListener listenerV4;
  private int disposed;
  private bool clientCertificateRequired;
  private bool checkCertifcateRevocation;
  private SslProtocols sslProtocols;

  public SecureTcpServer(
    int listenPort,
    X509Certificate serverCertificate,
    SecureConnectionResultsCallback callback)
    : this(listenPort, serverCertificate, callback, (RemoteCertificateValidationCallback) null)
  {
  }

  public SecureTcpServer(
    int listenPort,
    X509Certificate serverCertificate,
    SecureConnectionResultsCallback callback,
    RemoteCertificateValidationCallback certValidationCallback)
  {
    if (listenPort < 0 || listenPort > (int) ushort.MaxValue)
      throw new ArgumentOutOfRangeException(nameof (listenPort));
    if (serverCertificate == null)
      throw new ArgumentNullException(nameof (serverCertificate));
    if (callback == null)
      throw new ArgumentNullException(nameof (callback));
    this.onAcceptConnection = new AsyncCallback(this.OnAcceptConnection);
    this.onAuthenticateAsServer = new AsyncCallback(this.OnAuthenticateAsServer);
    this.serverCert = serverCertificate;
    this.certValidationCallback = certValidationCallback;
    this.connectionCallback = callback;
    this.listenPort = listenPort;
    this.disposed = 0;
    this.checkCertifcateRevocation = false;
    this.clientCertificateRequired = false;
    this.sslProtocols = SslProtocols.Default;
  }

  ~SecureTcpServer() => this.Dispose();

  public SslProtocols SslProtocols
  {
    get => this.sslProtocols;
    set => this.sslProtocols = value;
  }

  public bool CheckCertifcateRevocation
  {
    get => this.checkCertifcateRevocation;
    set => this.checkCertifcateRevocation = value;
  }

  public bool ClientCertificateRequired
  {
    get => this.clientCertificateRequired;
    set => this.clientCertificateRequired = value;
  }

  public void StartListening()
  {
    if (this.started)
      throw new InvalidOperationException("Already started...");
    if (Socket.OSSupportsIPv4 && this.listenerV4 == null)
    {
      IPEndPoint localEP = new IPEndPoint(IPAddress.Any, this.listenPort);
      Logger.Log("Redirector Started listening on " + localEP?.ToString());
      this.listenerV4 = new TcpListener(localEP);
    }
    if (this.listenerV4 != null)
    {
      this.listenerV4.Start();
      this.listenerV4.BeginAcceptTcpClient(this.onAcceptConnection, (object) this.listenerV4);
    }
    this.started = true;
  }

  public void StopListening()
  {
    if (!this.started)
      return;
    this.started = false;
    if (this.listenerV4 == null)
      return;
    this.listenerV4.Stop();
  }

  private void OnAcceptConnection(IAsyncResult result)
  {
    TcpListener asyncState1 = result.AsyncState as TcpListener;
    SslStream asyncState2 = (SslStream) null;
    try
    {
      if (!this.started)
        return;
      asyncState1.BeginAcceptTcpClient(this.onAcceptConnection, (object) asyncState1);
      TcpClient tcpClient = asyncState1.EndAcceptTcpClient(result);
      bool leaveInnerStreamOpen = false;
      asyncState2 = this.certValidationCallback == null ? new SslStream((Stream) tcpClient.GetStream(), leaveInnerStreamOpen) : new SslStream((Stream) tcpClient.GetStream(), leaveInnerStreamOpen, this.certValidationCallback);
      asyncState2.BeginAuthenticateAsServer(this.serverCert, this.clientCertificateRequired, this.sslProtocols, this.checkCertifcateRevocation, this.onAuthenticateAsServer, (object) asyncState2);
    }
    catch (Exception ex)
    {
      asyncState2?.Dispose();
      this.connectionCallback((object) this, new SecureConnectionResults(ex));
    }
  }

  private void OnAuthenticateAsServer(IAsyncResult result)
  {
    SslStream sslStream = (SslStream) null;
    try
    {
      sslStream = result.AsyncState as SslStream;
      sslStream.EndAuthenticateAsServer(result);
      this.connectionCallback((object) this, new SecureConnectionResults(sslStream));
    }
    catch (Exception ex)
    {
      sslStream?.Dispose();
      this.connectionCallback((object) this, new SecureConnectionResults(ex));
    }
  }

  public void Dispose()
  {
    if (Interlocked.Increment(ref this.disposed) != 1)
      return;
    if (this.listenerV4 != null)
      this.listenerV4.Stop();
    this.listenerV4 = (TcpListener) null;
    GC.SuppressFinalize((object) this);
  }
}
