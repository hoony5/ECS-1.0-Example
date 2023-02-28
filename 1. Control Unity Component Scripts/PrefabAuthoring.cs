using Unity.Entities;
using UnityEngine;

// generating prefab from authoring.
public class PrefabAuthoring : MonoBehaviour
{
    public GameObject prefab;
}
// if not GameObject but Entity , skinnedRenderer is stuck.
// so, I use GameObject. until update ECS contains Unity.Animation LTS version. 
public class PresentationGO : IComponentData
{
    public GameObject prefab;
}

// if you want to Add Unity Component to Entity, you can use this.
// but in this case, I don't use this.
// because, I want to control Multiple Animators of prefab's Children

/*public class TransformGO : ICleanupComponentData
{
    public Transform transform;
}
public class AnimatorGO : IComponentData
{
    public Animator animator;
}*/


// If you want to use managed something, baker could be something special.
// PresentationGO is managed, first create instance of PresentationGO, then set prefab reference.
// AddComponentObject not AddComponent, because PresentationGO is managed.
public class PrefabBaker : Baker<PrefabAuthoring>
{
    public override void Bake(PrefabAuthoring authoring)
    {
        PresentationGO presentation = new PresentationGO();
        presentation.prefab = authoring.prefab;
        
        AddComponentObject(presentation);
    }
}