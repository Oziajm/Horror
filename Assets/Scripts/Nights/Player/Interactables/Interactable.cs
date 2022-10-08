using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();
    public abstract string GetHoverText();
    public bool active = true;
}
