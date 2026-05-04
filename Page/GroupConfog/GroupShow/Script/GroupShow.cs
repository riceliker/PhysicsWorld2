// using Godot;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// public partial class GroupShow : Node
// {
//     [Export] private Node3D first_character_container;
//     [Export] private Node3D second_character_container;
//     [Export] private Node3D third_character_container;
//     [Export] private TextureRect first_weapon_rect;
//     [Export] private TextureRect second_weapon_rect;
//     [Export] private TextureRect third_weapon_rect;
//     [Export] private Button first_character_button;
//     [Export] private Button second_character_button;
//     [Export] private Button third_character_button;
//     [Export] private Button first_weapon_button;
//     [Export] private Button second_weapon_button;
//     [Export] private Button third_weapon_button;
//     private DataUniqueID[] had_in_list = {null, null, null};
//     public override void _Ready()
//     {
//         renderWeapon();
//         registryWeaponButton();
//         // first_character_button.Pressed += () =>
//         // {
//         //     GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/WeaponList/WeaponList.tscn");
//         // };
        
//     }
//     private void registryWeaponButton()
//     {
//         first_weapon_button.Pressed += () =>
//         {
//             GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/WeaponList/WeaponList.tscn");
//             GroupShowOpenWho.choose_which_weapon_plot_index = 0;
//         };
//         second_weapon_button.Pressed += () =>
//         {
//             GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/WeaponList/WeaponList.tscn");
//             GroupShowOpenWho.choose_which_weapon_plot_index = 1;
//         };
//         third_weapon_button.Pressed += () =>
//         {
//             GetTree().ChangeSceneToFile("res://Framework/RootMenu/Battle/Group/WeaponList/WeaponList.tscn");
//             GroupShowOpenWho.choose_which_weapon_plot_index = 2;
//         };
//     }
//     private void renderWeapon()
//     {   
//         (DataUniqueID id, Texture2D texture) = PlayerData.local_player.getWeaponImageUniqueID(GroupShowOpenWho.choose_which_weapon_plot_index);
//         if (had_in_list.Contains(id))
//             return;
//         if (texture != null)
//         {
//             switch (GroupShowOpenWho.choose_which_weapon_plot_index)
//             {
//                 case 0:
//                     first_weapon_rect.Texture = texture;
//                     had_in_list[0] = id;
//                     break;
//                 case 1:
//                     second_weapon_rect.Texture = texture;
//                     had_in_list[1] = id;
//                     break;
//                 case 2:
//                     third_weapon_rect.Texture = texture;
//                     had_in_list[2] = id;
//                     break;
//             }
            
//         }
//     }
// }
