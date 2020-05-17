using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    public CharacterSettingCollection characterSettingCollection;
    public ChestSettingCollection chestSettingCollection;
    public LevelSettingCollection levelSettingCollection;

    public List<CharacterManager> allCharacterManages = new List<CharacterManager>();
    public ChestManager playerChestManager;
    public ChestManager enemyChestManager;
    public SummonManager summonManager;

    private float curSummonPower;

    private void Start()
    {
        playerChestManager.Setup(chestSettingCollection.chestSettings[0]);
        enemyChestManager.Setup(chestSettingCollection.chestSettings[0]);
        summonManager.Setup(GetCanSummonSetting(), playerData.summonPower);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return Starting();
        yield return Playing();
        yield return Ending();
    }

    private IEnumerator Starting()
    {
        Debug.Log("Starting");
        yield return null;
    }

    private IEnumerator Playing()
    {
        Debug.Log("Playing");
        summonManager.onSummonClick += OnSummonClick;
        StartCoroutine(ProcessLevelLoop());
        while (playerChestManager.IsLive() && enemyChestManager.IsLive())
        {
            yield return null;
            curSummonPower += Time.deltaTime;
            curSummonPower = Mathf.Min(curSummonPower, playerData.summonPower);
            summonManager.UpdateSummonPower(Mathf.RoundToInt(curSummonPower), playerData.summonPower);
            for (int i = allCharacterManages.Count - 1; i >= 0; i--)
            {
                if (allCharacterManages[i].IsLive() == false)
                    allCharacterManages.RemoveAt(i);
            }
        }
        StopCoroutine(ProcessLevelLoop());
        summonManager.onSummonClick -= OnSummonClick;
    }

    private IEnumerator Ending()
    {
        Debug.Log("Ending");
        DisableAllCharacter();
        yield return null;
    }

    private IEnumerator ProcessLevelLoop()
    {
        var levelSetting = levelSettingCollection.levelSettings[0];
        yield return new WaitForSeconds(levelSetting.startSpawnTime);
        int totalWaveCount = levelSetting.waveCount;
        while (totalWaveCount > 0)
        {
            int totalSpawnCount = levelSetting.spawnCount;
            while (totalSpawnCount > 0)
            {
                int enemyIndex = Random.Range(0, levelSetting.enemyIDs.Length);
                SpawnEnemyCharacter(levelSetting.enemyIDs[enemyIndex]);
                yield return new WaitForSeconds(levelSetting.spawnInterval);
                totalSpawnCount--;
            }
            totalWaveCount--;
        }
    }

    private void OnSummonClick(int id)
    {
        var characterSetting = characterSettingCollection.GetSetting(id);
        if (curSummonPower >= characterSetting.summonCost)
        {
            curSummonPower -= characterSetting.summonCost;
            SpawnPlayerCharacter(id);
            summonManager.CooldownButton(id);
            summonManager.UpdateSummonPower(Mathf.RoundToInt(curSummonPower), playerData.summonPower);
        }
    }

    private CharacterSetting[] GetCanSummonSetting()
    {
        CharacterSetting[] characterSettings = new CharacterSetting[playerData.ownCharacters.Length];
        for (int i = 0; i < playerData.ownCharacters.Length; i++)
        {
            characterSettings[i] = characterSettingCollection.GetSetting(playerData.ownCharacters[i]);
        }
        return characterSettings;
    }

    private void SpawnPlayerCharacter(int id)
    {
        var setting = characterSettingCollection.GetSetting(id);
        var manager = new CharacterManager();
        allCharacterManages.Add(manager);
        manager.Setup(setting, LayerMask.NameToLayer("Player"), playerChestManager.spawnPoint.position, 1);
    }

    private void SpawnEnemyCharacter(int id)
    {
        var setting = characterSettingCollection.GetSetting(id);
        var manager = new CharacterManager();
        allCharacterManages.Add(manager);
        manager.Setup(setting, LayerMask.NameToLayer("Enemy"), enemyChestManager.spawnPoint.position, -1);
    }

    private void DisableAllCharacter()
    {
        foreach (var character in allCharacterManages)
        {
            character.Disable();
        }
    }
}
