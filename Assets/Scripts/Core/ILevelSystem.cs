using System;

public interface ILevelSystem
{
    event Action<float> ExperiencePercentChanged;
    event Action<int> LevelUp;
    int GetLevel();
    float GetExperiencePercent();
}
