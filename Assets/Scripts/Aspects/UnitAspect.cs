using Assets.Scripts.Authoring;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.Aspects
{
    readonly partial struct UnitAspect : IAspect
    {
        public readonly Entity Self;

        readonly TransformAspect Transform;

        readonly RefRW<Unit> Unit;

        public bool IsInFormation
        {
            get => Unit.ValueRO.IsInFormation;
            set => Unit.ValueRW.IsInFormation = value;
        }

        public float3 Position
        {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        public float Speed
        {
            get => Unit.ValueRO.Speed;
            set => Unit.ValueRW.Speed = value;
        }

        public bool Offense
        {
            get => Unit.ValueRO.Offense;
            set => Unit.ValueRW.Offense = value;
        }
    }
}