// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.Send.getTelemetryServer
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

#nullable disable
namespace BlazeServer.Network.Send;

internal class getTelemetryServer : SendPacket
{
  private ushort packetID;

  public getTelemetryServer(ushort packetID) => this.packetID = packetID;

  protected internal override void make()
  {
    this.WriteStr("ADRS", "10.0.0.1");
    this.WriteInt("ANON", 0L);
    this.WriteStr("DISA", "AD,AF,AG,AI,AL,AM,AN,AO,AQ,AR,AS,AW,AX,AZ,BA,BB,BD,BF,BH,BI,BJ,BM,BN,BO,BR,BS,BT,BV,BW,BY,BZ,CC,CD,CF,CG,CI,CK,CL,CM,CN,CO,CR,CU,CV,CX,DJ,DM,DO,DZ,EC,EG,EH,ER,ET,FJ,FK,FM,FO,GA,GD,GE,GF,GG,GH,GI,GL,GM,GN,GP,GQ,GS,GT,GU,GW,GY,HM,HN,HT,ID,IL,IM,IN,IO,IQ,IR,IS,JE,JM,JO,KE,KG,KH,KI,KM,KN,KP,KR,KW,KY,KZ,LA,LB,LC,LI,LK,LR,LS,LY,MA,MC,MD,ME,MG,MH,ML,MM,MN,MO,MP,MQ,MR,MS,MU,MV,MW,MY,MZ,NA,NC,NE,NF,NG,NI,NP,NR,NU,OM,PA,PE,PF,PG,PH,PK,PM,PN,PS,PW,PY,QA,RE,RS,RW,SA,SB,SC,SD,SG,SH,SJ,SL,SM,SN,SO,SR,ST,SV,SY,SZ,TC,TD,TF,TG,TH,TJ,TK,TL,TM,TN,TO,TT,TV,TZ,UA,UG,UM,UY,UZ,VA,VC,VE,VG,VN,VU,WF,WS,YE,YT,ZM,ZW,ZZ");
    this.WriteStr("FILT", "");
    this.WriteInt("LOC\0", 1701729619L);
    this.WriteStr("NOOK", "US,CA,MX");
    this.WriteInt("PORT", 9988L);
    this.WriteInt("SDLY", 15000L);
    this.WriteStr("SESS", "tele_sess");
    this.WriteStr("SKEY", "some_tele_key");
    this.WriteInt("SPCT", 75L);
    this.WriteStr("STIM", "Default");
    this.CreatePacket((ushort) 9, (ushort) 5, 0, (ushort) 4096 /*0x1000*/, this.packetID);
  }
}
