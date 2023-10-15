using System.Collections.Generic;
using Game.Presenters.Unit;
using Game.Presenters.Unit.Impl;

namespace Game.Services.UnitRepository
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> Units { get; }
        void Add(IUnit unit);
        bool Remove(IUnit unit);
    }
}