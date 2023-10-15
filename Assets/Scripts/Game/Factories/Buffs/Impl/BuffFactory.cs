using Game.Models.Buffs;
using Game.Settings.Buffs;
using Zenject;

namespace Game.Factories.Buffs.Impl
{
    public class BuffFactory : PlaceholderFactory<IBuffsSettingsBase, EBuffType, Buff>, 
        IBuffFactory
    {
       
    }
}