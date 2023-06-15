using System.ComponentModel;
namespace DreamTextsPlugin;

public class DreamData
{
  public string text = "";
  public float chance = 0.1f;
  [DefaultValue(null)]
  public string[]? trueKeys;
  [DefaultValue(null)]
  public string[]? falseKeys;
  [DefaultValue("")]
  public string requiredKeys = "";
  [DefaultValue("")]
  public string forbiddenKeys = "";
}
