using Godot;
using System;

public partial class WeaponItem : Control
{
	[Signal] public delegate void OnWeaponListItemButtonClickedEventHandler(string name);
	[Export] private Button button;
	[Export] private TextureRect texture_rect;
	private DataUniqueID weapon_id;
	public void setInformation(WeaponInformation info)
	{
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
