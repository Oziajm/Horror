using UnityEngine;
using TMPro;

public class MovingText : MonoBehaviour
{
    [SerializeField] 
    private Transform player;
    [SerializeField] 
    private TextMeshProUGUI text;
    [SerializeField] 
    private float displayDistance = 1f;

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance < displayDistance)
        {
            transform.LookAt(player);
        }
    }
}
