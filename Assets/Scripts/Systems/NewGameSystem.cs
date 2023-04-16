using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using System.Linq;
using System.Diagnostics;
using Assets.Scripts.Authoring;
using Unity.Burst.Intrinsics;
using Unity.Entities.UniversalDelegates;
using Unity.Transforms;

namespace Assets.Scripts.Systems
{
    [BurstCompile]
    partial struct NewGameSystem : ISystem
    {
        private EntityQuery query;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GameSettings>();
            state.RequireForUpdate<Prefabs>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            // 1. Read from the GameSettings to detect the player's avatar choice, etc. 
            // 2. Create a job that does overall game setup, including setting the player's avatar choice on the player object
            // 3. The player object will be baked with entity prefabs (?) - the player entity will spawn a renderer based on the above index? This could be spawned here in the game setup job.
            // 4. 
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            PrefabAspect prefabs = new();
            // We use foreach because systemAPI query's code generator requires it
            foreach (var prefab in SystemAPI.Query<PrefabAspect>())
            {
                prefabs = prefab;
                break;
            }
            
            foreach (var setting in SystemAPI.Query<RefRO<GameSettings>>())
            {                
                UnityEngine.Debug.Log(" --> Starting new game setup job in NewGameSystem.cs");

                // 1. Get the player's selected avatar entity and instantiate it 
                ecb.Instantiate(prefabs.GetPrefab(PrefabTypes.PlayerAvatars, setting.ValueRO.SelectedAvatar));
             
                // 2. Spawn jobs for more intensive setup, such as world generation 
                /*new NewGameJob
                {
                    ECB = ecb,
                    Random = new Random((uint)UnityEngine.Random.Range(1, 10000)),
                    Settings = setting.ValueRO                    
                }.Schedule();*/
                       
            }


        }
    }

    [BurstCompile]
    partial struct NewGameJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        public Random Random;
        public GameSettings Settings;  

        // Add any initial game setup here; this is executed when the new game is started 
        void Execute(in PrefabAspect Prefabs)
        {
            // @TODO this would work better with schedule parralell, done as such for prototyping only
            for (var i = 0; i < Settings.MaxStars; i++)
            {
                // there will be many pieces of logic like there; we need a more abstracted way to obtain a random set of features that might generate with dependencies                
                var starToSpawn = Random.NextFloat() > 0.3f ? GalaxyFeatures.Yellow : GalaxyFeatures.Red;
                
                var instance = ECB.Instantiate(Prefabs.GetPrefab(PrefabTypes.Galaxy, (int)starToSpawn));
                
                ECB.SetComponent(instance, new Star
                {
                    Size = Random.NextFloat(0.8f, 1.5f)
                });

                // Assign each star a random position
                // @TODO world extents will be provided by the GameSettings
                var starTransform = LocalTransform.FromPosition(
                    Random.NextFloat(-300f, 300f),
                    Random.NextFloat(-20f, 20f),
                    Random.NextFloat(-300f, 300f)
                );

                ECB.SetComponent(instance, starTransform);
            }
        }       
    }
}