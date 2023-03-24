using Assets.Scripts.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.UIElements;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    partial struct UnitMoveJob : IJobEntity
    {
        // A regular EntityCommandBuffer cannot be used in parallel, a ParallelWriter has to be explicitly used.
        public EntityCommandBuffer.ParallelWriter ECB;
        // Time cannot be directly accessed from a job, so DeltaTime has to be passed in as a parameter.
        public float DeltaTime;

        // The ChunkIndexInQuery attributes maps the chunk index to an int parameter.
        // Each chunk can only be processed by a single thread, so those indices are unique to each thread.
        // They are also fully deterministic, regardless of the amounts of parallel processing happening.
        // So those indices are used as a sorting key when recording commands in the EntityCommandBuffer,
        // this way we ensure that the playback of commands is always deterministic.
        void Execute([ChunkIndexInQuery] int chunkIndex, ref UnitAspect Unit)
        {            
            if (Unit.IsInFormation)
            {
                // for now, keep one army stationary as POC of basic systems
                if (!Unit.Offense) return; 

                // --> Classic non-phsyics movement
                //Unit.Position += new float3(0f, 0f, Unit.Speed) * DeltaTime;

                // --> DOTS compatible phyiscs movement
                Unit.Velocity = new float3(0f, 0f, Unit.Speed);

                // TODO complete logic to "break formation" will move to a different system
                if (Unit.Position.z < -10f)
                {
                    Unit.IsInFormation = false;
                }
            } else {
                // --> Classic non-phsyics movement
                //Unit.Position += new float3(0f, 0f, Unit.Speed * 3f) * DeltaTime;                 

                // --> DOTS compatible phyiscs movement
                if (!Unit.IsFighting)
                {
                    Unit.Velocity = new float3(0f, 0f, Unit.Speed * 3f);
                } else
                {
                    var velocity = math.up() * 1f;
                    if (math.distancesq(Unit.TargetPosition, Unit.Position) > 3f) {
                        velocity = math.normalize(Unit.TargetPosition - Unit.Position) * Unit.Speed;
                    }

                    Unit.Velocity = velocity;
                }
            }
        }
    }

    partial struct UnitMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var unitJob = new UnitMoveJob
            {
                // Note the function call required to get a parallel writer for an EntityCommandBuffer.
                ECB = ecb.AsParallelWriter(),
                // Time cannot be directly accessed from a job, so DeltaTime has to be passed in as a parameter.
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            unitJob.ScheduleParallel();
        }
    }
}