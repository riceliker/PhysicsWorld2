using Godot;
using System;
using PhysicsWorld.Src.DLCManager.DLCDataManager;

namespace PhysicsWorld.Src.DLCManager.StoreManager
{
    
    public class LoadDLC
    {
        public const string _load_folder_path = "res://DLCLocal";
        public LoadDLC()
        {
            loadDLCByPath(_load_folder_path);
            getAllDLCDataFromDir();
        }
        public void loadDLCByPath(string path)
        {
            // The folder is EMPTY?
            using DirAccess dir = DirAccess.Open(path);
            if (dir == null)
            {
                GD.PrintErr($"DLCReader:: The fold at {path} was not found!");
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
                    count++;
                    string DLC_path = path.PathJoin(file_name);
                    GD.Print($"GetDLCByLoadPath: The DLC(In {DLC_path} was found)");
                    DLCInformation info = new DLCInformation(DLC_path, file_name);
                    StaticDLCManager.addDLCInformation(file_name, info);
                    LoadingProcess.setInformationToProcess($"Loading DLC: {file_name}.");
                }
                
            }
            dir.ListDirEnd();
            GD.Print("GetDLCByLoadPath: All DLC loading was finished");
            LoadingProcess.setInformationToProcess($"All DLC({count}) was loaded.");
        }
        public void getAllDLCDataFromDir()
        {
            getOneDLCDataFromDir("Character");
            getOneDLCDataFromDir("Weapon");
        }
        public void getOneDLCDataFromDir(string type)
        {
            string data_type = type.ToString();
            StaticDLCManager.forDLCList((name, DLC) =>
            {
                string folder_path = DLC.path.PathJoin(type);
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
                        DLCDataInformation info = DLCDataInformationFactory.createNewInformationObject(type, id, folder_path.PathJoin(file_name).PathJoin("Description"));
                        StaticDataManager.addInformation(info);
                        count++;
                        GD.Print($"Get{data_type}FromDLC: loading {type.ToString()}:{id.ToString()}");
                    }
                }
                dir.ListDirEnd();
                GD.Print($"Get{data_type}FromDLC: All {data_type}(number: {count}) loading was finished");
                LoadingProcess.setInformationToProcess($"The {data_type}({count}): was load.");
            });
        }
    }
}
