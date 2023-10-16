using System.Collections.Generic;
using Game.Settings.Buffs.Impl;

namespace Game.Settings.Buffs
{
    public interface IBuffsSettingsBase
    {
        IReadOnlyList<BuffSettings> BuffSettingsList { get; }
        BuffSettings Get(string buffName);
    }
}