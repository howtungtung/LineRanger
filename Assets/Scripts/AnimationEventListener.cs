using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class AnimationEventListener : MonoBehaviour
{
    public event Action<string> onEventTrigger;

    public void OnEventTrigger(string evt)
    {
        onEventTrigger?.Invoke(evt);
    }
}