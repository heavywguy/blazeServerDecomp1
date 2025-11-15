// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.EmptyPacket
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.IO;
using System.Runtime.Remoting.Contexts;

#nullable disable
namespace BlazeServer.Network;

[Synchronization]
public abstract class EmptyPacket
{
  private MemoryStream full = new MemoryStream();

  public void CreatePacket(ushort component, ushort command, int error, ushort qtype, ushort id)
  {
    MemoryStream memoryStream = new MemoryStream();
    memoryStream.WriteByte((byte) 0);
    memoryStream.WriteByte((byte) 0);
    memoryStream.WriteByte((byte) ((uint) component >> 8));
    memoryStream.WriteByte((byte) ((uint) component & (uint) byte.MaxValue));
    memoryStream.WriteByte((byte) ((uint) command >> 8));
    memoryStream.WriteByte((byte) ((uint) command & (uint) byte.MaxValue));
    memoryStream.WriteByte((byte) (error >> 8));
    memoryStream.WriteByte((byte) (error & (int) byte.MaxValue));
    memoryStream.WriteByte((byte) ((uint) qtype >> 8));
    memoryStream.WriteByte((byte) 0);
    memoryStream.WriteByte((byte) ((uint) id >> 8));
    memoryStream.WriteByte((byte) ((uint) id & (uint) byte.MaxValue));
    memoryStream.ToArray();
    this.full.Write(memoryStream.ToArray(), 0, memoryStream.ToArray().Length);
  }

  public byte[] ByteArray() => this.full.ToArray();

  protected internal abstract void make();
}
