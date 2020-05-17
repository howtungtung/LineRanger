using UnityEngine;
using UnityEngine.Rendering;

public class CharacterManager
{
    private MoveBehavior moveBehavior;
    private HealthBehavior healthBehavior;
    private AttackBehavior attackBehavior;
    private GameObject characterInstance;

    public void Setup(CharacterSetting characterSetting, int layerID, Vector2 spawnPos, int direction)
    {
        characterInstance = GameObject.Instantiate(characterSetting.prefab, spawnPos, Quaternion.identity);
        characterInstance.gameObject.layer = layerID;
        characterInstance.GetComponent<SortingGroup>().sortingLayerID = layerID;
        characterInstance.transform.localPosition = Vector3.zero;
        moveBehavior = characterInstance.GetComponent<MoveBehavior>();
        healthBehavior = characterInstance.GetComponent<HealthBehavior>();
        attackBehavior = characterInstance.GetComponent<AttackBehavior>();
        moveBehavior.Setup(characterSetting.speed, direction);
        healthBehavior.Setup(characterSetting.hp);
        attackBehavior.Setup(characterSetting.attack, layerID);
    }

    public bool IsLive()
    {
        return healthBehavior.IsLive();
    }

    public void Disable()
    {
        moveBehavior.enabled = false;
        attackBehavior.enabled = false;
    }
}
