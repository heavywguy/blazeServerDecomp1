// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.GameManagerComponent50
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;

#nullable disable
namespace BlazeServer.Network.Send;

internal class GameManagerComponent50 : SendPacket
{
  private long longid;

  public GameManagerComponent50(long longid) => this.longid = longid;

  protected internal override void make()
  {
    this.WriteDoubleList("ATTR", (byte) 1, (byte) 1, (object) new List<string>()
    {
      "level"
    }, (object) new List<string>() { "Web_Loading" }, 1);
    this.WriteInt("GID\0", this.longid);
    this.CreatePacket((ushort) 4, (ushort) 80 /*0x50*/, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
