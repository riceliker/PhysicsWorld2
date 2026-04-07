using Godot;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
/*
	Get DLC information from DLCReader, And display it from DLC_data
*/
public partial class DLCManager : Control
{
	[Export] private Control control;
	[Export] private PackedScene DLC_list_item;
	private static int show_what_by_id = 0;
	private List<DLCListItem> _itemList = new List<DLCListItem>();

	public override void _Ready()
	{
		List<DLCReaderMain.DLCInfoPack> DLC_data = DLCReaderMain.getDLCData();
		for (int j = 0; j < DLC_data.Count; j++)
		{
			DLCReaderMain.DLCInfoPack i = DLC_data[j];
			DLCListItem item_node = DLC_list_item.Instantiate<DLCListItem>();
			item_node.setInformation(i);
			item_node.Position = new Vector2(0, 180 * j);

			item_node.OnDLCListItemButtonClicked += onDLCItemClicked;
			
			control.AddChild(item_node);
			_itemList.Add(item_node);
		}
		int totalHeight = 180 * DLC_data.Count;
		control.CustomMinimumSize = new Vector2(control.Size.X, totalHeight);
	}
	public void OnAbleButtonClicked(int temp_id)
    {
		DLCListItem target = _itemList[temp_id];
		target.switchAble();
    }
	
	private void onDLCItemClicked(int temp_id)
	{
		Description description_script = GetNode<Description>("Description");
		description_script.getShowWhatDLCId(temp_id);
		Able able_script = GetNode<Able>("Able");
		able_script.setTempId(temp_id);
	}
}
