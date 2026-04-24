

using Godot;

public partial class WeaponInformation
{
	public enum WeaponBaseEnum
	{
		WeaponGumEnum,
		WeaponCloseEnum
	}
	public enum WeaponTrueEnum
	{
		StandardGun,
		ShotGun,
		HowitzerGun,
		Sword,
		Axe,
		Spear
	}
	public class WeaponBase
	{
		public float break_time;
		public int range;
		public int damage;

	}
	public class WeaponGun : WeaponBase
	{
		public float bullet_speed;
	}
	public class StandardGun : WeaponGun
	{
	}

	public class Shotgun : WeaponGun
	{
		public int pellets_line_number;
		public int pellets_number_on_line;
		public int pellets_degree;
	}

	public class Howitzer : WeaponGun
	{
		
		public float radius;
		public float duration;
	}
	public class WeaponClose : WeaponBase
	{
		
	}
	public class SwordData : WeaponClose
	{
		
	}
	public class AxeData : WeaponClose
	{
		
	}
	public class SpearData : WeaponClose
	{
		
	}
}
