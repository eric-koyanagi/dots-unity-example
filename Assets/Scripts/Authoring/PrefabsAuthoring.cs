using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Authoring
{
    public class PrefabsAuthoring : MonoBehaviour
    {
        [Header("Player Avatar Prefabs")]
        public GameObject DefaultSpecies;
        public GameObject CollectiveSpecies;

        [Header("World Prefabs")]
        public GameObject YellowStar;
        public GameObject RedStar;

        [Header("Fleet Ship Prefabs")]
        public GameObject Ship1;

        class PrefabBaker : Baker<PrefabsAuthoring>
        {
            public override void Bake(PrefabsAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new Prefabs
                {
                    DefaultSpecies = GetEntity(authoring.DefaultSpecies, TransformUsageFlags.None),
                    CollectiveSpecies = GetEntity(authoring.CollectiveSpecies, TransformUsageFlags.None),
                    Ship1 = GetEntity(authoring.Ship1, TransformUsageFlags.None),
                    YellowStar = GetEntity(authoring.YellowStar, TransformUsageFlags.None),
                    RedStar = GetEntity(authoring.RedStar, TransformUsageFlags.None),
                });
            }
        }
    }

    struct Prefabs : IComponentData
    {
        public Entity DefaultSpecies;
        public Entity CollectiveSpecies;
        public Entity Ship1;

        public Entity YellowStar;
        public Entity RedStar;
    }
}