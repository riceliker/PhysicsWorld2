using Godot;
using System;
using System.Collections.Generic;

/*
	This class only one function:
	1. Try to find DLC From folder_path;
	2. Init the information_package Data;
	3. Set data to DLC_List
*/

public partial class GetDLCByLoadPath : RefCounted
{
	private string folder_path;
	public void StartLoad(string folder_path)
	{
		GD.Print($"GetDLCByLoadPath: Start loading from path {folder_path}");
		searchDLCFromDirPath(folder_path);
	}
	private void searchDLCFromDirPath(string folder_path)
	{
		List<string> DLC_folder_list = new List<string>();
		using DirAccess dir = DirAccess.Open(folder_path);
		if (dir == null)
		{
			GD.PrintErr($"DLCReader:: The fold at {folder_path} was not found!");
			return;
		}
        	
		dir.ListDirBegin();
		string file_name;
        while ((file_name = dir.GetNext()) != "")
        {
			if (file_name == "." || file_name == "..")
                continue;
            if (dir.CurrentIsDir())
            {
				GD.Print($"GetDLCByLoadPath: The DLC({file_name} in {folder_path.PathJoin(file_name)} was found)");
				setDLCInformationPackage(file_name, folder_path.PathJoin(file_name));
            }
			
        }
        dir.ListDirEnd();
		GD.Print("GetDLCByLoadPath: All DLC loading was finished");
	}
	private void setDLCInformationPackage(string name, string path)
	{
		DLCInformationPackageFactory.DLCInformationPackage info_pack = new DLCInformationPackageFactory.DLCInformationPackage();
		info_pack.name = name;
		info_pack.path = path;
		info_pack.able = true;
		info_pack.manifest = getDLCManifest(name, path);
		DLCInformationPackageFactory.addInformationPackage(info_pack);
	}
	private Godot.Collections.Dictionary getDLCManifest(string name, string path)
	{
		string manifest_file_path = path.PathJoin("manifest.json");
		if (!FileAccess.FileExists(manifest_file_path))
		{
			GD.PushWarning($"DLCReader: Important file({manifest_file_path}) in DLC({name}) is not found!");
			return new Godot.Collections.Dictionary();
		}
		else
		{
			string manifest_file = FileAccess.GetFileAsString(manifest_file_path);
			Json manifest_json = new Json();
			Error error = manifest_json.Parse(manifest_file);
			if (error != Error.Ok)
			{
				GD.PushWarning($"DLCReader: Important file({manifest_file_path}) in DLC({name}) is not a JSON file!");
				return new Godot.Collections.Dictionary();
			}
			else
			{
				Godot.Collections.Dictionary manifest = manifest_json.Data.AsGodotDictionary();
				GD.Print($"DLCReader: Get DLC({name}) was successful!");
				return manifest;
			}
		}
		
	}
	

}
