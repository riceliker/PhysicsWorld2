using Godot;
using System.Collections.Generic;
using System.ComponentModel;

public partial class DLCManager : Control
{
	[Export] private HBoxContainer h_box_container;
	[Export] private PackedScene DLC_list_item;
	public override void _Ready()
	{
		List<DLCReaderMain.DLCInfoPack> DLC_data = DLCReaderMain.getDLCData();
		int count = 1;
		foreach (DLCReaderMain.DLCInfoPack i in DLC_data)
		{
			DLCListItem item_node = DLC_list_item.Instantiate<DLCListItem>();
			item_node.setInformation(i);
			item_node.Position = new Vector2(0, 160 * count + 20);
			count++;
			AddChild(item_node);
			item_node.GetButton().Pressed += () =>
			{
				
			};
		
		}
	}

	public override void _Process(double delta)
	{
		
	}
}
