using Godot;
using System;

[Tool]
public partial class FreeMapMeshLibraryLabel : Control
{
	[Signal] public delegate void OnFreeMapMeshLibraryReFreshEventHandler();
	[Export] private Label label;
	[Export] private Button switch_able;
	[Export] private TextureRect show_able;
	[Export] private Button delete;
	private string name;
	public bool able = true;
	private Texture2D red;
	private Texture2D green;
	public override void _Ready()
	{
		red = CreateSolidColorTexture(128, 32, new Color(0.5f, 0f, 0f));
		green = CreateSolidColorTexture(128 ,32, new Color(0f, 0.5f, 0f));
		
	}
	public static Texture2D CreateSolidColorTexture(int width, int height, Color color)
	{
		Image image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
		image.Fill(color);
		return ImageTexture.CreateFromImage(image);
	}
	public override void _Process(double delta)
	{
		show_able.Texture = this.able ? green : red;
	}
	public void changeAble(bool able)
	{
		this.able = able;
	}
	public void setInformation(string name)
	{
		this.name = name;
		label.Text = name;
		switch_able.Pressed += () =>
		{
			able = ! able;
			FreeMapMeshLibraryManager.changeDataAble(name, able);
		};
		delete.Pressed += () =>
		{
			FreeMapMeshLibraryManager.removeDataInList(name);
			EmitSignal(SignalName.OnFreeMapMeshLibraryReFresh);
		};
	}
}
