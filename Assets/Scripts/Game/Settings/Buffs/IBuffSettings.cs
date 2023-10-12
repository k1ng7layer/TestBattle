using Game.Models.Buffs;

namespace Game.Settings.Buffs
{
    public interface IBuffSettings
    {
        EBuffType BuffType { get; }
        int LifeTime { get; }
        
    }
}