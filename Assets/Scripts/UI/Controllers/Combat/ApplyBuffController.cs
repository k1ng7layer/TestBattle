using Game.Battle;
using Game.Models.Buffs;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Services.AttackQueueProvider;
using Game.Settings.Battle;
using UI.Controllers.Abstractions;
using UI.Views.Combat;
using UniRx;
using Zenject;

namespace UI.Controllers.Combat
{
    public class ApplyBuffController : UnitLinkableController<ApplyBuffButtonView>
    {
        [Inject] private readonly ICombatManager _combatManager;
        [Inject] private readonly IAttackQueueService _attackQueueService;
        [Inject] private readonly IBattleSettingsBase _battleSettingsBase;

        public ApplyBuffController(
            ApplyBuffButtonView view, 
            IUnit unit) : base(view, unit)
        {
            
        }

        public override void Initialize()
        {
            _attackQueueService.ActiveUnitChanged += OnAttackUnitChanged;
            
            View.SetState(false);
            View.applyButton.OnClickAsObservable().Subscribe(_ => AddBuff()).AddTo(View);
            View.SetState(_attackQueueService.ActiveMember.Unit == Unit);
            Unit.Buffed += OnBuffed;
            Unit.BuffExpired += OnBuffExpired;
        }

        private bool CanUseBuff()
        {
            var isTurn = _attackQueueService.ActiveMember.Unit == Unit;
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
            _combatManager.ApplyBuff();
        }
        
        private void OnAttackUnitChanged(BattleMember battleMember)
        {
            var canUse = CanUseBuff();
            
            View.SetState(canUse);
        }

        protected override void OnDispose()
        {
            _attackQueueService.ActiveUnitChanged -= OnAttackUnitChanged;
        }
    }
}