// using Godot;
// using System;
// using System.Collections.Generic;

// public class GroupData
// {
//     private PackedScene[] character_scene_list = {null, null, null};
//     private DataUniqueID[] character_id_list = {null, null, null};
//     private Texture2D[] weapon_image_list = {null, null, null};
//     private DataUniqueID[] weapon_id_list = {null, null, null};
//     public void setCharacterInGroup(int index, DataUniqueID id, PackedScene scene)
//     {
//         if (3 > index && index >= 0)
//         {
//             GD.PrintErr("GroupData->Error: Index Out");
//             return;
//         }
//         character_id_list[index] = id;
//         character_scene_list[index] = scene;
//     }
//     public void setWeaponInGroup(int index, DataUniqueID id)
//     {
//         WeaponInformation weapon_information = DataManager.getInformation<WeaponInformation>(id);
//         weapon_image_list[index] = weapon_information.getIcon();
//         weapon_id_list[index] = weapon_information.getUniqueID();
//     }
//     public (DataUniqueID,Texture2D) getWeaponImageUniqueID(int index)
//     {
//         if (3 > index && index >= 0 )
//         {
//             if (weapon_image_list[index] != null)
//                 return (weapon_id_list[index] ,weapon_image_list[index]);
//             else
//                 return (null,null);
//         }
//         else
//         {
//             return (null,null);
//         } 
//     }
// }
