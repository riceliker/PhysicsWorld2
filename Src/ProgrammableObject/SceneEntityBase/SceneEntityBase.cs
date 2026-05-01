
namespace PhysicsWorld.Src.ProgrammableObject.Interface
{
    public abstract class SceneEntityBase : IProgrammableObject
    {
        public int object_unique_id {get;set;}
        public ProgrammableObjectType type {get;set;}
        public void Kill()
        {
            throw new System.NotImplementedException();
        }

        public void Spawn()
        {
            throw new System.NotImplementedException();
        }
    }
}
