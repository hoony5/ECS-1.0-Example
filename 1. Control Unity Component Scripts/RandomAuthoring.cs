using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

// this is Singleton.
// because we don't need multiple Random Generators.
public class RamdomAuthoring : MonoBehaviour
{
    // seed must be uint.
    public uint seed = 1;
}

public struct RandomData : IComponentData
{
    public Unity.Mathematics.Random Random;
}

public class RandomBaker : Baker<RamdomAuthoring>
{
    public override void Bake(RamdomAuthoring authoring)
    {
        AddComponent(new RandomData()
        {
            Random = new Random(authoring.seed)
        });
    }
}
