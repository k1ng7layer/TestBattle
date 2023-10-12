using Game.Models.Buffs;
using Game.Presenters.Unit;

namespace Game.Services.Buff
{
    public interface IBuffService
    {
        bool TryGetBuff(IUnit target, out BuffBase buff);
    }
}