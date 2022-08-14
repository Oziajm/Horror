using UnityEngine;

public class FreddyAI : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform player;
    private void Update()
    {
        head.LookAt(player);
    }
}
