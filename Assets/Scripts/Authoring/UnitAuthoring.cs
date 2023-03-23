﻿
using Unity.Entities;

namespace Assets.Scripts.Authoring
{
    public class UnitAuthoring : UnityEngine.MonoBehaviour
    {
        public float Speed = 1f; 

        class UnitBaker : Baker<UnitAuthoring>
        {
            public override void Bake(UnitAuthoring authoring)
            {                
                AddComponent(new Unit
                {
                    Speed = authoring.Speed,
                    IsInFormation = true,
                    IsFighting = false
                });
            }
        }
    }

    struct Unit : IComponentData
    {
        public bool IsInFormation;
        public float Speed;
        public bool Offense;
        public bool IsFighting;
    }
}