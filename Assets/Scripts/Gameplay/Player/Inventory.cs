using Gameplay.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryGUI gui;
    private List<BaseItem> itemList  = new List<BaseItem>();


    private void Start()
    {
        EventsManager.Instance.ItemPickedUp += OnItemPickedUp;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.ItemPickedUp -= OnItemPickedUp;
    }

    private void OnItemPickedUp(BaseItem item)
    {
        Debug.Log($"Picked up {item.name}");
        itemList.Add(item);
        gui.AddItem(item);
    }
}
