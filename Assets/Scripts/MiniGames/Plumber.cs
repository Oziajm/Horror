using UnityEngine;

public class Plumber : MonoBehaviour
{
    [SerializeField] private GameObject straithLine;
    [SerializeField] private GameObject curvedLine;

    private void Start()
    {
        GenerateNewMap();


    }

    private void GenerateNewMap()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                int random = Random.Range(0, 2);
                Vector3 newPosition = new ((i * 100)+50, (j * 100)+50, 0);
                Quaternion newRotation = new (0, 0, 0, 0);
                Debug.Log(j + " " + i);
                Instantiate(random == 0 ? straithLine : curvedLine, newPosition, newRotation, transform);
            }
        }
    }
}