using Godot;
using System;

public partial class DLCSafe : Control
{
	[Export] private Button button;
    private bool able;
    public override void _Ready()
    {
        able = SettingConfig.DebugSafeGDScriptOnly;
        button.ButtonPressed = able;
        button.Pressed += onButtonClicked;
    }
    private void onButtonClicked()
    {
        able = ! able;
        button.ButtonPressed = able;
        if (able)
            button.Text = "Able";
        else
            button.Text = "Unable";
        SettingConfig.DebugSafeGDScriptOnly = able;
        SettingConfig.Save();
    }
}
