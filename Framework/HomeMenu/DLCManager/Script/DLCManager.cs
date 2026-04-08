using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
/*
	Get DLC information from DLCReader, And display it from DLC_data
*/
public partial class DLCManager : Control
{
	[Export] private Control control;
	[Export] private PackedScene DLC_list_item;
	private Godot.Collections.Dictionary<string, DLCListItem> _itemList = new Godot.Collections.Dictionary<string, DLCListItem>();

	public override void _Ready()
	{
		GD.Print("TSCN:DLCManger");
		int count = 0;
		foreach (string name in DLCInformationPackageFactory.DLC_Dictionary.Keys)
		{
			DLCInformationPackageFactory.DLCInformationPackage i = DLCInformationPackageFactory.getInformationPackageByName(name);
			DLCListItem item_node = DLC_list_item.Instantiate<DLCListItem>();
			item_node.setInformation(i);
			item_node.Position = new Vector2(0, 180 * count);
			count++;
			// Get Signal: DLCListItem -> DLCManager : Clicked DLC button to description the DLC.
			item_node.OnDLCListItemButtonClicked += onDLCItemClicked;
			
			control.AddChild(item_node);
			_itemList.Add(i.name ,item_node);
		}
		int totalHeight = 180 * count;
		control.CustomMinimumSize = new Vector2(control.Size.X, totalHeight);
	}
	public void OnAbleButtonClicked(string name)
    {
		if (_itemList.TryGetValue(name, out var p_target))
		{
			DLCListItem target = p_target;
			target.switchAble();
		}
			
    }
	
	private void onDLCItemClicked(string name)
	{
		Description description_script = GetNode<Description>("Description");
		description_script.getShowWhatDLCName(name);
		Able able_script = GetNode<Able>("Able");
		// Sent Signal: DLCManager -> Able: Switch DLC is able.
		able_script.setTempName(name);
	}
}
