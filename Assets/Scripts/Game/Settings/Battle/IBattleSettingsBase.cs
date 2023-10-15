using Game.Models.Combat;

namespace Game.Settings.Battle
{
    public interface IBattleSettingsBase
    {
        EBattleTeam FirstTurn { get; }
    }
}