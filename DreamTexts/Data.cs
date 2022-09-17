
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BepInEx;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Plugin;


public class Data : MonoBehaviour {

  public static void SetupWatcher(string pattern, Action action) {
    FileSystemWatcher watcher = new(Plugin.ConfigPath, pattern);
    watcher.Created += (s, e) => action();
    watcher.Changed += (s, e) => action();
    watcher.Renamed += (s, e) => action();
    watcher.Deleted += (s, e) => action();
    watcher.IncludeSubdirectories = true;
    watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
    watcher.EnableRaisingEvents = true;
  }
  public static IDeserializer Deserializer() => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
    .WithTypeConverter(new FloatConverter()).Build();
  public static IDeserializer DeserializerUnSafe() => new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
  .WithTypeConverter(new FloatConverter()).IgnoreUnmatchedProperties().Build();
  public static ISerializer Serializer() => new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).DisableAliases()
    .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults).WithTypeConverter(new FloatConverter())
      .Build();

  public static List<T> Deserialize<T>(string raw, string fileName) {
    try {
      return Deserializer().Deserialize<List<T>>(raw);
    } catch (Exception ex1) {
      Plugin.Log.LogError($"{fileName}: {ex1.Message}");
      try {
        return DeserializerUnSafe().Deserialize<List<T>>(raw);
      } catch (Exception) {
        return new();
      }
    }
  }


  public static string Read(string pattern) {
    var data = Directory.GetFiles(Plugin.ConfigPath, pattern).Select(name => File.ReadAllText(name));
    return string.Join("\n", data);
  }

}
#nullable disable
public class FloatConverter : IYamlTypeConverter {
  public bool Accepts(Type type) => type == typeof(float);

  public object ReadYaml(IParser parser, Type type) {
    var scalar = (YamlDotNet.Core.Events.Scalar)parser.Current;
    var number = float.Parse(scalar.Value, NumberStyles.Float, CultureInfo.InvariantCulture);
    parser.MoveNext();
    return number;
  }

  public void WriteYaml(IEmitter emitter, object value, Type type) {
    var number = (float)value;
    emitter.Emit(new YamlDotNet.Core.Events.Scalar(number.ToString("0.###", CultureInfo.InvariantCulture)));
  }
}
#nullable enable
