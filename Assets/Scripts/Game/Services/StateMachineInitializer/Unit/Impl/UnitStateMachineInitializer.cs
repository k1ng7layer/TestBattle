using Game.Factories.States.Unit;
using Game.Presenters.Unit;
using Game.StateMachine.StateMachine.Impl;

namespace Game.Services.StateMachineInitializer.Unit.Impl
{
    public class UnitStateMachineInitializer : IUnitStateMachineInitializer
    {
        private readonly IUnitApplyBuffStateFactory _unitApplyBuffStateFactory;
        private readonly IUnitAttackStateFactory _unitAttackStateFactory;
        private readonly IUnitStartNewRoundStateFactory _unitStartNewRoundStateFactory;
        private readonly IUnitWaitForRoundEndStateFactory _unitWaitForRoundEndStateFactory;
        private readonly IUnitWaitForActionStateFactory _waitForActionStateFactory;
        private readonly IUnitWaitForTurnStateFactory _waitForTurnStateFactory;

        public UnitStateMachineInitializer(
            IUnitApplyBuffStateFactory unitApplyBuffStateFactory,
            IUnitAttackStateFactory unitAttackStateFactory,
            IUnitStartNewRoundStateFactory unitStartNewRoundStateFactory,
            IUnitWaitForRoundEndStateFactory unitWaitForRoundEndStateFactory,
            IUnitWaitForActionStateFactory waitForActionStateFactory,
            IUnitWaitForTurnStateFactory waitForTurnStateFactory
        )
        {
            _unitApplyBuffStateFactory = unitApplyBuffStateFactory;
            _unitAttackStateFactory = unitAttackStateFactory;
            _unitStartNewRoundStateFactory = unitStartNewRoundStateFactory;
            _unitWaitForRoundEndStateFactory = unitWaitForRoundEndStateFactory;
            _waitForActionStateFactory = waitForActionStateFactory;
            _waitForTurnStateFactory = waitForTurnStateFactory;
        }
        
        public void InitializeStates(IUnit selfUnit, IUnit enemyUnit, UnitStateMachine stateMachine)
        {
            stateMachine.AddState(_unitApplyBuffStateFactory.Create(selfUnit, stateMachine));
            stateMachine.AddState(_unitAttackStateFactory.Create(selfUnit, enemyUnit, stateMachine));
            stateMachine.AddState(_unitStartNewRoundStateFactory.Create(selfUnit, stateMachine));
            stateMachine.AddState(_unitWaitForRoundEndStateFactory.Create(selfUnit, stateMachine));
            stateMachine.AddState(_waitForActionStateFactory.Create(selfUnit, stateMachine));
            stateMachine.AddState(_waitForTurnStateFactory.Create(selfUnit, stateMachine));
        }
    }
}