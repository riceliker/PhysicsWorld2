using Godot;
using System;

public partial class WeaponInformation : GameInformation
{
	public DataUniqueID parent_id;
	private WeaponBaseEnum weapon_major_type;
	private WeaponTrueEnum weapon_true_type;
	public StandardGun standard_gun = new StandardGun();
	public WeaponInformation(DataUniqueID id, string path, DataUniqueID parent_id) : base(id, path, 160, parent_id)
	{
		this.parent_id = parent_id;

		Godot.Collections.Dictionary manifest = getManifest();

		this.weapon_major_type = StringToEnum<WeaponBaseEnum>(findValueByKeyDictionary<string>(manifest, "Major_Type", this.path), this.path);
		switch (weapon_major_type)
		{
			// If it is abstract gun.
			case WeaponBaseEnum.WeaponGumEnum:
				WeaponTrueEnum weapon_enum_unknown = StringToEnum<WeaponTrueEnum>(findValueByKeyDictionary<string>(manifest, "Minor_Type", this.path), this.path);
				switch(weapon_enum_unknown.ToString())
				{
					case "StandardGun":
						if (manifest.TryGetValue("WeaponBase", out var wb))
						{
							Godot.Collections.Dictionary weapon_base = wb.AsGodotDictionary();
							standard_gun.break_time = findValueByKeyDictionary<float>(weapon_base, "break_time", this.path);
							standard_gun.range = findValueByKeyDictionary<int>(weapon_base, "range", this.path);
							standard_gun.damage = findValueByKeyDictionary<int>(weapon_base, "damage", this.path);
						}
						if (manifest.TryGetValue("WeaponGun", out var wg))
						{
							Godot.Collections.Dictionary weapon_gun_data = wg.AsGodotDictionary();
							standard_gun.bullet_speed = findValueByKeyDictionary<int>(weapon_gun_data, "bullet_speed", this.path);
						}
						break;
					default:
						break;

				}
				break;
			case WeaponBaseEnum.WeaponCloseEnum:
				// this.weapon_enum = StringToEnum<WeaponCloseEnum>(findValueByKeyDictionary<string>(manifest, "Minor_Type", this.path), this.path);
				// this.weapon = new WeaponClose();
				break;
			default:
				GD.PrintErr("WeaponInformation:No find key in WeaponBaseEnum");
				break;
		}

		
	}
	public WeaponBaseEnum getWeaponBaseType()
	{
		return this.weapon_major_type;
	}
	public WeaponTrueEnum GetWeaponTrueEnum()
	{
		return this.weapon_true_type;
	}
}
