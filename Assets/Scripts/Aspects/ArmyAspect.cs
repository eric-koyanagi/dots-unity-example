using Unity.Entities;
using Unity.Mathematics;

readonly partial struct ArmyAspect : IAspect
{
    readonly RefRO<Army> m_Army;

    public float3 SpawnPos => m_Army.ValueRO.ArmyTransform;
    public int UnitCount => m_Army.ValueRO.UnitCount;
    public int ColumnLength => m_Army.ValueRO.ColumnLength;
    public float ColumnScale => m_Army.ValueRO.ColumnScale;
    public Entity UnitToSpawn => m_Army.ValueRO.UnitPrefab;

    public bool Offense => m_Army.ValueRO.Offense;
    public bool Defense => !m_Army.ValueRO.Offense;
}
