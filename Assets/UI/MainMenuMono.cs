using Assets.Scripts.MonoBehaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuMono : MonoBehaviour
{
    private Button _continue;
    private Button _newGame;   
    private Button _loadGame;
    private Button _settings;
    private Button _exit;

    // Toggle the main menu
    public void SetMainMenuVisibility(bool state = true)
    {
        gameObject.SetActive(state);
    }

    // Find each button from the visual tree, then register all callbacks
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _continue = uiDocument.rootVisualElement.Q("continue") as Button;
        _newGame = uiDocument.rootVisualElement.Q("newGame") as Button;
        _loadGame = uiDocument.rootVisualElement.Q("loadGame") as Button;
        _settings = uiDocument.rootVisualElement.Q("settings") as Button;
        _exit = uiDocument.rootVisualElement.Q("exit") as Button;

        RegisterButtonCallbacks();
    }    

    private void RegisterButtonCallbacks()
    {
        _continue.RegisterCallback<ClickEvent>(Continue);
        _newGame.RegisterCallback<ClickEvent>(NewGame);
        _loadGame.RegisterCallback<ClickEvent>(LoadGame);
        _settings.RegisterCallback<ClickEvent>(Settings);
        _exit.RegisterCallback<ClickEvent>(Exit);
    }

    private void OnDisable()
    {
        _continue.UnregisterCallback<ClickEvent>(Continue);
        _newGame.UnregisterCallback<ClickEvent>(NewGame);
        _loadGame.UnregisterCallback<ClickEvent>(LoadGame);
        _settings.UnregisterCallback<ClickEvent>(Settings);
        _exit.UnregisterCallback<ClickEvent>(Exit);
    }

    private void Continue(ClickEvent evt)
    {
        Debug.Log("Continue Game Button Pressed. Starting New Game.");
        SetMainMenuVisibility(false);
    }
    
    // Hides the main menu, 
    private void NewGame(ClickEvent evt)
    {
        Debug.Log("New Game Button Pressed. Starting New Game.");
        SetMainMenuVisibility(false);
        UIManager.instance.NewGameMenu.SetActive(true);
        //GameContainer.Instance.NewGameMenu.gameObject.SetActive(true);
    }

    private void LoadGame(ClickEvent evt)
    {
        Debug.Log("Load Game Button Pressed. Loading a Game.");
        SetMainMenuVisibility(false);

    }
    private void Settings(ClickEvent evt)
    {
        Debug.Log("Settings button pressed");        
    }

    private void Exit(ClickEvent evt)
    {
        Application.Quit();
    }



}
