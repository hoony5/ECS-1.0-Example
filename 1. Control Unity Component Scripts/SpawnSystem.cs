using Unity.Burst;
using Unity.Entities;
using UnityEngine;

// RequireMatchingQueriesForUpdate is required to make sure that the system only runs when there are entities that match the query.
// This is a performance optimization.
// It is new release in 0.44
[BurstCompile, RequireMatchingQueriesForUpdate]
public partial struct SpawnSystem : ISystem
{
        public void OnCreate(ref SystemState state)
        {

        }

        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
                // Get ECB from the BeginFixedStepSimulationEntityCommandBufferSystem.Singleton
                
                EntityCommandBuffer ecbBOS = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>()
                        .CreateCommandBuffer(state.WorldUnmanaged);

                // Find PresentationGO and Instantiate GameObject not Entity.
                // Entity is injected by EntityGameObject.
                foreach ((PresentationGO prefabData, Entity entity) in SystemAPI.Query<PresentationGO>().WithEntityAccess())
                {
                        GameObject go = GameObject.Instantiate(prefabData.prefab);
                        // Instead of Creating new Entity , PresentationGO's Entity is injected to EntityGameObject. 
                        go.AddComponent<EntityGameObject>().Inject(entity, state.World);
                        // Remove PresentationGO Component because of once generating is done.
                        ecbBOS.RemoveComponent<PresentationGO>(entity);
                }

                // Get Random Generator
                RandomData random = SystemAPI.GetSingleton<RandomData>();

                // CharacterData is injected by CharacterAuthoring. 
                // And it is already added in the prefab.
                // Entity is created and Inject to Default World's EntityManager when CharacterAuthoring script's Start() callback.
                // If there was not CharacterAuthoring injected, it will not found in the world by query.
                foreach ((CharacterData data, Entity entity) in SystemAPI.Query<CharacterData>().WithEntityAccess())
                {
                        // Job system not execute Animator's Methods so we cannot use JobSystem for this. 
                        // foo
                        if (data.animator.GetCurrentAnimatorStateInfo(0).IsName("run")) continue;
                        
                        // set Animator Controller Value
                        float ranValue = random.Random.NextFloat(1, 3f);
                        // If has afford to id, not use "Speed" but use id.
                        data.animator.SetFloat("Speed", ranValue);
                        data.animator.Play("run");
                }
        }
}