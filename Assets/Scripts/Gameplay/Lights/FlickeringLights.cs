using System.Collections;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    #region Variables

    [Space(10)]
    [Header("Lights")]
    [Space(10)]
    [SerializeField] private Renderer lightRenderer;

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
            lightRenderer.material.SetColor("_EmissiveColor", lightRenderer.material.color * 50f);
            timeDelay = Random.Range(0.02f, 1f);
            yield return new WaitForSeconds(timeDelay);
            lightRenderer.material.SetColor("_EmissiveColor", lightRenderer.material.color * 0f);
            timeDelay = Random.Range(0.02f, 1f);
            yield return new WaitForSeconds(timeDelay);
        }
    }

    #endregion
}
