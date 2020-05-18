using UnityEngine;
using UnityEngine.Rendering;

public class CharacterManager
{
    private MoveBehavior moveBehavior;
    private HealthBehavior healthBehavior;
    private AttackBehavior attackBehavior;
    private GameObject characterInstance;

    public void Setup(CharacterSetting characterSetting, CharacterType characterType,Vector2 spawnPos, int direction)
    {
        characterInstance = GameObject.Instantiate(characterSetting.prefab, spawnPos, Quaternion.identity);
        moveBehavior = characterInstance.GetComponent<MoveBehavior>();
        healthBehavior = characterInstance.GetComponent<HealthBehavior>();
        attackBehavior = characterInstance.GetComponent<AttackBehavior>();
        moveBehavior.Setup(characterSetting.speed, direction);
        healthBehavior.Setup(characterSetting.hp);
        switch (characterType)
        {
            case CharacterType.ENEMY:
                characterInstance.gameObject.layer = LayerMask.NameToLayer("Enemy");
                characterInstance.GetComponent<SortingGroup>().sortingLayerID = SortingLayer.NameToID("Enemy");
                attackBehavior.Setup(characterSetting.attack, LayerMask.NameToLayer("Player"));
                break;
            case CharacterType.PLAYER:
                characterInstance.gameObject.layer = LayerMask.NameToLayer("Player");
                characterInstance.GetComponent<SortingGroup>().sortingLayerID = SortingLayer.NameToID("Player");
                attackBehavior.Setup(characterSetting.attack, LayerMask.NameToLayer("Enemy"));
                break;
        }
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
