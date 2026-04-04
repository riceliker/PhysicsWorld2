using Godot;
using System;
using System.Data.Common;

public partial class Language : MenuButton
{
	[Export] private MenuButton menu_button;
	public override void _Ready()
	{
		menu_button.GetPopup().AddItem("English", 0);
		menu_button.GetPopup().AddItem("简体中文", 7);

		menu_button.GetPopup().IdPressed += onPopupMenuClick;
    
    }

	private void onPopupMenuClick(long id)
	{
		switch(id)
		{
			case 0:
				TranslationServer.SetLocale("en");
				break;
			case 7:
				TranslationServer.SetLocale("zh_CN");
				break;
			default:
				TranslationServer.SetLocale("en");
				break;
		}
	}

}
