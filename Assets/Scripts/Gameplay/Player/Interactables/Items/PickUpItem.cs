using Gameplay.Managers;
using System;
using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] 
    private BaseItem item;

    public override void Interact()
    {
        TakeItem();
    }
    public override string GetHoverText()
    {
        return "Take";
    }

    private void TakeItem()
    {
        Destroy(gameObject);
        //EventsManager.Instance.ItemPickedUp?.Invoke(item);
    }
}