using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using UI.Controllers.Combat;
using UI.Views.Combat;
using Zenject;

namespace Game.Factories.Ui.Impl
{
    public class ApplyBuffControllerFactory : PlaceholderFactory<ApplyBuffButtonView, IUnit, UnitStateMachine, ApplyBuffController>,
        IApplyBuffControllerFactory
    {
        
    }
}