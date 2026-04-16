using Godot;
using System;

public partial class WeaponInformation : GameInformation
{
	public DataUniqueID parent_id;
	public WeaponType weapon_type;
	public WeaponInformation(DataUniqueID id, string path, DataUniqueID parent_id) : base(id, path, 160, parent_id)
	{
		this.parent_id = parent_id;
	}
}
