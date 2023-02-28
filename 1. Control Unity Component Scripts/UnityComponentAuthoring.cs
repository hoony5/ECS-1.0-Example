using Unity.Entities;
using UnityEngine;

// this authoring is used to add a managed component, unity component class, to the entity 
public class UnityComponentAuthoring : MonoBehaviour
{
    // NavAgent or AudioSource or etc..
    public Animator animator;

    // inject the entity and world to the entity game object
    /// TODO :: Migrating Injection to SystemGroup EntityCommandBuffer.
    private void Start()
    {
        // creating entity is very important. If not created, System cannot found the entity by SystemAPI.Query<T>   
        Entity entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        // Another way creating entity is using Archetype
        // ex. EntityArchetype archetype = World.DefaultGameObjectInjectionWorld.EntityManager.CreateArchetype(typeof(CharacterData));
        // Entity entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(archetype);
        
        // AddComponent entity and this Authoring
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentObject(entity, this);
        // instead of using Baker class, you can add component directly
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentObject(entity, new CharacterData(){animator = animator});
    }
}

// this is the data class for the entity not struct !!
public class CharacterData : IComponentData 
{
    public Animator animator;
}

/* If need Baker class.
public class CharacterAuthoringBaker : Baker<CharacterAuthoring>
{
    public override void Bake(CharacterAuthoring authoring)
    {
        AddComponentObject(new CharacterData(){animator = authoring.animator});
    }
}
*/
