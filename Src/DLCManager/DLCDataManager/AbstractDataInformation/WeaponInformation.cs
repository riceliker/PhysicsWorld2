using Godot;
using System;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    public enum WeaponBaseType
    {
        Melee, Ranged
    }
    public interface IWeaponAbstractDataInterface;
    public class WeaponBaseData
    {
        public float break_time;
		public int range;
		public int damage;
    }
    public class WeaponBaseData<T> : WeaponBaseData where T : IWeaponAbstractDataInterface
	{
        public T abstract_data;
	}
    public class RangedData : IWeaponAbstractDataInterface
    {
        public float bullet_speed;
    }
    /// <summary>
    /// This class will get all data about Weapon from DLC.
    /// </summary>
    public class WeaponInformation : DLCDataInformation
    {
        public WeaponBaseType weapon_base_type {get;init;}
        public WeaponBaseData weapon_base_data {get;init;}
        public string name {get;init;}
        public WeaponInformation(DLCDataID id, string path) : base(id, path)
        {
            string weapon_type_string = GetManifestValue<string>("Type");
            this.name = GetManifestValue<string>("Name");
            GetManifestArray("WeaponBase", (weapon_base_data) =>
            {
                this.weapon_base_data.break_time = GetManifestValue<int>(weapon_base_data, "break_time");
				this.weapon_base_data.range = GetManifestValue<int>(weapon_base_data, "range");
				this.weapon_base_data.damage = GetManifestValue<int>(weapon_base_data, "damage");
            });

            switch(StringToEnum<WeaponBaseType>(weapon_type_string, id))
            {
                case WeaponBaseType.Ranged:
                    this.weapon_base_data = new WeaponBaseData<RangedData>();
                    if (weapon_base_data is WeaponBaseData<RangedData> ranged)
                    {
                        GetManifestArray("Ranged", (ranged_data) =>
                        {
                            ranged.abstract_data.bullet_speed = GetManifestValue<float>(ranged_data, "bullet_speed");
                        });                       
                    }
                    break;
                    
                    
            }
        }
    }
}
