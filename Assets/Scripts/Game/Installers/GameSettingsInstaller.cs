using Game.Settings.Buffs;
using Game.Settings.Buffs.Impl;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BuffsSettingsBase buffsSettingsBase;

        public override void InstallBindings()
        {
            Container.Bind<IBuffsSettingsBase>().To<BuffsSettingsBase>().FromInstance(buffsSettingsBase).AsSingle();
        }
    }
}