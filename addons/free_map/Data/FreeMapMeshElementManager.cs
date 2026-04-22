using Godot;
using System;
using System.Collections.Generic;

public partial class FreeMapMeshElementManager
{
    public static List<MeshInstance3DData> mesh_elements_list = new List<MeshInstance3DData>();
    public class MeshInstance3DData
    {
        public int id;
        public Texture2D image;
        public MeshInstance3D mesh_instance;
    }
    public static void getElementFromMeshLibrary()
    {
        mesh_elements_list.Clear();
        int count = 0;
        foreach (string name in FreeMapMeshLibraryManager.mesh_library_list.Keys)
        {
            if (FreeMapMeshLibraryManager.mesh_library_list.TryGetValue(name, out var data))
            {
                MeshLibrary mesh_library = data.mesh_library;
                int[] item_id_list = mesh_library.GetItemList();
                foreach (int id in item_id_list)
                {
                    Mesh mesh = mesh_library.GetItemMesh(id);
                    if (mesh == null) continue;
                    MeshInstance3D mi = new MeshInstance3D();
                    mi.Mesh = mesh;
                    mi.Name = mesh_library.GetItemName(id);
                    MeshInstance3DData mi_data = new MeshInstance3DData();
                    mi_data.id = count;
                    count++;
                    mi_data.image = mesh_library.GetItemPreview(id);
                    mi_data.mesh_instance = mi;
                    mesh_elements_list.Add(mi_data);
                    GD.Print($"Addon->FreeMap:Get mesh:{mi.Name}");
                }
            }
        }
    }
}
