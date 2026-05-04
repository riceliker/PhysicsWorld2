using Godot;
using System;

namespace PhysicsWorld.GUI.Page.Setting
{
	public partial class WindowModel : Control
	{
		private Window root;
		[Export] public OptionButton window_model;
		public override void _Ready()
		{
			this.root = GetTree().Root;	
			initWindowModel();
		}
		private void initWindowModel()
		{
			window_model.AddItem("Windowed", 0);
			window_model.AddItem("Exclusive Fullscreen", 1);
			window_model.AddItem("Max Screen", 2);
			window_model.AddItem("FullScreen", 3);
			window_model.Select(0);
			window_model.ItemSelected += onWindowModelClicked;
		}
		private void onWindowModelClicked(long id)
		{
			switch (id)
			{
				case 0:
					root.Mode = Window.ModeEnum.Windowed;
					break;
				case 1:
					root.Mode = Window.ModeEnum.ExclusiveFullscreen;
					break;
				case 2:
					root.Mode = Window.ModeEnum.Maximized;
					break;
				case 3:
					root.Mode = Window.ModeEnum.Fullscreen;
					break;
			}
		}
	}
}
