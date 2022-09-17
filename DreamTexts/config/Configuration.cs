using ServerSync;
using Service;
namespace Plugin;
public class Configuration {
#nullable disable
  public static CustomSyncedValue<string> valueDreamData;
#nullable enable
  public static void Init(ConfigWrapper wrapper) {
    valueDreamData = wrapper.AddValue("dream_data");
  }
}
