using System;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States;
using SimpleUi.Abstracts;
using UI.Views.Round;
using Zenject;

namespace UI.Controllers.Round
{
    public class RoundCounterController : UiController<RoundCounterView>, 
        IInitializable, 
        IDisposable
    {
        [Inject] private readonly BattleStateMachine _battleStateMachine;

        private int _roundNumValue;
        
        public void Initialize()
        {
            _battleStateMachine.StateChanged += OnBattleStateChanged;
        }

        private void OnBattleStateChanged(EBattleState state)
        {
            if(state != EBattleState.StartNewRound)
                return;

            _roundNumValue++;

            DisplayRound(_roundNumValue);
        }

        private void DisplayRound(int round)
        {
            View.SetRoundCounter(round);
        }

        public void Dispose()
        {
            _battleStateMachine.StateChanged -= OnBattleStateChanged;
        }
    }
}