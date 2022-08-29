using System.Collections;
using UnityEngine;

public class ArcadeFlickeringLights : MonoBehaviour
{
    [SerializeField] private Renderer arcadeLight;

    private float timeDelay;

    private void Start()
    {
        StartCoroutine(ArcadeLights());
    }

    IEnumerator ArcadeLights()
    {
        while (true)
        {
            arcadeLight.material.SetColor("_EmissionColor", arcadeLight.material.color * 0f);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            arcadeLight.material.SetColor("_EmissionColor", arcadeLight.material.color * 1f);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
