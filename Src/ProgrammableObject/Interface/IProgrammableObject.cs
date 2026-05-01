
namespace PhysicsWorld.Src.ProgrammableObject.Interface
{
    public enum ProgrammableObjectType
    {
        Character, Vehicle, Mob, NPC, Item, Summon, Logic
    }
    /// <summary>
    /// This interface will registry the method about the Programmable Object
    /// When it was loaded. This function will be control by PWS
    /// </summary>
    public interface IProgrammableObject
    {
        // the unique id, will be create when scene build.
        int object_unique_id {get; set;}
        ProgrammableObjectType type {get;set;}
        int getObjectUniqueId()
        {
            return object_unique_id;
        }
        // when use `Game.summon`
        void Spawn();
        // when use `Game.kill`
        void Kill();

        
    }
}


