using Godot;

[Tool]
public partial class FreeMapElements : Control
{
    [Export] private PackedScene scene;
    [Export] private Control container;
    [Export] private Button refresh;
    [Export] private ScrollContainer scroll;
    public override void _Ready()
    {
        
        SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        MouseFilter = MouseFilterEnum.Stop;
        ClipContents = true;
        buttonRegistry();
        
    }
    public void renderList()
    {
        scroll.CustomMinimumSize = new Vector2(0, FreeMapMeshElementManager.mesh_elements_list.Count / 8 * 196 + 64);
        int count = 0;
        foreach (FreeMapMeshElementManager.MeshInstance3DData mi_data in FreeMapMeshElementManager.mesh_elements_list)
        {
            FreeMapElementItem i = scene.Instantiate<FreeMapElementItem>();
            container.AddChild(i);
            Callable.From(() => {
                i.setInformation(mi_data.id, mi_data.mesh_instance.Name, mi_data.image);
            }).CallDeferred();
            int col = count % 8;
            int row = count / 8;
            
            i.Position = new Vector2(196 * col + 32, 196 * row);
            i.OnFreeMapElementItemClicked += onElementItemClicked;
            count++;
        }
        
    }
    private void onElementItemClicked(int index)
    {
        
    }
    private void buttonRegistry()
    {
        refresh.Pressed += () =>
        {
            foreach (var child in container.GetChildren())
            {
                child.QueueFree();
            }
            FreeMapMeshElementManager.getElementFromMeshLibrary();
            renderList();
        };
    }
}
