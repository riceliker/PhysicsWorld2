using Godot;
using System;

public partial class WeaponItem : DataListItem<WeaponInformation>
{
	[Signal] public delegate void OnWeaponListItemButtonClickedEventHandler(string name);
	[Export] private Button button;
	[Export] private TextureRect texture_rect;
	private DataUniqueID weapon_id;
	public override void setInformationCanOverride(WeaponInformation info)
	{
		
	}
	
	
}
