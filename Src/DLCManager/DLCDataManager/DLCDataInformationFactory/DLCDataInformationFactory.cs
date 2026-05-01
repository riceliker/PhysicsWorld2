using Godot;
using System;

namespace PhysicsWorld.Src.DLCManager.DLCDataManager
{
    /// <summary>
    /// When you input the data from DLC, if you want to store in the class
    /// </summary>
    public static class DLCDataInformationFactory
    {
        /// <summary>
        /// If you want to make new object which parent is DLCInformation
        /// Use it will create it which your registry in AbstractInformation
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static DLCDataInformation createNewInformationObject(string type, DLCDataID id, string path)
        {
            return type switch
            {
                "Character" => new CharacterInformation(id, path),
                "Weapon" => new WeaponInformation(id, path),
                _ => throw new ArgumentException($"unknown type: {type}"),
            };
        }
    }
}
