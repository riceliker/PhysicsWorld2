using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class FreeMapMeshLibraryManager
{
    public static Dictionary<string, MeshLibraryData> mesh_library_list = new Dictionary<string, MeshLibraryData>();
    public class MeshLibraryData
    {
        public MeshLibrary mesh_library;
        public bool able;
        public string name;
        public MeshLibraryData(string name, MeshLibrary mesh_library)
        {
            this.name = name;
            this.mesh_library = mesh_library;
            this.able = true;
        }
    }
    public static void setDataInList(string name, MeshLibrary data)
    {
        if (mesh_library_list.TryGetValue(name, out var value))
        {
            GD.PrintErr($"Addons->FreeMap: Do not load ({name} again!)");
        }
        else
        {
            mesh_library_list.Add(name, new MeshLibraryData(name, data));
        }
        
    }
    public static void removeDataInList(string name)
    {
        mesh_library_list.Remove(name);
    }
    public static void changeDataAble(string name, bool able)
    {
        if (FreeMapMeshLibraryManager.mesh_library_list.TryGetValue(name, out var data))
        {
            data.able = able;
        }
    }
    public static void clear()
    {
        mesh_library_list.Clear();
    }
}
