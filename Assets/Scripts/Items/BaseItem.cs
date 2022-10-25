using UnityEngine;

public abstract class BaseItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;

    public abstract void Use();
}
