using Godot;
using System;

public partial class WeaponDescription : ListItemDescription
{
	[Signal] public delegate void OnChooseWeaponButtonClickedEventHandler(string full_name);
	[Export] private Label name;
	[Export] private Label type;
	[Export] private Label range;
	[Export] private Label speed;
	[Export] private Label damage;
	[Export] private Label break_time;
	[Export] private TextEdit description;
	[Export] private Button choose;
	public override void setCharacterDescription(DataUniqueID id)
	{
		WeaponInformation weapon_information = DataManager.getInformation<WeaponInformation>(id);
		name.Text = weapon_information.getUniqueID().getThisName();
		type.Text = weapon_information.getWeaponBaseType().ToString();
		range.Text = "Range:" + weapon_information.standard_gun.range.ToString();
		break_time.Text = "Break:" + weapon_information.standard_gun.bullet_speed.ToString();
		damage.Text = "Damage:" + weapon_information.standard_gun.break_time.ToString();
		speed.Text = "Speed:" + weapon_information.standard_gun.bullet_speed.ToString();
		choose.Pressed += () =>
		{
			PlayerData.local_player.setWeaponInGroup(GroupShowOpenWho.choose_which_weapon_plot_index, id);
			GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/GroupShow/GroupShow.tscn");
		};
	}
}
