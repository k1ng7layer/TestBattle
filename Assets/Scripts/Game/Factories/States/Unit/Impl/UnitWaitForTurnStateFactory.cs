﻿using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Unit;
using Zenject;

namespace Game.Factories.States.Unit.Impl
{
    public class UnitWaitForTurnStateFactory : PlaceholderFactory<IUnit, UnitStateMachine, UnitWaitForTurnState>, 
        IUnitWaitForTurnStateFactory
    {
        
    }
}