using Godot;
using System.Text.Json;


public partial class Description : Control
{
	[Export] private Label DLC_name;
	[Export] private Label version;
	[Export] private Label author;
	[Export] private TextEdit information;
	private string show_DLC_name;
	private ShowInclude show_include;
	public void render()
	{
		DLCInformationPackageFactory.DLCInformationPackage info_pack = DLCInformationPackageFactory.getInformationPackageByName(show_DLC_name);
		renderText(info_pack);
		renderInclude(info_pack);
	}
	private void renderText(DLCInformationPackageFactory.DLCInformationPackage info_pack)
	{
		GD.Print($"Show DLC({info_pack.name}) information");
		DLC_name.Text = info_pack.name;
		Godot.Collections.Dictionary root = DLCInformationPackageFactory.getManifestByInfoPack(info_pack);
		version.Text = "Version: " + setVersion(root);
		Godot.Collections.Dictionary DLC_information = root["DLC_Information"].AsGodotDictionary();
		author.Text = "Author: " + DLC_information["author"].AsString();
		information.Text = DLC_information["description"].AsString();
	}
	private void renderInclude(DLCInformationPackageFactory.DLCInformationPackage info_pack)
	{
		ShowInclude show_include = GetNode<ShowInclude>("ShowInclude");
		Godot.Collections.Dictionary root = DLCInformationPackageFactory.getManifestByInfoPack(info_pack);
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
	private string setVersion(Godot.Collections.Dictionary root)
	{
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		return ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
	}
	public void getShowWhatDLCName(string name)
	{
		
		show_DLC_name = name;
		render();
	}
	public override void _Ready()
	{
		show_include = GetNode<ShowInclude>("ShowInclude");
		
	}	
}
