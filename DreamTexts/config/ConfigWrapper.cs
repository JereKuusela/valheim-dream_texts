using BepInEx.Configuration;
using ServerSync;

namespace Service;

public class ConfigWrapper
{
  private ConfigFile ConfigFile;
  private ConfigSync ConfigSync;
  public ConfigWrapper(string command, ConfigFile configFile, ConfigSync configSync)
  {
    ConfigFile = configFile;
    ConfigSync = configSync;
  }
  public CustomSyncedValue<string> AddValue(string identifier) => new CustomSyncedValue<string>(ConfigSync, identifier);
}
