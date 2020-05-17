using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    public CharacterSettingCollection characterSettingCollection;
    public ChestSettingCollection chestSettingCollection;
    public LevelSettingCollection levelSettingCollection;

    public CharacterManager playerPrefab;
    public CharacterManager enemyPrefab;
    public ChestManager playerChestManager;
    public ChestManager enemyChestManager;
    public SummonManager summonManager;

    private void Start()
    {
        playerChestManager.Setup(chestSettingCollection.chestSettings[0]);
        enemyChestManager.Setup(chestSettingCollection.chestSettings[0]);
        summonManager.Setup(GetCanSummonSetting());
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
        while (playerChestManager.IsLive() && enemyChestManager.IsLive())
        {
            yield return null;
        }
        summonManager.onSummonClick -= OnSummonClick;
    }

    private IEnumerator Ending()
    {
        Debug.Log("Ending");
        DisableAllCharacter();
        yield return null;
    }

    private void OnSummonClick(int id)
    {
        SpawnCharacter(id);
        summonManager.CooldownButton(id);
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

    private void SpawnCharacter(int id)
    {
        var instance = Instantiate(playerPrefab);
        instance.transform.position = playerChestManager.spawnPoint.position;
        var setting = characterSettingCollection.GetSetting(id);
        instance.Setup(setting);
    }

    private void DisableAllCharacter()
    {
        var allCharacters = FindObjectsOfType<CharacterManager>();
        foreach (var character in allCharacters)
        {
            character.Disable();
        }
    }
}
