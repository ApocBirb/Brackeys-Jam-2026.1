using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _exitButton;

    void OnEnable()
    {
        _document = GetComponent<UIDocument>();

        var root = _document.rootVisualElement;

        _exitButton = root.Q<Button>("Exit_Button");

        _exitButton.clicked += OnExitClicked;
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit button clicked");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
