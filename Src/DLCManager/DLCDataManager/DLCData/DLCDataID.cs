
using System;
using Godot;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <summary>
    /// This class will record the unique ID from DLC.
    /// </summary>
    public class DLCDataID
    {
        public string DLC_name {get;}
        public string type {get;}
        public string object_name {get;}
        public DLCDataID(string DLC, string type, string name)
        {
            this.DLC_name = DLC;
            this.type = type;
            this.object_name = name;
        }
        public override string ToString()
        {
            return $"{DLC_name}.{type}.{object_name}";
        }
        public static DLCDataID Parse(string name)
        {
            string[] parts = name.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid full name format. Expected format: 'DLC_Folder_Name.Type.This_Name'");
            }

            string DLC_name = parts[0];
            string type = parts[1];
            string object_name = parts[2];
            return new DLCDataID(DLC_name, type, object_name);
           
        }
        public override bool Equals(object obj)
        {
        if (obj is DLCDataID other)
        {
            return this.ToString() == other.ToString();
        }
        return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

    }
}