using Godot;
using System;

public partial class MainMenu: Control
{
	[Export] private Button to_story_mode_button;
	[Export] private Button to_battle_mode_button;
	[Export] private Button to_optional_button;
	[Export] private Button to_DLC_manager_button;
	[Export] private Button quit_game_button;
	
	public override void _Ready()
	{
		to_story_mode_button.Pressed += onStoryModeButtonClicked;
		to_battle_mode_button.Pressed += onBattleModeButtonClicked;
		to_optional_button.Pressed += onOptionalButtonClicked;
		to_DLC_manager_button.Pressed += onDLCManagerButtonClicked;
		quit_game_button.Pressed += quitGame;
	}

	private void onStoryModeButtonClicked()
	{
		
	}

	private void onBattleModeButtonClicked()
	{
		GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/GroupShow/GroupShow.tscn");
	}

	private void onOptionalButtonClicked()
	{
		GetTree().ChangeSceneToFile("res://Framework/RootMenu/Optional/Optional.tscn");
	}

	private void onDLCManagerButtonClicked()
	{
		GetTree().ChangeSceneToFile("res://Framework/RootMenu/DLCManager/DLCManager.tscn");
	}

	private void quitGame()
	{
		GetTree().Quit();
	}
}
