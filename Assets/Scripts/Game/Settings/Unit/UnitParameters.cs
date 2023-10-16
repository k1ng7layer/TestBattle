using System;
using Game.Models.Attributes;

namespace Game.Settings.Unit
{
    [Serializable]
    public class UnitAttributeParameters
    {
        public EAttributeType AttributeType;
        public float InitialValue;
        public float MaxValue;
        public float MinValue;
    }
}