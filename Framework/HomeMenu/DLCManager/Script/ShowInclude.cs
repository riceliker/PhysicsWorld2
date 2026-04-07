using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class ShowInclude : Control
{
	[Export] private CheckBox story_button; // 0
	[Export] private CheckBox map_button; // 1
	[Export] private CheckBox character_button; // 2
	[Export] private CheckBox item_button; // 3
	private bool[] what_button_will_light = {false, false, false, false};

	private void SetBoxChecked(CheckBox box, bool on)
	{
		box.ButtonPressed = on;
	}
	public void SetWhatButtonWillLight(bool[] list)
	{
		what_button_will_light = list;
	}
    public override void _Process(double _delta)
    {
        if (what_button_will_light[0])
			SetBoxChecked(story_button, true);
		else 
			SetBoxChecked(story_button, false);

		if (what_button_will_light[1])
			SetBoxChecked(map_button, true);
		else 
			SetBoxChecked(map_button, false);

		if (what_button_will_light[2])
			SetBoxChecked(character_button, true);
		else 
			SetBoxChecked(character_button, false);
		if (what_button_will_light[3])
			SetBoxChecked(item_button, true);
		else 
			SetBoxChecked(item_button, false);
    }
	
	
}
