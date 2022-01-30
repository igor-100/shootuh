public interface IViewFactory
{
    IPauseScreenView CreatePauseScreen();
    IGameOverScreenView CreateGameOverScreen();
    IMainMenuScreenView CreateMainMenuScreen();
    ILoadScreenView CreateLoadScreen();
    IHUDWeaponView CreateHUDWeapon();
    IHUDLevelView CreateHUDLevel();
}
