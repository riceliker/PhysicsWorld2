using Godot;
using System;
using System.ComponentModel.Design;

public partial class WindowsOption : Control
{
	private Window root;
	[Export] private OptionButton window_model;
	public override void _Ready()
	{
		this.root = GetTree().Root;
		initWindowModel();
    
	}
	private void initWindowModel()
	{
		window_model.AddItem("Windowed", 1);
		window_model.AddItem("Fullscreen", 2);
		window_model.AddItem("Exclusive Fullscreen", 3);
		window_model.AddItem("Max Screen", 4);
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
				root.Mode = Window.ModeEnum.Fullscreen;
				break;
			case 3:
				root.Mode = Window.ModeEnum.ExclusiveFullscreen;
				break;
			case 4:
				root.Mode = Window.ModeEnum.Maximized;
				break;
		}
	}
	private void initWindowResolution()
	{
		
	}
}
