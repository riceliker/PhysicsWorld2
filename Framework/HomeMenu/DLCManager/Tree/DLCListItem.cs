using Godot;
using System;
using System.Dynamic;

public partial class DLCListItem : Control
{
	[Signal] public delegate void OnDLCListItemButtonClickedEventHandler(string name);
	[Export] private Button button;
	[Export] private TextureRect icon;
	[Export] private Label name;
	[Export] private Label version;
	[Export] private TextureRect show_able;
	private DLCInformationPackageFactory.DLCInformationPackage local_info_pack;
	private bool is_able_bool = true;
	private Texture2D unable;
	private Texture2D able;

	// Set information from DLCManager
	public override void _Ready()
	{
		CustomMinimumSize = new Vector2(640, 160);

		Image img_R = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_R.Fill(Colors.Red);
		unable = ImageTexture.CreateFromImage(img_R);
		Image img_G = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_G.Fill(Colors.Green);
		able = ImageTexture.CreateFromImage(img_G);

		// Sent Signal: DLCListItem -> DLCManager: Clicked DLC button to description the DLC.
		button.Pressed += () => {
			EmitSignal(SignalName.OnDLCListItemButtonClicked, local_info_pack.name);
			
		};
	}
	public void setInformation(DLCInformationPackageFactory.DLCInformationPackage info_pack)
	{
		local_info_pack = info_pack;

		name.Text = info_pack.name;

		Godot.Collections.Dictionary manifest = DLCInformationPackageFactory.getManifestByInfoPack(info_pack);
		version.Text = setVersion(manifest);
		
		icon.Texture = setImageTexture(info_pack);
	}
	private string setVersion(Godot.Collections.Dictionary root)
	{
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		return ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
	}
	private ImageTexture setImageTexture(DLCInformationPackageFactory.DLCInformationPackage info_pack)
	{
		string icon_path = info_pack.path.PathJoin("icon.png");
		Image image_icon = Image.CreateEmpty(1, 1, false, Image.Format.Rgba8);
		if (image_icon.Load(icon_path) != Error.Ok)
		{
			GD.Print("Error->DLC: The icon image in DLC(" + info_pack.name + ") was lost!");
		}
		image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
		return ImageTexture.CreateFromImage(image_icon);
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
	}
}
