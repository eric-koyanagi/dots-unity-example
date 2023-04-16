using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : UnityEngine.MonoBehaviour
{
    public GameObject UnitPrefab;

    class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Player
            {                

            });
        }
    }
}
struct Player : IComponentData 
{
    public Entity UnitPrefab;

}
