using Assets.Scripts.MonoBehaviors;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UIElements;

public class NewGameMono : MonoBehaviour
{
    public NewGameDefaults Defaults;
    private Button _start;

    UIDocument _document;

    private PlayerAvatars AvatarSelection = PlayerAvatars.Default;

    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        _document = GetComponent<UIDocument>();

        // Initialize events to select different species
        RegisterAvatarSelectEvents();

        // Register Start game event
        _start = _document.rootVisualElement.Q("start") as Button;
        _start.RegisterCallback<ClickEvent>(StartGame);
    }

    void RegisterAvatarSelectEvents()
    {
        _document.rootVisualElement.Q("defaultSpecies").RegisterCallback<ClickEvent>(SelectDefaultAvatar);
        _document.rootVisualElement.Q("collectiveSpecies").RegisterCallback<ClickEvent>(SelectCollectiveAvatar);
    }

    void SelectDefaultAvatar(ClickEvent evt) { AvatarSelection  = PlayerAvatars.Default; }
    void SelectCollectiveAvatar(ClickEvent evt) { AvatarSelection  = PlayerAvatars.Collective; }


    void StartGame(ClickEvent evt)
    {        
        // @TODO move this logic out of the UI; just emit an event here instead

        // Create a settings entity, which will trigger NewGameSystem to run
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity entity = entityManager.CreateEntity();
        entityManager.AddComponentData(entity, new GameSettings
        {
            SpeedScale = this.Defaults.SpeedScale,
            DamageScale = this.Defaults.DamageScale,
            XpScale = this.Defaults.XpScale,
            CreditScale = this.Defaults.CreditScale,

            StartFleets = this.Defaults.StartFleets,
            StartCredits = this.Defaults.StartCredits,

            SelectedAvatar = (int)AvatarSelection,
            MinStars = this.Defaults.MinStars,
            MaxStars = this.Defaults.MaxStars
        });

        UIManager.instance.CloseAll();
    }


}
