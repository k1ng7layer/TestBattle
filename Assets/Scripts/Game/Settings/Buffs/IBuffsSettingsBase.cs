using Game.Models.Buffs;

namespace Game.Settings.Buffs
{
    public interface IBuffsSettingsBase
    {
        BuffSettings Get(EBuffType buffType);
    }
}