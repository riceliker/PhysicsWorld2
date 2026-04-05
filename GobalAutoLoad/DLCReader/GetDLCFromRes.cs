using Godot;
using System;
using System.Collections.Generic;

public partial class GetDLCFromRes : Node
{
	const string DLC_RES_PATH_HEAD = "res://DLC/";
	const string DLC_NEED_LOAD_PATH = "res://DLC/WhatDLCNameYouLoad.json";
	private List<string> dlc_need_load_list;
	private List<string> dlc_name_which_load_successful;
	private List<string> dlc_path_list;
 	public override void _Ready()
	{
		GD.Print("Log->DLCReaderRES: Start load DLC from RES://");
		getWhatDLCWillLoad();
		getDLCPathFromRes();
	}

	private void getWhatDLCWillLoad()
	{
		if (!FileAccess.FileExists(DLC_NEED_LOAD_PATH))
		{
			GD.Print("Error->DLCReaderRES: Important file(res://DLC/WhatDLCNameYouLoad.json) is not found!");
			return;
		}
		string dlc_need_load_list_json_text = FileAccess.GetFileAsString(DLC_NEED_LOAD_PATH);
		Json dlc_need_load_list_json = new Json();
		Error error = dlc_need_load_list_json.Parse(dlc_need_load_list_json_text);
		if (error == Error.Ok)
		{
			Godot.Collections.Dictionary dlc_need_load_list_dictionary = dlc_need_load_list_json.Data.AsGodotDictionary();
			string[] dlc_need_load_list_string = Array.Empty<string>();
			if ( dlc_need_load_list_dictionary.ContainsKey("WhatDLCNameYouLoad") )
			{
				dlc_need_load_list_string = dlc_need_load_list_dictionary["WhatDLCNameYouLoad"].AsStringArray();
				dlc_need_load_list = new List<string>(dlc_need_load_list_string);
			}
			else
			{
    		GD.Print("Error->DLCReaderRES: File(Res://DLC/WhatDLCNameYouLoad.json) need important key(WhatDLCNameYouLoad) is lost!");
			}
		}
		else
		{
			GD.Print("Error->DLCReaderRES: Important file(Res://DLC/WhatDLCNameYouLoad.json) is not a JSON file!");
		}
	}

	private void getDLCPathFromRes()
	{
		foreach (string DLC_name in dlc_need_load_list)
		{
			string DLC_res_path = DLC_RES_PATH_HEAD + DLC_name;
			bool is_this_DLC_have = DirAccess.DirExistsAbsolute(DLC_res_path);
			if (!is_this_DLC_have)
			{
				GD.Print("Warning->DLCManagerRES: This DLC was registry, But not found!");
			}
			dlc_name_which_load_successful.Add(DLC_name);
			dlc_path_list.Add(DLC_res_path);
			GD.Print("Log->DLCReader: The path of DLC: "+ DLC_name +" was got successful.");
		}
	}


	public (List<string>, List<string>) getDLCNameAndPathList()
	{
		return (dlc_name_which_load_successful, dlc_path_list);
	}

}
