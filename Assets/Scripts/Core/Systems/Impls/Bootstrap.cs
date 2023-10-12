﻿using System.Collections.Generic;
using Zenject;

namespace Core.Systems.Impls
{
    public class Bootstrap : ITickable, 
        IInitializable, 
        ILateTickable, 
        IFixedTickable
    {
        private readonly List<ILateSystem> _late = new();
        private readonly List<IFixedSystem> _fixed = new();
        private readonly List<IUpdateSystem> _update = new();
        private readonly List<IInitializeSystem> _initializeSystems = new();
        
        private bool _isInitialized;
        
        public Bootstrap(List<ISystem> systems)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                var system = systems[i];
                
                if(system is IInitializeSystem initializeSystem)
                    _initializeSystems.Add(initializeSystem);
                
                if(system is IFixedSystem fixedUpdate)
                    _fixed.Add(fixedUpdate);
                
                if(system is ILateSystem late)
                    _late.Add(late);
                
                if(system is IUpdateSystem updateSystem)
                    _update.Add(updateSystem);
            }
        }
        
        public void Initialize()
        {
            if(_isInitialized)
                return;
            
            _isInitialized = true;

            foreach (var initializeSystem in _initializeSystems)
            {
                initializeSystem.Initialize();
            }
        }
        
        public void FixedTick()
        {
            foreach (var fixedUpdate in _fixed)
            {
                fixedUpdate.Fixed();
            }
        }
        
        public void Tick()
        {
            foreach (var updateSystem in _update)
            {
                updateSystem.Update();
            }
        }

        public void LateTick()
        {
            foreach (var lateUpdateSystem in _late)
            {
                lateUpdateSystem.Late();
            }
        }
    }
}