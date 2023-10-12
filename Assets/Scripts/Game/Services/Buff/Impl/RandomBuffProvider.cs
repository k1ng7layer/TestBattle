using System;
using System.Collections.Generic;
using Game.Models.Buffs;
using Game.Presenters.Unit;
using Game.Services.BuffFactory;

namespace Game.Services.Buff.Impl
{
    public class RandomBuffService : IBuffService
    {
        private readonly IBuffFactoryProvider _buffFactoryProvider;

        public RandomBuffService(IBuffFactoryProvider buffFactoryProvider)
        {
            _buffFactoryProvider = buffFactoryProvider;
        }
        
        public bool TryGetBuff(IUnit target, out BuffBase buff)
        {
            if (target.StaticBuffs.Count == 2)
            {
                buff = null;
                
                return false;
            }
                
            var availableBuffs = GetAvailableBuffs(target);
            
            var randomBuff = GetRandomBuff(availableBuffs);

            var buffFactory = _buffFactoryProvider.Get(randomBuff);
            
            buff = buffFactory.Create();

            return true;
        }

        private EBuffType GetRandomBuff(HashSet<EBuffType> availableBuffs)
        {
            var buffTypes = (EBuffType[])Enum.GetValues(typeof(EBuffType));

            var random = new Random();
            var randomBuffTypeIndex = random.Next(0, buffTypes.Length);
            var randomBuff = buffTypes[randomBuffTypeIndex];

            return randomBuff;
        }

        private HashSet<EBuffType> GetAvailableBuffs(IUnit target)
        {
            var buffTypes = (EBuffType[])Enum.GetValues(typeof(EBuffType));
            
            var availableBuffs = new HashSet<EBuffType>();
           
            foreach (var buffType in buffTypes)
            {
                if(!target.StaticBuffs.ContainsKey(buffType))
                    availableBuffs.Add(buffType);
            }

            return availableBuffs;
        }
    }
}