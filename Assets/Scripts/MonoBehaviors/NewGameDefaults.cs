using UnityEngine;

public class NewGameDefaults : MonoBehaviour
{
    [Header("Prototyping Settings")]
    public float SpeedScale = 1f;
    public float DamageScale = 1f;
    public float XpScale = 1f;
    public float CreditScale = 1f;

    [Header("Player Defaults")]
    [Range(1, 100)]
    public int StartFleets = 4;
    public int StartCredits = 0;

    [Header("Avatar Choices")]
    public int SelectedAvatar = 0;

    [Header("World Defaults")]
    public int MinStars = 500;
    public int MaxStars = 800;
}