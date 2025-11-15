// Decompiled with JetBrains decompiler
// Type: MSCoreeTypeLib.ICorThreadpool
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.Runtime.InteropServices;

#nullable disable
namespace MSCoreeTypeLib;

[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("84680D3A-B2C1-46e8-ACC2-DBC0A359159A")]
public interface ICorThreadpool
{
  void RegisterWaitForSingleObject();

  void UnregisterWait();

  void QueueUserWorkItem();

  void CreateTimer();

  void ChangeTimer();

  void DeleteTimer();

  void BindIoCompletionCallback();

  void CallOrQueueUserWorkItem();

  void SetMaxThreads(uint MaxWorkerThreads, uint MaxIOCompletionThreads);

  void GetMaxThreads(out uint MaxWorkerThreads, out uint MaxIOCompletionThreads);

  void GetAvailableThreads(out uint AvailableWorkerThreads, out uint AvailableIOCompletionThreads);
}
