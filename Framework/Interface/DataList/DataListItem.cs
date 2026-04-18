using Godot;

/*
	This class is the base of DataItem in Data Show.
	If you want to use it: Extend it.
    If you want to change the layout OR add more thing in item, please override the method `setInformationCanOverride()`.
*/

public partial class DataListItem<T> : Control
where T : GameInformation
{ 
    [Signal] public delegate void OnListItemButtonClickedEventHandler(int type, string name);
    [Export] private Button button;
    [Export] private TextureRect texture_rect;
    private DataUniqueID item_id;
    public void setInformation(T info) 
    {
        item_id = info.getUniqueID();
        texture_rect.Texture = info.getIcon();
        setInformationCanOverride(info);
        button.Pressed += () => {
            /// Signal: XXXListItem -> XXXList <see cref="DataList.cs" />
			EmitSignal(SignalName.OnListItemButtonClicked, (int) item_id.getTypeName() , item_id.getFullName());
		};
    }
    public virtual void setInformationCanOverride(T info)
    {
        
    }
}