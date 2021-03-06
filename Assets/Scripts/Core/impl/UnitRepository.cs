using System;
using System.Collections.Generic;

public class UnitRepository : IUnitRepository
{
    private Dictionary<string, List<IAlive>> unitRepository = new Dictionary<string, List<IAlive>>();

    public event Action<IAlive> UnitAdded = unit => { };
    public event Action<IAlive> UnitRemoved = unit => { };
    public event Action AllUnitsRemoved = () => { };

    public void AddUnit(IAlive unit)
    {
        var type = unit.GetType().Name;
        if (!unitRepository.ContainsKey(type))
        {
            unitRepository.Add(type, new List<IAlive>());
        }

        unitRepository.TryGetValue(type, out List<IAlive> units);
        units.Add(unit);

        UnitAdded(unit);
        unit.Died += OnUnitDied;
    }

    private void OnUnitDied(IAlive unit)
    {
        unit.Died -= OnUnitDied;

        var type = unit.GetType().Name;
        unitRepository.TryGetValue(type, out List<IAlive> units);
        units.Remove(unit);
        UnitRemoved(unit);
        if (units.Count == 0)
        {
            AllUnitsRemoved();
        }
    }
}
