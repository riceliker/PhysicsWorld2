using Godot;


public partial class DLCDescription : Control
{
	[Export] private Label DLC_name;
	[Export] private Label version;
	[Export] private Label author;
	[Export] private TextEdit information;
	private DataUniqueID id;
	private string full_name;
	private ShowInclude show_include;
	public void render()
	{
		renderText();
		renderInclude();
	}
	private void renderText()
	{
		if (id != null)
		{
			Godot.Collections.Dictionary root = DataManager.getInformation<DLCInformation>(id).getManifest();
			DLC_name.Text = root["DLC_Name"].AsString();
			version.Text = "Version: " + setVersion(root);
			Godot.Collections.Dictionary DLC_information = root["DLC_Information"].AsGodotDictionary();
			author.Text = "Author: " + DLC_information["author"].AsString();
			information.Text = DLC_information["description"].AsString();
		}
	}
	private void renderInclude()
	{
		ShowInclude show_include = GetNode<ShowInclude>("ShowInclude");
		bool[] light_list = {false, false, false, false};
		if (DataManager.isContain<DLCInformation>(id))
		{
			Godot.Collections.Dictionary root = DataManager.getInformation<DLCInformation>(id).getManifest();
			Godot.Collections.Array<string> registry_list = root["Registry"].AsGodotArray<string>();
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
		}
		
		
		show_include.SetWhatButtonWillLight(light_list);
	}
	private string setVersion(Godot.Collections.Dictionary root)
	{
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		return ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
	}
	public void getShowWhatDLCName(string full_name)
	{
		DataUniqueID load_id = DataUniqueID.fullNameToUniqueID(full_name);
		if (DataManager.isContain<DLCInformation>(load_id))
		{
			this.id = load_id;
		}
		else
		{
			GD.PrintErr($"Description: ID Name:({full_name}) is not found!");
			this.id = null;
		}
		render();
	}
	public override void _Ready()
	{
		show_include = GetNode<ShowInclude>("ShowInclude");
		DLC_name.Text = "Please select a DLC!";
		version.Text = "Version: No Data!";
		author.Text = "Author: No Data!";
		information.Text = "No Data!";
	}	
}
