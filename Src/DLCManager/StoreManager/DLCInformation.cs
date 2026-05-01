using Godot;
using System;
using System.Collections.Generic;


namespace PhysicsWorld.Src.DLCManager.StoreManager
{
    
    public class DLCInformation
    {
        public const int _this_framework_api_version = 1;
        public string DLC_name {get;init;}
        public string DLC_folder_name {get;init;}
        public string DLC_version {get;init;}
        public int API_version {get;init;}
        public HashSet<string> addon_things {get;init;}
        public List<string> error_message {get;init;}
        

    }
}
