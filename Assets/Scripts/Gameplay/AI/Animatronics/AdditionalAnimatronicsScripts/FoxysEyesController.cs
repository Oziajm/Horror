using UnityEngine;

public class FoxysEyesController : MonoBehaviour
{
    [SerializeField]
    private Material eyesMaterial;

    [SerializeField]
    private Light[] lights;

    public void SetFoxyTriggeredEyes()
    {
        eyesMaterial.SetColor("_EmissionColor", Color.red);

        foreach (Light light in lights)
        {
            light.color = Color.red;
        }
    }

    public void SetFoxyCalmEyes()
    {
        eyesMaterial.SetColor("_EmissionColor", Color.white);

        foreach (Light light in lights)
        {
            light.color = Color.white;
        }
    }
}
