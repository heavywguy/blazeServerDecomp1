// Decompiled with JetBrains decompiler
// Type: BlazeServer.SecureConnectionResults
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Net.Security;

#nullable disable
namespace BlazeServer;

public class SecureConnectionResults
{
  private SslStream secureStream;
  private Exception asyncException;

  internal SecureConnectionResults(SslStream sslStream) => this.secureStream = sslStream;

  internal SecureConnectionResults(Exception exception) => this.asyncException = exception;

  public Exception AsyncException => this.asyncException;

  public SslStream SecureStream => this.secureStream;
}
