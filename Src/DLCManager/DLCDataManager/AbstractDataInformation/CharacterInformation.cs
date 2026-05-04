
using System;
using System.Text.Json;
using Godot;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <summary>
    /// This class will get all data about character from DLC.
    /// </summary>
    public class CharacterInformation : DLCDataInformation
    {
        /*
            This class will be used to store the information of a character in the InformationManager.
            The class will use to the Class `BasicCharacter`, which is the parent class of all characters in the game.
        */
        public struct CharacterBaseInformation
        {
            public float HP;
            public float Speed;
        }
        public CharacterBaseInformation basic;
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
        }
        public CharacterDescription description;
        public CharacterInformation(DLCDataID id, string path) : base(id, path, "char")
        {   
            (this as IGetJsonData).getJsonObject("Description", (description) => {
                this.description.name = (this as IGetJsonData).getJsonValue<string>(description, "Full_Name");
                this.description.type = (this as IGetJsonData).getJsonValue<string>(description, "Type");
                this.description.description = (this as IGetJsonData).getJsonValue<string>(description, "Description");
                this.description.skill_name = (this as IGetJsonData).getJsonValue<string>(description, "Skill_Name");
                this.description.ultimate_skill_name = (this as IGetJsonData).getJsonValue<string>(description, "Ultimate_Skill_Name");
            });
            (this as IGetJsonData).getJsonObject("Character_Information", (character_information) =>
            {
                this.basic.HP = (this as IGetJsonData).getJsonValue<float>(character_information, "HP");
                this.basic.Speed = (this as IGetJsonData).getJsonValue<float>(character_information, "Speed");
            });
        }
    }
}

