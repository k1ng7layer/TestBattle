using Game.Presenters.Unit;
using UI.Controllers.Combat;
using UI.Views.Combat;
using Zenject;

namespace Game.Factories.Ui.Impl
{
    public class PerformAttackButtonControllerFactory : PlaceholderFactory<AttackButtonView, IUnit, PerformAttackButtonController>,
        IPerformAttackButtonControllerFactory
    {
        
    }
}