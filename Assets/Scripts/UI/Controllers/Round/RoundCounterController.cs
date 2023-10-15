using System;
using Game.Services.Round;
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

        public RoundCounterController(IRoundProvider roundProvider)
        {
            _roundProvider = roundProvider;
        }

        public void Initialize()
        {
            _roundProvider.RoundChanged += DisplayRound;
            
            DisplayRound(_roundProvider.CurrentRound);
        }

        private void DisplayRound(int round)
        {
            View.SetRoundCounter(round);
        }

        public void Dispose()
        {
            _roundProvider.RoundChanged -= DisplayRound;
        }
    }
}