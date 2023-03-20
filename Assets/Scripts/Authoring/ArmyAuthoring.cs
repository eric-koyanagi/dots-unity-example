using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ArmyAuthoring : UnityEngine.MonoBehaviour
{
    public int UnitCount;
    public int ColumnLength;
    public float ColumnScale = 1f;
    public bool Offense = true;

    public GameObject UnitPrefab;

    class ArmyBaker : Baker<ArmyAuthoring>
    {
        public override void Bake(ArmyAuthoring authoring)
        {
            AddComponent(new Army
            {
                UnitCount = authoring.UnitCount,
                UnitPrefab = GetEntity(authoring.UnitPrefab),
                ArmyTransform = authoring.transform.position,
                ColumnLength = authoring.ColumnLength,
                Offense = authoring.Offense,
                ColumnScale = authoring.ColumnScale,
            }) ;
        }
    }
}
struct Army : IComponentData
{
    public int UnitCount;
 
    public Entity UnitPrefab;

    public float3 ArmyTransform;

    public int ColumnLength;
    public float ColumnScale;

    public bool Offense; 
}
