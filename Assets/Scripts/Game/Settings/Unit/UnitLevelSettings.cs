using System;
using System.Collections.Generic;
using Game.Views.Unit.Impl;

namespace Game.Settings.Unit
{
    [Serializable]
    public class UnitSceneSettings
    {
        public UnitView View;
        public List<UnitAttributeParameters> attributeParameters;
    }
}