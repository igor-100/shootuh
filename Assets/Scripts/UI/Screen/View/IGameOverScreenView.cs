using System;

public interface IGameOverScreenView : IView
{
    public event Action RestartClicked;
    public event Action QuitClicked;
}
