﻿using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;
using Game.StateMachine.States.Impl.Unit;
using Zenject;

namespace Game.Factories.States.Unit
{
    public interface IUnitAttackStateFactory : IFactory<IUnit, IUnit, UnitStateMachine, UnitAttackState>
    {
        
    }
}