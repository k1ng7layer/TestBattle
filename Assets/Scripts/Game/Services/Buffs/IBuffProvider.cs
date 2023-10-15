using System.Collections.Generic;
using Game.Models.Buffs;

namespace Game.Services.Buffs
{
    public interface IBuffProvider
    {
        Buff TryGetBuff(IReadOnlyDictionary<EBuffType, Buff> excludeBuffs);
    }
}