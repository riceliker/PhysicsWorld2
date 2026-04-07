using Godot;
using System;

public partial class DLCListItem : Control
{
	[Signal] public delegate void OnDLCListItemButtonClickedEventHandler(int DLC_id);
	[Export] private Button button;
	[Export] private TextureRect icon;
	[Export] private Label name;
	[Export] private Label version;
	[Export] private TextureRect is_able;
	private DLCReaderMain.DLCInfoPack info_pack;
	private bool is_able_bool = true;
	private Texture2D unable;
	private Texture2D able;

	// Set information from DLCManager
	public void setInformation(DLCReaderMain.DLCInfoPack info_pack)
	{
		this.info_pack = info_pack;
		name.Text = info_pack.name;
		version.Text = GetDataFromDLCReader.getVersionStringByInfoPack(info_pack);
		Image image_icon = GetDataFromDLCReader.getIconImageByInfoPack(info_pack);
		image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
		icon.Texture = ImageTexture.CreateFromImage(image_icon);
	}
	public override void _Ready()
	{
		Image img_R = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_R.Fill(Colors.Red);
		unable = ImageTexture.CreateFromImage(img_R);
		Image img_G = Image.CreateEmpty(64, 64, false, Image.Format.Rgba8);
		img_G.Fill(Colors.Green);
		able = ImageTexture.CreateFromImage(img_G);

		button.Pressed += () => {EmitSignal(SignalName.OnDLCListItemButtonClicked, info_pack.temp_id);};
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
	public void switchAble()
	{
		is_able_bool = ! is_able_bool;
	}
}
