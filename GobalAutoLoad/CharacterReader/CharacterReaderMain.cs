using Godot;
using System;

public partial class CharacterReaderMain : Node
{

	public void loadCharacterByDLCInformationPackage()
	{
		foreach (string name in DLCInformationPackageFactory.DLC_Dictionary.Keys)
		{
			GD.Print($"CharacterReaderMain: Start loading character information of DLC {name}.");
			DLCInformationPackageFactory.DLCInformationPackage info_pack = DLCInformationPackageFactory.getInformationPackageByName(name);
			string character_registry_path = info_pack.path.PathJoin("Character").PathJoin("CharacterRegistry.json");
			string json_string = FileAccess.GetFileAsString(character_registry_path);
			Json json = new Json();
			Error err = json.Parse(json_string);
			if (err != Error.Ok)
			{
				GD.PrintErr($"CharacterInformationFactory: The character registry in DLC from {info_pack.path} was not found!");
				return;
			}
			Godot.Collections.Dictionary character_dictionary = json.Data.AsGodotDictionary();
			if (character_dictionary.TryGetValue("Character_List", out var cl))
			{
				Godot.Collections.Array<string> character_list = cl.AsGodotArray<string>();
				foreach(string character_name in character_list)
				{
					CharacterInformationFactory.setCharacterByCharacterName(info_pack,character_name);
				}
			}
			else GD.PrintErr($"CharacterInformationFactory: The key word `Character_List` in character registry from {info_pack.path} was not found!");

		}
	}
}
