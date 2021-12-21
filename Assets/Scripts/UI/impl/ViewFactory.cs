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

    public IHUDWeaponView CreateHUDWeapon()
    {
        var view = ResourceManager.CreatePrefabInstance<IHUDWeaponView, EViews>(EViews.HUDWeapon);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }

    public IHUDLevelView CreateHUDLevel()
    {
        var view = ResourceManager.CreatePrefabInstance<IHUDLevelView, EViews>(EViews.HUDLevel);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }
}
