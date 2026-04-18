using Godot;
using System;


public partial class GetDataByDLCList
{
	public GetDataByDLCList()
	{
		GD.Print("===== Start Loading Character =====");
		GetCharacterByDLCList character = new GetCharacterByDLCList();
		character.startGet(DataUniqueID.DataUniqueIDEnum.Character);
		
	}
	public class GetCharacterByDLCList : AbstractGetDataFromDLCList<CharacterInformation>
	{
		public override CharacterInformation getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id)
		{
			return new CharacterInformation(id, folder_path.PathJoin("Description"), parent_id);
		}
	}
}
