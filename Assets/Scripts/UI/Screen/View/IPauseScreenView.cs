using System;

public interface IPauseScreenView : IView
{
    public event Action ResumeClicked;
    public event Action RestartClicked;
    public event Action QuitClicked;
}
