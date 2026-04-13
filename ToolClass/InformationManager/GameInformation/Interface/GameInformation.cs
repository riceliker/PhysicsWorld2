using Godot;
/*
	The parent class of all information classes in the InformationManager. 
	It contains the basic information of a game element, such as its unique ID, its path in the file system, and its icon.
	Each subclass of GameInformation will implement the clone() method to return a new instance of itself with the same data.
*/

public class GameInformation 
{
	protected DataUniqueID id;
	protected string path;
	protected Texture2D icon;
	protected int icon_size;
	protected Godot.Collections.Dictionary manifest;
	protected DataUniqueID dlc_id;
	public GameInformation(DataUniqueID id, string path, int icon_size)
	{
		this.id = id;
		this.path = path;
		this.icon_size = icon_size;
		setManifest();
		setIcon();

	}
	public DataUniqueID getUniqueID()
	{
		return this.id;
	}
	public string getPath()
	{
		return this.path;
	}
	public void setUniqueID(DataUniqueID id)
	{
		this.id = id;
	}
	public void setPath(string path)
	{
		this.path = path;
	}
	public Texture2D getIcon()
	{
		return this.icon;
	}
	public Godot.Collections.Dictionary getManifest()
	{
		return this.manifest;
	}
	public DataUniqueID getDLCID()
	{
		return this.dlc_id;
	}
	public void setDLCID(DataUniqueID dlc_id)
	{
		this.dlc_id = dlc_id;
	}
	protected void setManifest()
	{
		string manifest_file_path = this.getPath().PathJoin("manifest.json");
		if (!FileAccess.FileExists(manifest_file_path))
		{
			GD.PushWarning($"DLCReader: Important file({manifest_file_path}) in DLC({this.id.getFullName()}) is not found!");
			this.manifest = new Godot.Collections.Dictionary();
		}
		else
		{
			string manifest_file = FileAccess.GetFileAsString(manifest_file_path);
			Json manifest_json = new Json();
			Error error = manifest_json.Parse(manifest_file);
			if (error != Error.Ok)
			{
				GD.PushWarning($"DLCReader: Important file({manifest_file_path}) in DLC({this.id.getFullName()}) is not a JSON file!");
				this.manifest = new Godot.Collections.Dictionary();
			}
			else
			{
				Godot.Collections.Dictionary manifest = manifest_json.Data.AsGodotDictionary();
				GD.Print($"DLCReader: Get DLC({this.id.getFullName()}) was successful!");
				this.manifest = manifest;
			}
		}
	}
	protected void setIcon()
	{
		string icon_path = this.path.PathJoin("icon.png");

		if (ResourceLoader.Exists(icon_path))
		{
			Texture2D loadedTex = ResourceLoader.Load<Texture2D>(icon_path);
			Image image_icon = loadedTex.GetImage();

			image_icon.Resize(160, 160, Image.Interpolation.Bilinear);
			this.icon = ImageTexture.CreateFromImage(image_icon);
		}
		else
		{
			GD.PrintErr($"Error->DLC: The icon image in DLC({this.id.getFullName()}) was lost!");
			Image errorImage = Image.CreateEmpty(icon_size, icon_size, false, Image.Format.Rgba8);
			errorImage.Fill(new Color(0.6f, 0.2f, 0.8f));
			this.icon = ImageTexture.CreateFromImage(errorImage);
		}
	}
}
