using Godot;
using System.Collections.Generic; 

public partial class DLCReaderMain : Node
{
	private static List<DLCInfoPack> DLC_data = new List<DLCInfoPack>();
	public struct DLCInfoPack
	{
		public DLCInfoPack(string name, string path)
		{
			this.name = name;
			this.path = path;
		}
		public string name;
		public string path;
		public bool is_able = true;
	}

	public static List<DLCInfoPack> getDLCData()
	{
		return DLC_data;
	}
	public override void _Ready()
	{
		GD.Print("Log->AutoLoad: Start Loading.");
		GetDLCFromRes get_DLC_from_res = new GetDLCFromRes();
		AddChild(get_DLC_from_res);
		(List<string> name_list, List<string> path_list) = get_DLC_from_res.getDLCNameAndPathList();
		for(int i = 0; i < name_list.Count; i++)
		{
			DLCInfoPack pack = new DLCInfoPack(name_list[i], path_list[i]);
			DLC_data.Add(pack);
		}
	}

}
