using Game.Views.Unit;
using Zenject;

namespace Game.Factories.Unit.Impl
{
    public class UnitFactory : PlaceholderFactory<IUnitView, Presenters.Unit.Impl.Unit>, IUnitFactory
    {
        
    }
}