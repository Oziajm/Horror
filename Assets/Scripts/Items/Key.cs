using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Item/Key", fileName = "Key")]
public class Key : BaseItem
{
    public override void Use()
    {
        Debug.Log("Used a key");
    }
}
