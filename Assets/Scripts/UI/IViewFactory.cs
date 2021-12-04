public interface IViewFactory
{
    IPauseScreenView CreatePauseScreen();
    IGameOverScreenView CreateGameOverScreen();
    IMainMenuScreenView CreateMainMenuScreen();
    IHUDView CreateHUD();
}
