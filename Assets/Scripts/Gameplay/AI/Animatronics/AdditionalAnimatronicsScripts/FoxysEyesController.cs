using UnityEngine;

public class FoxysEyesController : MonoBehaviour
{
    [SerializeField]
    private Material eyesMaterial;

    [SerializeField]
    private Light[] lights;

    public void SetFoxyTriggeredEyes()
    {
        eyesMaterial.SetColor("_EmissiveColor", Color.red * 50f);

        foreach (Light light in lights)
        {
            light.color = Color.red;
        }
    }

    public void SetFoxyCalmEyes()
    {
        eyesMaterial.SetColor("_EmissiveColor", Color.white * 50f);

        foreach (Light light in lights)
        {
            light.color = Color.white;
        }
    }
}
