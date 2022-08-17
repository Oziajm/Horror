using UnityEngine;
using TMPro;

public class CamerasController : MonoBehaviour
{
    public int cameraNumber;

    [SerializeField] private TextMeshPro cameraNumberAtScreen;
    [SerializeField] private MeshRenderer TVScreen;
    [SerializeField] private Material[] camerasMaterials;
    [SerializeField] private GameObject[] cameras;
    private void Start()
    {
        ChangeCameraAtScreen();
    }

    public void ChangeCameraAtScreen()
    {
        for(int i=0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
        cameras[cameraNumber].SetActive(true);
        cameraNumberAtScreen.text = cameraNumber.ToString();
        TVScreen.material = camerasMaterials[cameraNumber];
    }
}
