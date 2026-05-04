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
        public int break_time;
		public int range;
		public int damage;
    }
    public class WeaponBaseData<T> : WeaponBaseData where T : IWeaponAbstractDataInterface, new()
	{
        public T abstract_data = new T();
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
        public WeaponBaseType weapon_base_type = new WeaponBaseType();
        public WeaponBaseData weapon_base_data = new WeaponBaseData();
        public string name {get;init;}
        public WeaponInformation(DLCDataID id, string path) : base(id, path, "")
        {
            string weapon_type_string = (this as IGetJsonData).getJsonValue<string>("Type");
            this.name = (this as IGetJsonData).getJsonValue<string>("Name");
            (this as IGetJsonData).getJsonObject("WeaponBase", (weapon_base_data) =>
            {
                this.weapon_base_data.break_time = (this as IGetJsonData).getJsonValue<int>(weapon_base_data, "break_time");
				this.weapon_base_data.range = (this as IGetJsonData).getJsonValue<int>(weapon_base_data, "range");
				this.weapon_base_data.damage = (this as IGetJsonData).getJsonValue<int>(weapon_base_data, "damage");
            });

            switch((this as IGetJsonData).getJsonEnum<WeaponBaseType>("Type"))
            {
                case WeaponBaseType.Ranged:
                    this.weapon_base_data = new WeaponBaseData<RangedData>();
                    if (weapon_base_data is WeaponBaseData<RangedData> ranged)
                    {
                        (this as IGetJsonData).getJsonObject("Ranged", (ranged_data) =>
                        {
                            ranged.abstract_data.bullet_speed = (this as IGetJsonData).getJsonValue<float>(ranged_data, "bullet_speed");
                        });                       
                    }
                    break;
                    
                    
            }
        }
    }
}
