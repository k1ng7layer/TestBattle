using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States;
using UI.Controllers.Abstractions;
using UI.Views.Combat;
using UniRx;

namespace UI.Controllers.Combat
{
    public class PerformAttackButtonController : UnitLinkableController<AttackButtonView>
    {
        private readonly UnitStateMachine _stateMachine;

        public PerformAttackButtonController(
            AttackButtonView view, 
            IUnit unit, 
            UnitStateMachine stateMachine) : base(view, unit)
        {
            _stateMachine = stateMachine;
        }

        public override void Initialize()
        {
            View.attackButton.OnClickAsObservable().Subscribe(_ => Attack()).AddTo(View);
            _stateMachine.StateChanged += OnStateChanged;
            
            var stateName = _stateMachine.CurrentStateBase.StateName;
            var canAttack = stateName is EBattleState.WaitForAction;
            
            View.SetState(canAttack);
        }

        private void Attack()
        {
            _stateMachine.ChangeState(EBattleState.Attack);
        }

        private void OnStateChanged(EBattleState state)
        {
            var canAttack = state != EBattleState.WaitForRoundEnd;
            
            View.SetState(canAttack);
        }

        protected override void OnDispose()
        {
            _stateMachine.StateChanged -= OnStateChanged;
        }
    }
}