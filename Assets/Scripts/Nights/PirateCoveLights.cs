using UnityEngine;
using System.Collections;

public class PirateCoveLights : MonoBehaviour
{
    [SerializeField] private Renderer[] lights;

    private void Start()
    {
        StartCoroutine(TurnOnLights());
    }

    IEnumerator TurnOnLights()
    {
        while (true)
        {
            for(int i = 0; i < lights.Length; i++)
            {
                lights[i].material.SetColor("_EmissionColor", i % 2 == 0 ? lights[i].material.color * 5f : Color.black);
            }
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].material.SetColor("_EmissionColor", i % 2 != 0 ? lights[i].material.color * 5f : Color.black);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
