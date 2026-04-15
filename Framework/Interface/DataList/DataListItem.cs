using System.Net.NetworkInformation;
using Godot;

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
			EmitSignal(SignalName.OnListItemButtonClicked, (int) DataUniqueID.DataUniqueIDEnum.Character , item_id.getFullName());
		};
    }
    public virtual void setInformationCanOverride(T info)
    {
        
    }
}