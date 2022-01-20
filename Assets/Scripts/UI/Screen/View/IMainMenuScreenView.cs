using System;

public interface IMainMenuScreenView : IView
{
    public event Action StartClicked;
    public event Action LoadClicked;
    public event Action QuitClicked;
}
