using System.Collections.Generic;
using Game.Models.Modifiers;
using Game.Presenters.Unit;

namespace Game.Models.Combat
{
    public class AttackData
    {
        public IUnit Attacker;
        public IUnit Target;
        public float Damage;
        public List<AttributeModifier> AttackModifiers;
    }
}