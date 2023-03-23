using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Physics;
using Assets.Scripts.Authoring;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSimulationGroup))] // We are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
public partial struct UnitCollisionSystem : ISystem
{
    [BurstCompile]
    public partial struct UnitColllisionEvent : ICollisionEventsJob
    {
        ComponentLookup<Unit> lkup;

        public void Execute(CollisionEvent collisionEvent)
        {
            var data = new Unit();
            lkup.TryGetComponent(collisionEvent.EntityA, out data);
            
            if (!data.IsFighting)
            {
                data.IsFighting = true;
            }
        }
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Dependency = new UnitColllisionEvent
        {

        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), new Unity.Jobs.JobHandle());
        
    }
}
