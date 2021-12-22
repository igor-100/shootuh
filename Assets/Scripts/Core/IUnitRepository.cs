using System;

public interface IUnitRepository
{
    event Action<IAlive> UnitAdded;
    event Action<IAlive> UnitRemoved;

    void AddUnit(IAlive unit);
}
