using Godot;
using System;

namespace PhysicsWorld.GUI.Page.Setting
{
	public partial class WindowsModela: Control
	{
		private Window root;
		[Export] public OptionButton window_model;
		public override void _Ready()
		{
			this.root = GetTree().Root;	
		}
		private void initWindowModel()
		{
			window_model.AddItem("Windowed", 1);
			window_model.AddItem("Exclusive Fullscreen", 2);
			window_model.AddItem("Max Screen", 3);
			window_model.Select(1);
			window_model.ItemSelected += onWindowModelClicked;
		}
		private void onWindowModelClicked(long id)
		{
			switch (id)
			{
				case 1:
					root.Mode = Window.ModeEnum.Windowed;
					break;
				case 2:
					root.Mode = Window.ModeEnum.ExclusiveFullscreen;
					break;
				case 3:
					root.Mode = Window.ModeEnum.Maximized;
					break;
			}
		}
	}
}

