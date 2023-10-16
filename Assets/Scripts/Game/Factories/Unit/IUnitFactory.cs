using System.Collections.Generic;
using Game.Settings.Unit;
using Game.Views.Unit;
using Zenject;

namespace Game.Factories.Unit
{
    public interface IUnitFactory : IFactory<IUnitView, List<UnitAttributeParameters>, Presenters.Unit.Impl.Unit>
    {
        
    }
}