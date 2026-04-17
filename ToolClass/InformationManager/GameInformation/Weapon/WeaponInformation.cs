using Godot;
using System;

public partial class WeaponInformation : GameInformation
{
	public DataUniqueID parent_id;
	private WeaponBaseEnum weapon_major_type;
	private object weapon_enum;
	private object weapon;
	public WeaponInformation(DataUniqueID id, string path, DataUniqueID parent_id) : base(id, path, 160, parent_id)
	{
		this.parent_id = parent_id;

		Godot.Collections.Dictionary manifest = getManifest();

		this.weapon_major_type = StringToEnum<WeaponBaseEnum>(findValueByKeyDictionary<string>(manifest, "Major_Type", this.path), this.path);
		switch (weapon_major_type)
		{
			case WeaponBaseEnum.WeaponGumEnum:
				this.weapon_enum = StringToEnum<WeaponGunEnum>(findValueByKeyDictionary<string>(manifest, "Minor_Type", this.path), this.path);
				this.weapon = new WeaponGun();
				break;
			case WeaponBaseEnum.WeaponCloseEnum:
				this.weapon_enum = StringToEnum<WeaponCloseEnum>(findValueByKeyDictionary<string>(manifest, "Minor_Type", this.path), this.path);
				this.weapon = new WeaponGun();
				break;
			default:
				GD.PrintErr("WeaponInformation:No find key in WeaponBaseEnum");
				break;
		}

		if (manifest.TryGetValue("WeaponBase", out var wb))
		{
			Godot.Collections.Dictionary weapon_base = wb.AsGodotDictionary();
			weapon = findValueByKeyDictionary<string>(weapon_base, "break_time", this.path);
		}
	}
}
