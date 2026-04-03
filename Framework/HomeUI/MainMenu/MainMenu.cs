using Godot;
using System;

public partial class MainMenu : Control
{
	[Export] private Button to_story_mode_button;
	[Export] private Button to_battle_mode_Button;
	[Export] private Button to_optional_Button;
	[Export] private Button to_dlc_manager_Button;
	
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
