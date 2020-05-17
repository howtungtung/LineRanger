using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonManager : MonoBehaviour
{
    public SummonButton[] summonButtons;
    public Text summonPowerText;
    public event Action<int> onSummonClick;
    public float cooldownTime;

    private void Awake()
    {
        summonButtons = GetComponentsInChildren<SummonButton>();
    }

    public void OnSummonClick(SummonButton summonButton)
    {
        onSummonClick?.Invoke(summonButton.summonID);
    }

    public void Setup(CharacterSetting[] characterSettings)
    {
        for (int i = 0; i < summonButtons.Length; i++)
        {
            if (i < characterSettings.Length)
            {
                summonButtons[i].Setup(characterSettings[i].id, characterSettings[i].name);
            }
            else
            {
                summonButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void CooldownButton(int id)
    {
        var button = Array.Find(summonButtons, data => data.summonID == id);
        button.SetCooldown(cooldownTime);
    }

    public void UpdateSummonPower()
    {

    }
}
