using Game.Models.Buffs;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Settings.Battle;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States;
using UI.Controllers.Abstractions;
using UI.Views.Combat;
using UniRx;
using Zenject;

namespace UI.Controllers.Combat
{
    public class ApplyBuffController : UnitLinkableController<ApplyBuffButtonView>
    {
        private readonly UnitStateMachine _unitStateMachine;
        
        [Inject] private readonly IBattleSettingsBase _battleSettingsBase;

        public ApplyBuffController(
            ApplyBuffButtonView view, 
            IUnit unit,
            UnitStateMachine unitStateMachine
        ) : base(view, unit)
        {
            _unitStateMachine = unitStateMachine;
        }

        public override void Initialize()
        {
            var state = _unitStateMachine.CurrentStateBase.StateName;
            _unitStateMachine.StateChanged += OnStateChanged;
            View.SetState(false);
            View.applyButton.OnClickAsObservable().Subscribe(_ => AddBuff()).AddTo(View);
            View.SetState(state == EBattleState.WaitForAction);
            Unit.Buffed += OnBuffed;
            Unit.BuffExpired += OnBuffExpired;
        }

        private void OnStateChanged(EBattleState state)
        {
            var canUse = CanUseBuff();
         
            View.SetState(canUse);
        }

        private bool CanUseBuff()
        {
            var state = _unitStateMachine.CurrentStateBase.StateName;
            var isTurn = state != EBattleState.WaitForRoundEnd && state != EBattleState.WaitForTurn;
            var hasMaxBuffs = Unit.StaticBuffs.Count == _battleSettingsBase.MaxActiveBuffs;

            return isTurn && !hasMaxBuffs;
        }

        private void OnBuffExpired(Buff obj)
        {
            var canUse = CanUseBuff();
            
            View.SetState(canUse);
        }

        private void OnBuffed(Buff obj)
        {
            var canUse = CanUseBuff();
            
            View.SetState(canUse);
        }

        private void AddBuff()
        {
            _unitStateMachine.ChangeState(EBattleState.ApplyBuff);
        }
        
        private void OnAttackUnitChanged(BattleMember battleMember)
        {
            var canUse = CanUseBuff();
            
            View.SetState(canUse);
        }

        protected override void OnDispose()
        {
            _unitStateMachine.StateChanged -= OnStateChanged;
        }
    }
}