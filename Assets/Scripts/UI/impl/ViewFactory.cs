public class ViewFactory : IViewFactory
{
    private IUIRoot UIRoot;
    private IResourceManager ResourceManager;

    public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager)
    {
        UIRoot = uiRoot;
        ResourceManager = resourceManager;
    }

    public IGameOverScreenView CreateGameOverScreen()
    {
        var view = ResourceManager.CreatePrefabInstance<IGameOverScreenView, EViews>(EViews.GameOver_Screen);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }

    public IPauseScreenView CreatePauseScreen()
    {
        var view = ResourceManager.CreatePrefabInstance<IPauseScreenView, EViews>(EViews.Pause_Screen);
        view.SetParent(UIRoot.OverlayCanvas);

        return view;
    }

    public IMainMenuScreenView CreateMainMenuScreen()
    {
        var view = ResourceManager.CreatePrefabInstance<IMainMenuScreenView, EViews>(EViews.MainMenu_Screen);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }

    public IHUDView CreateHUD()
    {
        var view = ResourceManager.CreatePrefabInstance<IHUDView, EViews>(EViews.HUD);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }
}
