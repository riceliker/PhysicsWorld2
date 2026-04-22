using Godot;

[Tool]
public partial class FreeMapMeshLibrary : Control
{
    [Export] private PackedScene scene;
    [Export] private Control container;
    [Export] private Button refresh_button;
    [Export] private Button remove_all_button;
    public override void _Ready()
    {
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        MouseFilter = MouseFilterEnum.Stop;
        ClipContents = true;
        renderList();
        buttonRegistry();
        
    }
    private void renderList()
    {
        foreach (var child in container.GetChildren())
        {
        child.QueueFree();
        }

        int count = 0;
        foreach (string name in FreeMapMeshLibraryManager.mesh_library_list.Keys)
        {
            if (FreeMapMeshLibraryManager.mesh_library_list.TryGetValue(name, out FreeMapMeshLibraryManager.MeshLibraryData data))
            {
                string file_name = data.name;
                MeshLibrary mesh_library = data.mesh_library;
                FreeMapMeshLibraryLabel i = scene.Instantiate<FreeMapMeshLibraryLabel>();
                i.setInformation(file_name);
                i.Position = new Vector2(0, 128 * count);
                i.OnFreeMapMeshLibraryReFresh += renderList;
                count++;
                container.AddChild(i);
            }
        }
    }
    private void buttonRegistry()
    {
        refresh_button.Pressed += () =>
        {
            renderList();
        };
        remove_all_button.Pressed += () =>
        {
            FreeMapMeshLibraryManager.clear();
            renderList();
            GD.Print($"Addon->FreeMap:MeshLibrary was clear successful");
        };
    }
}
