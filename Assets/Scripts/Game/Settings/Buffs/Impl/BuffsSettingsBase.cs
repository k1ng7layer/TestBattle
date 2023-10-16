using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings.Buffs.Impl
{
    [CreateAssetMenu(menuName = "Settings/Buffs/" + nameof(BuffsSettingsBase), fileName = nameof(BuffsSettingsBase))]
    public class BuffsSettingsBase : ScriptableObject, 
        IBuffsSettingsBase
    {
        [SerializeField] private List<BuffSettings> buffSettingsList;

        public IReadOnlyList<BuffSettings> BuffSettingsList => buffSettingsList;

        public BuffSettings Get(string buffName)
        {
            for (var i = 0; i < buffSettingsList.Count; i++)
            {
                var buff = buffSettingsList[i];
                if (buff.BuffName == buffName)
                    return buff;
            }

            throw new Exception($"[{nameof(BuffsSettingsBase)}] Can't find prefab with name: {buffName}");
        }
    }
}