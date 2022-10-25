using UnityEngine;
using System;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnAnimatronicTurnedEvent;
    public void AnimatronicTurned()
    {
        OnAnimatronicTurnedEvent?.Invoke();
    }

    public event Action<BaseItem> OnItemPickedUp;
    public void PickUpItem(BaseItem item) { OnItemPickedUp?.Invoke(item); }
}
