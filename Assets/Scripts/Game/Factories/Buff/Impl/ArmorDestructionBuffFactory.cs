using Game.Models.Buffs;
using Game.Models.Buffs.Impl;
using Zenject;

namespace Game.Factories.Buff.Impl
{
    public class ArmorDestructionBuffFactory : PlaceholderFactory<ArmorDestructionBuff>, IBuffFactory
    {
        public EBuffType BuffType => EBuffType.ArmorDestruction;
        
        public new BuffBase Create()
        {
            return base.Create();
        }
    }
}