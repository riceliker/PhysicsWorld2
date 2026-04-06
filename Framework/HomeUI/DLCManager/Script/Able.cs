using Godot;
using System;
using System.Runtime.Serialization;

public partial class Able : Control
{
	// [Signal] public delegate void OnSwitchDLCIsAbleButtonClickedEventHandler();
	[Export] private Button button;
	private bool is_able = true;
	private int temp_id = 0;
	public override void _Ready()
	{
		button.Pressed += OnButtonPress;
	}
	private void OnButtonPress()
	{
		is_able = ! is_able;
		GetParent<DLCManager>().OnAbleButtonClicked(temp_id);

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
	public void setTempId(int temp_id)
	{
		this.temp_id = temp_id;
	}
}
