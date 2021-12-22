using System;

public interface IAlive
{
    event Action<IAlive> Died;
    event Action<float> HealthPercentChanged;
    void Hit(float damage);
}
