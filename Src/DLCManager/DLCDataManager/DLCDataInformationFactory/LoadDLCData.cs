using Godot;
using System;
using PhysicsWorld.Src.DLCManager;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <summary>
    /// All data in DLC will be loaded here
    /// If you want to add new type, used method `getDLCDataFromDir()`
    /// Notice: Make sure the folder only have the folder about DLC, It will be read all.
    /// </summary>
    public class LoadDLCData
    {
        public const string _local_DLC_folder = "res://DLCLocal";
        // If you want to add new game information, add here.
        public LoadDLCData()
        {
            getDLCDataFromDir("Character");
            getDLCDataFromDir("Weapon");
        }
        public void getDLCDataFromDir(string type)
        {
            string data_type = type.ToString();

            foreach (string DLC_name in StaticDLCManager.DLC_list.Keys)
            {
                DLCManager.DLCInformation DLC = StaticDLCManager.DLC_list[DLC_name];
                string folder_path = _local_DLC_folder.PathJoin(DLC.DLC_folder_name);
                // The folder is EMPTY?
                using DirAccess dir = DirAccess.Open(folder_path);
                if (dir == null)
                {
                    GD.PrintErr($"DLCReader: The fold at {folder_path} was not found!");
                    return;
                }
                // Start loading DLC from the folder	
                dir.ListDirBegin();
                string file_name;
                int count = 0;
                while ((file_name = dir.GetNext()) != "")
                {
                    if (file_name == "." || file_name == "..")
                        continue;
                    if (dir.CurrentIsDir())
                    {
                        DLCDataID id = new DLCDataID(DLC.DLC_name, type, file_name);
                        DLCDataInformation info = DLCDataInformationFactory.createNewInformationObject(type, id, folder_path.PathJoin(file_name));
                        StaticDataManager.addInformation(info);
                        count++;
                        GD.Print($"Get{data_type}FromDLC: loading {type.ToString()}:{id.ToString()}");
                    }
                }
                dir.ListDirEnd();
                GD.Print($"Get{data_type}FromDLC: All {data_type}(number: {count}) loading was finished");
                LoadingProcess.setInformationToProcess($"The {data_type}({count}): was load.");
            }
        }
    }
}
