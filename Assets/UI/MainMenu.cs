using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/CustomEditor")]
    public static void ShowExample()
    {
        MainMenu wnd = GetWindow<MainMenu>();
        wnd.titleContent = new GUIContent("CustomEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);

        // Bind Custom Button Events
        Debug.Log("Registering button handlers (all): ");
        SetupButtonHandlers();
    }

    private void SetupButtonHandlers()
    {        
        VisualElement root = rootVisualElement;

        var buttons = root.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }

    private void RegisterHandler(Button button)
    {
       Debug.Log("Registering button handler: " + button.name);

       button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {

        VisualElement root = rootVisualElement;

        Button button = evt.currentTarget as Button;
        Debug.Log("Button was clicked!" + button.name);

    }
}
