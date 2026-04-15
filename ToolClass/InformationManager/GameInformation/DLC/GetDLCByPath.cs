using Godot;
using System;
using System.Runtime.CompilerServices;

public class GetDLCByPath 
{
	private string folder_path;
	public GetDLCByPath(string folder_path)
	{
		this.folder_path = folder_path;
		// The folder is EMPTY?
		using DirAccess dir = DirAccess.Open(this.folder_path);
		if (dir == null)
		{
			GD.PrintErr($"DLCReader:: The fold at {this.folder_path} was not found!");
			return;
		}
		// Start loading DLC from the folder	
		dir.ListDirBegin();
		string file_name;
		int count = 0;
		while ((file_name = dir.GetNext()) != "")
		{
			if (file_name == "." || file_name == "..")
				continue;
			if (dir.CurrentIsDir())
			{
				count++;
				GD.Print($"GetDLCByLoadPath: The DLC({file_name} in {this.folder_path.PathJoin(file_name)} was found)");
				DataUniqueID id = new DataUniqueID(file_name, DataUniqueID.DataUniqueIDEnum.DLC, "");
				DLCInformation info = new DLCInformation(id, this.folder_path.PathJoin(file_name));
				DataManager.addInformation(info);
				LoadingProcess.setInformationToProcess($"Loading DLC: {id}.");
			}
			
		}
		dir.ListDirEnd();
		GD.Print("GetDLCByLoadPath: All DLC loading was finished");
		LoadingProcess.setInformationToProcess($"All DLC({count}) was loaded.");
	}
}
