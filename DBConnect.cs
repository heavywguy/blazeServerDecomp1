// Decompiled with JetBrains decompiler
// Type: BlazeServer.DBConnect
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using MySql.Data.MySqlClient;
using System.Runtime.Remoting.Contexts;

#nullable disable
namespace BlazeServer;

[Synchronization]
internal class DBConnect
{
  private static DBConnect sql = new DBConnect();
  protected MySqlConnectionStringBuilder connBuilder;

  public static DBConnect getInstance() => DBConnect.sql;

  public DBConnect()
  {
    this.connBuilder = new MySqlConnectionStringBuilder();
    this.connBuilder.Database = Config.DataBaseName;
    this.connBuilder.Server = Config.ServerDataBase;
    this.connBuilder.UserID = Config.DataBaseUID;
    this.connBuilder.Password = Config.DataBasePASS;
    this.connBuilder.Port = Config.dbConnectionPort;
    this.connBuilder.MinimumPoolSize = 40U;
    this.connBuilder.MaximumPoolSize = 100U;
    this.connBuilder.Pooling = true;
  }

  public MySqlConnection conn() => new MySqlConnection(this.connBuilder.ConnectionString);
}
