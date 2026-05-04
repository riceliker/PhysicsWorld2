using Godot;
using PhysicsWorld.Src.DLCManager.DLCDataManager;
using PhysicsWorld.Src.DLCManager.StoreManager;


namespace PhysicsWorld.GUI.Page.DLCManager
{
	public partial class DLCListItem : Control
	{
		[Signal] public delegate void OnDLCListItemButtonClickedEventHandler(string full_name);
		[Export] private Button button;
		[Export] private TextureRect icon;
		[Export] private Label name;
		[Export] private Label version;
		[Export] private TextureRect warning;
		private DLCInformation info;
		// Set information from DLCManager
		public override void _Ready()
		{
			// Sent Signal: DLCListItem -> DLCManager: Clicked DLC button to description the DLC.
			button.Pressed += () => {
				EmitSignal(SignalName.OnDLCListItemButtonClicked, info.DLC_folder_name);	
			};
		}
		public void setInformation(DLCInformation info)
		{
			this.info = info;
			name.Text = info.DLC_name;
			version.Text = info.base_information.DLC_version;
			icon.Texture = info.icon;
		}
	}
}
