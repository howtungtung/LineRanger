using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ChestManager
{
    private HealthBehavior healthBehavior;
    public Transform spawnPoint;
    public GameObject chestInstance;

    public void Setup(ChestSetting chestSetting)
    {
        healthBehavior = chestInstance.GetComponent<HealthBehavior>();
        healthBehavior.Setup(chestSetting.hp);
    }

    public bool IsLive()
    {
        return healthBehavior.IsLive();
    }
}
