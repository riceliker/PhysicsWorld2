using Godot;
using System;


public partial class GetDataByDLCList
{
	public GetDataByDLCList()
	{
		GetCharacterByDLCList character = new GetCharacterByDLCList();
		LoadingProcess.setInformationToProcess($"The character({character.getCharacterCount()}) was load.");
	}
	public class GetCharacterByDLCList : AbstractGetDataFromDLCList<CharacterInformation>
	{
		public override string getType()
		{
			return "Character";
		}
		public override CharacterInformation getBasicDataToInit(DataUniqueID id, string folder_path, DataUniqueID parent_id)
		{
			return new CharacterInformation(id, folder_path.PathJoin("Description"), parent_id);
		}
	}
}
