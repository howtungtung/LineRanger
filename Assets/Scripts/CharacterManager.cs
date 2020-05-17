using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private MoveBehavior moveBehavior;
    private HealthBehavior healthBehavior;
    private AttackBehavior attackBehavior;
    private Animator animatorInstance;

    private void Awake()
    {
        moveBehavior = GetComponent<MoveBehavior>();
        healthBehavior = GetComponent<HealthBehavior>();
        attackBehavior = GetComponent<AttackBehavior>();
    }

    public void Setup(CharacterSetting characterSetting)
    {
        var characterInstance = Instantiate(characterSetting.prefab, transform);
        characterInstance.transform.localPosition = Vector3.zero;
        animatorInstance = characterInstance.GetComponent<Animator>();
        moveBehavior.Setup(animatorInstance, characterSetting.speed);
        healthBehavior.Setup(animatorInstance, characterSetting.hp);
        attackBehavior.Setup(animatorInstance, characterSetting.attack);
        StartCoroutine(ProcessLifeCycle());
    }

    private IEnumerator ProcessLifeCycle()
    {
        while (IsLive())
        {
            yield return null;
        }
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    public bool IsLive()
    {
        return !animatorInstance.GetCurrentAnimatorStateInfo(0).IsName("Death");
    }

    public void Disable()
    {
        moveBehavior.enabled = false;
        attackBehavior.enabled = false;
    }
}
