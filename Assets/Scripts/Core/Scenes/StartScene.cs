using UnityEngine;

public class StartScene : MonoBehaviour
{
    private IMainMenuScreen MainMenuScreen;

    private void Awake()
    {
        var uiRoot = CompositionRoot.GetUIRoot();

        MainMenuScreen = CompositionRoot.GetMainMenuScreen();
        MainMenuScreen.Show();
    }
}
