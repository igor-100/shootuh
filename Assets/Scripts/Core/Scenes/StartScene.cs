using UnityEngine;

public class StartScene : MonoBehaviour
{
    private IMainMenuScreen MainMenuScreen;

    private void Awake()
    {
        var uiRoot = CompositionRoot.GetUIRoot();
        var saveManager = CompositionRoot.GetSaveManager();

        MainMenuScreen = CompositionRoot.GetMainMenuScreen();
        MainMenuScreen.Show();
    }
}
