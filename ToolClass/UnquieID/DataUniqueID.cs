using Godot;
using System;
using System.Xml;

public partial class DataUniqueID 
{
	public override bool Equals(object obj)
	{
    if (obj is DataUniqueID other)
    {
        return this.getFullName() == other.getFullName();
    }
    return false;
	}

	public override int GetHashCode()
	{
		return getFullName().GetHashCode();
	}
	public enum DataUniqueIDEnum
	{
		DLC, 
		Character,
		Weapon,
		Item,
		Map
	}
	private string DLC_Folder_Name; // Only DLC folder Name, Not DLC Name.
	private DataUniqueIDEnum Type;
	private string This_Name; 
	public DataUniqueID(string DLC_Folder_Name, DataUniqueIDEnum dataUniqueIDEnum, string this_name)
	{
		this.DLC_Folder_Name = DLC_Folder_Name.Trim();
        this.This_Name = this_name.Trim();
        this.Type = dataUniqueIDEnum;
		
	}
	public static DataUniqueID fullNameToUniqueID(string full_name)
	{
		string[] parts = full_name.Split('.');
		if (parts.Length != 3)
		{
			throw new ArgumentException("Invalid full name format. Expected format: 'DLC_Folder_Name.Type.This_Name'");
		}

		string dlcName = parts[0];
		if (!Enum.TryParse(parts[1], out DataUniqueIDEnum type))
		{
			throw new ArgumentException($"Invalid type: {parts[1]}");
		}
		string thisName = parts[2];

		return new DataUniqueID(dlcName, type, thisName);
	}
	public static string UniqueIDToFullName(DataUniqueID id)
	{
		return $"{id.getDLCName()}.{id.getTypeName()}.{id.getThisName()}";
	}
	public string getDLCName()
	{
		return DLC_Folder_Name;
	}
	public DataUniqueIDEnum getTypeName()
	{
		return Type;
	}
	public string getThisName()
	{
		return This_Name;
	}
	public string getFullName()
	{
		return $"{DLC_Folder_Name}.{Type.ToString()}.{This_Name}";
	}
}
