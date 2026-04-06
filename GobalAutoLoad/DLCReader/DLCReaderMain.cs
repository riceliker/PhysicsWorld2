using Godot;
using System.Collections.Generic; 
/*
	This class only two functions:
	1. Store all DLC Data as DLC Information Package
	2. Get DLC From GetDLCFromRes And GetDLCFromUser
*/

public partial class DLCReaderMain : Node
{
	private static List<DLCInfoPack> DLC_data = new List<DLCInfoPack>();
	private int the_number_of_res_DLC;
	public struct DLCInfoPack
	{
		public string name;
		public string path;
		public int temp_id;
		public bool is_able;
		public string from;
	}

	public static List<DLCInfoPack> getDLCData()
	{
		return DLC_data;
	}

	private void GetDLCFromResFunc()
	{
		GetDLCFromRes get_DLC_from_res = new GetDLCFromRes();
		AddChild(get_DLC_from_res);
		(List<string> name_list, List<string> path_list) = get_DLC_from_res.getDLCNameAndPathList();
		the_number_of_res_DLC = name_list.Count;
		for(int i = 0; i < name_list.Count; i++)
		{
			DLCInfoPack pack = new DLCInfoPack();
			pack.name = name_list[i];
			pack.path = path_list[i];
			pack.from = "Res";
			pack.is_able = true;
			pack.temp_id = i;
			DLC_data.Add(pack);
		}
		GD.Print("Log->DLC<RES>: The number of The DLCs from res:// are " + the_number_of_res_DLC + " DLCs");
	}
	private void GetDLCFromUserFunc()
	{
		
	}
	
	public override void _Ready()
	{
		GD.Print("Log->DLCReader: Start Loading.");
		GetDLCFromResFunc();
		GD.Print("Log->DLCReader: All DLC has been read.");
	}

}
