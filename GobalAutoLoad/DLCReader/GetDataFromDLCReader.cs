using Godot;
using System;
using System.Collections.Generic;

public static partial class GetDataFromDLCReader
{
	public static DLCReaderMain.DLCInfoPack getInfoPackById(int id)
	{
		List<DLCReaderMain.DLCInfoPack> DLC_list = DLCReaderMain.getDLCData();
		foreach (DLCReaderMain.DLCInfoPack i in DLC_list)
		{
			if (i.temp_id == id)
			{
				return i;
			}
		}
		GD.Print("Error->FindInfoPackByID: Not find this id!");
		return new DLCReaderMain.DLCInfoPack();
	}
	public static Json getManifestByInfoPack(DLCReaderMain.DLCInfoPack info_pack)
	{
		string name = info_pack.name;
		string path = info_pack.path;
		string json_string = FileAccess.GetFileAsString(path + "manifest.json");
		Json json = new Json();
		Error err = json.Parse(json_string);
		if (err == Error.Ok)
		{
    		Godot.Collections.Dictionary jsonObj = json.Data.AsGodotDictionary();
		}
		else
		{
			GD.Print("Error->DLC: The manifest file was missed in DLC(" + name + ").");
		}
		return json;
	}
	public static Image getIconImageByInfoPack(DLCReaderMain.DLCInfoPack info_pack)
	{
		string icon_path = info_pack.path + "icon.png";
		Image image_icon = Image.CreateEmpty(1, 1, false, Image.Format.Rgba8);
		if (image_icon.Load(icon_path) != Error.Ok)
		{
			GD.Print("Error->DLC: The icon image in DLC(" + info_pack.name + ") was lost!");
		}
		return image_icon;
	}
	public static string getVersionStringByInfoPack(DLCReaderMain.DLCInfoPack info_pack)
	{
		Json json = GetDataFromDLCReader.getManifestByInfoPack(info_pack);
		Godot.Collections.Dictionary root = json.Data.AsGodotDictionary();
		Godot.Collections.Array version_list = root["DLC_Version"].AsGodotArray();
		return ((int) version_list[0]).ToString() + "," + ((int) version_list[1]).ToString() + "," + ((int) version_list[2]).ToString();
	}
}
