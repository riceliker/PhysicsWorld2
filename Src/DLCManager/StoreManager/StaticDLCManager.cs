using Godot;
using System;
using System.Collections.Generic;

namespace PhysicsWorld.Src.DLCManager.StoreManager
{
    public static class StaticDLCManager
    {
        private static Dictionary<string, DLCInformation> DLC_list = new Dictionary<string, DLCInformation>();
        public static List<string> error_message = new List<string>();
        public static void addErrorInformation(string DLC_name, string mes)
        {
            string err = $"DLCManager: The DLC:{DLC_name} throw {mes}";
            error_message.Add(err);
        }
        public static void addDLCInformation(string name, DLCInformation info)
        {
            DLC_list.Add(name, info);
        }
        public static void forDLCList(Action<string, DLCInformation> action)
        {
            foreach(string name in DLC_list.Keys)
            {
                DLCInformation info = DLC_list.GetValueOrDefault(name);
                action(name, info);
            }
        }
        public static DLCInformation getDLCInformation(string name)
        {
            if(DLC_list.TryGetValue(name, out var info))
            {
                return info;
            }
            else
            {
                string err = $"DLCManager: The DLC:{name} not found.";
                error_message.Add(err);
                return default;
            }
        }
    }
}
