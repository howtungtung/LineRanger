using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummonButton : MonoBehaviour
{
    public Text label;
    public int summonID;
    public Image cooldownMask;
    public Button button;

    public void Setup(int id, string name)
    {
        summonID = id;
        label.text = name;
    }

    public void SetCooldown(float cooldownTime)
    {
        StartCoroutine(ProcessCooldown(cooldownTime));
    }

    private IEnumerator ProcessCooldown(float cooldownTime)
    {
        button.interactable = false;
        cooldownMask.enabled = true;
        float startTime = cooldownTime;
        while (cooldownTime > 0)
        {
            yield return null;
            cooldownTime -= Time.deltaTime;
            cooldownMask.fillAmount = cooldownTime / startTime;
        }
        cooldownMask.enabled = false;
        button.interactable = true;
    }
}
