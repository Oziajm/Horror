using System.Collections;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    #region Variables

    [Space(10)]
    [Header("Lights")]
    [Space(10)]
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private Light lightSource;

    private float timeDelay;

    #endregion

    #region Unity Methods

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

    #endregion
}
