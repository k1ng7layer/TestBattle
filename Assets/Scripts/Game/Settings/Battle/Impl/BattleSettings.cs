using UnityEngine;

namespace Game.Settings.Battle.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(BattleSettings), fileName = nameof(BattleSettings))]
    public class BattleSettings : ScriptableObject, 
        IBattleSettingsBase
    {
        [SerializeField] private int maxActiveBuffs;

        public int MaxActiveBuffs => maxActiveBuffs;
    }
}