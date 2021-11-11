using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerBehaviour : MonoBehaviour
{
    public Action OnTriggered;

    public void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        OnTriggered.Invoke();
    }
}
