using Godot;
using System.Collections.Generic;

public static partial class DLCInformationPackageFactory
{
	public static Dictionary<string, DLCInformationPackage> DLC_Dictionary = new Dictionary<string, DLCInformationPackage>();
	public static readonly DLCInformationPackage Empty = new();
	public struct DLCInformationPackage
	{
		public string name;
		public string path;
		public bool able;
		public Godot.Collections.Dictionary manifest;
	}

	public static DLCInformationPackage getInformationPackageByName(string name)
	{
		if (string.IsNullOrEmpty(name)) 
    	{
        GD.PushWarning("GetDataFromDLCReader->getInformationPackageByName: NULL NAME");
        return Empty;
    	}
		if (name == "_First")
		{
			return DLCInformationPackageFactory.Empty;
		}
		if (DLC_Dictionary.TryGetValue(name, out var information_package))
		{
			return information_package;
		}
		GD.PushWarning("GetDataFromDLCReader->getInformationPackageByName: Not find this Name(" + name + ")!");
		return Empty;
	}
	public static void addInformationPackage(DLCInformationPackage info_pack)
	{
		if (string.IsNullOrEmpty(info_pack.name))
            return;
        if (!DLC_Dictionary.ContainsKey(info_pack.name))
        {
            DLC_Dictionary.Add(info_pack.name, info_pack);
        }
	}
	
	public static Godot.Collections.Dictionary getManifestByInfoPack(DLCInformationPackageFactory.DLCInformationPackage info_pack)
	{
		string name = info_pack.name;
		string path = info_pack.path;
		string json_string = FileAccess.GetFileAsString(path.PathJoin("manifest.json"));
		Json json = new Json();
		Error err = json.Parse(json_string);
		if (err == Error.Ok)
		{
    		Godot.Collections.Dictionary json_as_dictionary = json.Data.AsGodotDictionary();
			return json_as_dictionary;
		}
		else
		{
			GD.PushWarning("DLC: The manifest file was missed in DLC(" + name + ").");
			return new Godot.Collections.Dictionary();
		}
	}
		

	
}
