using UnityEngine;

public class Mo : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        Vector3 playerPosition = new(player.position.x, 0f, player.position.z);

        transform.LookAt(playerPosition);
    }
}
