using Game.Models.Buffs;
using Game.Settings.Buffs;
using Zenject;

namespace Game.Factories.Buffs
{
    public interface IBuffFactory : IFactory<IBuffsSettingsBase, string, Buff>
    {
        
    }
}