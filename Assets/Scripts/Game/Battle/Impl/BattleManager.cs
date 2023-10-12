using System.Collections.Generic;
using Game.Models.Attributes;
using Game.Presenters.Unit;
using Game.Services.Buff;

namespace Game.Battle.Impl
{
    public class CombatManager : ICombatManager
    {
        private readonly IBuffService _buffService;
        private readonly Queue<IUnit> _units = new();
        private bool _combatStarted;
        private int _currentRound;
        private IUnit _currentUnit;

        public CombatManager(IBuffService buffService)
        {
            _buffService = buffService;
        }
        
        public void StartCombat(IEnumerable<IUnit> units)
        {
            if(_combatStarted)
                return;
            
            _combatStarted = true;
            
            foreach (var unit in units)
            {
                _units.Enqueue(unit);
                
                unit.Dead += CompleteBattle;
            }

            _currentUnit = _units.Dequeue();
        }

        public void Attack()
        {
            var target = GetTarget();
            var damage = _currentUnit.Attributes[EAttributeType.AttackDamage];
            
            target.HandleAttack(damage.Value, _currentUnit.AttackModifiers);

            NextRound();
        }

        public void ApplyBuff()
        {
            var canTakeBuff = _buffService.TryGetBuff(_currentUnit, out var buff);
            
            if(canTakeBuff)
                _currentUnit.AddBuff(buff);
        }

        private void NextRound()
        {
            _units.Enqueue(_currentUnit);

            _currentUnit = _units.Dequeue();

            _currentRound++;
        }

        private void CompleteBattle()
        {
            foreach (var unit in _units)
            {
                unit.Dead -= CompleteBattle;
            }
        }

        private IUnit GetTarget()
        {
            var target = _units.Peek();

            return target;
        }
        
    }
}