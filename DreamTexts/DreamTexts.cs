using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Service;

namespace DreamTextsPlugin;
[BepInPlugin(GUID, NAME, VERSION)]
public class DreamTextsPlugin : BaseUnityPlugin
{
  const string GUID = "dream_texts";
  const string NAME = "Dream Texts";
  const string VERSION = "1.6";
#nullable disable
  public static ManualLogSource Log;
#nullable enable
  public static ServerSync.ConfigSync ConfigSync = new(GUID)
  {
    DisplayName = NAME,
    CurrentVersion = VERSION,
    ModRequired = false,
    IsLocked = true
  };
  public static string ConfigPath = "";
  public void Awake()
  {
    Log = Logger;
    ConfigPath = Paths.ConfigPath;
    ConfigWrapper wrapper = new(ConfigSync);
    Configuration.Init(wrapper);
    new Harmony(GUID).PatchAll();
    DreamManager.SetupWatcher();
  }
}

