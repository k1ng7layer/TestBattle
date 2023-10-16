using System.Collections.Generic;
using Game.Settings.Unit;
using Game.Views.Unit;
using Zenject;

namespace Game.Factories.Unit.Impl
{
    public class UnitFactory : PlaceholderFactory<IUnitView, List<UnitAttributeParameters>, Presenters.Unit.Impl.Unit>, 
        IUnitFactory
    {
        
    }
}