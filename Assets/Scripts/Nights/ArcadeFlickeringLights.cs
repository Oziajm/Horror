using System.Collections;
using UnityEngine;

public class ArcadeFlickeringLights : MonoBehaviour
{
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private Light lightSource;

    private float timeDelay;

    private void Start()
    {
        StartCoroutine(lightSource == null ? FlickerOnlyRenderers() : FlickerRenderersAndLights());
    }

    IEnumerator FlickerOnlyRenderers()
    {
        while (true)
        {
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0f);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0.5f);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
    }

    IEnumerator FlickerRenderersAndLights()
    {
        while (true)
        {
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0f);
            lightSource.gameObject.SetActive(false);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            lightRenderer.material.SetColor("_EmissionColor", lightRenderer.material.color * 0.5f);
            lightSource.gameObject.SetActive(true);
            timeDelay = Random.Range(0.02f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
