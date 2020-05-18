using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class LevelSetting
{
    public float startSpawnTime;
    public int chestID;
    public int[] enemyIDs;
    public int waveCount;
    public int spawnCount;
    public float spawnInterval;
    public float waveInterval;
}
