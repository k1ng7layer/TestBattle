using System;
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
    public class ApplyBuffController : UnitLinkableController<ApplyBuffButtonView>, 
        IDisposable
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
            View.applyButton.OnClickAsObservable().Subscribe(_ => AddBuff()).AddTo(View);
            _attackQueueService.ActiveUnitChanged += OnAttackUnitChanged;
            View.SetState(_attackQueueService.ActiveMember.Unit == Unit);
            
            Unit.Buffed += OnBuffed;
            Unit.BuffExpired += OnBuffExpired;
        }

        private void OnBuffExpired(Buff obj)
        {
            var hasMaxBuffs = Unit.StaticBuffs.Count == _battleSettingsBase.MaxActiveBuffs;
            
            View.SetState(!hasMaxBuffs);
        }

        private void OnBuffed(Buff obj)
        {
            var hasMaxBuffs = Unit.StaticBuffs.Count == _battleSettingsBase.MaxActiveBuffs;
            
            View.SetState(!hasMaxBuffs);
        }

        private void AddBuff()
        {
            _combatManager.ApplyBuff();
        }
        
        private void OnAttackUnitChanged(BattleMember battleMember)
        {
           View.SetState(battleMember.Unit == Unit);
        }

        public void Dispose()
        {
            _attackQueueService.ActiveUnitChanged -= OnAttackUnitChanged;
        }
    }
}