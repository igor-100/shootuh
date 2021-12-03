public class ViewFactory : IViewFactory
{
    private IUIRoot UIRoot;
    private ISceneLoader SceneLoader;
    private IResourceManager ResourceManager;

    public ViewFactory(IUIRoot uiRoot, IResourceManager resourceManager, ISceneLoader sceneLoader)
    {
        UIRoot = uiRoot;
        ResourceManager = resourceManager;
        SceneLoader = sceneLoader;
    }

    public IGameOverScreenView CreateGameOverScreen()
    {
        var view = ResourceManager.CreatePrefabInstance<IGameOverScreenView, EViews>(EViews.GameOver_Screen);
        view.SetParent(UIRoot.MainCanvas);

        return view;
    }
}
