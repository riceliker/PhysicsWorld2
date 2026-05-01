
using System;
using System.Data.Common;
using System.Text.Json;
using Godot;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <Summary>
    ///	The parent class of all information classes in the InformationManager. 
    ///	It contains the basic information of a game element:
    /// such as its unique ID, its path in the file system.
    /// </Summary>
    public abstract class DLCDataInformation
    {
        public DLCDataID id {get;set;}
        public string path {get;set;}
        public Texture2D icon {get;set;}
        public JsonElement _root {get;set;}
        public Json manifest {get;set;}
        public DLCDataInformation(DLCDataID id, string path)
        {
            this.id = id;
            this.path = path;
        }
        /// <summary>
        /// If you want to get element from Json Easily. Using it is a good choice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from">The parent element in Json</param>
        /// <param name="key">The key in Json</param>
        /// <returns>The type which you want.</returns>
        protected T GetManifestValue<T>(JsonElement from, string key)
        {
            string str = from.GetProperty(key).GetString() ?? $"{key} was not found";

            if (typeof(T) == typeof(string))
                return (T)(object)str;
            try
            {
                return (T)Convert.ChangeType(str, typeof(T));
            }
            catch
            {
                GD.PushWarning("This type can not switch from string");
                return default;
            }
        }
        protected T GetManifestValue<T>(string key)
        {
            return GetManifestValue<T>(_root, key);
        }
        /// <summary>
        /// If you want to get Array form Json Easily. Using it is a good choice.
        /// Used template:
        /// GetManifestArray("Description", desc => {
        ///     // TODO;
        /// });
        /// </summary>
        /// <param name="from">The parent element in Json</param>
        /// <param name="key">The key in Json</param>
        /// <param name="action">Any operation will be done here</param>
        protected void GetManifestArray(JsonElement from, string key, Action<JsonElement> action)
        {
            JsonElement array = from.GetProperty(key);

            if (array.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in array.EnumerateArray())
                {
                    action(item);
                }
            }
        }
        protected void GetManifestArray(string key, Action<JsonElement> action)
        {
            GetManifestArray(_root, key, action);
        }
        private Json setManifest(string path)
        {
            string manifest_file_path = path.PathJoin("manifest.json");
            if (!FileAccess.FileExists(manifest_file_path))
                GD.PushWarning($"DLCData: Important file({manifest_file_path}) in DLC({this.id.ToString()}) is not found!");
            else
            {
                string manifest_file = FileAccess.GetFileAsString(manifest_file_path);
                Json manifest_json = new Json();
                Error error = manifest_json.Parse(manifest_file);
                if (error != Error.Ok)
                    GD.PushWarning($"DLCData: Important file({manifest_file_path}) in DLC({this.id.ToString()}) is not a JSON file!");
                else
                    return manifest;
            }
            return null;
        }
        private Texture2D setIcon(string path)
        {
            string icon_path = path.PathJoin("icon.png");
            if (ResourceLoader.Exists(icon_path))
            {
                Texture2D loadedTex = ResourceLoader.Load<Texture2D>(icon_path);
                Image image_icon = loadedTex.GetImage();
                image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
                return ImageTexture.CreateFromImage(image_icon);
            }
            else
            {
                GD.PushWarning($"DLCData: The icon image in DLC({this.id.ToString()}) was lost!");
                return null;
            }
        }
        /// <summary>
        /// Try get enum by string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        
    }
}
