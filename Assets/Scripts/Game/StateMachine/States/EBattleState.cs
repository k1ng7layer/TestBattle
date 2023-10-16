namespace Game.StateMachine.States
{
    public enum EBattleState
    {
        Attack,
        ApplyBuff,
        WaitForAction,
        WaitForAttack,
        WaitForRoundEnd,
        WaitForTurn,
        WaitForPlayersAttack,
        StartNewRound
    }
}