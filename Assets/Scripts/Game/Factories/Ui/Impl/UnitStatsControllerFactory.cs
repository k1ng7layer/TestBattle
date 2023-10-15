using Game.Presenters.Unit;
using UI.Controllers.Stats;
using UI.Views.Stats;
using Zenject;

namespace Game.Factories.Ui.Impl
{
    public class UnitStatsControllerFactory : PlaceholderFactory<UnitStatsView, IUnit, UnitStatsController>,
        IUnitStatsControllerFactory
    {
        
    }
}