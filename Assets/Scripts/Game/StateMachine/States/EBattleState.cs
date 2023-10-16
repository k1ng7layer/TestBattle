namespace Game.StateMachine.States
{
    public enum EBattleState
    {
        Attack,
        ApplyBuff,
        WaitForAction,
        WaitForRoundEnd,
        WaitForTurn,
        WaitForPlayersAttack,
        StartNewRound,
        BattleComplete
    }
}