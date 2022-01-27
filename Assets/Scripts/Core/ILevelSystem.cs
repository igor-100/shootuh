using System;

public interface ILevelSystem : ISaveable
{
    event Action<float> ExperiencePercentChanged;
    event Action<int> LevelUp;

    void Init();
    int GetLevel();
    float GetExperiencePercent();
}
