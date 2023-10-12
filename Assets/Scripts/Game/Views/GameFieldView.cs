using System.Collections.Generic;
using Game.Views.Unit.Impl;
using UnityEngine;

namespace Game.Views
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private List<UnitView> unitsViews;
        public List<UnitView> UnitsViews => unitsViews;
    }
}