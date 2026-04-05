using Godot;
using System;

public partial class DLCListItem : Control
{
	[Export] private Button button;
	[Export] private TextureRect icon;
	[Export] private Label name;
	[Export] private Label version;
	[Export] private TextureRect is_able;
	private bool is_able_bool = true;
	private Texture2D unable;
	private Texture2D able;

	// Set information from DLCManager
	public void setInformation(DLCReaderMain.DLCInfoPack dlc_info)
	{
		name.Text = dlc_info.name;
		string path = dlc_info.path;
		Json json = readJson(dlc_info.name, dlc_info.path);
		Godot.Collections.Dictionary root = json.Data.AsGodotDictionary();
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		version.Text = ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
		string icon_path = path + "icon.png";
		Image image_icon = Image.CreateEmpty(1, 1, false, Image.Format.Rgba8);
		if (image_icon.Load(icon_path) != Error.Ok)
		{
			GD.Print("Error->DLE: The icon image in DLC(" + dlc_info.name + ") was lost!");
		}
		image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
		icon.Texture = ImageTexture.CreateFromImage(image_icon);
	}
	// Get manifest json path and return json
	private Json readJson(string name, string path)
	{
		string json_string = FileAccess.GetFileAsString(path + "manifest.json");
		Json json = new Json();
		Error err = json.Parse(json_string);
		if (err == Error.Ok)
		{
    		Godot.Collections.Dictionary jsonObj = json.Data.AsGodotDictionary();
			GD.Print("Log->DLC: The manifest file was got in DLC(" + name + ").");
		}
		else
		{
			GD.Print("Error->DLC: The manifest file was missed in DLC(" + name + ").");
		}
		return json;
	}
	public Button GetButton()
    {
        return button;
    }
	public void switchIsAble(bool is_able)
	{
		is_able_bool = is_able;
	}
	public override void _Ready()
	{
		Image img_R = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_R.Fill(Colors.Red);
		unable = ImageTexture.CreateFromImage(img_R);
		Image img_G = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_G.Fill(Colors.Green);
		able = ImageTexture.CreateFromImage(img_G);

	}
	public override void _Process(double delta)
	{
		if (is_able_bool)
		{
			is_able.Texture = able;
		}
		else
		{
			is_able.Texture = unable;
		}
		
	}
}
