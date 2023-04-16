using Assets.Scripts.Authoring;
using Unity.Entities;
using Unity.Mathematics;

readonly partial struct PrefabAspect : IAspect
{
    readonly RefRO<Prefabs> m_Prefab;

    public Prefabs Prefabs => m_Prefab.ValueRO;

    public Entity GetPrefab(PrefabTypes prefabType, int index)
    {
        return prefabType switch
        {
            PrefabTypes.PlayerAvatars => GetPlayerAvatar(index),
            PrefabTypes.Galaxy => GetGalaxyFeature(index),
            _ => this.Prefabs.DefaultSpecies,
        };
    }

    Entity GetPlayerAvatar(int i)
    {
        return i switch
        {
            (int)PlayerAvatars.Collective => this.Prefabs.CollectiveSpecies,
            _ => this.Prefabs.DefaultSpecies,
        };
    }
    
    Entity GetGalaxyFeature(int i)
    {
        return i switch
        {
            (int)GalaxyFeatures.Yellow => this.Prefabs.YellowStar,
            (int)GalaxyFeatures.Red => this.Prefabs.RedStar,
            _ => this.Prefabs.YellowStar,
        };
    }

}