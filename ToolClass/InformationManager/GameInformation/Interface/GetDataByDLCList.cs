using Godot;
using System;


public partial class GetDataByDLCList
{
	public GetDataByDLCList()
	{
		GD.Print("===== Start Loading Character =====");
		GetCharacterByDLCList character = new GetCharacterByDLCList();
		character.startGet(DataUniqueID.DataUniqueIDEnum.Character);
		GD.Print("===== Start Loading Weapon =====");
		GetWeaponByDLCList weapon = new GetWeaponByDLCList();
		weapon.startGet(DataUniqueID.DataUniqueIDEnum.Weapon);
		
	}
	public class GetCharacterByDLCList : AbstractGetDataFromDLCList<CharacterInformation>
	{
		public override CharacterInformation getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id)
		{
			return new CharacterInformation(id, folder_path.PathJoin("Description"), parent_id);
		}
	}
	public class GetWeaponByDLCList : AbstractGetDataFromDLCList<WeaponInformation>
	{
        public override WeaponInformation getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id)
        {
            return new WeaponInformation(id, folder_path.PathJoin("Description"), parent_id);
        }
	}
}
