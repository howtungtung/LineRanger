using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CharacterSettingCollection : ScriptableObject
{
    public CharacterSetting[] characterSettings;

    public CharacterSetting GetSetting(int id)
    {
        return Array.Find(characterSettings, data => data.id == id);
    }
}
