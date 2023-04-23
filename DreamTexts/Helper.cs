namespace DreamTextsPlugin;

public static class Helper
{
  public static bool IsServer() => ZNet.instance && ZNet.instance.IsServer();
  public static bool IsClient() => ZNet.instance && !ZNet.instance.IsServer();
}

