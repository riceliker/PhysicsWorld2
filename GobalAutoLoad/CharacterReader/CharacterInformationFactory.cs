using Godot;
using System;
using System.Collections.Generic;

public static partial class CharacterInformationFactory
{
	public static Dictionary<string, CharacterInformation> character_list = new Dictionary<string, CharacterInformation>();
	public struct CharacterBasicExtend
	{
		public int HP;
		public float Speed;

	}
	public class CharacterDescription
	{
		public string full_name;
		public string type;
		public string skill_name;
		public string ultimate_skill_name;
		public string description;
		public Texture2D portrait = new Texture2D();
		public Texture2D full_body = new Texture2D();

	}
	public class CharacterInformation
	{
		public string path;
		public string name;
		public CharacterBasicExtend basic = new CharacterBasicExtend();
		public CharacterDescription description = new CharacterDescription();
	}
	public static void setCharacterByCharacterName(DLCInformationPackageFactory.DLCInformationPackage info_pack, string character_name)
	{
		CharacterInformation character = new CharacterInformation();
		character.name = character_name;
		
		character.path = info_pack.path.PathJoin("Character").PathJoin(character_name);

		string json_string = FileAccess.GetFileAsString(character.path.PathJoin("Description").PathJoin("CharacterInformation.json"));
		Json char_json = new Json();
		Error err = char_json.Parse(json_string);
		if (err != Error.Ok)
		{
			GD.PrintErr($"CharacterInformationFactory: The character information of character in DLC from {character.path} was not found!");
			return;
		}
		Godot.Collections.Dictionary char_Dictionary = char_json.Data.AsGodotDictionary();

		if (char_Dictionary.TryGetValue("Description", out var des))
		{
			Godot.Collections.Dictionary description = des.AsGodotDictionary();

			if (description.TryGetValue("Full_Name", out var fn)) character.description.full_name = fn.AsString();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Full_Name` in character information from {character.path} was not found!");
			
			if (description.TryGetValue("Type", out var tp)) character.description.type = tp.AsString();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Type` in character information from {character.path} was not found!");

			if (description.TryGetValue("Description", out var ds)) character.description.description = ds.AsString();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Description` in character information from {character.path} was not found!");

			if (description.TryGetValue("Skill_Name", out var sn)) character.description.skill_name = sn.AsString();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Skill_Name` in character information from {character.path} was not found!");

			if (description.TryGetValue("Ultimate_Skill_Name", out var usn)) character.description.full_name = usn.AsString();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Ultimate_Skill_Name` in character information from {character.path} was not found!");
		}
		else GD.PrintErr($"CharacterInformationFactory: The key word `Description` in character information from {character.path} was not found!");
		if (char_Dictionary.TryGetValue("Character_Information", out var ci))
		{
			Godot.Collections.Dictionary character_information = ci.AsGodotDictionary();

			if (character_information.TryGetValue("HP", out var h)) character.basic.HP = h.AsInt32();
			else GD.PrintErr($"CharacterInformationFactory: The key word `HP` in character information from {character.path} was not found!");

			if (character_information.TryGetValue("Speed", out var sp)) character.basic.Speed = sp.AsSingle();
			else GD.PrintErr($"CharacterInformationFactory: The key word `Speed` in character information from {character.path} was not found!");
		}
		else GD.PrintErr($"CharacterInformationFactory: The key word `Character_Information` in character information from {character.path} was not found!");

		character.description.portrait = setPortraitByPath(character.path.PathJoin("Description").PathJoin("portrait.png"));

		character_list.Add(character.name, character);
		GD.Print($"CharacterInformationFactory: Successfully set character information of {character_name} from {character.path}.");
	}
	private static Texture2D setPortraitByPath(string path)
	{
		Image image_icon = Image.CreateEmpty(1, 1, false, Image.Format.Rgba8);
		if (image_icon.Load(path) != Error.Ok)
		{
			GD.PrintErr($"CharacterInformationFactory: The image in DLC from {path} was not found!");
			image_icon.Fill(new Color(0.6f, 0.2f, 0.8f));
			return ImageTexture.CreateFromImage(image_icon);
		}
		image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
		return ImageTexture.CreateFromImage(image_icon);
	}
}
