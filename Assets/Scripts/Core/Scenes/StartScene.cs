using Assets.Scripts.Core.Audio;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private IMainMenuScreen MainMenuScreen;
    private IAudioManager AudioManager;

    private void Awake()
    {
        var resourceManager = CompositionRoot.GetResourceManager();
        var uiRoot = CompositionRoot.GetUIRoot();
        var saveManager = CompositionRoot.GetSaveManager();
        AudioManager = CompositionRoot.GetAudioManager();

        resourceManager.CreatePrefabInstance<EComponents>(EComponents.AudioListener);

        MainMenuScreen = CompositionRoot.GetMainMenuScreen();
        MainMenuScreen.Show();
    }

    private void Start()
    {
        if (!AudioManager.IsMusicAlreadyPlaying(EAudio.Main_Music))
        {
            AudioManager.PlayMusic(EAudio.Main_Music);
        }
    }
}
