using BepInEx.Configuration;
using ServerSync;

namespace Service;

public class ConfigWrapper
{
  private readonly ConfigSync ConfigSync;
  public ConfigWrapper(ConfigSync configSync)
  {
    ConfigSync = configSync;
  }
  public CustomSyncedValue<string> AddValue(string identifier) => new(ConfigSync, identifier);
}
