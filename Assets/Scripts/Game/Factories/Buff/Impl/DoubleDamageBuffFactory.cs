using Game.Models.Buffs;
using Game.Models.Buffs.Impl;
using Zenject;

namespace Game.Factories.Buff.Impl
{
    public class DoubleDamageBuffFactory : PlaceholderFactory<DoubleDamageBuff>, 
        IBuffFactory
    {
        public EBuffType BuffType => EBuffType.DoubleDamage;

        public BuffBase Create()
        {
            return base.Create();
        }
    }
}