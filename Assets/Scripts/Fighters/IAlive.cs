using System;

public interface IAlive
{
    event Action Died;
    event Action<float> HealthPercentChanged;
    void Hit(float damage);
}
