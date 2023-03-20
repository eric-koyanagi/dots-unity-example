using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using static UnityEngine.GraphicsBuffer;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using Assets.Scripts.Authoring;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    partial struct ArmySystem : ISystem
    {
        //private JobHandle spawnJob;
        

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            new ArmySpawnjob
            {
                ECB = ecb
            }.Schedule();

            state.Enabled = false;
        }
    }

    [BurstCompile]
    partial struct ArmySpawnjob : IJobEntity 
    {
        public EntityCommandBuffer ECB;
        void Execute(in ArmyAspect army) 
        {
            for (var i = 0; i < army.UnitCount; i++)
            {
                var instance = ECB.Instantiate(army.UnitToSpawn);
                //ECB.SetComponent(instance, WorldTransform.FromPosition(army.SpawnPos + GetOffset(i)));
                ECB.SetComponent(instance, new Unit
                {
                    Speed = -0.25f,
                    IsInFormation = true,
                    Offense = army.Offense
                });
                
                
                var unitTransform = LocalTransform.FromPosition(army.SpawnPos + GetOffset(i, army.ColumnLength, army.ColumnScale));
                ECB.SetComponent(instance, unitTransform);
                

                
            }
            //var spawnLocalToWorld = WorldTransformLookup[turret.CannonBallSpawn];
            //var cannonBallTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);

            // We are about to overwrite the transform of the new instance. If we didn't explicitly
            // copy the scale it would get reset to 1 and we'd have oversized cannon balls.
            //cannonBallTransform.Scale = WorldTransformLookup[turret.CannonBallPrefab].Scale;
            /*ECB.SetComponent(instance, cannonBallTransform);
            ECB.SetComponent(instance, new CannonBall
            {
                Speed = spawnLocalToWorld.Forward() * 20.0f
            });*/
        }

        float3 GetOffset(int i, int columnLength, float columnScale)
        {
            float row = (i % columnLength) * columnScale;
            float col = math.floor(i / columnLength) * columnScale;

            float x = col;
            float y = 0f;
            float z = row;
            return new float3(x, y, z);
        }
    }
}