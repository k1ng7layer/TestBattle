using System;

namespace Game.Services.Round.Impl
{
    public class RoundProvider : IRoundProvider
    {
        private int _currentRound = 1;
        
        public int CurrentRound => _currentRound;
        
        public event Action<int> RoundChanged;
        
        public void ChangeRound()
        {
            _currentRound++;
            
            RoundChanged?.Invoke(_currentRound);
        }
    }
}