using Godot;
using System;

public partial class Back : Button
{
	[Export] private Button back_button;
	public override void _Ready()
	{
		back_button.Pressed += to_Main_Menu;
	}

	private void to_Main_Menu()
	{
		GetTree().ChangeSceneToFile("res://Framework/HomeUI/MainMenu/MainMenu.tscn");
	}
}
