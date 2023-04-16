using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ArmyAuthoring : UnityEngine.MonoBehaviour
{
    public int UnitCount;
    public float UnitSpeed = 1f;
    public int ColumnLength;
    public float ColumnScale = 1f;
    public bool Offense = true;

    public GameObject UnitPrefab;

    class ArmyBaker : Baker<ArmyAuthoring>
    {
        public override void Bake(ArmyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new Army
            {
                UnitCount = authoring.UnitCount,
                UnitSpeed = authoring.UnitSpeed,
                UnitPrefab = GetEntity(authoring.UnitPrefab, TransformUsageFlags.None),
                ArmyTransform = authoring.transform.position,
                ColumnLength = authoring.ColumnLength,
                ColumnScale = authoring.ColumnScale
            });
        }
    }
}
struct Army : IComponentData
{
    public int UnitCount;
    public float UnitSpeed;
 
    public Entity UnitPrefab;

    public float3 ArmyTransform;

    public int ColumnLength;
    public float ColumnScale;

}
