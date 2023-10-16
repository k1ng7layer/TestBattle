using System;
using Game.Services.Round;
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
        private readonly IRoundProvider _roundProvider;
        
        [Inject] private readonly BattleStateMachine _battleStateMachine;

        private int _roundNumValue;
        
        public RoundCounterController(IRoundProvider roundProvider)
        {
            _roundProvider = roundProvider;
        }

        public void Initialize()
        {
            _roundProvider.RoundChanged += DisplayRound;
            
            DisplayRound(_roundProvider.CurrentRound);

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