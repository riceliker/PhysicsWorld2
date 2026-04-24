using Godot;
using System;
using System.Collections.Generic;

public class GroupData
{
    private List<PackedScene> character_scene_list;
    private List<DataUniqueID> character_id_list;
    private List<Texture2D> weapon_image_list;
    public void setCharacterInGroup(int index, DataUniqueID id, PackedScene scene)
    {
        if (3 > index && index >= 0)
        {
            GD.PrintErr("GroupData->Error: Index Out");
            return;
        }
        character_id_list[index] = id;
        character_scene_list[index] = scene;
    }
    public void setWeaponInGroup(int index, DataUniqueID id)
    {
        WeaponInformation weapon_information = DataManager.getInformation<WeaponInformation>(id);
        weapon_image_list[index] = weapon_information.getIcon();
    }
}
