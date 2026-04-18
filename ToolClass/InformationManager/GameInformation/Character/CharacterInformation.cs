using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class CharacterInformation : GameInformation
{
	/*
		This class will be used to store the information of a character in the InformationManager.
		The class will use to the Class `BasicCharacter`, which is the parent class of all characters in the game.
	*/
	public struct characterBaseInformation
	{
		public float HP;
		public float Speed;
	}
	public characterBaseInformation basic;
	/*
		This class will be used to show the character's description in the Character Show
		When CharacterShow is loaded, This data will be read and then shown in the CharacterShow.
	*/
	public struct CharacterDescription
	{
		public string name;
		public string description;
		public string type;
		public string skill_name;
		public string ultimate_skill_name;
		public Texture2D full_body_image;
	}
	public CharacterDescription description;
	public DataUniqueID parent_id;

	public CharacterInformation(DataUniqueID id, string path, DataUniqueID parent_id) : base(id, path, 160, parent_id)
	{
		
		Godot.Collections.Dictionary char_Dictionary = getManifest();

		if (char_Dictionary.TryGetValue("Description", out var temp_description_var))
		{
			Godot.Collections.Dictionary description = temp_description_var.AsGodotDictionary();
			this.description.name = findValueByKeyDictionary<string>(description, "Full_Name", this.path);
			this.description.type = findValueByKeyDictionary<string>(description, "Type", this.path);
			this.description.description = findValueByKeyDictionary<string>(description, "Description", this.path);
			this.description.skill_name = findValueByKeyDictionary<string>(description, "Skill_Name", this.path);
			this.description.ultimate_skill_name = findValueByKeyDictionary<string>(description, "Ultimate_Skill_Name", this.path);

		}
		else 
		{
			GD.PrintErr($"CharacterInformation: The key word `Description` in character information from {this.path} was not found!");
		}

		if (char_Dictionary.TryGetValue("Character_Information", out var ci))
		{
			Godot.Collections.Dictionary character_information = ci.AsGodotDictionary();

			this.basic.HP = findValueByKeyDictionary<int>(character_information, "HP", this.path);
			this.basic.Speed = findValueByKeyDictionary<float>(character_information, "Speed", this.path);
			
		}
		else 
		{
			GD.PrintErr($"CharacterInformation: The key word `Character_Information` in character information from {this.path} was not found!");
		}

	}
	
}
