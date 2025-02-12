using System.Collections;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    #region Variables

    [Space(10)]
    [Header("Lights")]
    [Space(10)]
    [SerializeField] private Renderer lightRenderer;
    [SerializeField] private Light light;
    [SerializeField] private AudioSource audioSource;

    private float timeDelay;

    #endregion

    #region Unity Methods

    private void Start()
    {
        StartCoroutine(FlickerOnlyRenderers());
    }

    private IEnumerator FlickerOnlyRenderers()
    {
        while (true)
        {
            if (lightRenderer)
                lightRenderer.material.SetColor("_EmissiveColor", lightRenderer.material.color * 50f);
            if (light)
                light.enabled = true;
            if (audioSource)
                audioSource.Play();
            yield return new WaitForSeconds(Random.Range(0.02f, 1f));

            if (lightRenderer)
                lightRenderer.material.SetColor("_EmissiveColor", lightRenderer.material.color * 0f);
            if (light)
                light.enabled = false;
            if (audioSource)
                audioSource.Stop();
            yield return new WaitForSeconds(Random.Range(0.02f, 1f));
        }
    }

    #endregion
}
