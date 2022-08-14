using System.Collections;
using UnityEngine;


public class FlickeringLights : MonoBehaviour
{
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private GameObject lightSource;

    void Start()
    {
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        while (true)
        {
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 1f);
            lightSource.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0f);
            lightSource.SetActive(false);
            yield return new WaitForSeconds(1f);
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 1f);
            lightSource.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0f);
            lightSource.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
