using System.IO;
using Godot;

[Tool]
public partial class FreeMapBottomDock : Control
{
    public TabBar tab_bar;
    public TabContainer tab_container;
    private Control mesh_library_scene_script;
    public FreeMapBottomDock()
    {
        Name = "FreeMap";
        CustomMinimumSize = new Vector2(0, 80);

        var style = new StyleBoxFlat();
        style.BgColor = new Color(0.13f, 0.13f, 0.17f, 1);
        AddThemeStyleboxOverride("panel", style);

    }
    public override void _Ready()
    {
        CreateLayout();
    }
    private void CreateLayout()
    {
        var margin = new MarginContainer();
        AddChild(margin);
        margin.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect);
        margin.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        margin.SizeFlagsVertical = SizeFlags.ExpandFill;

        var mainVBox = new VBoxContainer();
        margin.AddChild(mainVBox);
        mainVBox.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        mainVBox.SizeFlagsVertical = SizeFlags.ExpandFill;

        tab_container = new TabContainer();
        mainVBox.AddChild(tab_container);
        tab_container.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        tab_container.SizeFlagsVertical = SizeFlags.ExpandFill;

        AddTabPage("MeshLibrary", "res://addons/free_map/UI/MeshLibrary/FreeMapMeshLibrary.tscn");
        AddTabPage("Elements", "res://addons/free_map/UI/Elements/FreeMapElements.tscn");
    }
    private void AddTabPage(string title, string scenePath)
    {
        var scroll = new ScrollContainer();
        scroll.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        scroll.SizeFlagsVertical = SizeFlags.ExpandFill;
        scroll.ClipContents = true;

        var scene = GD.Load<PackedScene>(scenePath);
        var page = scene.Instantiate<Control>();

        scroll.AddChild(page);
        tab_container.AddChild(scroll);
        scroll.Name = title;

        page.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        page.SizeFlagsVertical = SizeFlags.ExpandFill;

        // tab_container.AddChild(page);
        int index = tab_container.GetChildCount() - 1;
        tab_container.SetTabTitle(index, title);
    }
    private void OnTabChanged(long tabIndex)
{
    if (tabIndex == 1)
    {
        var elements = tab_container.GetChild<FreeMapElements>((int)tabIndex);
        elements?.renderList(); 
    }
}
}
