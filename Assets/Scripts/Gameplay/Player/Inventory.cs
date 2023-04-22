using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryGUI gui;
    private List<BaseItem> itemList  = new List<BaseItem>();

    private void Start()
    {
        //EventManager.current.OnItemPickedUp += OnItemPickedUp;
    }

    private void OnDestroy()
    {
        //EventManager.current.OnItemPickedUp -= OnItemPickedUp;
    }

    private void OnItemPickedUp(BaseItem item)
    {
        Debug.Log($"Picked up {item.name}");
        itemList.Add(item);
        gui.AddItem(item);
    }
}
