using Godot;
using System.Collections.Generic;
/*
	This static class will be used to store all the information of the game, and provide the methods to operate the information.
	Only the class which inherit from `GameInformation` can be stored in the DataManager, and the information will be stored in the Dictionary with the key of `DataUniqueID`.
*/

public static class DataManager 
{
	public static class Storage<T> where T : GameInformation
    {
        public static Dictionary<DataUniqueID, T> list = new();
		public static IReadOnlyDictionary<DataUniqueID, T> getListInStorageButReadOnly()
		{
			return list.AsReadOnly();
		}
    }
	public static void setInformation<T>(DataUniqueID id, T information) where T : GameInformation
	{
		Storage<T>.list[id] = information;
	}
	public static IReadOnlyDictionary<DataUniqueID, T> getListButReadOnly<T>() where T : GameInformation
	{
		return Storage<T>.getListInStorageButReadOnly();
	}
	public static T getInformation<T>(DataUniqueID id) where T : GameInformation
	{
		if (Storage<T>.list.TryGetValue(id, out var information))
		{
			return information;
		}
		GD.PrintErr($"DataManager: Not find this Name({id.getFullName()})!");
		return null;
	}
	public static bool isContain<T>(DataUniqueID id) where T : GameInformation
	{
		return Storage<T>.list.ContainsKey(id);
	}
	
	public static void addInformation<T>(T information) where T : GameInformation
	{
		if (information == null || information.getUniqueID() == null)
			return;
		if (!Storage<T>.list.ContainsKey(information.getUniqueID()))
		{
			Storage<T>.list.Add(information.getUniqueID(), information);
			GD.Print($"DataManager: Add information({information.getUniqueID().getFullName()}) to DataManager.");
		}
	}
}
