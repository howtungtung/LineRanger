using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    private HealthBehavior healthBehavior;
    private Animator animatorInstance;
    public int maxSummonPower;
    public int curSummonPower;
    public Transform spawnPoint;

    private void Awake()
    {
        healthBehavior = GetComponent<HealthBehavior>();
    }

    public void Setup(ChestSetting chestSetting)
    {
        var chestInstance = Instantiate(chestSetting.cheshPrefab, transform);
        chestInstance.transform.localPosition = Vector3.zero;
        animatorInstance = chestInstance.GetComponent<Animator>();
        healthBehavior.Setup(chestSetting.hp);
        maxSummonPower = chestSetting.summonPower;
        curSummonPower = 0;
        StartCoroutine(ProcessLifeCycle());
    }

    private IEnumerator ProcessLifeCycle()
    {
        float tempSummonPower = 0;
        while (IsLive())
        {
            yield return null;
            tempSummonPower += Time.deltaTime;
            curSummonPower = Mathf.RoundToInt(tempSummonPower);
        }
        GetComponent<Collider2D>().enabled = false;
    }

    public bool IsLive()
    {
        return !animatorInstance.GetCurrentAnimatorStateInfo(0).IsName("Death");
    }
}
