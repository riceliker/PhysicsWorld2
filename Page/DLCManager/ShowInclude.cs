using Godot;

public partial class ShowInclude : Control
{
	[Export] private CheckBox script_button; // 0
	[Export] private CheckBox lite_map_button; // 1
	[Export] private CheckBox world_map_button; // 2
	[Export] private CheckBox entity_button; // 3
	[Export] private CheckBox item_button; //4
	private bool[] what_button_will_light = {false, false, false, false, false};

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
			SetBoxChecked(script_button, true);
		else 
			SetBoxChecked(script_button, false);

		if (what_button_will_light[1])
			SetBoxChecked(lite_map_button, true);
		else 
			SetBoxChecked(lite_map_button, false);

		if (what_button_will_light[2])
			SetBoxChecked(world_map_button, true);
		else 
			SetBoxChecked(world_map_button, false);
		if (what_button_will_light[3])
			SetBoxChecked(entity_button, true);
		else 
			SetBoxChecked(entity_button, false);
		if (what_button_will_light[4])
			SetBoxChecked(item_button, true);
		else 
			SetBoxChecked(item_button, false);
    }
	
	
}
