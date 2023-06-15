using System.Collections.Generic;
using System.Linq;

namespace DreamTextsPlugin;

public static class Helper
{
  public static bool IsServer() => ZNet.instance && ZNet.instance.IsServer();
  public static bool IsClient() => ZNet.instance && !ZNet.instance.IsServer();
  public static string[] Split(string arg, bool removeEmpty = true, char split = ',') => arg.Split(split).Select(s => s.Trim()).Where(s => !removeEmpty || s != "").ToArray();

  public static List<string> ToList(string str, bool removeEmpty = true) => Split(str, removeEmpty).ToList();
  public static string FromList(IEnumerable<string> array) => string.Join(", ", array);
}

