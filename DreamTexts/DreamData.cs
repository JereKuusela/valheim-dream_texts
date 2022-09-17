using System.ComponentModel;
namespace Plugin;

public class DreamData {
  public string text = "";
  [DefaultValue(0.1f)]
  public float chance = 0.1f;
  public string[] trueKeys = new string[0];
  public string[] falseKeys = new string[0];
}
