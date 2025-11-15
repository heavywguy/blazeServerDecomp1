// Decompiled with JetBrains decompiler
// Type: CodeBlooded.Spades.Services.SpadesInstaller
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#nullable disable
namespace CodeBlooded.Spades.Services;

[RunInstaller(true)]
public class SpadesInstaller : Installer
{
  public SpadesInstaller()
  {
    ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
    processInstaller.Account = ServiceAccount.LocalSystem;
    ServiceInstaller serviceInstaller = new ServiceInstaller();
    serviceInstaller.StartType = ServiceStartMode.Manual;
    serviceInstaller.ServiceName = "BlazeServer";
    serviceInstaller.DisplayName = "BlazeServer Service";
    this.Installers.Add((Installer) processInstaller);
    this.Installers.Add((Installer) serviceInstaller);
  }
}
