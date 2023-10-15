using System;

namespace Game.Services.Round
{
    public interface IRoundProvider
    {
        int CurrentRound { get; }
        event Action<int> RoundChanged;
        void ChangeRound();
    }
}