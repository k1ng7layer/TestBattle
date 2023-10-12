using System;
using System.Collections.Generic;
using Game.Factories.Buff;
using Game.Models.Buffs;

namespace Game.Services.BuffFactory.Impl
{
    public class BuffFactoryProvider : IBuffFactoryProvider
    {
        private readonly Dictionary<EBuffType, IBuffFactory> _buffFactories = new();

        public BuffFactoryProvider(List<IBuffFactory> buffFactories)
        {
            foreach (var buffFactory in buffFactories)
            {
                _buffFactories.Add(buffFactory.BuffType, buffFactory);
            }
        }
        
        public IBuffFactory Get(EBuffType buffType)
        {
            if (_buffFactories.TryGetValue(buffType, out var factory))
            {
                return factory;
            }

            throw new Exception($"[{nameof(BuffFactory)} can't find factory for buff type {buffType}]");
        }
    }
}