
using System;
using System.Text.Json;
using Godot;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <Summary>
    ///	The parent class of all information classes in the InformationManager. 
    ///	It contains the basic information of a game element:
    /// such as its unique ID, its path in the file system.
    /// </Summary>
    public abstract class DLCDataInformation : IGetJsonData, IGetIcon
    {
        public DLCDataID id {get;set;}
        public string path {get;init;}
        public JsonElement _root {get;set;}
        public JsonDocument _doc {get;set;} 
        public Texture2D icon {get;set;}

        public DLCDataInformation(DLCDataID id, string path, string err)
        {
            this.id = id;
            this.path = path;

            (this as IGetJsonData).initJson(path, "manifest.json", err);

            (this as IGetIcon).setIcon();
        }
        
    }
}
