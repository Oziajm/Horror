using UnityEngine;
using TMPro;

public class CamerasController : MonoBehaviour
{
    public int cameraNumber;

    [SerializeField] private TextMeshPro cameraNumberAtScreen;
    [SerializeField] private MeshRenderer TVScreen;
    [SerializeField] private Material[] camerasMaterials;
    private void Start()
    {
        ChangeCameraAtScreen();
    }

    public void ChangeCameraAtScreen()
    {
        cameraNumberAtScreen.text = cameraNumber.ToString();
        TVScreen.material = camerasMaterials[cameraNumber];
    }
}
