using System.Collections.Generic;
using Game.Models.Buffs;
using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States;
using UI.Controllers.Abstractions;
using UI.Views.Buffs;
using Zenject;

namespace UI.Controllers.Combat
{
    public class ActiveBuffsController : UnitLinkableController<ActiveBuffsView>
    {
        [Inject] private readonly BattleStateMachine _battleStateMachine;
        
        private readonly Dictionary<string, ActiveBuffViewElement> _activeBuffsViewElements = new();

        public ActiveBuffsController(
            ActiveBuffsView view, 
            IUnit unit
        ) : base(view, unit)
        {
            
        }

        public override void Initialize()
        {
            Unit.Buffed += OnBuffed;
            Unit.BuffExpired += OnBuffExpired;
            
            _battleStateMachine.StateChanged += UpdateBuffInfo;
        }

        private void OnBuffed(Buff buff)
        {
            var buffView = View.ActiveBuffCollection.Create();
            _activeBuffsViewElements.Add(buff.BuffName, buffView);
            
            buffView.SetBuffName(buff.BuffName);
            buffView.SetBuffTick(buff.TickLeft);
        }   

        private void OnBuffExpired(Buff buff)
        {
            var buffView = _activeBuffsViewElements[buff.BuffName];
            _activeBuffsViewElements.Remove(buff.BuffName);
            
            View.ActiveBuffCollection.Remove(buffView);
        }

        private void UpdateBuffInfo(EBattleState battleState)
        {
            if(battleState != EBattleState.StartNewRound)
                return;
            
            foreach (var buffView in _activeBuffsViewElements)
            {
                var buff = Unit.StaticBuffs[buffView.Key];
                
                buffView.Value.SetBuffTick(buff.TickLeft);
            }
        }
    }
}