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
                ECB = ecb,
                Random = new Random((uint)UnityEngine.Random.Range(1, 10000))
            }.Schedule();

            state.Enabled = false;
        }
    }

    [BurstCompile]
    partial struct ArmySpawnjob : IJobEntity 
    {
        public EntityCommandBuffer ECB;
        public Random Random;

        void Execute(in ArmyAspect army) 
        {
            for (var i = 0; i < army.UnitCount; i++)
            {
                var instance = ECB.Instantiate(army.UnitToSpawn);
                
                //ECB.SetComponent(instance, WorldTransform.FromPosition(army.SpawnPos + GetOffset(i)));
                ECB.SetComponent(instance, new Unit
                {
                    Speed = army.UnitSpeed,
                    IsInFormation = true,
                });
                
                
                var unitTransform = LocalTransform.FromPosition(army.SpawnPos + GetOffset(i, army.ColumnLength, army.ColumnScale));
                ECB.SetComponent(instance, unitTransform);
               
            }
        }

        float3 GetOffset(int i, int columnLength, float columnScale)
        {
            float row = (i % columnLength) * columnScale;
            float col = math.floor(i / columnLength) * columnScale;

            float x = col;
            float y = Random.NextFloat(-3f, 3f);
            float z = row;
            return new float3(x, y, z);
        }
       
    }
}