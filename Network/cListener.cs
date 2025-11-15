// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.cListener
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Net.Sockets;

#nullable disable
namespace BlazeServer.Network;

public class cListener : Socket
{
  public cListener()
    : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
  {
  }
}
