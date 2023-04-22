using UnityEngine;
using System.Collections;

public class PirateCoveLights : MonoBehaviour
{
    #region Variables

    [SerializeField] private Renderer[] lights;

    #endregion

    #region Unity Methods

    private void Start()
    {
        StartCoroutine(TurnOnLights());
    }

    IEnumerator TurnOnLights()
    {
        while (true)
        {
            lights[0].material.SetColor("_EmissionColor", lights[0].material.color * 5f);
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].material.SetColor("_EmissionColor", i % 2 == 0 ? lights[i].material.color * 5f : Color.black);
            }
            yield return new WaitForSeconds(1f);

            lights[0].material.SetColor("_EmissionColor", Color.black);
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].material.SetColor("_EmissionColor", i % 2 != 0 ? lights[i].material.color * 5f : Color.black);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion
}
