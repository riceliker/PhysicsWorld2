using Godot;
using System;
using System.Runtime.Serialization;

public partial class Able : Control
{
	// [Signal] public delegate void OnSwitchDLCIsAbleButtonClickedEventHandler();
	[Export] private Button button;
	private bool is_able = true;
	private string name = "_First";
	public override void _Ready()
	{
		button.Pressed += OnButtonPress;
	}
	private void OnButtonPress()
	{
		is_able = ! is_able;
		// Get Signal: DLCManager -> Able: Switch DLC is able.
		GetParent<DLCManager>().OnAbleButtonClicked(name);

	}
	public override void _Process(double delta)
	{
		if (is_able)
		{
			button.Text = "Switch DLC";
		}
		else
		{
			button.Text = "Switch DLC";
		}
	}
	public void setTempName(string name)
	{
		this.name = name;
	}
}
