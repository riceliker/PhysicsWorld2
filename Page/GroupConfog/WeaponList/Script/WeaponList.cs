// using Godot;
// using System;

// public partial class WeaponList : DataList<WeaponInformation, WeaponItem, WeaponDescription>
// {
//     public override WeaponItem listContainerVerb(WeaponItem item_node, int count)
//     {
//         int col = count % 3;
// 		int row = count / 3;
// 		item_node.Position = new Vector2(col * 180 + 20 , 180 * row + 20);
// 		return item_node;
//     }
// 	public override void _Ready()
//     {
//         startList(DataUniqueID.DataUniqueIDEnum.Weapon, "WeaponDescription");
//     }
// }
