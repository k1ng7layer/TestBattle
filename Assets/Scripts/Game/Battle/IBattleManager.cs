using System.Collections.Generic;
using Game.Presenters.Unit;

namespace Game.Battle
{
    public interface ICombatManager
    {
        void StartCombat(IEnumerable<IUnit> units);
        void Attack();
        void ApplyBuff();
    }
}