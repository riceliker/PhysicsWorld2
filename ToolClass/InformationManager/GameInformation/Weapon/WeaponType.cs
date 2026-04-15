

public partial class WeaponInformation
{
	public class WeaponType
	{
		public const WeaponGunEnum WeaponGun = new WeaponGunEnum();
		public const WeaponCloseEnum WeaponClose = new WeaponCloseEnum();
		public enum WeaponGunEnum
		{
			StandardGun,
			ShotGun,
			HowitzerGun
		}
		public enum WeaponCloseEnum
		{
			SwordData,
			AxeData,
			SpearData
		}
	}
	public class WeaponBaseData
	{
		public int break_time;
		public int range;
		public float attack_speed;
		public int bullet_damage;

	}
	public class StandardGunData : WeaponBaseData
	{
		public float bullet_speed;
	}

	public class ShotgunData : StandardGunData
	{
		public int pellets_line_number;
		public int pellets_number_on_line;
		public int pellets_degree;
	}

	public class HowitzerData : StandardGunData
	{
		public float radius;
		public float duration;
	}
	public class CloseData : WeaponBaseData
	{
		
	}
	public class SwordData : CloseData
	{
		
	}
	public class AxeData : CloseData
	{
		
	}
	public class SpearData : CloseData
	{
		
	}
}
