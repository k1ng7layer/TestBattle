using System;
using System.Collections.Generic;
using Game.Models.Combat;
using Game.Services.AttackManager;
using Game.Services.AttackQueueProvider;
using Game.Services.Round;
using Game.Services.SceneLoading;
using Game.Services.TargetService;
using UnityEngine;

namespace Game.Battle.Impl
{
    public class CombatManager : ICombatManager
    {
        private readonly IAttackManager _attackManager;
        private readonly IRoundProvider _roundProvider;
        private readonly ITargetService _targetService;
        private readonly IAttackQueueService _attackQueueService;
        private readonly ISceneLoadingManager _sceneLoadingManager;
        
        private List<BattleMember> _members;
        private bool _combatStarted;

        public CombatManager(IAttackManager attackManager,
            IRoundProvider roundProvider,
            ITargetService targetService,
            IAttackQueueService attackQueueService,
            ISceneLoadingManager sceneLoadingManager
        )
        {
            _attackManager = attackManager;
            _roundProvider = roundProvider;
            _targetService = targetService;
            _attackQueueService = attackQueueService;
            _sceneLoadingManager = sceneLoadingManager;
        }

        public event Action<BattleMember, BattleMember> BattleStarted;

        public void StartCombat(List<BattleMember> battleMembers)
        {
            if(_combatStarted)
                return;
            
            _combatStarted = true;
            _members = battleMembers;
            
            _attackQueueService.InitializeQueue(battleMembers);
            _targetService.AddBattleMembers(_members);
            
            _roundProvider.RoundChanged += OnRoundChanged;
            
            foreach (var battleMember in _members)
            {
                battleMember.Unit.Dead += CompleteBattle;
            }
            
            _attackQueueService.NextAttacker();
            
            BattleStarted?.Invoke(_members[0], _members[1]);
        }

        public void Attack()
        {
            var attacker = _attackQueueService.ActiveMember;
            Debug.Log($"Attack by Attacker {attacker.BattleTeam}");
            var target = _targetService.GetTarget(attacker);
            
            _attackManager.DoAttack(attacker.Unit, target.Unit);
            
            var completed = TryChangeRound();
            
            if(!completed)
                _attackQueueService.NextAttacker();
        }

        public void ApplyBuff()
        {
            _attackManager.ApplyBuff(_attackQueueService.ActiveMember.Unit);
        }

        private void OnRoundChanged(int _)
        {
            _attackQueueService.InitializeQueue(_members);
            _attackQueueService.NextAttacker();
            foreach (var battleMember in _members)
            {
                battleMember.Unit.UpdateState();
            }
            
        }

        private bool TryChangeRound()
        {
            if (_attackQueueService.AttackersInRoundLeft == 0)
            {
                _roundProvider.ChangeRound();

                return true;
            }

            return false;
        }

        private void CompleteBattle()
        {
            foreach (var unit in _members)
            {
                unit.Unit.Dead -= CompleteBattle;
            }
            
            _roundProvider.RoundChanged -= OnRoundChanged;
            _sceneLoadingManager.RestartGame();
        }
    }
}