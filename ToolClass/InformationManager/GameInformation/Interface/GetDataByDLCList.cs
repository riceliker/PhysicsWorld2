using Godot;
using System;


public partial class GetDataByDLCList
{
	public GetDataByDLCList()
	{
		new GetCharacterByDLCList();
	}
	public abstract class AbstractGetDataFromDLCList<T> where T : GameInformation
	{
		private string data_type;
		private string folder_path;
		public AbstractGetDataFromDLCList()
		{
			this.data_type = getType();
		
			foreach (DataUniqueID DLC in DataManager.getListButReadOnly<DLCInformation>().Keys)
			{
				this.folder_path = DataManager.getInformation<DLCInformation>(DLC).getPath().PathJoin(data_type);
				GD.Print($"GetDataFromDLC{data_type}: Start loading from path {folder_path}");
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
						GD.Print($"Get{this.data_type}FromDLC: The DLC({file_name} in {this.folder_path.PathJoin(file_name)} was found)");
						DataUniqueID id = new DataUniqueID(DLC.getDLCName(), DataUniqueID.DataUniqueIDEnum.DLC, file_name);
						T info = getBasicDataToInit(id, this.folder_path.PathJoin(file_name), DLC);
						DataManager.addInformation(info);
					}
					
				}
				dir.ListDirEnd();
				GD.Print($"Get{this.data_type}FromDLC: All  loading was finished");
			}
		}
		public abstract T getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id );
		public abstract string getType();
	}
	public class GetCharacterByDLCList : AbstractGetDataFromDLCList<CharacterInformation>
	{
		public override string getType()
		{
			return "Character";
		}
		public override CharacterInformation getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id)
		{
			return new CharacterInformation(id, folder_path.PathJoin("Description"), parent_id);
		}
	}
}
