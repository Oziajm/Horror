using UnityEngine;

public class Plumber : MonoBehaviour
{
    [SerializeField] private GameObject straithLine;
    [SerializeField] private GameObject curvedLine;

    private GameObject[] pipes;

    private void Start()
    {
        GenerateNewMap();
    }

    private void Update()
    {
        for (int i = 0; i < pipes.Length; i++)
        {
            PipeRotator pipeRotator = pipes[i].GetComponent<PipeRotator>();

            if (pipeRotator.pipeController.isPipePowered) ;
        }
    }

    private void GenerateNewMap()
    {
        pipes = new GameObject[70];
        int indx = 0;
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                int random = Random.Range(0, 2);
                Vector3 newPosition = new ((i * 100)+50, (j * 100)+50, 0);
                Quaternion newRotation = new (0, 0, 0, 0);
                GameObject obj = Instantiate(random == 0 ? straithLine : curvedLine, newPosition, newRotation, transform);
                obj.GetComponent<RectTransform>().anchoredPosition = newPosition;
                pipes[indx] = obj;
                indx++;
            }
        }
    }
}

