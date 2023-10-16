using System.Collections.Generic;
using Game.Settings.Unit;
using UnityEngine;

namespace Game.Views
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private List<UnitSceneSettings> unitSceneSettings;
        
        public List<UnitSceneSettings> UnitsViews => unitSceneSettings;
    }
}