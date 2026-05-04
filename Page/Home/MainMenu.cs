using Godot;

namespace PhysicsWorld.GUI.Page.MainMenu
{
	public partial class MainMenu: Control
	{
		[Export] private Button to_start_game;
		[Export] private PackedScene start_game;
		[Export] private Button to_optional_button;
		[Export] private PackedScene optional_button;
		[Export] private Button to_DLC_manager_button;
		[Export] private PackedScene DLC_manager_button;
		[Export] private Button quit_game_button;

		
		public override void _Ready()
		{
			to_start_game.Pressed += () => {GetTree().ChangeSceneToPacked(start_game);};
			to_optional_button.Pressed += () => {GetTree().ChangeSceneToPacked(optional_button);};
			to_DLC_manager_button.Pressed += () => {GetTree().ChangeSceneToPacked(DLC_manager_button);};
			quit_game_button.Pressed += () => {GetTree().Quit();};
		}

	}
}
