using Game.Presenters.Unit;
using UI.Controllers.Stats;
using UI.Views.Stats;
using Zenject;

namespace Game.Factories.Ui
{
    public interface IUnitStatsControllerFactory : IFactory<UnitStatsView, IUnit, UnitStatsController>
    {
        
    }
}