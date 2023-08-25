using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;

    public void AddItem(BaseItem item)
    {
        GameObject newItem = Instantiate(itemPrefab, gameObject.transform);
        Image itemImage = newItem.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI itemName = newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();

        itemImage.sprite = item.itemImage;
        itemName.text = item.itemName;

        HintSystem.InvokeHint($"Picked up {item.name}.");
    }
}
