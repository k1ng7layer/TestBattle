using System.Collections.Generic;
using Game.Models.Combat;
using Game.Presenters.Unit;
using Game.Services.Buff;

namespace Game.Battle
{
    public class BattleManager
    {
        private readonly IBuffService _buffService;
        private Queue<IUnit> _units;
        private int _currentRound;

        private IUnit _currentUnit;

        public BattleManager(IBuffService buffService)
        {
            _buffService = buffService;
        }

        public void StartBattle(List<IUnit> units)
        {
            foreach (var unit in units)
            {
                _units.Enqueue(unit);
                
                unit.Dead += CompleteBattle;
            }

            _currentUnit = _units.Dequeue();
        }

        private void Attack(AttackData attackData)
        {
            var target = attackData.Target;
            
            target.HandleAttack(attackData.Damage, attackData.AttackModifiers);

            NextRound();
        }

        private void ApplyBuff()
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
        
    }
}