using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using UnityEngine;
namespace Plugin;

[HarmonyPatch(typeof(Hud), nameof(Hud.Awake))]
public class SaveData {
  static void Postfix(Hud __instance) {
    if (!Helper.IsServer()) return;
    var text = __instance.m_sleepingProgress.GetComponent<SleepText>();
    DreamManager.ToFile(text);
  }
}
[HarmonyPatch]
public class DreamManager {
  public static string FileName = "dream_texts.yaml";
  public static string FilePath = Path.Combine(Plugin.ConfigPath, FileName);
  public static string Pattern = "dream_texts*.yaml";
  private static Dictionary<string, GameObject> Prefabs = new();
  [HarmonyPatch(typeof(DreamTexts), nameof(DreamTexts.GetRandomDreamText)), HarmonyPriority(Priority.VeryLow)]
  static void Prefix(DreamTexts __instance) {
    if (Dreams.Count > 0)
      __instance.m_texts = Dreams;
  }
  private static List<DreamTexts.DreamText> Dreams = new();
  public static DreamTexts.DreamText FromData(DreamData data) {
    DreamTexts.DreamText dream = new();
    dream.m_text = data.text;
    dream.m_chanceToDream = data.chance;
    dream.m_falseKeys = data.falseKeys.ToList();
    dream.m_trueKeys = data.trueKeys.ToList();
    return dream;
  }
  public static DreamData ToData(DreamTexts.DreamText dream) {
    DreamData data = new();
    data.text = dream.m_text;
    data.chance = dream.m_chanceToDream;
    data.falseKeys = dream.m_falseKeys.ToArray();
    data.trueKeys = dream.m_trueKeys.ToArray();
    return data;
  }

  public static void ToFile(SleepText obj) {
    if (!Helper.IsServer()) return;
    if (File.Exists(FilePath)) return;
    var dreams = obj?.m_dreamTexts?.m_texts;
    if (dreams == null) return;
    var yaml = Data.Serializer().Serialize(dreams.Select(ToData).ToList());
    File.WriteAllText(FilePath, yaml);
    Configuration.valueDreamData.Value = yaml;
  }
  public static void FromFile() {
    if (!Helper.IsServer()) return;
    var yaml = Data.Read(Pattern);
    Configuration.valueDreamData.Value = yaml;
    Set(yaml);
  }
  public static void FromSetting(string yaml) {
    if (Helper.IsClient()) Set(yaml);
  }
  private static void Set(string yaml) {
    if (yaml == "") return;
    try {
      var data = Data.Deserialize<DreamData>(yaml, FileName)
        .Select(FromData).ToList();
      if (data.Count == 0) {
        Plugin.Log.LogWarning($"Failed to load any dream data.");
        return;
      }
      Plugin.Log.LogInfo($"Reloading {data.Count} dream data.");
      Dreams = data;
    } catch (Exception e) {
      Plugin.Log.LogError(e.StackTrace);
    }
  }
  public static void SetupWatcher() {
    Data.SetupWatcher(Pattern, FromFile);
  }
}
