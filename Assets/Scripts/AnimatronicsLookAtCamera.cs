using UnityEngine;

public class AnimatronicsLookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform cam;

    private void Start()
    {
        head.LookAt(cam);
    }
}
