using Unity.Entities;
using Unity.Mathematics;

readonly partial struct ArmyAspect : IAspect
{
    readonly RefRO<Army> m_Army;

    public Army army => m_Army.ValueRO;

    public float3 SpawnPos => m_Army.ValueRO.ArmyTransform;
    public int UnitCount => m_Army.ValueRO.UnitCount;
    public float UnitSpeed => m_Army.ValueRO.UnitSpeed;
    public int ColumnLength => m_Army.ValueRO.ColumnLength;
    public float ColumnScale => m_Army.ValueRO.ColumnScale;
    public Entity UnitToSpawn => m_Army.ValueRO.UnitPrefab;

}
