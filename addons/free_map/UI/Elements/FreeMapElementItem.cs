using Godot;
using System;
using System.Linq;
[Tool]
public partial class FreeMapElementItem : Control
{
	[Signal] public delegate void OnFreeMapElementItemClickedEventHandler(int index);
	[Export] private Button button;
	[Export] private Label label;
	[Export] private TextureRect texture_rect;
	private int index;
	private string name;
	private MeshInstance3D mesh_instance;
	private bool is_dragging = false;
	public FreeMapElementItem()
    {
        MouseFilter = MouseFilterEnum.Stop;
    }
	public void setInformation(int index, string name, Texture2D texture)
	{
		this.index = index;
		label.Text = name;
		this.name = name;
		texture_rect.Texture = texture;
		button.Pressed += onButtonClicked;
		FreeMapMeshElementManager.MeshInstance3DData data = FreeMapMeshElementManager.mesh_elements_list[index];
		this.mesh_instance = data.mesh_instance;
	}
	private void SpawnMeshAtMousePosition()
    {
        if (!Engine.IsEditorHint()) return;
        if (mesh_instance == null) return;

        Node selected = EditorInterface.Singleton.GetSelection().GetSelectedNodes().FirstOrDefault();
        if (selected == null) return;
		if (selected is not FreeMap) return;

		Node3D model_root = new Node3D();
		model_root.Name = this.name;

		MeshInstance3D new_mesh = new MeshInstance3D();
		new_mesh.Name = mesh_instance.Name;
		new_mesh.Mesh = mesh_instance.Mesh;

		selected.AddChild(model_root);
		model_root.AddChild(new_mesh);
        
		Node root = EditorInterface.Singleton.GetEditedSceneRoot();
		model_root.Owner = root;

        EditorInterface.Singleton.EditNode(model_root);
    }
	private void onButtonClicked()
	{
		SpawnMeshAtMousePosition();
	}
}
