// Decompiled with JetBrains decompiler
// Type: BlazeServer.Blaze
// Assembly: BlazeServer, Version=1.0.5722.14633, Culture=neutral, PublicKeyToken=null
// MVID: D771C68D-1A8A-44C4-A3EF-292B7AC2C654
// Assembly location: C:\Users\1043\Desktop\Blaze\BlazeServer.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace BlazeServer;

public static class Blaze
{
  public static string ComponentNames = "Authentication Component=1,Example Component=3,Game Manager Component=4,Redirector Component=5,Play Groups Component=6,Stats Component=7,Util Component=9,Census Data Component=A,Clubs Component=B,Game Report Lagacy Component=C,League Component=D,Mail Component=E,Messaging Component=F,Locker Component=14,Rooms Component=15,Tournaments Component=17,Commerce Info Component=18,Association Lists Component=19,GPS Content Controller Component=1B,Game Reporting Component=1C,Dynamic Filter Component=7D0,RSP Component=801,User Sessions Component=7802";
  public static string[] DescComponent1 = new string[102]
  {
    "A",
    "createAccount",
    "14",
    "updateAccount",
    "1C",
    "updateParentalEmail",
    "1D",
    "listUserEntitlements2",
    "1E",
    "getAccount",
    "1F",
    "grantEntitlement",
    "20",
    "listEntitlements",
    "21",
    "hasEntitlement",
    "22",
    "getUseCount",
    "23",
    "decrementUseCount",
    "24",
    "getAuthToken",
    "25",
    "getHandoffToken",
    "26",
    "getPasswordRules",
    "27",
    "grantEntitlement2",
    "28",
    "login",
    "29",
    "acceptTos",
    "2A",
    "getTosInfo",
    "2B",
    "modifyEntitlement2",
    "2C",
    "consumecode",
    "2D",
    "passwordForgot",
    "2E",
    "getTermsAndConditionsContent",
    "2F",
    "getPrivacyPolicyContent",
    "30",
    "listPersonaEntitlements2",
    "32",
    "silentLogin",
    "33",
    "checkAgeReq",
    "34",
    "getOptIn",
    "35",
    "enableOptIn",
    "36",
    "disableOptIn",
    "3C",
    "expressLogin",
    "46",
    "logout",
    "50",
    "createPersona",
    "5A",
    "getPersona",
    "64",
    "listPersonas",
    "6E",
    "loginPersona",
    "78",
    "logoutPersona",
    "8C",
    "deletePersona",
    "8D",
    "disablePersona",
    "8F",
    "listDeviceAccounts",
    "96",
    "xboxCreateAccount",
    "98",
    "originLogin",
    "A0",
    "xboxAssociateAccount",
    "AA",
    "xboxLogin",
    "B4",
    "ps3CreateAccount",
    "BE",
    "ps3AssociateAccount",
    "C8",
    "ps3Login",
    "D2",
    "validateSessionKey",
    "E6",
    "createWalUserSession",
    "F1",
    "acceptLegalDocs",
    "F2",
    "getLegalDocsInfo",
    "F6",
    "getTermsOfServiceContent",
    "12C",
    "deviceLoginGuest"
  };
  public static string[] DescComponent4 = new string[92]
  {
    "1",
    "createGame",
    "2",
    "destroyGame",
    "3",
    "advanceGameState",
    "4",
    "setGameSettings",
    "5",
    "setPlayerCapacity",
    "6",
    "setPresenceMode",
    "7",
    "setGameAttributes",
    "8",
    "setPlayerAttributes",
    "9",
    "joinGame",
    "B",
    "removePlayer",
    "D",
    "startMatchmaking",
    "E",
    "cancelMatchmaking",
    "F",
    "finalizeGameCreation",
    "11",
    "listGames",
    "12",
    "setPlayerCustomData",
    "13",
    "replayGame",
    "14",
    "returnDedicatedServerToPool",
    "15",
    "joinGameByGroup",
    "16",
    "leaveGameByGroup",
    "17",
    "migrateGame",
    "18",
    "updateGameHostMigrationStatus",
    "19",
    "resetDedicatedServer",
    "1A",
    "updateGameSession",
    "1B",
    "banPlayer",
    "1D",
    "updateMeshConnection",
    "1F",
    "removePlayerFromBannedList",
    "20",
    "clearBannedList",
    "21",
    "getBannedList",
    "26",
    "addQueuedPlayerToGame",
    "27",
    "updateGameName",
    "28",
    "ejectHost",
    "64",
    "getGameListSnapshot",
    "65",
    "getGameListSubscription",
    "66",
    "destroyGameList",
    "67",
    "getFullGameData",
    "68",
    "getMatchmakingConfig",
    "69",
    "getGameDataFromId",
    "6A",
    "addAdminPlayer",
    "6B",
    "removeAdminPlayer",
    "6C",
    "setPlayerTeam",
    "6D",
    "changeGameTeamId",
    "6E",
    "migrateAdminPlayer",
    "6F",
    "getUserSetGameListSubscription",
    "70",
    "swapPlayersTeam",
    "96",
    "registerDynamicDedicatedServerCreator",
    "97",
    "unregisterDynamicDedicatedServerCreator"
  };
  public static string[] DescComponent7 = new string[44]
  {
    "1",
    "getStatDescs",
    "2",
    "getStats",
    "3",
    "getStatGroupList",
    "4",
    "getStatGroup",
    "5",
    "getStatsByGroup",
    "6",
    "getDateRange",
    "7",
    "getEntityCount",
    "A",
    "getLeaderboardGroup",
    "B",
    "getLeaderboardFolderGroup",
    "C",
    "getLeaderboard",
    "D",
    "getCenteredLeaderboard",
    "E",
    "getFilteredLeaderboard",
    "F",
    "getKeyScopesMap",
    "10",
    "getStatsByGroupAsync",
    "11",
    "getLeaderboardTreeAsync",
    "12",
    "getLeaderboardEntityCount",
    "13",
    "getStatCategoryList",
    "14",
    "getPeriodIds",
    "15",
    "getLeaderboardRaw",
    "16",
    "getCenteredLeaderboardRaw",
    "17",
    "getFilteredLeaderboardRaw",
    "18",
    "changeKeyscopeValue"
  };
  public static string[] DescComponent9 = new string[40]
  {
    "1",
    "fetchClientConfig",
    "2",
    "ping",
    "3",
    "setClientData",
    "4",
    "localizeStrings",
    "5",
    "getTelemetryServer",
    "6",
    "getTickerServer",
    "7",
    "preAuth",
    "8",
    "postAuth",
    "A",
    "userSettingsLoad",
    "B",
    "userSettingsSave",
    "C",
    "userSettingsLoadAll",
    "E",
    "deleteUserSettings",
    "14",
    "filterForProfanity",
    "15",
    "fetchQosConfig",
    "16",
    "setClientMetrics",
    "17",
    "setConnectionState",
    "18",
    "getPssConfig",
    "19",
    "getUserOptions",
    "1A",
    "setUserOptions",
    "1B",
    "suspendUserPing"
  };
  public static string[] DescComponentF = new string[10]
  {
    "1",
    "sendMessage",
    "2",
    "fetchMessages",
    "3",
    "purgeMessages",
    "4",
    "touchMessages",
    "5",
    "getMessages"
  };
  public static string[] DescComponent19 = new string[18]
  {
    "1",
    "addUsersToList",
    "2",
    "removeUsersFromList",
    "3",
    "clearLists",
    "4",
    "setUsersToList",
    "5",
    "getListForUser",
    "6",
    "getLists",
    "7",
    "subscribeToLists",
    "8",
    "unsubscribeFromLists",
    "9",
    "getConfigListsInfo"
  };
  public static string[] DescComponent1C = new string[30]
  {
    "1",
    "submitGameReport",
    "2",
    "submitOfflineGameReport",
    "3",
    "submitGameEvents",
    "4",
    "getGameReportQuery",
    "5",
    "getGameReportQueriesList",
    "6",
    "getGameReports",
    "7",
    "getGameReportView",
    "8",
    "getGameReportViewInfo",
    "9",
    "getGameReportViewInfoList",
    "A",
    "getGameReportTypes",
    "B",
    "updateMetric",
    "C",
    "getGameReportColumnInfo",
    "D",
    "getGameReportColumnValues",
    "64",
    "submitTrustedMidGameReport",
    "65",
    "submitTrustedEndGameReport"
  };
  public static string[] DescComponent7802 = new string[32 /*0x20*/]
  {
    "3",
    "fetchExtendedData",
    "5",
    "updateExtendedDataAttribute",
    "8",
    "updateHardwareFlags",
    "C",
    "lookupUser",
    "D",
    "lookupUsers",
    "E",
    "lookupUsersByPrefix",
    "14",
    "updateNetworkInfo",
    "17",
    "lookupUserGeoIPData",
    "18",
    "overrideUserGeoIPData",
    "19",
    "updateUserSessionClientData",
    "1A",
    "setUserInfoAttribute",
    "1B",
    "resetUserGeoIPData",
    "20",
    "lookupUserSessionId",
    "21",
    "fetchLastLocaleUsedAndAuthError",
    "22",
    "fetchUserFirstLastAuthTime",
    "23",
    "resumeSession"
  };

  public static Blaze.Packet ReadBlazePacket(BinaryReader s)
  {
    Blaze.Packet packet = new Blaze.Packet()
    {
      Length = Blaze.ReadUShort(s),
      Component = Blaze.ReadUShort(s),
      Command = Blaze.ReadUShort(s),
      Error = Blaze.ReadUShort(s),
      QType = Blaze.ReadUShort(s),
      ID = Blaze.ReadUShort(s)
    };
    packet.extLength = ((uint) packet.QType & 16U /*0x10*/) <= 0U ? (ushort) 0 : Blaze.ReadUShort(s);
    int count = (int) packet.Length + ((int) packet.extLength << 16 /*0x10*/);
    packet.Content = new byte[count];
    s.Read(packet.Content, 0, count);
    return packet;
  }

  public static Blaze.Packet ReadBlazePacketHeader(BinaryReader s)
  {
    Blaze.Packet packet = new Blaze.Packet()
    {
      Length = Blaze.ReadUShort(s),
      Component = Blaze.ReadUShort(s),
      Command = Blaze.ReadUShort(s),
      Error = Blaze.ReadUShort(s),
      QType = Blaze.ReadUShort(s),
      ID = Blaze.ReadUShort(s)
    };
    packet.extLength = ((uint) packet.QType & 16U /*0x10*/) <= 0U ? (ushort) 0 : Blaze.ReadUShort(s);
    int length = (int) packet.Length + ((int) packet.extLength << 16 /*0x10*/);
    packet.Content = new byte[length];
    return packet;
  }

  public static List<Blaze.Packet> FetchAllBlazePackets(BinaryReader s)
  {
    List<Blaze.Packet> packetList = new List<Blaze.Packet>();
    s.BaseStream.Seek(0L, SeekOrigin.Begin);
    while (s.BaseStream.Position < s.BaseStream.Length)
    {
      try
      {
        packetList.Add(Blaze.ReadBlazePacket(s));
      }
      catch (Exception ex)
      {
        s.BaseStream.Position = s.BaseStream.Length;
      }
    }
    return packetList;
  }

  public static ushort ReadUShort(BinaryReader s)
  {
    byte[] buffer = new byte[2];
    s.Read(buffer, 0, 2);
    return (ushort) (((uint) buffer[0] << 8) + (uint) buffer[1]);
  }

  public static uint ReadUInt(BinaryReader s)
  {
    byte[] buffer = new byte[4];
    s.Read(buffer, 0, 4);
    return (uint) (((int) buffer[0] << 24) + ((int) buffer[1] << 16 /*0x10*/) + ((int) buffer[2] << 8)) + (uint) buffer[3];
  }

  public static float ReadFloat(BinaryReader s)
  {
    byte[] buffer = new byte[4];
    byte[] numArray = new byte[4];
    s.Read(buffer, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray[index] = buffer[3 - index];
    return BitConverter.ToSingle(numArray, 0);
  }

  public static void WriteFloat(Stream s, float f)
  {
    byte[] bytes = BitConverter.GetBytes(f);
    byte[] buffer = new byte[4];
    s.Read(bytes, 0, 4);
    for (int index = 0; index < 4; ++index)
      buffer[index] = bytes[3 - index];
    s.Write(buffer, 0, 4);
  }

  public static string TagToLabelXYU(uint Tag)
  {
    string labelXyu = "";
    uint num1 = Tag >> 8;
    byte[] numArray = new byte[4];
    for (int index = 0; index < 4; ++index)
    {
      byte num2 = (byte) (num1 >> (3 - index) * 6 & 63U /*0x3F*/);
      if (num2 > (byte) 0)
        numArray[index] = (byte) (64 /*0x40*/ | (int) num2 & 31 /*0x1F*/);
      labelXyu += ((char) numArray[index]).ToString();
    }
    return labelXyu;
  }

  public static string TagToLabel(uint Tag)
  {
    string label = "";
    List<byte> byteList = new List<byte>((IEnumerable<byte>) BitConverter.GetBytes(Tag));
    byteList.Reverse();
    byte[] numArray = new byte[4];
    numArray[0] |= (byte) (((int) byteList[0] & 128 /*0x80*/) >> 1);
    numArray[0] |= (byte) (((int) byteList[0] & 64 /*0x40*/) >> 2);
    numArray[0] |= (byte) (((int) byteList[0] & 48 /*0x30*/) >> 2);
    numArray[0] |= (byte) (((int) byteList[0] & 12) >> 2);
    numArray[1] |= (byte) (((int) byteList[0] & 2) << 5);
    numArray[1] |= (byte) (((int) byteList[0] & 1) << 4);
    numArray[1] |= (byte) (((int) byteList[1] & 240 /*0xF0*/) >> 4);
    numArray[2] |= (byte) (((int) byteList[1] & 8) << 3);
    numArray[2] |= (byte) (((int) byteList[1] & 4) << 2);
    numArray[2] |= (byte) (((int) byteList[1] & 3) << 2);
    numArray[2] |= (byte) (((int) byteList[2] & 192 /*0xC0*/) >> 6);
    numArray[3] |= (byte) (((int) byteList[2] & 32 /*0x20*/) << 1);
    numArray[3] |= (byte) ((uint) byteList[2] & 31U /*0x1F*/);
    for (int index = 0; index < 4; ++index)
    {
      if (numArray[index] == (byte) 0)
        numArray[index] = (byte) 32 /*0x20*/;
      label += ((char) numArray[index]).ToString();
    }
    return label;
  }

  public static byte[] Label2Tag(string Label)
  {
    byte[] numArray1 = new byte[3];
    while (Label.Length < 4)
      Label += "\0";
    if (Label.Length > 4)
      Label = Label.Substring(0, 4);
    byte[] numArray2 = new byte[4];
    for (int index = 0; index < 4; ++index)
      numArray2[index] = (byte) Label[index];
    numArray1[0] |= (byte) (((int) numArray2[0] & 64 /*0x40*/) << 1);
    numArray1[0] |= (byte) (((int) numArray2[0] & 16 /*0x10*/) << 2);
    numArray1[0] |= (byte) (((int) numArray2[0] & 15) << 2);
    numArray1[0] |= (byte) (((int) numArray2[1] & 64 /*0x40*/) >> 5);
    numArray1[0] |= (byte) (((int) numArray2[1] & 16 /*0x10*/) >> 4);
    numArray1[1] |= (byte) (((int) numArray2[1] & 15) << 4);
    numArray1[1] |= (byte) (((int) numArray2[2] & 64 /*0x40*/) >> 3);
    numArray1[1] |= (byte) (((int) numArray2[2] & 16 /*0x10*/) >> 2);
    numArray1[1] |= (byte) (((int) numArray2[2] & 12) >> 2);
    numArray1[2] |= (byte) (((int) numArray2[2] & 3) << 6);
    numArray1[2] |= (byte) (((int) numArray2[3] & 64 /*0x40*/) >> 1);
    numArray1[2] |= (byte) ((uint) numArray2[3] & 31U /*0x1F*/);
    return numArray1;
  }

  public static byte[] Label2TagXYU(string Label)
  {
    byte[] destinationArray = new byte[3];
    uint word = 0;
    for (int index = 0; index < 4; ++index)
    {
      if (Label.Length - 1 < index)
        Label += "\0";
      word |= Label[index] == char.MinValue ? 0U : (uint) ((32 /*0x20*/ | (int) Label[index] & 31 /*0x1F*/) << (3 - index) * 6);
    }
    Array.Copy((Array) BitConverter.GetBytes(Blaze.SwapBytes(word) >> 8), (Array) destinationArray, 3);
    return destinationArray;
  }

  public static uint SwapBytes(uint word)
  {
    return (uint) ((int) (word >> 24) & (int) byte.MaxValue | (int) (word >> 8) & 65280 | (int) word << 8 & 16711680 /*0xFF0000*/ | (int) word << 24 & -16777216 /*0xFF000000*/);
  }

  public static long DecompressInteger1(BinaryReader s)
  {
    List<byte> byteList = new List<byte>();
    byte num1;
    while ((num1 = s.ReadByte()) >= (byte) 128 /*0x80*/)
      byteList.Add(num1);
    byteList.Add(num1);
    ulong num2 = (ulong) ((int) byteList[0] & 63 /*0x3F*/);
    for (int index = 1; index < byteList.Count; ++index)
      num2 |= (ulong) ((int) byteList[index] & (int) sbyte.MaxValue) << index * 7 - 1;
    return (long) num2;
  }

  public static long DecompressInteger(BinaryReader s)
  {
    List<byte> byteList = new List<byte>();
    byte num1;
    while ((num1 = s.ReadByte()) >= (byte) 128 /*0x80*/)
      byteList.Add(num1);
    byteList.Add(num1);
    byte[] array = byteList.ToArray();
    int num2 = 6;
    ulong num3 = (ulong) ((int) array[0] & 63 /*0x3F*/);
    for (int index = 1; index < array.Length; ++index)
    {
      ulong num4 = (ulong) ((int) array[index] & (int) sbyte.MaxValue) << num2;
      num3 |= num4;
      num2 += 7;
    }
    return (long) num3;
  }

  public static void CompressInteger(long l, Stream s)
  {
    List<byte> byteList = new List<byte>();
    if (l < 64L /*0x40*/)
    {
      byteList.Add((byte) ((ulong) l & (ulong) byte.MaxValue));
    }
    else
    {
      byte num1 = (byte) ((ulong) (l & 63L /*0x3F*/) | 128UL /*0x80*/);
      byteList.Add(num1);
      long num2 = l >> 6;
      while (num2 >= 128L /*0x80*/)
      {
        byte num3 = (byte) ((ulong) (num2 & (long) sbyte.MaxValue) | 128UL /*0x80*/);
        num2 >>= 7;
        byteList.Add(num3);
      }
      byteList.Add((byte) num2);
    }
    foreach (byte num in byteList)
      s.WriteByte(num);
  }

  public static string ReadString(BinaryReader s)
  {
    int num1 = (int) Blaze.DecompressInteger(s);
    string str = "";
    for (int index = 0; index < num1 - 1; ++index)
      str += ((char) s.ReadByte()).ToString();
    int num2 = (int) s.ReadByte();
    return str;
  }

  public static void WriteString(string str, Stream s)
  {
    int l = !str.EndsWith("\0") ? str.Length + 1 : str.Length;
    Blaze.CompressInteger((long) l, s);
    for (int index = 0; index < l - 1; ++index)
      s.WriteByte((byte) str[index]);
    s.WriteByte((byte) 0);
  }

  public static byte[] StringToByteArray(string hex)
  {
    return Enumerable.Range(0, hex.Length).Where<int>((Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16 /*0x10*/))).ToArray<byte>();
  }

  public static byte[] PacketToRaw(Blaze.Packet p)
  {
    List<byte> byteList = new List<byte>();
    byteList.Add((byte) ((uint) p.Length >> 8));
    byteList.Add((byte) ((uint) p.Length & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) p.Component >> 8));
    byteList.Add((byte) ((uint) p.Component & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) p.Command >> 8));
    byteList.Add((byte) ((uint) p.Command & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) p.Error >> 8));
    byteList.Add((byte) ((uint) p.Error & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) p.QType >> 8));
    byteList.Add((byte) ((uint) p.QType & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) p.ID >> 8));
    byteList.Add((byte) ((uint) p.ID & (uint) byte.MaxValue));
    if (((uint) p.QType & 16U /*0x10*/) > 0U)
    {
      byteList.Add((byte) ((uint) p.extLength >> 8));
      byteList.Add((byte) ((uint) p.extLength & (uint) byte.MaxValue));
    }
    byteList.AddRange((IEnumerable<byte>) p.Content);
    return byteList.ToArray();
  }

  public static byte[] CreatePacket1(
    ushort Component,
    ushort Command,
    int Error,
    ushort QType,
    ushort ID,
    List<Blaze.Tdf> Content)
  {
    List<byte> byteList = new List<byte>();
    byteList.Add((byte) 0);
    byteList.Add((byte) 0);
    byteList.Add((byte) ((uint) Component >> 8));
    byteList.Add((byte) ((uint) Component & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) Command >> 8));
    byteList.Add((byte) ((uint) Command & (uint) byte.MaxValue));
    byteList.Add((byte) (Error >> 8));
    byteList.Add((byte) (Error & (int) byte.MaxValue));
    byteList.Add((byte) ((uint) QType >> 8));
    byteList.Add((byte) ((uint) QType & (uint) byte.MaxValue));
    byteList.Add((byte) ((uint) ID >> 8));
    byteList.Add((byte) ((uint) ID & (uint) byte.MaxValue));
    MemoryStream s = new MemoryStream();
    foreach (Blaze.Tdf tdf in Content)
      Blaze.WriteTdf(tdf, (Stream) s);
    int length = (int) s.Length;
    byteList[0] = (byte) ((length & (int) ushort.MaxValue) >> 8);
    byteList[1] = (byte) (length & (int) byte.MaxValue);
    if (length > (int) ushort.MaxValue)
    {
      byteList[9] = (byte) 16 /*0x10*/;
      byteList.Add((byte) (((long) length & -16777216L) >> 24));
      byteList.Add((byte) ((length & 16711680 /*0xFF0000*/) >> 16 /*0x10*/));
    }
    else
      byteList[9] = (byte) 0;
    byteList.AddRange((IEnumerable<byte>) s.ToArray());
    return byteList.ToArray();
  }

  public static byte[] CreatePacket(
    ushort Component,
    ushort Command,
    int Error,
    ushort QType,
    ushort ID,
    List<Blaze.Tdf> Content)
  {
    MemoryStream memoryStream1 = new MemoryStream();
    MemoryStream s = new MemoryStream();
    foreach (Blaze.Tdf tdf in Content)
      Blaze.WriteTdf(tdf, (Stream) s);
    int length = (int) s.Length;
    MemoryStream memoryStream2 = new MemoryStream();
    memoryStream2.WriteByte((byte) ((length & (int) ushort.MaxValue) >> 8));
    memoryStream2.WriteByte((byte) (length & (int) byte.MaxValue));
    memoryStream2.WriteByte((byte) ((uint) Component >> 8));
    memoryStream2.WriteByte((byte) ((uint) Component & (uint) byte.MaxValue));
    memoryStream2.WriteByte((byte) ((uint) Command >> 8));
    memoryStream2.WriteByte((byte) ((uint) Command & (uint) byte.MaxValue));
    memoryStream2.WriteByte((byte) (Error >> 8));
    memoryStream2.WriteByte((byte) (Error & (int) byte.MaxValue));
    memoryStream2.WriteByte((byte) ((uint) QType >> 8));
    if (length > (int) ushort.MaxValue)
      memoryStream2.WriteByte((byte) 16 /*0x10*/);
    else
      memoryStream2.WriteByte((byte) 0);
    memoryStream2.WriteByte((byte) ((uint) ID >> 8));
    memoryStream2.WriteByte((byte) ((uint) ID & (uint) byte.MaxValue));
    if (length > (int) ushort.MaxValue)
    {
      memoryStream2.WriteByte((byte) (((long) length & -16777216L) >> 24));
      memoryStream2.WriteByte((byte) ((length & 16711680 /*0xFF0000*/) >> 16 /*0x10*/));
    }
    memoryStream2.ToArray();
    memoryStream1.Write(memoryStream2.ToArray(), 0, memoryStream2.ToArray().Length);
    memoryStream1.Write(s.ToArray(), 0, s.ToArray().Length);
    return memoryStream1.ToArray();
  }

  public static Blaze.TdfStruct CreateStructStub(List<Blaze.Tdf> tdfs, bool has2 = false)
  {
    return new Blaze.TdfStruct()
    {
      Values = tdfs,
      startswith2 = has2
    };
  }

  public static Blaze.Tdf ReadTdf(BinaryReader s)
  {
    Blaze.Tdf head = new Blaze.Tdf();
    uint num = Blaze.ReadUInt(s);
    head.Tag = num & 4294967040U;
    head.Label = Blaze.TagToLabel(head.Tag);
    head.Type = (byte) (num & (uint) byte.MaxValue);
    switch (head.Type)
    {
      case 0:
      case 6:
        return (Blaze.Tdf) Blaze.ReadTdfInteger(head, s);
      case 1:
        return (Blaze.Tdf) Blaze.ReadTdfString(head, s);
      case 2:
        return (Blaze.Tdf) Blaze.ReadTdfBlob(head, s);
      case 3:
        return (Blaze.Tdf) Blaze.ReadTdfStruct(head, s);
      case 4:
        return (Blaze.Tdf) Blaze.ReadTdfList(head, s);
      case 5:
        return (Blaze.Tdf) Blaze.ReadTdfDoubleList(head, s);
      case 7:
        return (Blaze.Tdf) Blaze.ReadTdfIntegerList(head, s);
      case 8:
        return (Blaze.Tdf) Blaze.ReadTdfDoubleVal(head, s);
      case 9:
        return (Blaze.Tdf) Blaze.ReadTdfTrippleVal(head, s);
      case 10:
        return (Blaze.Tdf) Blaze.ReadTdfFloat(head, s);
      default:
        throw new Exception("Unknown Tdf Type: " + head.Type.ToString());
    }
  }

  public static Blaze.TdfFloat ReadTdfFloat(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfFloat tdfFloat = new Blaze.TdfFloat();
    tdfFloat.Label = head.Label;
    tdfFloat.Tag = head.Tag;
    tdfFloat.Type = head.Type;
    byte[] buffer = new byte[4];
    s.Read(buffer, 0, 4);
    tdfFloat.Value = BitConverter.ToSingle(buffer, 0);
    return tdfFloat;
  }

  public static Blaze.TdfBlob ReadTdfBlob(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfBlob tdfBlob = new Blaze.TdfBlob();
    tdfBlob.Label = head.Label;
    tdfBlob.Tag = head.Tag;
    tdfBlob.Type = head.Type;
    tdfBlob.Length = Blaze.DecompressInteger(s);
    tdfBlob.data = new byte[tdfBlob.Length];
    for (int index = 0; (long) index < tdfBlob.Length; ++index)
      tdfBlob.data[index] = s.ReadByte();
    return tdfBlob;
  }

  public static Blaze.TdfInteger ReadTdfInteger(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfInteger tdfInteger = new Blaze.TdfInteger();
    tdfInteger.Label = head.Label;
    tdfInteger.Tag = head.Tag;
    tdfInteger.Type = head.Type;
    tdfInteger.Value = Blaze.DecompressInteger(s);
    return tdfInteger;
  }

  public static Blaze.TdfString ReadTdfString(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfString tdfString = new Blaze.TdfString();
    tdfString.Label = head.Label;
    tdfString.Tag = head.Tag;
    tdfString.Type = head.Type;
    tdfString.Value = Blaze.ReadString(s);
    return tdfString;
  }

  public static Blaze.TdfStruct ReadTdfStruct(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfStruct tdfStruct = new Blaze.TdfStruct();
    tdfStruct.Label = head.Label;
    tdfStruct.Tag = head.Tag;
    tdfStruct.Type = head.Type;
    bool has2 = false;
    tdfStruct.Values = Blaze.ReadStruct(s, out has2);
    tdfStruct.startswith2 = has2;
    return tdfStruct;
  }

  public static Blaze.TdfTrippleVal ReadTdfTrippleVal(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfTrippleVal tdfTrippleVal = new Blaze.TdfTrippleVal();
    tdfTrippleVal.Label = head.Label;
    tdfTrippleVal.Tag = head.Tag;
    tdfTrippleVal.Type = head.Type;
    tdfTrippleVal.Value = Blaze.ReadTrippleVal(s);
    return tdfTrippleVal;
  }

  public static Blaze.TdfDoubleVal ReadTdfDoubleVal(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfDoubleVal tdfDoubleVal = new Blaze.TdfDoubleVal();
    tdfDoubleVal.Label = head.Label;
    tdfDoubleVal.Tag = head.Tag;
    tdfDoubleVal.Type = head.Type;
    tdfDoubleVal.Value = Blaze.ReadDoubleVal(s);
    return tdfDoubleVal;
  }

  public static Blaze.TdfList ReadTdfList(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfList tdfList = new Blaze.TdfList();
    tdfList.Label = head.Label;
    tdfList.Tag = head.Tag;
    tdfList.Type = head.Type;
    tdfList.SubType = s.ReadByte();
    tdfList.Count = (int) Blaze.DecompressInteger(s);
    for (int index = 0; index < tdfList.Count; ++index)
    {
      switch (tdfList.SubType)
      {
        case 0:
          if (tdfList.List == null)
            tdfList.List = (object) new List<long>();
          List<long> list1 = (List<long>) tdfList.List;
          list1.Add(Blaze.DecompressInteger(s));
          tdfList.List = (object) list1;
          break;
        case 1:
          if (tdfList.List == null)
            tdfList.List = (object) new List<string>();
          List<string> list2 = (List<string>) tdfList.List;
          list2.Add(Blaze.ReadString(s));
          tdfList.List = (object) list2;
          break;
        case 3:
          if (tdfList.List == null)
            tdfList.List = (object) new List<Blaze.TdfStruct>();
          List<Blaze.TdfStruct> list3 = (List<Blaze.TdfStruct>) tdfList.List;
          Blaze.TdfStruct tdfStruct = new Blaze.TdfStruct()
          {
            startswith2 = false
          };
          tdfStruct.Values = Blaze.ReadStruct(s, out tdfStruct.startswith2);
          list3.Add(tdfStruct);
          tdfList.List = (object) list3;
          break;
        case 9:
          if (tdfList.List == null)
            tdfList.List = (object) new List<Blaze.TrippleVal>();
          List<Blaze.TrippleVal> list4 = (List<Blaze.TrippleVal>) tdfList.List;
          list4.Add(Blaze.ReadTrippleVal(s));
          tdfList.List = (object) list4;
          break;
        default:
          throw new Exception("Unknown Tdf Type in List: " + tdfList.Type.ToString());
      }
    }
    return tdfList;
  }

  public static Blaze.TdfIntegerList ReadTdfIntegerList(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfIntegerList tdfIntegerList = new Blaze.TdfIntegerList();
    tdfIntegerList.Label = head.Label;
    tdfIntegerList.Tag = head.Tag;
    tdfIntegerList.Type = head.Type;
    tdfIntegerList.Count = (int) Blaze.DecompressInteger(s);
    for (int index = 0; index < tdfIntegerList.Count; ++index)
    {
      if (tdfIntegerList.List == null)
        tdfIntegerList.List = new List<long>();
      List<long> list = tdfIntegerList.List;
      list.Add(Blaze.DecompressInteger(s));
      tdfIntegerList.List = list;
    }
    return tdfIntegerList;
  }

  public static Blaze.TdfDoubleList ReadTdfDoubleList(Blaze.Tdf head, BinaryReader s)
  {
    Blaze.TdfDoubleList tdfDoubleList = new Blaze.TdfDoubleList();
    tdfDoubleList.Label = head.Label;
    tdfDoubleList.Tag = head.Tag;
    tdfDoubleList.Type = head.Type;
    tdfDoubleList.SubType1 = s.ReadByte();
    tdfDoubleList.SubType2 = s.ReadByte();
    tdfDoubleList.Count = (int) Blaze.DecompressInteger(s);
    for (int index = 0; index < tdfDoubleList.Count; ++index)
    {
      switch (tdfDoubleList.SubType1)
      {
        case 0:
          if (tdfDoubleList.List1 == null)
            tdfDoubleList.List1 = (object) new List<long>();
          List<long> list1_1 = (List<long>) tdfDoubleList.List1;
          list1_1.Add(Blaze.DecompressInteger(s));
          tdfDoubleList.List1 = (object) list1_1;
          break;
        case 1:
          if (tdfDoubleList.List1 == null)
            tdfDoubleList.List1 = (object) new List<string>();
          List<string> list1_2 = (List<string>) tdfDoubleList.List1;
          list1_2.Add(Blaze.ReadString(s));
          tdfDoubleList.List1 = (object) list1_2;
          break;
        case 3:
          if (tdfDoubleList.List1 == null)
            tdfDoubleList.List1 = (object) new List<Blaze.TdfStruct>();
          List<Blaze.TdfStruct> list1_3 = (List<Blaze.TdfStruct>) tdfDoubleList.List1;
          Blaze.TdfStruct tdfStruct1 = new Blaze.TdfStruct()
          {
            startswith2 = false
          };
          tdfStruct1.Values = Blaze.ReadStruct(s, out tdfStruct1.startswith2);
          list1_3.Add(tdfStruct1);
          tdfDoubleList.List1 = (object) list1_3;
          break;
        case 10:
          if (tdfDoubleList.List1 == null)
            tdfDoubleList.List1 = (object) new List<float>();
          List<float> list1_4 = (List<float>) tdfDoubleList.List1;
          list1_4.Add(Blaze.ReadFloat(s));
          tdfDoubleList.List1 = (object) list1_4;
          break;
        default:
          throw new Exception("Unknown Tdf Type in Double List: " + tdfDoubleList.SubType1.ToString());
      }
      switch (tdfDoubleList.SubType2)
      {
        case 0:
          if (tdfDoubleList.List2 == null)
            tdfDoubleList.List2 = (object) new List<long>();
          List<long> list2_1 = (List<long>) tdfDoubleList.List2;
          list2_1.Add(Blaze.DecompressInteger(s));
          tdfDoubleList.List2 = (object) list2_1;
          break;
        case 1:
          if (tdfDoubleList.List2 == null)
            tdfDoubleList.List2 = (object) new List<string>();
          List<string> list2_2 = (List<string>) tdfDoubleList.List2;
          list2_2.Add(Blaze.ReadString(s));
          tdfDoubleList.List2 = (object) list2_2;
          break;
        case 3:
          if (tdfDoubleList.List2 == null)
            tdfDoubleList.List2 = (object) new List<Blaze.TdfStruct>();
          List<Blaze.TdfStruct> list2_3 = (List<Blaze.TdfStruct>) tdfDoubleList.List2;
          Blaze.TdfStruct tdfStruct2 = new Blaze.TdfStruct()
          {
            startswith2 = false
          };
          tdfStruct2.Values = Blaze.ReadStruct(s, out tdfStruct2.startswith2);
          list2_3.Add(tdfStruct2);
          tdfDoubleList.List2 = (object) list2_3;
          break;
        case 10:
          if (tdfDoubleList.List2 == null)
            tdfDoubleList.List2 = (object) new List<float>();
          List<float> list2_4 = (List<float>) tdfDoubleList.List2;
          list2_4.Add(Blaze.ReadFloat(s));
          tdfDoubleList.List2 = (object) list2_4;
          break;
        default:
          throw new Exception("Unknown Tdf Type in Double List: " + tdfDoubleList.SubType2.ToString());
      }
    }
    return tdfDoubleList;
  }

  public static List<Blaze.Tdf> ReadStruct(BinaryReader s, out bool has2)
  {
    List<Blaze.Tdf> tdfList = new List<Blaze.Tdf>();
    bool flag = false;
    byte num;
    while ((num = s.ReadByte()) > (byte) 0)
    {
      if (num != (byte) 2)
        s.BaseStream.Seek(-1L, SeekOrigin.Current);
      else
        flag = true;
      tdfList.Add(Blaze.ReadTdf(s));
    }
    has2 = flag;
    return tdfList;
  }

  public static List<Blaze.Tdf> ReadPacketContent(Blaze.Packet p)
  {
    List<Blaze.Tdf> tdfList = new List<Blaze.Tdf>();
    BinaryReader s = new BinaryReader((Stream) new MemoryStream(p.Content));
    s.BaseStream.Seek(0L, SeekOrigin.Begin);
    try
    {
      while (s.BaseStream.Position < s.BaseStream.Length - 4L)
        tdfList.Add(Blaze.ReadTdf(s));
    }
    catch (Exception ex)
    {
    }
    return tdfList;
  }

  public static Blaze.DoubleVal ReadDoubleVal(BinaryReader s)
  {
    return new Blaze.DoubleVal()
    {
      v1 = Blaze.DecompressInteger(s),
      v2 = Blaze.DecompressInteger(s)
    };
  }

  public static Blaze.TrippleVal ReadTrippleVal(BinaryReader s)
  {
    return new Blaze.TrippleVal()
    {
      v1 = Blaze.DecompressInteger(s),
      v2 = Blaze.DecompressInteger(s),
      v3 = Blaze.DecompressInteger(s)
    };
  }

  public static void WriteTdf(Blaze.Tdf tdf, Stream s)
  {
    s.WriteByte((byte) (tdf.Tag >> 24));
    s.WriteByte((byte) (tdf.Tag >> 16 /*0x10*/));
    s.WriteByte((byte) (tdf.Tag >> 8));
    s.WriteByte(tdf.Type);
    switch (tdf.Type)
    {
      case 0:
      case 6:
        Blaze.CompressInteger(((Blaze.TdfInteger) tdf).Value, s);
        break;
      case 1:
        Blaze.WriteString(((Blaze.TdfString) tdf).Value, s);
        break;
      case 2:
        Blaze.TdfBlob tdfBlob = (Blaze.TdfBlob) tdf;
        Blaze.CompressInteger(tdfBlob.Length, s);
        s.Write(tdfBlob.data, 0, (int) tdfBlob.Length);
        break;
      case 3:
        Blaze.TdfStruct tdfStruct = (Blaze.TdfStruct) tdf;
        if (tdfStruct.startswith2)
          s.WriteByte((byte) 2);
        foreach (Blaze.Tdf tdf1 in tdfStruct.Values)
          Blaze.WriteTdf(tdf1, s);
        s.WriteByte((byte) 0);
        break;
      case 4:
        Blaze.WriteTdfList((Blaze.TdfList) tdf, s);
        break;
      case 5:
        Blaze.WriteTdfDoubleList((Blaze.TdfDoubleList) tdf, s);
        break;
      case 7:
        Blaze.TdfIntegerList tdfIntegerList = (Blaze.TdfIntegerList) tdf;
        Blaze.CompressInteger((long) tdfIntegerList.Count, s);
        if (tdfIntegerList.Count == 0)
          break;
        using (List<long>.Enumerator enumerator = tdfIntegerList.List.GetEnumerator())
        {
          while (enumerator.MoveNext())
            Blaze.CompressInteger(enumerator.Current, s);
          break;
        }
      case 8:
        Blaze.WriteDoubleValue(((Blaze.TdfDoubleVal) tdf).Value, s);
        break;
      case 9:
        Blaze.WriteTrippleValue(((Blaze.TdfTrippleVal) tdf).Value, s);
        break;
      case 10:
        Blaze.TdfFloat tdfFloat = (Blaze.TdfFloat) tdf;
        Blaze.WriteFloat(s, tdfFloat.Value);
        break;
    }
  }

  public static void WriteTdfList(Blaze.TdfList tdf, Stream s)
  {
    s.WriteByte(tdf.SubType);
    Blaze.CompressInteger((long) tdf.Count, s);
    for (int index = 0; index < tdf.Count; ++index)
    {
      switch (tdf.SubType)
      {
        case 0:
          Blaze.CompressInteger(((List<long>) tdf.List)[index], s);
          break;
        case 1:
          Blaze.WriteString(((List<string>) tdf.List)[index], s);
          break;
        case 3:
          Blaze.TdfStruct tdfStruct = ((List<Blaze.TdfStruct>) tdf.List)[index];
          if (tdfStruct.startswith2)
            s.WriteByte((byte) 2);
          foreach (Blaze.Tdf tdf1 in tdfStruct.Values)
            Blaze.WriteTdf(tdf1, s);
          s.WriteByte((byte) 0);
          break;
        case 9:
          Blaze.WriteTrippleValue(((List<Blaze.TrippleVal>) tdf.List)[index], s);
          break;
      }
    }
  }

  public static void WriteTdfDoubleList(Blaze.TdfDoubleList tdf, Stream s)
  {
    s.WriteByte(tdf.SubType1);
    s.WriteByte(tdf.SubType2);
    Blaze.CompressInteger((long) tdf.Count, s);
    for (int index = 0; index < tdf.Count; ++index)
    {
      byte subType1 = tdf.SubType1;
      switch (subType1)
      {
        case 0:
          Blaze.CompressInteger(((List<long>) tdf.List1)[index], s);
          goto case 2;
        case 1:
          Blaze.WriteString(((List<string>) tdf.List1)[index], s);
          goto case 2;
        case 2:
label_15:
          byte subType2 = tdf.SubType2;
          switch (subType2)
          {
            case 0:
              Blaze.CompressInteger(((List<long>) tdf.List2)[index], s);
              continue;
            case 1:
              Blaze.WriteString(((List<string>) tdf.List2)[index], s);
              continue;
            case 2:
              continue;
            case 3:
              Blaze.TdfStruct tdfStruct1 = ((List<Blaze.TdfStruct>) tdf.List2)[index];
              if (tdfStruct1.startswith2)
                s.WriteByte((byte) 2);
              foreach (Blaze.Tdf tdf1 in tdfStruct1.Values)
                Blaze.WriteTdf(tdf1, s);
              s.WriteByte((byte) 0);
              continue;
            default:
              switch (subType2)
              {
                case 9:
                  Blaze.WriteTrippleValue(((List<Blaze.TrippleVal>) tdf.List2)[index], s);
                  continue;
                case 10:
                  Blaze.WriteFloat(s, ((List<float>) tdf.List2)[index]);
                  continue;
                default:
                  continue;
              }
          }
        case 3:
          Blaze.TdfStruct tdfStruct2 = ((List<Blaze.TdfStruct>) tdf.List1)[index];
          if (tdfStruct2.startswith2)
            s.WriteByte((byte) 2);
          foreach (Blaze.Tdf tdf2 in tdfStruct2.Values)
            Blaze.WriteTdf(tdf2, s);
          s.WriteByte((byte) 0);
          goto case 2;
        default:
          switch (subType1)
          {
            case 9:
              Blaze.WriteTrippleValue(((List<Blaze.TrippleVal>) tdf.List1)[index], s);
              goto label_15;
            case 10:
              Blaze.WriteFloat(s, ((List<float>) tdf.List1)[index]);
              goto label_15;
            default:
              goto label_15;
          }
      }
    }
  }

  public static void WriteTrippleValue(Blaze.TrippleVal v, Stream s)
  {
    Blaze.CompressInteger(v.v1, s);
    Blaze.CompressInteger(v.v2, s);
    Blaze.CompressInteger(v.v3, s);
  }

  public static void WriteDoubleValue(Blaze.DoubleVal v, Stream s)
  {
    Blaze.CompressInteger(v.v1, s);
    Blaze.CompressInteger(v.v2, s);
  }

  public static string PacketToDescriber(Blaze.Packet p)
  {
    string str1 = p.Command.ToString("X");
    string str2 = p.Component.ToString("X");
    string[] strArray1 = Blaze.ComponentNames.Split(',');
    string str3 = "";
    foreach (string str4 in strArray1)
    {
      char[] chArray = new char[1]{ '=' };
      string[] strArray2 = str4.Split(chArray);
      if (strArray2.Length == 2 && strArray2[1] == str2)
      {
        str3 = strArray2[0];
        break;
      }
    }
    ushort component = p.Component;
    if (component <= (ushort) 9)
    {
      if (component != (ushort) 1)
      {
        if (component != (ushort) 4)
        {
          switch (component)
          {
            case 7:
              for (int index = 0; index < Blaze.DescComponent7.Length / 2; ++index)
              {
                if (Blaze.DescComponent7[index * 2] == str1)
                  return $"{str3} : {Blaze.DescComponent7[index * 2 + 1]}";
              }
              break;
            case 9:
              for (int index = 0; index < Blaze.DescComponent9.Length / 2; ++index)
              {
                if (Blaze.DescComponent9[index * 2] == str1)
                  return $"{str3} : {Blaze.DescComponent9[index * 2 + 1]}";
              }
              break;
          }
        }
        else
        {
          for (int index = 0; index < Blaze.DescComponent4.Length / 2; ++index)
          {
            if (Blaze.DescComponent4[index * 2] == str1)
              return $"{str3} : {Blaze.DescComponent4[index * 2 + 1]}";
          }
        }
      }
      else
      {
        for (int index = 0; index < Blaze.DescComponent1.Length / 2; ++index)
        {
          if (Blaze.DescComponent1[index * 2] == str1)
            return $"{str3} : {Blaze.DescComponent1[index * 2 + 1]}";
        }
      }
    }
    else if (component <= (ushort) 25)
    {
      if (component != (ushort) 15)
      {
        if (component == (ushort) 25)
        {
          for (int index = 0; index < Blaze.DescComponent19.Length / 2; ++index)
          {
            if (Blaze.DescComponent19[index * 2] == str1)
              return $"{str3} : {Blaze.DescComponent19[index * 2 + 1]}";
          }
        }
      }
      else
      {
        for (int index = 0; index < Blaze.DescComponentF.Length / 2; ++index)
        {
          if (Blaze.DescComponentF[index * 2] == str1)
            return $"{str3} : {Blaze.DescComponentF[index * 2 + 1]}";
        }
      }
    }
    else if (component != (ushort) 28)
    {
      if (component == (ushort) 30722)
      {
        for (int index = 0; index < Blaze.DescComponent7802.Length / 2; ++index)
        {
          if (Blaze.DescComponent7802[index * 2] == str1)
            return $"{str3} : {Blaze.DescComponent7802[index * 2 + 1]}";
        }
      }
    }
    else
    {
      for (int index = 0; index < Blaze.DescComponent1C.Length / 2; ++index)
      {
        if (Blaze.DescComponent1C[index * 2] == str1)
          return $"{str3} : {Blaze.DescComponent1C[index * 2 + 1]}";
      }
    }
    return $"{str3} : {p.Command.ToString("X")}";
  }

  public class Packet
  {
    public ushort Length;
    public ushort Component;
    public ushort Command;
    public ushort Error;
    public ushort QType;
    public ushort ID;
    public ushort extLength;
    public byte[] Content;
  }

  public struct DoubleVal(long V1, long V2)
  {
    public long v1 = V1;
    public long v2 = V2;
  }

  public struct TrippleVal(long V1, long V2, long V3)
  {
    public long v1 = V1;
    public long v2 = V2;
    public long v3 = V3;
  }

  public class Tdf
  {
    public string Label;
    public uint Tag;
    public byte Type;

    public TreeNode ToTree() => new TreeNode($"{this.Label} : {this.Type.ToString()}");

    public void Set(string label, byte type)
    {
      this.Label = label;
      this.Type = type;
      this.Tag = 0U;
      byte[] numArray = Blaze.Label2Tag(label);
      this.Tag |= (uint) numArray[0] << 24;
      this.Tag |= (uint) numArray[1] << 16 /*0x10*/;
      this.Tag |= (uint) numArray[2] << 8;
    }
  }

  public class TdfInteger : Blaze.Tdf
  {
    public long Value;

    public static Blaze.TdfInteger Create(string Label, long value)
    {
      Blaze.TdfInteger tdfInteger = new Blaze.TdfInteger();
      tdfInteger.Set(Label, (byte) 0);
      tdfInteger.Value = value;
      return tdfInteger;
    }
  }

  public class TdfBlob : Blaze.Tdf
  {
    public long Length;
    public byte[] data;

    public static Blaze.TdfBlob Create(string Label, byte[] buff)
    {
      Blaze.TdfBlob tdfBlob = new Blaze.TdfBlob();
      tdfBlob.Set(Label, (byte) 2);
      tdfBlob.Length = (long) buff.Length;
      tdfBlob.data = buff;
      return tdfBlob;
    }
  }

  public class TdfFloat : Blaze.Tdf
  {
    public float Value;

    public static Blaze.TdfFloat Create(string Label, float value)
    {
      Blaze.TdfFloat tdfFloat = new Blaze.TdfFloat();
      tdfFloat.Set(Label, (byte) 10);
      tdfFloat.Value = value;
      return tdfFloat;
    }
  }

  public class TdfString : Blaze.Tdf
  {
    public string Value;

    public static Blaze.TdfString Create(string Label, string value)
    {
      Blaze.TdfString tdfString = new Blaze.TdfString();
      tdfString.Set(Label, (byte) 1);
      tdfString.Value = value;
      return tdfString;
    }
  }

  public class TdfStruct : Blaze.Tdf
  {
    public List<Blaze.Tdf> Values;
    public bool startswith2;

    public static Blaze.TdfStruct Create(string Label, List<Blaze.Tdf> list, bool start2 = false)
    {
      Blaze.TdfStruct tdfStruct = new Blaze.TdfStruct();
      tdfStruct.startswith2 = start2;
      tdfStruct.Set(Label, (byte) 3);
      tdfStruct.Values = list;
      return tdfStruct;
    }
  }

  public class TdfList : Blaze.Tdf
  {
    public byte SubType;
    public int Count;
    public object List;

    public static Blaze.TdfList Create(string Label, byte subtype, int count, object list)
    {
      Blaze.TdfList tdfList = new Blaze.TdfList();
      tdfList.Set(Label, (byte) 4);
      tdfList.SubType = subtype;
      tdfList.Count = count;
      tdfList.List = list;
      return tdfList;
    }
  }

  public class TdfIntegerList : Blaze.Tdf
  {
    public int Count;
    public List<long> List;

    public static Blaze.TdfIntegerList Create(string Label, int count, List<long> list)
    {
      Blaze.TdfIntegerList tdfIntegerList = new Blaze.TdfIntegerList();
      tdfIntegerList.Set(Label, (byte) 7);
      tdfIntegerList.Count = count;
      tdfIntegerList.List = list;
      return tdfIntegerList;
    }
  }

  public class TdfDoubleList : Blaze.Tdf
  {
    public byte SubType1;
    public byte SubType2;
    public int Count;
    public object List1;
    public object List2;

    public static Blaze.TdfDoubleList Create(
      string Label,
      byte subtype1,
      byte subtype2,
      object list1,
      object list2,
      int count)
    {
      Blaze.TdfDoubleList tdfDoubleList = new Blaze.TdfDoubleList();
      tdfDoubleList.Set(Label, (byte) 5);
      tdfDoubleList.SubType1 = subtype1;
      tdfDoubleList.SubType2 = subtype2;
      tdfDoubleList.List1 = list1;
      tdfDoubleList.List2 = list2;
      tdfDoubleList.Count = count;
      return tdfDoubleList;
    }
  }

  public class TdfDoubleVal : Blaze.Tdf
  {
    public Blaze.DoubleVal Value;

    public static Blaze.TdfDoubleVal Create(string Label, Blaze.DoubleVal v)
    {
      Blaze.TdfDoubleVal tdfDoubleVal = new Blaze.TdfDoubleVal();
      tdfDoubleVal.Set(Label, (byte) 8);
      tdfDoubleVal.Value = v;
      return tdfDoubleVal;
    }
  }

  public class TdfTrippleVal : Blaze.Tdf
  {
    public Blaze.TrippleVal Value;

    public static Blaze.TdfTrippleVal Create(string Label, Blaze.TrippleVal v)
    {
      Blaze.TdfTrippleVal tdfTrippleVal = new Blaze.TdfTrippleVal();
      tdfTrippleVal.Set(Label, (byte) 9);
      tdfTrippleVal.Value = v;
      return tdfTrippleVal;
    }
  }
}
