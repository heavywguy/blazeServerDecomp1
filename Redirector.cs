// Decompiled with JetBrains decompiler
// Type: BlazeServer.Redirector
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using BlazeServer.Network.Send;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

#nullable disable
namespace BlazeServer;

public class Redirector
{
  private static Redirector redirector;
  public static X509Certificate2 RedirectorCert = new X509Certificate2(Certificate.X509, "123456");

  public static Redirector getInstance()
  {
    if (Redirector.redirector == null)
      Redirector.redirector = new Redirector();
    return Redirector.redirector;
  }

  public Redirector()
  {
    try
    {
      new SecureTcpServer(42127, (X509Certificate) Redirector.RedirectorCert, new SecureConnectionResultsCallback(Redirector.OnServerConnectionAvailable)).StartListening();
    }
    catch (Exception ex)
    {
      Logger.Error("Start Redirector Listener error :", ex);
    }
  }

  private static void OnServerConnectionAvailable(object sender, SecureConnectionResults args)
  {
    if (args.AsyncException != null)
    {
      Logger.Error("Redirector error : ", args.AsyncException);
    }
    else
    {
      SslStream secureStream = args.SecureStream;
      SslStream stream = new Redirector.RedirectorHandlerStruct()
      {
        stream = secureStream
      }.stream;
      try
      {
        while (stream.IsAuthenticated)
        {
          byte[] numArray = new byte[0];
          byte[] buffer1 = Helper.ReadContentSSL(stream);
          if (buffer1.Length != 0)
          {
            stream.Flush();
            if (buffer1[0] == (byte) 0)
            {
              using (List<Blaze.Packet>.Enumerator enumerator = Blaze.FetchAllBlazePackets(new BinaryReader((Stream) new MemoryStream(buffer1))).GetEnumerator())
              {
                if (enumerator.MoveNext())
                {
                  Blaze.Packet current = enumerator.Current;
                  Blaze.TdfString tdfString = (Blaze.TdfString) Blaze.ReadPacketContent(current)[2];
                  int port = !(tdfString.Value == "bf3 server") && !(tdfString.Value == "warsaw server") ? 42129 : 42128;
                  getServerInstance getServerInstance = new getServerInstance(current.ID, port);
                  getServerInstance.make();
                  byte[] buffer2 = getServerInstance.ByteArray();
                  stream.Write(buffer2);
                  stream.Flush();
                  stream.Close();
                  break;
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Error("Redirector error : ", ex);
      }
    }
  }

  private static bool IgnoreCertificateErrorsCallback(
    object sender,
    X509Certificate certificate,
    X509Chain chain,
    SslPolicyErrors sslPolicyErrors)
  {
    if (sslPolicyErrors != 0)
    {
      Logger.Log("IgnoreCertificateErrorsCallback: " + sslPolicyErrors.ToString());
      if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
      {
        foreach (X509ChainStatus chainStatu in chain.ChainStatus)
          Logger.Log("\t" + chainStatu.Status.ToString());
      }
    }
    return true;
  }

  public struct RedirectorHandlerStruct
  {
    public SslStream stream;
  }
}
