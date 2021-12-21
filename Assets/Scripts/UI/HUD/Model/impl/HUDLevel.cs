using UnityEngine;

public class HUDLevel : MonoBehaviour, IHUDLevel
{
    private IHUDLevelView View;
    private ILevelSystem LevelSystem;

    private void Awake()
    {
        var viewFactory = CompositionRoot.GetViewFactory();
        View = viewFactory.CreateHUDLevel();
        LevelSystem = CompositionRoot.GetLevelSystem();
    }

    private void Start()
    {
        LevelSystem.ExperiencePercentageChanged += OnExperiencePercentageChanged;
        LevelSystem.LevelUp += OnLevelUp;
    }

    private void OnExperiencePercentageChanged(float levelPercent)
{
        View.SetLevelBarPercent(levelPercent);
    }

    private void OnLevelUp(int level)
    {
        View.SetLevelText(level.ToString());
    }

    public void Hide()
    {
        View.Hide();
    }

    public void Show()
    {
        View.Show();
    }
}
