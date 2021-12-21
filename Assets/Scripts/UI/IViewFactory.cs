public interface IViewFactory
{
    IPauseScreenView CreatePauseScreen();
    IGameOverScreenView CreateGameOverScreen();
    IMainMenuScreenView CreateMainMenuScreen();
    IHUDWeaponView CreateHUDWeapon();
    IHUDLevelView CreateHUDLevel();
}
