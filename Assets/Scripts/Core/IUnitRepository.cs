using System;

public interface IUnitRepository
{
    event Action<IAlive> UnitAdded;
    event Action<IAlive> UnitRemoved;
    event Action AllUnitsRemoved;

    void AddUnit(IAlive unit);
}
