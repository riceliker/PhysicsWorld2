using Godot;
using PhysicsWorld.Src.DLCManager.DLCDataManager;
using PhysicsWorld.Src.DLCManager.StoreManager;
/*
	Get DLC information from DLCReader, And display it from DLC_data
*/
namespace PhysicsWorld.GUI.Page.DLCManager
{
	public partial class DLCManager : Control
	{
		[Export] private Control control;
		[Export] private PackedScene DLC_list_item;
		private Godot.Collections.Dictionary<string, DLCListItem> _itemList = new Godot.Collections.Dictionary<string, DLCListItem>();

		public override void _Ready()
		{
			int count = 0;
			StaticDLCManager.forDLCList((name, info) =>
			{
				DLCListItem item_node = DLC_list_item.Instantiate<DLCListItem>();
				item_node.setInformation(info);
				item_node.Position = new Vector2(0, 180 * count);
				count++;
				// Get Signal: DLCListItem -> DLCManager : Clicked DLC button to description the DLC.
				item_node.OnDLCListItemButtonClicked += onDLCItemClicked;
				
				control.AddChild(item_node);
				_itemList.Add(info.DLC_name, item_node);
			});
			int totalHeight = 180 * count;
			control.CustomMinimumSize = new Vector2(control.Size.X, totalHeight);
		}
		
		private void onDLCItemClicked(string name)
		{
			DLCDescription description_script = GetNode<DLCDescription>("Description");
			description_script.getShowWhatDLCName(name);
		}
	}
}
