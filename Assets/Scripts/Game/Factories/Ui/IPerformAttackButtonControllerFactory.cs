using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using UI.Controllers.Combat;
using UI.Views.Combat;
using Zenject;

namespace Game.Factories.Ui
{
    public interface IPerformAttackButtonControllerFactory : IFactory<AttackButtonView, IUnit, UnitStateMachine, PerformAttackButtonController>
    {
        
    }
}