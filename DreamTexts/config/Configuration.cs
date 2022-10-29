using ServerSync;
using Service;
namespace DreamTextsPlugin;
public class Configuration {
#nullable disable
  public static CustomSyncedValue<string> valueDreamData;
#nullable enable
  public static void Init(ConfigWrapper wrapper) {
    valueDreamData = wrapper.AddValue("dream_data");
    valueDreamData.ValueChanged += () => DreamManager.FromSetting(valueDreamData.Value);

  }
}
