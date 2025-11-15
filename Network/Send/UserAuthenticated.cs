// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.UserAuthenticated
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class UserAuthenticated : SendPacket
{
  private string name;
  private string mail;
  private long gameid;
  private long userid;
  private long aloc;

  public UserAuthenticated(string name, string mail, long gameid, long userid, long aloc)
  {
    this.name = name;
    this.mail = mail;
    this.gameid = gameid;
    this.userid = userid;
    this.aloc = aloc;
  }

  protected internal override void make()
  {
    this.WriteInt("ALOC", this.aloc);
    this.WriteInt("BUID", this.gameid);
    this.WriteStr("DSNM", this.name);
    this.WriteInt("FRST", 0L);
    this.WriteStr("KEY\0", "85e940b5-59a3-4cdf-a322-701d6477f1ec");
    this.WriteInt("LAST", (long) Helper.GetUnixTimeStamp());
    this.WriteInt("LLOG", (long) Helper.GetUnixTimeStamp());
    this.WriteStr("MAIL", this.mail);
    this.WriteInt("PID\0", this.gameid);
    this.WriteInt("PLAT", 4L);
    this.WriteInt("UID\0", this.gameid);
    this.WriteInt("USTP", 1L);
    this.WriteInt("XREF", 0L);
    this.CreatePacket((ushort) 30722, (ushort) 8, 0, (ushort) 8192 /*0x2000*/, (ushort) 0);
  }
}
