using Game.Models.Buffs;
using Zenject;

namespace Game.Factories.Buff
{
    public interface IBuffFactory : IFactory<BuffBase>
    {
        EBuffType BuffType { get; }
    }
}