using System.Collections;
using System.ComponentModel;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Authoring
{
    public class StarAuthoring : MonoBehaviour
    {
        // currently this doesn't "bake" anywhere; could move this to Components if no authoring component required 

        class StarBaker : Baker<StarAuthoring>
        {
            public override void Bake(StarAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Star
                {

                });
            }
        }
    }

    struct Star : IComponentData
    {
        public float Size; 

    }
}