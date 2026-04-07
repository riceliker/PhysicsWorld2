using Godot;
using System.Text.Json;


public partial class Description : Control
{
	[Export] private Label name;
	[Export] private Label version;
	[Export] private Label author;
	[Export] private TextEdit information;
	private int temp_id = 0;
	private DLCReaderMain.DLCInfoPack info_pack;
	private ShowInclude show_include;
	public void render()
	{
		info_pack = GetDataFromDLCReader.getInfoPackById(temp_id);
		renderText();
		renderInclude();
	}
	private void renderText()
	{
		name.Text = info_pack.name;
		version.Text = "Version: " + GetDataFromDLCReader.getVersionStringByInfoPack(info_pack);
		Json json = GetDataFromDLCReader.getManifestByInfoPack(info_pack);
		Godot.Collections.Dictionary root = json.Data.AsGodotDictionary();
		Godot.Collections.Dictionary DLC_information = root["DLC_Information"].AsGodotDictionary();
		author.Text = "Author: " + DLC_information["author"].AsString();
		information.Text = DLC_information["description"].AsString();
	}
	private void renderInclude()
	{
		ShowInclude show_include = GetNode<ShowInclude>("ShowInclude");
		Json json = GetDataFromDLCReader.getManifestByInfoPack(info_pack);
		Godot.Collections.Dictionary root = json.Data.AsGodotDictionary();
		Godot.Collections.Array<string> registry_list = root["Registry"].AsGodotArray<string>();
		bool[] light_list = {false, false, false, false};
		foreach (string i in registry_list)
		{
			switch (i)
			{
				case "Story":
					light_list[0] = true;
					break;
				case "Map":
					light_list[1] = true;
					break;
				case "Character":
					light_list[2] = true;
					break;
				case "Item":
					light_list[3] = true;
					break;	
			}
		}
		show_include.SetWhatButtonWillLight(light_list);
	}
	public void getShowWhatDLCId(int temp_id)
	{
		this.temp_id = temp_id;
		render();
	}
	public override void _Ready()
	{
		show_include = GetNode<ShowInclude>("ShowInclude");
		render();;
		
	}	
}
