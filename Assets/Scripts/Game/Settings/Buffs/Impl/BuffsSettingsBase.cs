using System;
using System.Collections.Generic;
using Game.Models.Buffs;
using UnityEngine;

namespace Game.Settings.Buffs.Impl
{
    [CreateAssetMenu(menuName = "Settings/Buffs/" + nameof(BuffsSettingsBase), fileName = nameof(BuffsSettingsBase))]
    public class BuffsSettingsBase : ScriptableObject, IBuffsSettingsBase
    {
        [SerializeField] private List<BuffSettings> buffSettingsList;
        
        public BuffSettings Get(EBuffType buffType)
        {
            for (var i = 0; i < buffSettingsList.Count; i++)
            {
                var buff = buffSettingsList[i];
                if (buff.BuffType == buffType)
                    return buff;
            }

            throw new Exception($"[{nameof(BuffsSettingsBase)}] Can't find prefab with name: {buffType}");
        }
    }
}