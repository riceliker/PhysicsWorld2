using Godot;
using System;
using System.IO;

public partial class FreeMapMeshLibrary : Control
{
	[Signal]
    public delegate void MeshLibraryDroppedEventHandler(string path, MeshLibrary library);

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return true;
    }

	public override void _DropData(Vector2 atPosition, Variant data)
    {
        Godot.Collections.Dictionary data_json = data.AsGodotDictionary();
        Godot.Collections.Array files = data_json["files"].AsGodotArray();
        string path = files[0].AsString();

        Resource res = ResourceLoader.Load(path);
        if (res is MeshLibrary ml)
        {
            GD.Print($"Addon->FreeMap:Get MeshLibrary from({getFileNameByPath(path)}) successful");
            FreeMapMeshLibraryManager.setDataInList(getFileNameByPath(path), ml);
            renderList();
        }
        else
        {
            GD.PrintErr("Addon->FreeMap:This File is not MeshLibrary");
        }
    }
    private static string getFileNameByPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return "";

        int lastSlash = path.LastIndexOf('/');
        if (lastSlash == -1) lastSlash = 0;

        int lastDot = path.LastIndexOf('.');
        if (lastDot == -1) lastDot = path.Length;

        int start = lastSlash + 1;
        int length = lastDot - start;
        string name = path.Substring(start, length);

        return name;
    }
}
