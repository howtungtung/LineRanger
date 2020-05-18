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
    private string summonTextFormat;
    private CharacterSetting[] characterSettings;
    public float curSummonPower;
    public int maxSummonPower;

    private void Awake()
    {
        summonButtons = GetComponentsInChildren<SummonButton>();
        summonTextFormat = summonPowerText.text;
    }

    public void OnSummonClick(SummonButton summonButton)
    {
        summonButton.SetCooldown(cooldownTime);
        curSummonPower -= summonButton.summonCost;
        UpdateSummonPower(Mathf.FloorToInt(curSummonPower));
        onSummonClick?.Invoke(summonButton.summonID);
    }

    public void Setup(CharacterSetting[] characterSettings, int maxSummonPower)
    {
        this.characterSettings = characterSettings;
        for (int i = 0; i < summonButtons.Length; i++)
        {
            if (i < characterSettings.Length)
            {
                summonButtons[i].Setup(characterSettings[i].id, characterSettings[i].name, characterSettings[i].summonCost);
            }
            else
            {
                summonButtons[i].gameObject.SetActive(false);
            }
        }
        this.maxSummonPower = maxSummonPower;
        StartCoroutine(ProcessSummonPower());
    }

    private IEnumerator ProcessSummonPower()
    {
        UpdateSummonPower(0);
        while (true)
        {
            yield return null;
            curSummonPower += Time.deltaTime;
            curSummonPower = Mathf.Min(curSummonPower, maxSummonPower);
            UpdateSummonPower(Mathf.FloorToInt(curSummonPower));
            for (int i = 0; i < characterSettings.Length; i++)
                summonButtons[i].button.interactable = characterSettings[i].summonCost <= curSummonPower;
        }
    }

    private void UpdateSummonPower(int curSummonPower)
    {
        summonPowerText.text = string.Format(summonTextFormat, curSummonPower, maxSummonPower);
    }
}
