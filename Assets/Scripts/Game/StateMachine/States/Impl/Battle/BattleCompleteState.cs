using Game.Services.SceneLoading;
using Game.StateMachine.StateMachine;
using Zenject;

namespace Game.StateMachine.States.Impl.Battle
{
    public class BattleCompleteState : StateBase
    {
        [Inject] private readonly ISceneLoadingManager _sceneLoadingManager;
        
        public BattleCompleteState(StateMachineBase stateMachine) : base(stateMachine)
        {
        }

        public override EBattleState StateName => EBattleState.BattleComplete;

        protected override void OnExecute()
        {
            _sceneLoadingManager.RestartGame();
        }
    }
}