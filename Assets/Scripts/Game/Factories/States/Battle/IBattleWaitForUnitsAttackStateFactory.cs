﻿using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Battle;
using Zenject;

namespace Game.Factories.States.Battle
{
    public interface IBattleWaitForUnitsAttackStateFactory : IFactory<BattleStateMachine, BattleWaitForUnitsActionsState>
    {
        
    }
}