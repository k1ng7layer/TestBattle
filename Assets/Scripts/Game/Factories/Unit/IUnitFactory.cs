using Game.Views.Unit;
using Zenject;

namespace Game.Factories.Unit
{
    public interface IUnitFactory : IFactory<IUnitView, Presenters.Unit.Impl.Unit>
    {
        
    }
}