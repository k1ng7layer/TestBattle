using Game.Settings.Unit;
using UnityEngine;

namespace Game.Views
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private UnitSceneSettings leftUnitSettings;
        [SerializeField] private UnitSceneSettings rightUnitSettings;
        
        public UnitSceneSettings LeftUnitSettings => leftUnitSettings;
        public UnitSceneSettings RightUnitSettings => rightUnitSettings;
    }
}