﻿using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Unit;
using Zenject;

namespace Game.Factories.States.Unit
{
    public interface IUnitWaitForRoundEndStateFactory : IFactory<IUnit, UnitStateMachine, UnitWaitForRoundEndState>
    {
        
    }
}