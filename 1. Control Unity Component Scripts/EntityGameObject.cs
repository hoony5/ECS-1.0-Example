using Unity.Entities;
using UnityEngine;

public class EntityGameObject : MonoBehaviour
{
    public Entity entity;
    public World world;

    public void Inject(Entity e, World w)
    {
        entity = e;
        world = w;
    }

    private void OnDisable()
    {
        if (world is null) return;

        if (world.IsCreated && world.EntityManager.Exists(entity))
        {
            world.EntityManager.DestroyEntity(entity);
        }
    }
}