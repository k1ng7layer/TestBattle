using System;
using System.Collections.Generic;
using Game.Factories.Buffs;
using Game.Models.Buffs;
using Game.Settings.Buffs;

namespace Game.Services.Buffs.Impl
{
    public class RandomBuffProvider : IBuffProvider
    {
        private readonly IBuffFactory _buffFactory;
        private readonly IBuffsSettingsBase _buffsSettingsBase;

        public RandomBuffProvider(
            IBuffFactory buffFactory, 
            IBuffsSettingsBase buffsSettingsBase
        )
        {
            _buffFactory = buffFactory;
            _buffsSettingsBase = buffsSettingsBase;
        }
        
        private EBuffType GetRandomBuff(IList<EBuffType> availableBuffs)
        {
            var random = new Random();
            var randomBuffTypeIndex = random.Next(0, availableBuffs.Count);
            var randomBuff = availableBuffs[randomBuffTypeIndex];

            return randomBuff;
        }

        private List<EBuffType> GetAvailableBuffs(IReadOnlyDictionary<EBuffType, Buff> excludeBuffs)
        {
            var buffTypes = (EBuffType[])Enum.GetValues(typeof(EBuffType));
            
            var availableBuffs = new List<EBuffType>();
           
            foreach (var buffType in buffTypes)
            {
                if(!excludeBuffs.ContainsKey(buffType))
                    availableBuffs.Add(buffType);
            }

            return availableBuffs;
        }

        public Buff TryGetBuff(
            IReadOnlyDictionary<EBuffType, Buff> excludeBuffs)
        {
            var availableBuffs = GetAvailableBuffs(excludeBuffs);
            
            var randomBuff = GetRandomBuff(availableBuffs);

            var buff = _buffFactory.Create(_buffsSettingsBase, randomBuff);
            
            return buff;
        }
    }
}