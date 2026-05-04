using Godot;
using System.Threading.Tasks;
using PhysicsWorld.Src.DLCManager.StoreManager;
/*
	All thing which need load and then go to main menu must run here.
	When it start: First Loading
	When it end: Go To Main Menu
*/


public partial class FirstLoading : Node
{
	public async override void _Ready()
	{
		new LoadDLC();
	}
}
