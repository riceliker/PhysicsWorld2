using Godot;
using System.Threading.Tasks;
/*
	All thing which need load and then go to main menu must run here.
	When it start: First Loading
	When it end: Go To Main Menu
*/


public partial class FirstLoading : Node
{
	public async override void _Ready()
	{
		await ToSignal(GetTree(), "process_frame");
		GetTree().ChangeSceneToFile("res://Framework/FirstLoadingScene/FirstLoadingScene.tscn");
		LoadingProcess.Reset();
		LoadingProcess.setInformationToProcess("Start loading DLC.");
		new GetDLCByPath("res://DLCLocal");
		LoadingProcess.setProcess(20);
		LoadingProcess.setInformationToProcess("Start loading information from DLC");
        new GetDataByDLCList();
		LoadingProcess.setProcess(100);
		GetTree().ChangeSceneToFile("res://Framework/RootMenu/MainMenu/MainMenu.tscn");
	}
}
