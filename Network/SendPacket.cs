// Decompiled with JetBrains decompiler
// Type: BlazeServer.Network.SendPacket
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

#nullable disable
namespace BlazeServer.Network;

[Synchronization]
public abstract class SendPacket
{
  private byte[] buff;
  private List<Blaze.Tdf> form = new List<Blaze.Tdf>();

  public Blaze.TdfString SetStr(string tag, string value) => Blaze.TdfString.Create(tag, value);

  public Blaze.TdfInteger SetInt(string tag, long value, byte type = 0)
  {
    Blaze.TdfInteger tdfInteger = Blaze.TdfInteger.Create(tag, value);
    tdfInteger.Type = type;
    return tdfInteger;
  }

  public Blaze.TdfBlob SetBlob(string tag, byte[] value) => Blaze.TdfBlob.Create(tag, value);

  public Blaze.TdfStruct SetStruct(string tag, List<Blaze.Tdf> values)
  {
    return Blaze.TdfStruct.Create(tag, values);
  }

  public Blaze.TdfList SetList(string label, byte subtype, int count, object list)
  {
    return Blaze.TdfList.Create(label, subtype, count, list);
  }

  public Blaze.TdfIntegerList SetIntegerList(string label, int count, List<long> list)
  {
    return Blaze.TdfIntegerList.Create(label, count, list);
  }

  public Blaze.TdfDoubleVal SetDoubleVal(string tag, Blaze.DoubleVal value)
  {
    return Blaze.TdfDoubleVal.Create(tag, value);
  }

  public Blaze.TdfTrippleVal SetTrippleVal(string tag, long val1, long val2, long val3)
  {
    return Blaze.TdfTrippleVal.Create(tag, new Blaze.TrippleVal(val1, val2, val3));
  }

  public Blaze.TdfDoubleList SetDoubleList(
    string label,
    byte subtype1,
    byte subtype2,
    object list1,
    object list2,
    int count)
  {
    return Blaze.TdfDoubleList.Create(label, subtype1, subtype2, list1, list2, count);
  }

  public void WriteInt(string tag, long value, byte type = 0)
  {
    Blaze.TdfInteger tdfInteger = Blaze.TdfInteger.Create(tag, value);
    tdfInteger.Type = type;
    this.form.Add((Blaze.Tdf) tdfInteger);
  }

  public void WriteBlob(string tag, byte[] value)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfBlob.Create(tag, value));
  }

  public void WriteStr(string tag, string value)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfString.Create(tag, value));
  }

  public void WriteDoubleVal(string tag, long val1, long val2)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfDoubleVal.Create(tag, new Blaze.DoubleVal(val1, val2)));
  }

  public void WriteTrippleVal(string tag, long val1, long val2, long val3)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfTrippleVal.Create(tag, new Blaze.TrippleVal(val1, val2, val3)));
  }

  public void WriteStruct(string tag, List<Blaze.Tdf> values)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfStruct.Create(tag, values));
  }

  public void WriteList(string label, byte subtype, int count, object list)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfList.Create(label, subtype, count, list));
  }

  public void WriteDoubleList(
    string label,
    byte subtype1,
    byte subtype2,
    object list1,
    object list2,
    int count)
  {
    this.form.Add((Blaze.Tdf) Blaze.TdfDoubleList.Create(label, subtype1, subtype2, list1, list2, count));
  }

  public void CreatePacket(ushort component, ushort command, int error, ushort qtype, ushort id)
  {
    this.buff = Blaze.CreatePacket(component, command, error, qtype, id, this.form);
  }

  public byte[] ByteArray() => this.buff;

  protected internal abstract void make();
}
