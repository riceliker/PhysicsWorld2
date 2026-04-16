using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public abstract partial class DataList<T, U, V> : Control
where T : GameInformation
where U : DataListItem<T>
where V : ListItemDescription
{
	[Export] protected PackedScene item_scene;
	[Export] private Control list_container;
	private DataUniqueID.DataUniqueIDEnum type;
	private string description_name;
	private void renderList()
	{
		int count = 0;
		foreach (DataUniqueID id in DataManager.getListButReadOnly<T>().Keys)
		{
			T i = DataManager.getInformation<T>(id);

			// DLC is able?
			if (! DataManager.getInformation<DLCInformation>(i.getDLCID()).getIsAble()) continue;

			U item_node = item_scene.Instantiate<U>();
			item_node.setInformation(i);
			item_node = listContainerVerb(item_node, count);
			count++;
			// Get Signal: CharacterListItem -> CharacterShow : Clicked character button to description the character.
			item_node.OnListItemButtonClicked += onListItemButtonClicked;
			
			list_container.AddChild(item_node);
		}
	}
	public void startList(DataUniqueID.DataUniqueIDEnum type, string description_name)
	{
		this.type = type;
		this.description_name = description_name;
		renderList();
	}
	public abstract U listContainerVerb(U item_node, int count);
	
	private void onListItemButtonClicked(int num, string name)
	{
		DataUniqueID.DataUniqueIDEnum type = (DataUniqueID.DataUniqueIDEnum) num;
		if (type == this.type)
		{
			V description_script = GetNode<V>(this.description_name);
			description_script.setCharacterDescription(DataUniqueID.fullNameToUniqueID(name));
		}
	}
}
