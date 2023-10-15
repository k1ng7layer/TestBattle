using System.Collections.Generic;
using Game.Models.Combat;
using Game.Settings.Unit;
using UnityEngine;

namespace Game.Views
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private EBattleTeam firstTurn;
        [SerializeField] private List<UnitSceneSettings> unitSceneSettings;
        
        public List<UnitSceneSettings> UnitsViews => unitSceneSettings;
        public EBattleTeam FirstTurn => firstTurn;
    }
}