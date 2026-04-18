using Godot;
using System;

public partial class HeadRowBack : Control
{
	[Export] private TextureButton back_button;
	[Export] private PackedScene back_scene;
	[Export] private Label label;
	[Export] private string title;
	public override void _Ready()
	{
		label.Text = title;
		back_button.Pressed += onBackButtonPressed;
	}
	private void onBackButtonPressed()
	{
		GetTree().ChangeSceneToPacked(back_scene);
	}
}
