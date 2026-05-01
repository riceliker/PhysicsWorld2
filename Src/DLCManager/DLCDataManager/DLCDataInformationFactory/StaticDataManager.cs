using System;
using System.Collections.Generic;
using Godot;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <Summary>
	/// This static class will be used to store all the information of the game.
    /// And provide the methods to operate the information.
	/// Only the class which inherit from `DLCDataInformation` can be stored in the DataManager.
    /// And the information will be stored in the Dictionary with the key of `DLCDataID`.
    /// </Summary>
    public static class StaticDataManager
    {
        public static class Storage<T> where T : DLCDataInformation
        {
            public static Dictionary<DLCDataID, T> list = new();
            public static IReadOnlyDictionary<DLCDataID, T> getListInStorageButReadOnly()
            {
                return list.AsReadOnly();
            }
        }
        public static void setInformation<T>(DLCDataID id, T information) where T : DLCDataInformation
        {
            Storage<T>.list[id] = information;
        }
        public static IReadOnlyDictionary<DLCDataID, T> getListButReadOnly<T>() where T :DLCDataInformation
        {
            return Storage<T>.getListInStorageButReadOnly();
        }
        public static T getInformation<T>(DLCDataID id) where T : DLCDataInformation
        {
            if (Storage<T>.list.TryGetValue(id, out var information))
            {
                return information;
            }
            return null;
        }
        public static bool isContain<T>(DLCDataID id) where T : DLCDataInformation
        {
            return Storage<T>.list.ContainsKey(id);
        }
        
        public static void addInformation<T>(T information) where T : DLCDataInformation
        {
            if (information == null || information.id == null)
                return;
            if (!Storage<T>.list.ContainsKey(information.id))
            {
                Storage<T>.list.Add(information.id, information);
            }
        }
        public static T StringToEnum<T>(string name, DLCDataID id) where T : struct, Enum
        {
            if(Enum.TryParse<T>(name, out var result))
                return result;
            else
            {	
                GD.PushWarning($"GameInformation Manifest.json: The key word `{name}` in Enum {id.ToString()}  was not found!");
                return default;
            }
        }
    }
}
