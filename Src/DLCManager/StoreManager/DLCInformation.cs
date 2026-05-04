using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;


namespace PhysicsWorld.Src.DLCManager.StoreManager
{
    public class DLCInformation : IGetJsonData, IGetIcon
    {
        public const int _this_framework_api_version = 1;
        public class BaseInformation
        {
            public string DLC_version;
            public List<string> addon_things = new List<string>();
        }
        public BaseInformation base_information = new BaseInformation();
        public class Description
        {
            public string description;
            public string author;
            public string url;
        }
        public string path {get;init;}
        public Description description = new Description();

        public string DLC_folder_name {get;init;}
        public string DLC_name {get;init;}
        public Texture2D icon {get;set;}
        public JsonElement _root {get;set;}
        public JsonDocument _doc {get;set;}

        public DLCInformation(string load_path, string DLC_folder_name)
        {
            (this as IGetJsonData).initJson(load_path, "manifest.json", "DLC manifest");
            int this_api_version = (this as IGetJsonData).getJsonValue<int>("API_Version");
            if (this_api_version != _this_framework_api_version)
                StaticDLCManager.addErrorInformation(DLC_folder_name, "Incorrect DLC API version, which may cause the exception.");

            this.path = load_path;
            this.DLC_folder_name = DLC_folder_name;
            this.DLC_name = (this as IGetJsonData).getJsonValue<string>("DLC_Name");

            (this as IGetIcon).setIcon();

            (this as IGetJsonData).getJsonObject("DLC_Base", (base_data) => 
            {
                base_information.DLC_version = (this as IGetJsonData).getJsonValue<string>(base_data, "DLC_version");
                (this as IGetJsonData).getJsonArray(base_data, "DLC_addon", (count, item) =>
                {
                    base_information.addon_things.Add(item.ToString());
                });

            });


        }
        

        
        
    }
}
