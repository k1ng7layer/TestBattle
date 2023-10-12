using Game.Factories.Buff;
using Game.Models.Buffs;

namespace Game.Services.BuffFactory
{
    public interface IBuffFactoryProvider
    {
        IBuffFactory Get(EBuffType buffType);
    }
}