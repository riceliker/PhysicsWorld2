using Godot;
using System;

public partial class WindowsModel : MenuButton
{
	[Export] private MenuButton menu_button;
	public override void _Ready()
	{
	PopupMenu popup_menu = menu_button.GetPopup();

    popup_menu.AddItem("Windowed", 1);
    popup_menu.AddItem("Fullscreen", 2);
    popup_menu.AddItem("Exclusive Fullscreen", 3);
	popup_menu.AddItem("Max Screen", 4);

	popup_menu.IdPressed += onPopupMenuClicked;
	}

	private void onPopupMenuClicked(long id)
	{
		switch (id)
		{
			case 1:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				break;
			case 2:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				break;
			case 3:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
				break;
			case 4:
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
				break;
		}
	}
}
