using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Physics;
using Assets.Scripts.Authoring;
using Assets.Scripts.Aspects;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSystemGroup))] 
public partial struct UnitCollisionSystem : ISystem
{
    ComponentLookup<Unit> _componentLookup;   

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        _componentLookup = state.GetComponentLookup<Unit>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        _componentLookup.Update(ref state); 
        state.Dependency.Complete();

        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        state.Dependency = new UnitColllisionEvent
        {
            ComponentLookup = _componentLookup,
            ECB = ecb

        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), new Unity.Jobs.JobHandle());
        
    }
}

[BurstCompile]
public partial struct UnitColllisionEvent : ICollisionEventsJob
{
    [NativeDisableParallelForRestriction] public ComponentLookup<Unit> ComponentLookup;
    public EntityCommandBuffer ECB;

    public void Execute(CollisionEvent collisionEvent)
    {
        SetFighting(collisionEvent.EntityA);
        SetFighting(collisionEvent.EntityB);
    }

    void SetFighting(Entity entity)
    {
        var unit = ComponentLookup[entity];
        unit.IsFighting = true;

        ECB.SetComponent(entity, unit);
    }
}
