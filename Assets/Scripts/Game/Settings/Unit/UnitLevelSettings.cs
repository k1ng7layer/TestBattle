using System;
using Game.Models.Combat;
using Game.Views.Unit.Impl;

namespace Game.Settings.Unit
{
    [Serializable]
    public class UnitSceneSettings
    {
        public UnitView View;
        public EBattleTeam BattleTeam;
        public int Order;
    }
}