using Unity.Entities;

struct GameSettings : IComponentData
{
    public float SpeedScale;
    public float DamageScale;
    public float XpScale;
    public float CreditScale;

    public int StartFleets;
    public int StartCredits;

    public Entity AvatarEntity;
    public int SelectedAvatar;

    public bool IsStarted;

    public int MinStars;
    public int MaxStars;
}