// Decompiled with JetBrains decompiler
// Type: BlazeServer.SQL_RUN
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace BlazeServer;

internal class SQL_RUN
{
  public void UpdateOnlinePlayers(GameClient player)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string str1 = string.Format("UPDATE gameservers SET online = @count WHERE ( gid = @gid)");
          connection.Open();
          using (new MySqlCommand(str1, connection))
            MySqlHelper.ExecuteNonQuery(connection, str1, new List<MySqlParameter>()
            {
              new MySqlParameter("@gid", (object) player.GameID),
              new MySqlParameter("@count", (object) Helper.CountActivePlayers(player.GameID))
            }.ToArray());
          if (player.isActive)
          {
            string str2 = string.Format("UPDATE players SET inGame = @isActive WHERE ( pid = @pid)");
            using (new MySqlCommand(str2, connection))
              MySqlHelper.ExecuteNonQuery(connection, str2, new List<MySqlParameter>()
              {
                new MySqlParameter("@isActive", (object) player.GameID),
                new MySqlParameter("@pid", (object) player.PlayerID)
              }.ToArray());
          }
          if (!player.isActive)
          {
            string str3 = string.Format("UPDATE players SET inGame = @isActive WHERE ( pid = @pid)");
            using (new MySqlCommand(str3, connection))
              MySqlHelper.ExecuteNonQuery(connection, str3, new List<MySqlParameter>()
              {
                new MySqlParameter("@isActive", MySqlDbType.Decimal),
                new MySqlParameter("@pid", (object) player.PlayerID)
              }.ToArray());
          }
          connection.Close();
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateOnlinePlayers) error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UpdateAllPlayers()
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = string.Format("UPDATE players SET inGame = 0 WHERE ( inGame != 0)");
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
            mySqlCommand.ExecuteNonQuery();
          connection.Close();
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateAllPlayers) error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UpdateOnlinePlayersOndisconnect(long gid, long pid, long isActive)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string str1 = string.Format("UPDATE gameservers SET online = @count WHERE ( gid = @gid)");
          connection.Open();
          using (new MySqlCommand(str1, connection))
            MySqlHelper.ExecuteNonQuery(connection, str1, new List<MySqlParameter>()
            {
              new MySqlParameter("@gid", (object) gid),
              new MySqlParameter("@count", (object) Helper.CountActivePlayers(gid))
            }.ToArray());
          string str2 = string.Format("UPDATE players SET inGame = @isActive WHERE ( pid = @pid)");
          using (new MySqlCommand(str2, connection))
          {
            MySqlHelper.ExecuteNonQuery(connection, str2, new List<MySqlParameter>()
            {
              new MySqlParameter("@isActive", MySqlDbType.Decimal),
              new MySqlParameter("@pid", (object) pid)
            }.ToArray());
            connection.Close();
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateOnlinePlayers) error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void SilentLoginAuthCheck(GameClient player)
  {
    try
    {
      using (MySqlConnection connection = DBConnect.getInstance().conn())
      {
        string cmdText = $"SELECT * FROM players WHERE pid ='{player.PlayerID}'";
        connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
        {
          mySqlCommand.ExecuteNonQuery();
          object obj = mySqlCommand.ExecuteScalar();
          if (obj != DBNull.Value && obj != null)
          {
            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
            {
              while (mySqlDataReader.Read())
              {
                if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("localPlayer")))
                {
                  short int16 = mySqlDataReader.GetInt16("localPlayer");
                  if (int16 == (short) 1)
                    player.localPlayer = false;
                  if (int16 == (short) 0)
                    player.localPlayer = false;
                }
                else
                  player.localPlayer = false;
                string str1 = mySqlDataReader.GetString("mail");
                if (str1 != null)
                  player.MAIL = str1;
                string str2 = mySqlDataReader.GetString("dsnm");
                if (str2 != null)
                  player.NAME = str2;
                if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("CustUserSettings")))
                {
                  string str3 = mySqlDataReader.GetString("CustUserSettings");
                  player.CustUserSettings = str3;
                }
                if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("SdtUserSettings")))
                {
                  string str4 = mySqlDataReader.GetString("SdtUserSettings");
                  player.SdtUserSettings = str4;
                }
                if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal("uatt")))
                {
                  long int64 = mySqlDataReader.GetInt64("uatt");
                  if (int64 != 0L)
                    player.UATT = int64;
                }
                player.BUID = player.PlayerID;
                player.UserID = player.PlayerID;
                player.AID = player.PlayerID;
              }
              mySqlDataReader.Close();
              player.Update = true;
            }
          }
          else
          {
            Logger.Log($"Silent login error: Personaref ({player.PlayerID.ToString()}) not found");
            player.isActive = false;
            player.Update = true;
          }
          if (player.NAME == "")
          {
            Logger.Log("Silent login error: empty names not allowed");
            player.isActive = false;
            player.Update = true;
          }
          connection.Close();
        }
      }
    }
    catch (MySqlException ex)
    {
      Logger.Error("MySQL (SilentLoginAuthCheck) error: ", (Exception) ex);
    }
  }

  public void ServerAuthCheck(Blaze.TdfString mail, Blaze.TdfString pass, GameManager player)
  {
    try
    {
      using (MySqlConnection connection = DBConnect.getInstance().conn())
      {
        string cmdText = $"SELECT * FROM loginpersona WHERE mail ='{mail.Value}'";
        connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
        {
          mySqlCommand.ExecuteNonQuery();
          object obj = mySqlCommand.ExecuteScalar();
          if (obj != DBNull.Value && obj != null)
          {
            using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
            {
              while (mySqlDataReader.Read())
              {
                if (!mySqlDataReader.IsDBNull(mySqlDataReader.GetOrdinal(nameof (pass))))
                {
                  if (mySqlDataReader.GetString(nameof (pass)) == pass.Value)
                  {
                    Logger.Log($"LoginPersona '{mail.Value}' found, password OK");
                    string str = mySqlDataReader.GetString(nameof (mail));
                    if (str != null)
                      player.MAIL = str;
                  }
                  else
                  {
                    Logger.Log($"LoginPersona ({mail.Value}) not found or password error");
                    player.isActive = false;
                    player.Update = true;
                  }
                }
              }
              mySqlDataReader.Close();
              player.Update = true;
            }
          }
          else
          {
            Logger.Log($"LoginPersona  ({mail.Value}) not found");
            player.isActive = false;
            player.Update = true;
          }
          connection.Close();
        }
      }
    }
    catch (MySqlException ex)
    {
      Logger.Error("MySQL (ServerAuthCheck) error: ", (Exception) ex);
    }
  }

  public void UpdateNameofServer(string gnam, long gid)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = string.Format("UPDATE gameservers SET gnam = @gnam  WHERE ( gid = @gid)");
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
          {
            mySqlCommand.Parameters.AddWithValue("@gnam", (object) gnam);
            mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
            mySqlCommand.ExecuteNonQuery();
            connection.Close();
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateNameofServer) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UpdatePCAPofServer(GameManager player, long maxplayers)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = string.Format("UPDATE gameservers SET pcap = @pcap  WHERE ( gid = @gid)");
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
          {
            mySqlCommand.Parameters.AddWithValue("@gid", (object) player.GID);
            mySqlCommand.Parameters.AddWithValue("@pcap", (object) maxplayers);
            mySqlCommand.ExecuteNonQuery();
            connection.Close();
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdatePCAPofServer) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UpdateServerATTR(long gid, string mail, List<string> attribval, string version)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = $"SELECT gid FROM gameservers WHERE gid='{gid}'";
          connection.Open();
          if (version == "VeniceOpenBeta868283final-1.0")
          {
            using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
            {
              mySqlCommand.Parameters.AddWithValue("@online", (object) Helper.CountActivePlayers(gid));
              mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
              mySqlCommand.Parameters.AddWithValue("@mail", (object) mail);
              mySqlCommand.Parameters.AddWithValue("@dn1", (object) attribval[1]);
              mySqlCommand.Parameters.AddWithValue("@dn2", (object) attribval[2]);
              mySqlCommand.Parameters.AddWithValue("@lvl", (object) attribval[4]);
              mySqlCommand.Parameters.AddWithValue("@lvn", (object) attribval[5]);
              mySqlCommand.Parameters.AddWithValue("@mfo", (object) "0,1;0,1");
              mySqlCommand.Parameters.AddWithValue("@msg", (object) attribval[8]);
              mySqlCommand.Parameters.AddWithValue("@md", (object) attribval[9]);
              mySqlCommand.Parameters.AddWithValue("@mde", (object) attribval[10]);
              mySqlCommand.Parameters.AddWithValue("@prt", (object) attribval[11]);
              mySqlCommand.Parameters.AddWithValue("@sg1", (object) attribval[15]);
              mySqlCommand.Parameters.AddWithValue("@sg2", (object) attribval[16 /*0x10*/]);
              mySqlCommand.Parameters.AddWithValue("@tp", (object) attribval[17]);
              mySqlCommand.ExecuteNonQuery();
              object obj = mySqlCommand.ExecuteScalar();
              if (obj != DBNull.Value && obj != null)
              {
                mySqlCommand.CommandText = string.Format("UPDATE gameservers SET gid = @gid, online = @online, description1 = @dn1, description2 = @dn2, level = @lvl, levellocation = @lvn, mapsinfo = @mfo, message = @msg, modd = @md, mode = @mde, preset = @prt, settings1 = @sg1, settings2 = @sg2, type = @tp  WHERE ( gid = @gid)");
                mySqlCommand.ExecuteNonQuery();
              }
              else
              {
                mySqlCommand.CommandText = string.Format("INSERT INTO gameservers (mail, gid, online, description1, description2, level, levellocation, mapsinfo, message, modd, mode, preset, settings1, settings2, type)  VALUES (@mail, @gid, @online, @dn1, @dn2, @lvl, @lvn, @mfo, @msg, @md, @mde, @prt, @sg1, @sg2, @tp)");
                mySqlCommand.ExecuteNonQuery();
              }
              connection.Close();
            }
          }
          else
          {
            using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
            {
              mySqlCommand.Parameters.AddWithValue("@online", (object) Helper.CountActivePlayers(gid));
              mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
              mySqlCommand.Parameters.AddWithValue("@mail", (object) mail);
              mySqlCommand.Parameters.AddWithValue("@dn1", (object) attribval[2]);
              mySqlCommand.Parameters.AddWithValue("@dn2", (object) attribval[3]);
              mySqlCommand.Parameters.AddWithValue("@lvl", (object) attribval[5]);
              mySqlCommand.Parameters.AddWithValue("@lvn", (object) attribval[6]);
              mySqlCommand.Parameters.AddWithValue("@mfo", (object) attribval[9]);
              mySqlCommand.Parameters.AddWithValue("@msg", (object) attribval[11]);
              mySqlCommand.Parameters.AddWithValue("@md", (object) attribval[12]);
              mySqlCommand.Parameters.AddWithValue("@mde", (object) attribval[13]);
              mySqlCommand.Parameters.AddWithValue("@prt", (object) attribval[15]);
              mySqlCommand.Parameters.AddWithValue("@sg1", (object) attribval[21]);
              mySqlCommand.Parameters.AddWithValue("@sg2", (object) attribval[22]);
              mySqlCommand.Parameters.AddWithValue("@tp", (object) attribval[24]);
              mySqlCommand.ExecuteNonQuery();
              object obj = mySqlCommand.ExecuteScalar();
              if (obj != DBNull.Value && obj != null)
              {
                mySqlCommand.CommandText = string.Format("UPDATE gameservers SET gid = @gid, online = @online, description1 = @dn1, description2 = @dn2, level = @lvl, levellocation = @lvn, mapsinfo = @mfo, message = @msg, modd = @md, mode = @mde, preset = @prt, settings1 = @sg1, settings2 = @sg2, type = @tp  WHERE ( gid = @gid)");
                mySqlCommand.ExecuteNonQuery();
              }
              else
              {
                mySqlCommand.CommandText = string.Format("INSERT INTO gameservers (mail, gid, online, description1, description2, level, levellocation, mapsinfo, message, modd, mode, preset, settings1, settings2, type)  VALUES (@mail, @gid, @online, @dn1, @dn2, @lvl, @lvn, @mfo, @msg, @md, @mde, @prt, @sg1, @sg2, @tp)");
                mySqlCommand.ExecuteNonQuery();
              }
              connection.Close();
            }
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateServerATTR) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UpdateServer(
    long gid,
    string mail,
    List<string> attribval,
    string gnam,
    long maxplayers,
    string version)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = $"SELECT gid FROM gameservers WHERE gid='{gid}'";
          connection.Open();
          switch (version)
          {
            case "VeniceAlphaTrial749691retail-1.0":
              using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
              {
                mySqlCommand.Parameters.AddWithValue("@online", (object) Helper.CountActivePlayers(gid));
                mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
                mySqlCommand.Parameters.AddWithValue("@mail", (object) mail);
                mySqlCommand.Parameters.AddWithValue("@gnam", (object) gnam);
                mySqlCommand.Parameters.AddWithValue("@pcap", (object) maxplayers);
                mySqlCommand.Parameters.AddWithValue("@dn1", (object) attribval[1]);
                mySqlCommand.Parameters.AddWithValue("@dn2", (object) "");
                mySqlCommand.Parameters.AddWithValue("@lvl", (object) attribval[2]);
                mySqlCommand.Parameters.AddWithValue("@lvn", (object) "RushLarge0");
                mySqlCommand.Parameters.AddWithValue("@mfo", (object) "0,1;0,1");
                mySqlCommand.Parameters.AddWithValue("@msg", (object) attribval[4]);
                mySqlCommand.Parameters.AddWithValue("@md", (object) attribval[5]);
                mySqlCommand.Parameters.AddWithValue("@mde", (object) attribval[6]);
                mySqlCommand.Parameters.AddWithValue("@prt", (object) attribval[7]);
                mySqlCommand.Parameters.AddWithValue("@sg1", (object) attribval[11]);
                mySqlCommand.Parameters.AddWithValue("@sg2", (object) "");
                mySqlCommand.Parameters.AddWithValue("@tp", (object) attribval[12]);
                mySqlCommand.ExecuteNonQuery();
                object obj = mySqlCommand.ExecuteScalar();
                if (obj != DBNull.Value && obj != null)
                {
                  mySqlCommand.CommandText = string.Format("UPDATE gameservers SET gid = @gid, online = @online, description1 = @dn1, description2 = @dn2, level = @lvl, levellocation = @lvn, mapsinfo = @mfo, message = @msg, modd = @md, mode = @mde, preset = @prt, settings1 = @sg1, settings2 = @sg2, type = @tp, gnam = @gnam, pcap = @pcap   WHERE ( gid = @gid)");
                  mySqlCommand.ExecuteNonQuery();
                }
                else
                {
                  mySqlCommand.CommandText = string.Format("INSERT INTO gameservers (mail, gid, online, description1, description2, level, levellocation, mapsinfo, message, modd, mode, preset, settings1, settings2, type, gnam, pcap)  VALUES (@mail, @gid, @online, @dn1, @dn2, @lvl, @lvn, @mfo, @msg, @md, @mde, @prt, @sg1, @sg2, @tp, @gnam, @pcap)");
                  mySqlCommand.ExecuteNonQuery();
                }
                connection.Close();
                break;
              }
            case "VeniceOpenBeta868283final-1.0":
              using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
              {
                mySqlCommand.Parameters.AddWithValue("@online", (object) Helper.CountActivePlayers(gid));
                mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
                mySqlCommand.Parameters.AddWithValue("@mail", (object) mail);
                mySqlCommand.Parameters.AddWithValue("@gnam", (object) gnam);
                mySqlCommand.Parameters.AddWithValue("@pcap", (object) maxplayers);
                mySqlCommand.Parameters.AddWithValue("@dn1", (object) attribval[1]);
                mySqlCommand.Parameters.AddWithValue("@dn2", (object) attribval[2]);
                mySqlCommand.Parameters.AddWithValue("@lvl", (object) attribval[4]);
                mySqlCommand.Parameters.AddWithValue("@lvn", (object) attribval[5]);
                mySqlCommand.Parameters.AddWithValue("@mfo", (object) "0,1;0,1");
                mySqlCommand.Parameters.AddWithValue("@msg", (object) attribval[8]);
                mySqlCommand.Parameters.AddWithValue("@md", (object) attribval[9]);
                mySqlCommand.Parameters.AddWithValue("@mde", (object) attribval[10]);
                mySqlCommand.Parameters.AddWithValue("@prt", (object) attribval[11]);
                mySqlCommand.Parameters.AddWithValue("@sg1", (object) attribval[15]);
                mySqlCommand.Parameters.AddWithValue("@sg2", (object) attribval[16 /*0x10*/]);
                mySqlCommand.Parameters.AddWithValue("@tp", (object) attribval[17]);
                mySqlCommand.ExecuteNonQuery();
                object obj = mySqlCommand.ExecuteScalar();
                if (obj != DBNull.Value && obj != null)
                {
                  mySqlCommand.CommandText = string.Format("UPDATE gameservers SET gid = @gid, online = @online, description1 = @dn1, description2 = @dn2, level = @lvl, levellocation = @lvn, mapsinfo = @mfo, message = @msg, modd = @md, mode = @mde, preset = @prt, settings1 = @sg1, settings2 = @sg2, type = @tp, gnam = @gnam, pcap = @pcap   WHERE ( gid = @gid)");
                  mySqlCommand.ExecuteNonQuery();
                }
                else
                {
                  mySqlCommand.CommandText = string.Format("INSERT INTO gameservers (mail, gid, online, description1, description2, level, levellocation, mapsinfo, message, modd, mode, preset, settings1, settings2, type, gnam, pcap)  VALUES (@mail, @gid, @online, @dn1, @dn2, @lvl, @lvn, @mfo, @msg, @md, @mde, @prt, @sg1, @sg2, @tp, @gnam, @pcap)");
                  mySqlCommand.ExecuteNonQuery();
                }
                connection.Close();
                break;
              }
            default:
              using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
              {
                mySqlCommand.Parameters.AddWithValue("@online", (object) Helper.CountActivePlayers(gid));
                mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
                mySqlCommand.Parameters.AddWithValue("@mail", (object) mail);
                mySqlCommand.Parameters.AddWithValue("@gnam", (object) gnam);
                mySqlCommand.Parameters.AddWithValue("@pcap", (object) maxplayers);
                mySqlCommand.Parameters.AddWithValue("@dn1", (object) attribval[2]);
                mySqlCommand.Parameters.AddWithValue("@dn2", (object) attribval[3]);
                mySqlCommand.Parameters.AddWithValue("@lvl", (object) attribval[5]);
                mySqlCommand.Parameters.AddWithValue("@lvn", (object) attribval[6]);
                mySqlCommand.Parameters.AddWithValue("@mfo", (object) attribval[9]);
                mySqlCommand.Parameters.AddWithValue("@msg", (object) attribval[11]);
                mySqlCommand.Parameters.AddWithValue("@md", (object) attribval[12]);
                mySqlCommand.Parameters.AddWithValue("@mde", (object) attribval[13]);
                mySqlCommand.Parameters.AddWithValue("@prt", (object) attribval[15]);
                mySqlCommand.Parameters.AddWithValue("@sg1", (object) attribval[21]);
                mySqlCommand.Parameters.AddWithValue("@sg2", (object) attribval[22]);
                mySqlCommand.Parameters.AddWithValue("@tp", (object) attribval[24]);
                mySqlCommand.ExecuteNonQuery();
                object obj = mySqlCommand.ExecuteScalar();
                if (obj != DBNull.Value && obj != null)
                {
                  mySqlCommand.CommandText = string.Format("UPDATE gameservers SET gid = @gid, online = @online, description1 = @dn1, description2 = @dn2, level = @lvl, levellocation = @lvn, mapsinfo = @mfo, message = @msg, modd = @md, mode = @mde, preset = @prt, settings1 = @sg1, settings2 = @sg2, type = @tp, gnam = @gnam, pcap = @pcap   WHERE ( gid = @gid)");
                  mySqlCommand.ExecuteNonQuery();
                }
                else
                {
                  mySqlCommand.CommandText = string.Format("INSERT INTO gameservers (mail, gid, online, description1, description2, level, levellocation, mapsinfo, message, modd, mode, preset, settings1, settings2, type, gnam, pcap)  VALUES (@mail, @gid, @online, @dn1, @dn2, @lvl, @lvn, @mfo, @msg, @md, @mde, @prt, @sg1, @sg2, @tp, @gnam, @pcap)");
                  mySqlCommand.ExecuteNonQuery();
                }
                connection.Close();
                break;
              }
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (UpdateServer) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void DeleteAllServers()
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = string.Format("DELETE FROM gameservers");
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
            mySqlCommand.ExecuteNonQuery();
          connection.Close();
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (DeleteAllServers) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void DeleteServer(long gid)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = $"DELETE FROM gameservers WHERE gid='{gid}'";
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
          {
            mySqlCommand.Parameters.AddWithValue("@gid", (object) gid);
            mySqlCommand.ExecuteNonQuery();
          }
          connection.Close();
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (DeleteServer) Error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void CustUserSettingsSave(GameClient player, string value)
  {
    try
    {
      using (MySqlConnection connection = DBConnect.getInstance().conn())
      {
        string cmdText = string.Format("UPDATE players SET CustUserSettings = @usersettings   WHERE ( pid = @pid)");
        connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
        {
          try
          {
            mySqlCommand.Parameters.AddWithValue("@usersettings", (object) value);
            mySqlCommand.Parameters.AddWithValue("@pid", (object) player.PlayerID);
            mySqlCommand.ExecuteNonQuery();
          }
          catch (MySqlException ex)
          {
            Logger.Error("MySQL Error: ", (Exception) ex);
          }
          connection.Close();
          Logger.Log($"ID'{(object) player.ID}'{player.NAME}' ({(object) player.PlayerID}'{player.PlayerID.ToString("X")}') usersettings saved");
        }
      }
    }
    catch (MySqlException ex)
    {
      Logger.Error("MySQL (CustUserSettingsSave) error: ", (Exception) ex);
    }
  }

  public void SdtUserSettingsSave(long PlayerID, string value)
  {
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          string cmdText = string.Format("UPDATE players SET SdtUserSettings = @usersettings   WHERE ( pid = @pid)");
          connection.Open();
          using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
          {
            try
            {
              mySqlCommand.Parameters.AddWithValue("@usersettings", (object) value);
              mySqlCommand.Parameters.AddWithValue("@pid", (object) PlayerID);
              mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
              Logger.Error("MySQL Error: ", (Exception) ex);
            }
            connection.Close();
          }
        }
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (SdtUserSettingsSave) error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public void UserStatLoad(GameClient player, List<string> t2)
  {
    try
    {
      List<SQL_RUN.PlayerStatValue> playerStatValueList = new List<SQL_RUN.PlayerStatValue>();
      using (MySqlConnection connection = DBConnect.getInstance().conn())
      {
        string cmdText = $"SELECT * from playerstats WHERE pid = {player.PlayerID}";
        connection.Open();
        using (MySqlCommand mySqlCommand = new MySqlCommand(cmdText, connection))
        {
          using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
          {
            while (mySqlDataReader.Read())
            {
              SQL_RUN.PlayerStatValue playerStatValue = new SQL_RUN.PlayerStatValue();
              playerStatValue.statname = mySqlDataReader.GetString("statname");
              playerStatValue.value = $"{mySqlDataReader.GetFloat("value"):0.00000}";
              if (playerStatValue != null)
                playerStatValueList.Add(playerStatValue);
            }
            mySqlDataReader.Close();
          }
        }
        for (int index = 0; index < 1432; ++index)
        {
          bool flag = false;
          if (playerStatValueList != null)
          {
            foreach (SQL_RUN.PlayerStatValue playerStatValue in playerStatValueList)
            {
              if (playerStatValue.statname == Helper.statname[index])
              {
                t2.Add(playerStatValue.value);
                if (playerStatValue.statname == "rank")
                {
                  player.rank = playerStatValue.value;
                  player.Update = true;
                }
                flag = true;
              }
            }
          }
          if (!flag)
            t2.Add("0.00000");
        }
        connection.Close();
      }
    }
    catch (MySqlException ex)
    {
      Logger.Error("MySQL error: ", (Exception) ex);
    }
  }

  public void StatOnRoundEnd(List<AllPlayersStatValue> allstatvalue, long count1, long GID)
  {
    Logger.Log($"Saving stat receive({GID.ToString()}):");
    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (DoWorkEventHandler) ((sender, e) =>
    {
      int num = 0;
      try
      {
        using (MySqlConnection connection = DBConnect.getInstance().conn())
        {
          connection.Open();
          foreach (AllPlayersStatValue playersStatValue in allstatvalue)
          {
            if (((IEnumerable<string>) Helper.unlocknames).Contains<string>(playersStatValue.statname))
            {
              using (MySqlCommand mySqlCommand = new MySqlCommand($"INSERT INTO `playerstats` (`pid`, `statname`, `value`)  VALUES (@pid, @statname, @value) ON DUPLICATE KEY UPDATE `value` =`value` + '{playersStatValue.value}'", connection))
              {
                mySqlCommand.Parameters.AddWithValue("@pid", (object) playersStatValue.pid);
                mySqlCommand.Parameters.AddWithValue("@statname", (object) playersStatValue.statname);
                mySqlCommand.Parameters.AddWithValue("@value", (object) playersStatValue.value);
                mySqlCommand.ExecuteNonQuery();
                ++num;
              }
            }
            else
            {
              using (MySqlCommand mySqlCommand = new MySqlCommand(string.Format("INSERT INTO `playerstats` (`pid`, `statname`, `value`)  VALUES (@pid, @statname, @value) ON DUPLICATE KEY UPDATE `value` = @value"), connection))
              {
                mySqlCommand.Parameters.AddWithValue("@pid", (object) playersStatValue.pid);
                mySqlCommand.Parameters.AddWithValue("@statname", (object) playersStatValue.statname);
                mySqlCommand.Parameters.AddWithValue("@value", (object) playersStatValue.value);
                mySqlCommand.ExecuteNonQuery();
                ++num;
              }
            }
          }
          connection.Close();
        }
        if (num <= 1)
          return;
        Logger.Log($"Inserting or updating {num.ToString()} values of {count1.ToString()} players");
      }
      catch (MySqlException ex)
      {
        Logger.Error("MySQL (StatOnRoundEnd) error: ", (Exception) ex);
      }
    });
    backgroundWorker.RunWorkerAsync();
  }

  public class PlayerStatValue
  {
    public string statname;
    public string value;
  }
}
