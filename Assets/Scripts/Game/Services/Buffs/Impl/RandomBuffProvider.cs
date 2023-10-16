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
        
        private string GetRandomBuffName(IList<string> availableBuffs)
        {
            var random = new Random();
            var randomBuffTypeIndex = random.Next(0, availableBuffs.Count);
            var randomBuff = availableBuffs[randomBuffTypeIndex];

            return randomBuff;
        }

        private List<string> GetAvailableBuffNames(IReadOnlyDictionary<string, Buff> excludeBuffs)
        {
            var buffTypes = _buffsSettingsBase.BuffSettingsList;
            
            var availableBuffs = new List<string>();
           
            foreach (var buffSettings in buffTypes)
            {
                if(!excludeBuffs.ContainsKey(buffSettings.BuffName))
                    availableBuffs.Add(buffSettings.BuffName);
            }

            return availableBuffs;
        }

        public Buff TryGetBuff(
            IReadOnlyDictionary<string, Buff> excludeBuffs)
        {
            var availableBuffs = GetAvailableBuffNames(excludeBuffs);
            
            var randomBuff = GetRandomBuffName(availableBuffs);

            var buff = _buffFactory.Create(_buffsSettingsBase, randomBuff);
            
            return buff;
        }
    }
}