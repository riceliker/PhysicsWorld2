using Godot;
using System;
/*
    This class will store setting config in the `user://setting_config.json`
    When the user exit and boot again, the setting will stay here.
*/


public static class SettingConfig
{
    private static readonly string CONFIG_PATH = "user://setting_config.json";

    public static bool DebugSafeGDScriptOnly { get; set; } = true; 
    public static string Language { get; set; } = "zh_CN";

    public static void Load()
    {
        if (!FileAccess.FileExists(CONFIG_PATH))
        {
            Save(); 
            return;
        }

        try
        {
            using var file = FileAccess.Open(CONFIG_PATH, FileAccess.ModeFlags.Read);
            string json = file.GetAsText();
            var data = Json.ParseString(json).AsGodotDictionary();

            DebugSafeGDScriptOnly = GetValue(data, "DebugSafeGDScriptOnly", DebugSafeGDScriptOnly);
            Language = GetValue(data, "language", Language);
        }
        catch
        {
            Save(); 
        }
    }

    public static void Save()
    {
        var data = new Godot.Collections.Dictionary
        {
            { "DebugSafeGDScriptOnly", (Variant)DebugSafeGDScriptOnly },
            { "language", (Variant)Language }
        };

        string json = Json.Stringify(data);

        using var file = FileAccess.Open(CONFIG_PATH, FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }

    private static T GetValue<[MustBeVariant] T>(Godot.Collections.Dictionary data, string key, T defaultValue)
    {
        if (data.ContainsKey(key))
        {
            try { return (T)data[key].As<T>(); }
            catch { }
        }
        return defaultValue;
    }
}

