using Godot;
using PhysicsWorld.Src.DLCManager.StoreManager;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
	public partial class DLCDescription : Control
	{
		[Export] private Label DLC_name;
		[Export] private Label version;
		[Export] private Label author;
		[Export] private TextEdit information;
		DLCInformation info;
		private string full_name;
		private ShowInclude show_include;
		public void render()
		{
			if (info == null) return;
			renderText();
			renderInclude();
		}
		private void renderText()
		{
			
			DLC_name.Text = info.DLC_name;
			version.Text = "Version: " + info.base_information.DLC_version;
			author.Text = "Author: " + info.description.author;
			information.Text = info.description.description;
		}
		private void renderInclude()
		{
			ShowInclude show_include = GetNode<ShowInclude>("ShowInclude");
			bool[] light_list = {false, false, false, false, false};
			foreach (string i in info.base_information.addon_things)
			{
				switch (i)
				{
					case "Script":
						light_list[0] = true;
						break;
					case "LiteMap":
						light_list[1] = true;
						break;
					case "WorldMap":
						light_list[2] = true;
						break;
					case "Entity":
						light_list[3] = true;
						break;	
					case "Item":
						light_list[4] = true;
						break;
				}
				show_include.SetWhatButtonWillLight(light_list);
			}
		}
		public void getShowWhatDLCName(string name)
		{
			info = StaticDLCManager.getDLCInformation(name);
			render();
		}
		public override void _Ready()
		{
			show_include = GetNode<ShowInclude>("ShowInclude");
			DLC_name.Text = "Please select a DLC!";
			version.Text = "Version: No Data!";
			author.Text = "Author: No Data!";
			information.Text = "No Data!";
		}	
	}
}
