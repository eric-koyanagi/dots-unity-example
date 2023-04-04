using Assets.Scripts.Authoring;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Assets.Scripts.Aspects
{
    readonly partial struct UnitAspect : IAspect
    {
        public readonly Entity Self;


        readonly RefRW<Unit> Unit;
        readonly RefRW<LocalTransform> Transform;
        readonly RefRW<PhysicsVelocity> PhysicsVelocity;

        public Entity Army
        {
            get => Unit.ValueRO.Army;      
        }
        
        public bool IsInFormation
        {
            get => Unit.ValueRO.IsInFormation;
            set => Unit.ValueRW.IsInFormation = value;
        }

        public bool IsFighting
        {
            get => Unit.ValueRO.IsFighting;
            set => Unit.ValueRW.IsFighting = value;
        }

        public float3 Position
        {
            get => Transform.ValueRO.Position;
            set => Transform.ValueRW.Position = value;
        }

        public float3 Velocity
        {
            get => PhysicsVelocity.ValueRO.Linear;
            set => PhysicsVelocity.ValueRW.Linear = value;
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

        public float3 TargetPosition
        {
            get => Unit.ValueRO.AttackTarget.Position;
        }
    }
}