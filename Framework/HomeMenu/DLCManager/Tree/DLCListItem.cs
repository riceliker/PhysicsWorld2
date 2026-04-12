using Godot;
using System;
using System.Dynamic;

public partial class DLCListItem : Control
{
	[Signal] public delegate void OnDLCListItemButtonClickedEventHandler(string full_name);
	[Export] private Button button;
	[Export] private TextureRect icon;
	[Export] private Label name;
	[Export] private Label version;
	[Export] private TextureRect show_able;
	private DLCInformation info;
	private bool is_able_bool = true;
	private Texture2D unable;
	private Texture2D able;

	// Set information from DLCManager
	public override void _Ready()
	{
		CustomMinimumSize = new Vector2(640, 160);


		init_able();

		// Sent Signal: DLCListItem -> DLCManager: Clicked DLC button to description the DLC.
		button.Pressed += () => {
			EmitSignal(SignalName.OnDLCListItemButtonClicked, info.getUniqueID().getFullName());
			
		};
	}
	public void init_able()
	{
		Image img_R = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_R.Fill(Colors.Red);
		unable = ImageTexture.CreateFromImage(img_R);
		Image img_G = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_G.Fill(Colors.Green);
		able = ImageTexture.CreateFromImage(img_G);
	}
	public void setInformation(DLCInformation info)
	{
		this.info = info;

		Godot.Collections.Dictionary manifest = info.getManifest();
		name.Text = setText(manifest);
		version.Text = setVersion(manifest);
		icon.Texture = info.getIcon();
	}
	private string setVersion(Godot.Collections.Dictionary root)
	{
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		return ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
	}
	private string setText(Godot.Collections.Dictionary root)
	{
		string text = root["DLC_Name"].AsString();
		return text;
	}
	public override void _Process(double delta)
	{
		if (is_able_bool)
		{
			show_able.Texture = able;
		}
		else
		{
			show_able.Texture = unable;
		}
	}

	public void switchAble()
	{
		is_able_bool = ! is_able_bool;
		info.setAble(is_able_bool);
		DataManager.setInformation(info.getUniqueID(), info);
	}
}
