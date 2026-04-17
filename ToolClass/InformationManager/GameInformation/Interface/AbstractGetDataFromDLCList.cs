using Godot;
using System;
using System.Threading;

public abstract class AbstractGetDataFromDLCList<T> where T : GameInformation
{
	private string folder_path;
	private int count;
	
	public void startGet(DataUniqueID.DataUniqueIDEnum type)
	{
		string data_type = type.ToString();

		foreach (DataUniqueID DLC in DataManager.getListButReadOnly<DLCInformation>().Keys)
		{
			this.folder_path = DataManager.getInformation<DLCInformation>(DLC).getPath().PathJoin(data_type);
			// The folder is EMPTY?
			using DirAccess dir = DirAccess.Open(this.folder_path);
			if (dir == null)
			{
				GD.PrintErr($"DLCReader: The fold at {this.folder_path} was not found!");
				return;
			}
			// Start loading DLC from the folder	
			dir.ListDirBegin();
			string file_name;
			while ((file_name = dir.GetNext()) != "")
			{
				if (file_name == "." || file_name == "..")
					continue;
				if (dir.CurrentIsDir())
				{
					DataUniqueID id = new DataUniqueID(DLC.getDLCName(), type, file_name);
					T info = getBasicDataToInit(id, this.folder_path.PathJoin(file_name), DLC);
					DataManager.addInformation(info);
					count++;
					GD.Print($"Get{data_type}FromDLC: loading character:{id.getFullName()}");
				}
				
			}
			dir.ListDirEnd();
			GD.Print($"Get{data_type}FromDLC: All {data_type}(number: {count}) loading was finished");
			LoadingProcess.setInformationToProcess($"The {data_type}({count}): was load.");
		}
	}
	public abstract T getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id );
}
