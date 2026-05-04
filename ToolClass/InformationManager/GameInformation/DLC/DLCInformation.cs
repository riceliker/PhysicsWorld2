using System;
using Godot;
/*
	The DLCInformation class is a subclass of GameInformation that represents the information of a DLC in the InformationManager. 
	It contains the basic information of a DLC, such as its unique ID, its path in the file system, its icon, and its manifest.
	The manifest is a dictionary that contains the detailed information of the DLC, such as its name, description, and content.
	The DLCInformation class also has a boolean variable to indicate whether the DLC is able to be used or not.
*/

public partial class DLCInformation : GameInformation
{
	private bool DLC_is_Able;
	public DLCInformation(DataUniqueID id, string path) : base(id, path, 160, id)
	{
		this.DLC_is_Able = true;
		if (this.manifest.TryGetValue("DLC_Name", out var full_name))
		{
			DataUniqueID new_id = new DataUniqueID(id.getDLCName(), id.getTypeName(), full_name.AsString());
			setUniqueID(new_id);
		}
		else
		{
			GD.PrintErr($"DLCInformation: DLC({path})'s manifest is missing the 'full_name' field!");
			setUniqueID(id);
		}
	}
	public bool getIsAble()
	{
		return DLC_is_Able;
	}
	public void setAble(bool is_able)
	{
		DLC_is_Able = is_able;
	}
}
