using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Physics;
using Assets.Scripts.Authoring;
using Assets.Scripts.Aspects;
using Unity.Transforms;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSystemGroup))] 
public partial struct UnitCollisionSystem : ISystem
{
    ComponentLookup<Unit> _componentLookup;   
    ComponentLookup<LocalTransform> _transformLookup;   

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _componentLookup = state.GetComponentLookup<Unit>();
        _transformLookup = state.GetComponentLookup<LocalTransform>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        _componentLookup.Update(ref state);
        _transformLookup.Update(ref state); 
        state.Dependency.Complete();

        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        state.Dependency = new UnitColllisionEvent
        {
            ComponentLookup = _componentLookup,
            TransformLookup = _transformLookup,
            ECB = ecb

        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), new Unity.Jobs.JobHandle());
        
    }
}

[BurstCompile]
public partial struct UnitColllisionEvent : ICollisionEventsJob
{
    [NativeDisableParallelForRestriction] public ComponentLookup<Unit> ComponentLookup;
    [NativeDisableParallelForRestriction] public ComponentLookup<LocalTransform> TransformLookup;
    public EntityCommandBuffer ECB;

    public void Execute(CollisionEvent collisionEvent)
    {        
        SetFighting(collisionEvent.EntityA, collisionEvent.EntityB);
        SetFighting(collisionEvent.EntityB, collisionEvent.EntityA);
    }

    void SetFighting(Entity entity, Entity target)
    {
        var unit = ComponentLookup[entity];
        if (unit.IsFighting)
        {
            return;
        }

        unit.IsFighting = true;
        unit.IsInFormation = false;
        unit.AttackTarget = TransformLookup[target];

        ECB.SetComponent(entity, unit);
    }
}
