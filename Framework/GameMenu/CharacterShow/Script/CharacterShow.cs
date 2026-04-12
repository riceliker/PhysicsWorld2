using Godot;
using System;

public partial class CharacterShow : Control
{
	[Export] private PackedScene character_item_scene;
	[Export] private Control character_list_control;
	public override void _Ready()
	{
		GD.Print("TSCN:CharacterShow");
		int count = 0;
		foreach (DataUniqueID id in DataManager.getListButReadOnly<CharacterInformation>().Keys)
		{
			CharacterInformation i = DataManager.getInformation<CharacterInformation>(id);

			// DLC is able?
			if (! DataManager.getInformation<DLCInformation>(i.parent_id).getIsAble()) continue;

			CharacterItem item_node = character_item_scene.Instantiate<CharacterItem>();
			item_node.setInformation(i);
			int col = count % 3;
			int row = count / 3;
			item_node.Position = new Vector2(col * 180 + 20 , 180 * row + 20);
			count++;
			// Get Signal: CharacterListItem -> CharacterShow : Clicked character button to description the character.
			item_node.OnCharacterListItemButtonClicked += onCharacterListItemButtonClicked;
			
			character_list_control.AddChild(item_node);
		}
	}
	private void onCharacterListItemButtonClicked(string name)
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
