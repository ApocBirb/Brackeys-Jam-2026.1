using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _exitButton;
    private Button _settingsButton;
    private Button _settingsBackButton;
    private Slider _volumeSlider;
    private VisualElement _mainMenuContainer;
    private VisualElement _settingsContainer;

    void OnEnable()
    {
        _document = GetComponent<UIDocument>();

        var root = _document.rootVisualElement;

        _exitButton = root.Q<Button>("Exit_Button");
        _settingsButton = root.Q<Button>("Settings_Button");
        _settingsBackButton = root.Q<Button>("Settings_Back_Button");
        _volumeSlider = root.Q<Slider>("Volume_Slider");
        _mainMenuContainer = root.Q<VisualElement>("Main_Menu_Container");
        _settingsContainer = root.Q<VisualElement>("Settings_Container");

        //Persisting Sound Changes start
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = savedVolume;
        _volumeSlider.value = savedVolume;

        _volumeSlider.value = AudioListener.volume;

        _exitButton.clicked += OnExitClicked;
        _settingsButton.clicked += ToggleSettings;
        _settingsBackButton.clicked += ToggleMainMenu;

        _volumeSlider.RegisterValueChangedCallback(OnVolumeChanged);
    }

    private void OnExitClicked()
    {
        Debug.Log("Exit button clicked");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ToggleSettings()
    {
        if (_settingsContainer.style.display == DisplayStyle.None)
        {
            _settingsContainer.style.display = DisplayStyle.Flex;
            _mainMenuContainer.style.display = DisplayStyle.None;
        }
        else
        {
            _settingsContainer.style.display = DisplayStyle.None;
        }
    }

    private void ToggleMainMenu()
    {
        if (_mainMenuContainer.style.display == DisplayStyle.None)
        {
            _mainMenuContainer.style.display = DisplayStyle.Flex;
            _settingsContainer.style.display = DisplayStyle.None;
        }
        else
        {
            _mainMenuContainer.style.display = DisplayStyle.None;
        }
    }

    private void OnVolumeChanged(ChangeEvent<float> evt)
    {
        AudioListener.volume = evt.newValue;
        Debug.Log("Volume set to: " + evt.newValue);
        PlayerPrefs.SetFloat("GameVolume", evt.newValue);
        PlayerPrefs.Save();
    }
}
