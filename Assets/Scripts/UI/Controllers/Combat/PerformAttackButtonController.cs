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
        // [Inject] private readonly BattleStateMachine _battleStateMachine;
        
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
            
            View.SetState(_stateMachine.CurrentStateBase.StateName is EBattleState.WaitForAction or EBattleState.WaitForAttack);
            _stateMachine.StateChanged += OnAttackState;
        }

        private void Attack()
        {
            _stateMachine.ChangeState(EBattleState.Attack);
        }

        private void OnAttackState(EBattleState state)
        {
            var canAttack = state != EBattleState.WaitForRoundEnd;
            
            View.SetState(canAttack);
        }

        protected override void OnDispose()
        {
            _stateMachine.StateChanged -= OnAttackState;
        }
    }
}