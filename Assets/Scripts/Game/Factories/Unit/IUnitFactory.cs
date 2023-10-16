using Game.Settings.Unit;
using Game.Views.Unit;
using Zenject;

namespace Game.Factories.Unit
{
    public interface IUnitFactory : IFactory<IUnitView, UnitParameters, Presenters.Unit.Impl.Unit>
    {
        
    }
}