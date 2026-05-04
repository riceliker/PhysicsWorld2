using Godot;
using System;
using System.Text.Json;

namespace PhysicsWorld.Src.DLCManager
{
    /// <summary>
    /// This interface will help you to get JSON key easily
    /// Use `(this as IGetJsonData).initJson();` to init this interface
    /// </summary>
    public interface IGetJsonData
    {
        public string path {get;init;}
        public JsonElement _root {get;set;}
        public JsonDocument _doc {get;set;} 
        /// <summary>
        /// If the _root object was not null.
        /// You must run it firstly.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file_name"></param>
        /// <param name="err"></param>
        public void initJson(string path, string file_name ,string err)
        {
            string json_file_path = path.PathJoin(file_name);
            if (!FileAccess.FileExists(json_file_path))
                GD.PushWarning($"The Json file in {path} was missed.");
            else
            {
                string manifest_file = FileAccess.GetFileAsString(json_file_path);
                _doc = JsonDocument.Parse(manifest_file);
                _root = _doc.RootElement;
            }
        }
        /// <summary>
        /// If you want to get element from Json Easily. Using it is a good choice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from">The parent element in Json</param>
        /// <param name="key">The key in Json</param>
        /// <returns>The type which you want.</returns>
        public T getJsonValue<T>(JsonElement from, string key)
        {
            if (from.TryGetProperty(key, out var valueElem))
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)valueElem.GetString()!;
                }
                else if (typeof(T) == typeof(int))
                {
                    return (T)(object)valueElem.GetInt32();
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T)(object)valueElem.GetSingle();
                }
                else if (typeof(T) == typeof(double))
                {
                    return (T)(object)valueElem.GetDouble();
                }
                else if (typeof(T) == typeof(bool))
                {
                    return (T)(object)valueElem.GetBoolean();
                }
                else
                {
                    GD.PushWarning($"No support: {typeof(T)}");
                    return default;
                }
            }
            else
            {
                GD.PushWarning($"JsonError({key}): It is not found.");
                return default;
            }

            
        }
        public T getJsonValue<T>(string key)
        {
            return getJsonValue<T>(_root, key);
        }
        /// <summary>
        /// If you want to get Array form Json Easily. Using it is a good choice.
        /// Used template:
        /// GetJsonArray("Description", desc => {
        ///     // TODO;
        /// });
        /// </summary>
        /// <param name="from">The parent element in Json</param>
        /// <param name="key">The key in Json</param>
        /// <param name="action">Any operation will be done here</param>
        public void getJsonArray(JsonElement from, string key, Action<int, JsonElement> action)
        {
            JsonElement array = from.GetProperty(key);

            if (array.ValueKind == JsonValueKind.Array)
            {
                int count = 0;
                foreach (var item in array.EnumerateArray())
                {
                    action(count, item);
                }
                count++;
            }
            else
            {
                GD.PushWarning($"JsonError({key}): It is not a array.");
            }
        }
        public void getJsonArray(string key, Action<int, JsonElement> action)
        {
            getJsonArray(_root, key, action);
        }
        /// <summary>
        /// If you want to get object form Json Easily. Using it is a good choice.
        /// Used template:
        /// GetJsonObject("Description", desc => {
        ///     // TODO;
        /// });
        /// </summary>
        /// <param name="from"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public void getJsonObject(JsonElement from, string key, Action<JsonElement> action)
        {
            if (from.TryGetProperty(key, out var dictionary))
            {
                if (dictionary.ValueKind == JsonValueKind.Object)
                {
                    action(dictionary);
                }
                else
                {
                    GD.PushWarning($"JsonError({key}): It is not a object.");
                }
            }
            else
            {
                GD.PushWarning($"JsonError({key}): Key not found.");
            }
            
        }
        public void getJsonObject(string key, Action<JsonElement> action)
        {
            getJsonObject(_root, key, action);
        }
        /// <summary>
        /// If you want to get enum from Json Easily. Using it is a good choice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public T getJsonEnum<T>(JsonElement from, string key) where T : Enum
        {
            string enum_string = getJsonValue<string>(from, key);
            if (Enum.TryParse(typeof(T), enum_string, true, out var result))
            {
                return (T)result;
            }
            else
            {
                GD.PushWarning($"JsonError({key}): It is string, but can not switch enum.");
                return default;
            }
        }
        public T getJsonEnum<T>(string key) where T : Enum
        {
            return getJsonEnum<T>(_root, key);
        }
    }
}
