using Game.Models.Buffs;
using Game.Models.Buffs.Impl;
using Zenject;

namespace Game.Factories.Buff.Impl
{
    public class ArmorSelfBuffFactory : PlaceholderFactory<ArmorSelfBuff>, 
        IBuffFactory
    {
        public EBuffType BuffType => EBuffType.ArmorSelf;

        public BuffBase Create()
        {
            return base.Create();
        }
    }
}