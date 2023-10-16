using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Battle;
using Zenject;

namespace Game.Factories.States.Battle.Impl
{
    public class BattleStartNewRoundStateFactory : PlaceholderFactory<BattleStateMachine, BattleStartNewRoundState>,
        IBattleStartNewRoundStateFactory
    {
        
    }
}